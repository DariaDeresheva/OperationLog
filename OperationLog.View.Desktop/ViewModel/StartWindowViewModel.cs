﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Ninject;
using OperationLog.BusinessLogic.Services;
using OperationLog.Presentation.Desktop.Infrastructure;
using OperationLog.Presentation.Desktop.Model;
using OperationLog.Presentation.Desktop.Model.DTO;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Presentation.Desktop.ViewModel
{
    public class StartWindowViewModel : ObservableObject, IDisposable
    {
        private readonly IService _service = NinjectKernel.Kernel.Get<IService>();

        private List<Operation> _latestSelectedOperations = new List<Operation>();

        private List<Selectable<User>> _users;
        private List<Selectable<UserType>> _userTypes;
        private List<Selectable<OperationType>> _operationTypes;
        private List<Selectable<Program>> _programs;
        private List<Selectable<Department>> _departments;

        private readonly DateTime _initialDateFrom = DateTime.Now.AddDays(-14);
        private readonly DateTime _initialDateTo = DateTime.Now;

        private Func<Operation, bool> OperationTypeSelected
            =>
                operation =>
                    _operationTypes.Any(
                        operationType =>
                            operationType.Instanse.OperationTypeId == operation.OperationType.OperationTypeId &&
                            operationType.IsSelected);

        private Func<Operation, bool> UserSelected
            => operation => _users.Any(user => user.Instanse.UserId == operation.User.UserId && user.IsSelected);


        private Func<Operation, bool> UserTypeSelected
            =>
                operation =>
                    _userTypes.Any(
                        userType =>
                            userType.Instanse.UserTypeId == operation.User.UserType.UserTypeId && userType.IsSelected);

        private Func<Operation, bool> ProgramSelected
            =>
                operation =>
                    _programs.Any(
                        program => program.Instanse.ProgramId == operation.Program.ProgramId && program.IsSelected);

        private Func<Operation, bool> DepartmentSelected
            =>
                operation =>
                    _departments.Any(
                        department =>
                            department.Instanse.DepartmentId == operation.Department.DepartmentId &&
                            department.IsSelected);

        private KeyValuePair<string, GridOption> _gridOptionSelected;

        private SeriesCollection _seriesCollection;

        private readonly CartesianMapper<OperationWithIndex> _cartesianMapper =
            Mappers.Xy<OperationWithIndex>().X(x => x.Operation.DateTime.Ticks).Y(y => y.Index);

        private DateTime DateTimeFromQuery => DateFrom.Date.Add(TimeFrom);
        private DateTime DateTimeToQuery => DateTo.Date.Add(TimeTo);

        private ICommand InitializeChart => new Command(async _ =>
        {
            SeriesCollection = GetSeriesCollection();
            await OnSeriesResultAsync();
        });

        private string _textSearchQuery = string.Empty;
        public string TextSearchQuery
        {
            get { return _textSearchQuery; }
            set
            {
                _textSearchQuery = value;
                OnPropertyChanged(nameof(TextSearchQuery));
            }
        }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }

        public DateTime DateTimeFromAxesLimit { get; set; }
        public DateTime DateTimeToAxesLimit { get; set; }

        public ObservableCollection<Selectable<User>> UsersGrid { get; set; } =
            new ObservableCollection<Selectable<User>>();

        public ObservableCollection<Selectable<UserType>> UserTypesGrid { get; set; } =
            new ObservableCollection<Selectable<UserType>>();

        public ObservableCollection<Selectable<OperationType>> OperationTypesGrid { get; set; } =
            new ObservableCollection<Selectable<OperationType>>();

        public ObservableCollection<Selectable<Program>> ProgramsGrid { get; set; } =
            new ObservableCollection<Selectable<Program>>();

        public ObservableCollection<Selectable<Department>> DepartmentsGrid { get; set; } =
            new ObservableCollection<Selectable<Department>>();

        public IDictionary<string, GridOption> GridOptions { get; }

        public KeyValuePair<string, GridOption> GridOptionSelected
        {
            get { return _gridOptionSelected; }
            set
            {
                _gridOptionSelected = value;
                OnPropertyChanged(nameof(GridOptionSelected));
            }
        }

        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set
            {
                _seriesCollection = value;
                OnPropertyChanged(nameof(YAxisMax));
                OnPropertyChanged(nameof(SeriesCollection));
            }
        }

        public double YAxisMax => SeriesCollection.Count;
        public double YAxisMin { get; } = -1;

        public Func<double, string> DateTimeFormatter
            => value => new DateTime((long) value).ToString("dd.MM.yyyy HH:mm:ss");

        public Func<double, string> YFormatter
            =>
                value =>
                    SeriesCollection.FirstOrDefault(
                        collection =>
                            collection.Values.Cast<OperationWithIndex>()
                                .Any(operation => Math.Abs(operation.Index - value) < double.Epsilon))
                        ?.Values.Cast<OperationWithIndex>()
                        .First()
                        .Operation.User.UserName.Trim() ?? string.Empty;

        public ICommand ApplyFilter => new Command(async _ =>
        {
            SeriesCollection = GetSeriesCollection();
            await WaitChartUpdateAsync();
            await OnSeriesResultAsync(NothingFoundAsync);
        });

        public ICommand ResetFilter => new Command(_ =>
        {
            PrepareDateTimeLimits();
            OnPropertyChanged(nameof(DateFrom));
            OnPropertyChanged(nameof(DateTo));
            OnPropertyChanged(nameof(TimeFrom));
            OnPropertyChanged(nameof(TimeTo));
            foreach (var gridOption in GridOptions)
            {
                gridOption.Value.AllSelected = true;
            }
            TextSearchQuery = string.Empty;
        });

        public StartWindowViewModel()
        {
            PrepareEventHandlers();
            PrepareDateTimeLimits();
            PrepareCollectionsFromDatabase();
            GridOptions = GetGridOptions();
            GridOptionSelected = GridOptions.FirstOrDefault();
            InitializeChart.Execute(null);
        }

        private async Task OnSeriesResultAsync(Func<Task> onEmptyCollection = null)
        {
            if (_latestSelectedOperations.Any())
            {
                DateTimeFromAxesLimit = _latestSelectedOperations.Min(value => value.DateTime);
                DateTimeToAxesLimit = _latestSelectedOperations.Max(value => value.DateTime);
            }
            else
            {
                DateTimeFromAxesLimit = DateTimeFromQuery;
                DateTimeToAxesLimit = DateTimeToQuery;
                if (onEmptyCollection != null)
                {
                    await onEmptyCollection();
                }
            }
            OnPropertyChanged(nameof(DateTimeFromAxesLimit));
            OnPropertyChanged(nameof(DateTimeToAxesLimit));
        }

        private void PrepareDateTimeLimits()
        {
            DateFrom = DateTimeFromAxesLimit = _initialDateFrom;
            DateTo = DateTimeToAxesLimit = _initialDateTo;
            TimeFrom = DateFrom.TimeOfDay;
            TimeTo = DateTo.TimeOfDay;
        }

        private void PrepareCollectionsFromDatabase()
        {
            _userTypes =
                _service.GetAll<UserType>()
                    .Select(type => new Selectable<UserType>(type))
                    .OrderBy(type => type.Instanse.TypeName)
                    .ToList();

            _programs =
                _service.GetAll<Program>()
                    .Select(program => new Selectable<Program>(program))
                    .OrderBy(program => program.Instanse.ProgramName)
                    .ToList();

            _users =
                _service.GetAll<User>()
                    .Select(user => new Selectable<User>(user))
                    .OrderBy(user => user.Instanse.UserName)
                    .ToList();

            _operationTypes =
                _service.GetAll<OperationType>()
                    .Select(type => new Selectable<OperationType>(type))
                    .OrderBy(type => type.Instanse.TypeName)
                    .ToList();

            _departments =
                _service.GetAll<Department>()
                    .Select(department => new Selectable<Department>(department))
                    .OrderBy(department => department.Instanse.DepartmentName)
                    .ToList();
        }

        private IEnumerable<Operation> SelectOperations() => _latestSelectedOperations = _service.GetAllWhere<Operation>(
            operation =>
                operation.DateTime >= DateTimeFromQuery &&
                operation.DateTime <= DateTimeToQuery &&
                OperationTypeSelected(operation) &&
                UserTypeSelected(operation) &&
                DepartmentSelected(operation) &&
                ProgramSelected(operation) &&
                UserSelected(operation));

        private static async Task WaitSeriesRefreshAsync() => await Task.Run(() => Thread.Sleep(500));

        private static async Task WaitChartUpdateAsync()
        {
            var progressAlert =
                await
                    (Application.Current.MainWindow as MetroWindow)
                        .ShowProgressAsync("Пожалуйста подождите...", "Применение фильтров...");

            progressAlert.SetIndeterminate();
            await WaitSeriesRefreshAsync();
            await progressAlert.CloseAsync();
        }

        private static async Task NothingFoundAsync() => await
            (Application.Current.MainWindow as MetroWindow)
                .ShowMessageAsync("По вашему запросу ничего не найдено!", "Попробуйте изменить параметры поиска.");

        private SeriesCollection NewSeriesCollection() => new SeriesCollection(_cartesianMapper);

        private SeriesCollection GetSeriesCollection()
        {
            var operationsFiltered = SelectOperations();

            var operationsOrdered = operationsFiltered
                .OrderByDescending(operation => operation.User.UserName)
                .ThenBy(operation => operation.DateTime);

            var operationsByUser = operationsOrdered
                .GroupBy(operation => operation.User.UserId)
                .Select((group, index) => new {Group = group, Index = index});

            var collectionOfChartValues =
                operationsByUser.Select(
                    operationGroup =>
                        operationGroup.Group.Select(
                            operation => new OperationWithIndex {Index = operationGroup.Index, Operation = operation})
                            .AsChartValues());

            var chartSeries =
                collectionOfChartValues.Select(values => NewLineSeries(values, values.First().Operation.User.UserName));

            var seriesCollection = NewSeriesCollection();
            seriesCollection.AddRange(chartSeries);
            return seriesCollection;
        }

        private static LineSeries NewLineSeries(IChartValues values, string title) => new LineSeries
        {
            Values = values,
            PointDiameter = 20,
            StrokeThickness = 3,
            Fill = Brushes.Transparent,
            LabelPoint = point => ((OperationWithIndex) point.Instance).Operation.OperationType.TypeName,
            Title = title
        };

        private IDictionary<string, GridOption> GetGridOptions() => new Dictionary<string, GridOption>
        {
            ["Пользователи"] = GridOption.Create(UsersGrid, _users, user => user.UserName),
            ["Программы"] = GridOption.Create(ProgramsGrid, _programs, program => program.ProgramName),
            ["Типы операций"] = GridOption.Create(OperationTypesGrid, _operationTypes, type => type.TypeName),
            ["Уровни доступа"] = GridOption.Create(UserTypesGrid, _userTypes, type => type.TypeName),
            ["Филиалы"] = GridOption.Create(DepartmentsGrid, _departments, department => department.DepartmentName),
        };

        private void PrepareEventHandlers() => PropertyChanged += (s, e) =>
        {
            switch (e.PropertyName)
            {
                case nameof(TextSearchQuery):
                    GridOptionSelected.Value.SearchHandler.Execute(TextSearchQuery);
                    break;
                case nameof(GridOptionSelected):
                    GridOptions.ForEach(option => option.Value.Visibility = Visibility.Collapsed);
                    GridOptionSelected.Value.Visibility = Visibility.Visible;
                    TextSearchQuery = string.Empty;
                    break;
            }
        };

        public void Dispose()
        {
            _service?.Dispose();
        }
    }
}