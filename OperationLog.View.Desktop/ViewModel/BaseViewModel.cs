using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Ninject;
using OperationLog.BusinessLogic.Services;
using OperationLog.Presentation.Desktop.Infrastructure;
using OperationLog.Presentation.Desktop.Model;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Presentation.Desktop.ViewModel
{
    public class BaseViewModel : ObservableObject
    {
        private readonly IService _service = NinjectKernel.Kernel.Get<IService>();

        private readonly List<Selectable<User>> _users;
        private readonly List<Selectable<UserType>> _userTypes;
        private readonly List<Selectable<OperationType>> _operationTypes;
        private readonly List<Selectable<Program>> _programs;
        private readonly List<Selectable<Department>> _departments;

        private KeyValuePair<string, GridOption> _gridOptionSelected;

        private string _textSearchQuery;

        private DateTime _dateFrom = DateTime.Now.AddDays(-7);
        private DateTime _dateTo = DateTime.Now;
        private TimeSpan _timeFrom = DateTime.Now.TimeOfDay;
        private TimeSpan _timeTo = DateTime.Now.TimeOfDay;

        public string TextSearchQuery
        {
            get { return _textSearchQuery; }
            set
            {
                _textSearchQuery = value;
                OnPropertyChanged(nameof(TextSearchQuery));
            }
        }

        public DateTime DateFrom
        {
            get { return _dateFrom; }
            set
            {
                _dateFrom = value;
                OnPropertyChanged(nameof(DateFrom));
            }
        }

        public DateTime DateTo
        {
            get { return _dateTo; }
            set
            {
                _dateTo = value;
                OnPropertyChanged(nameof(DateTo));
            }
        }

        public TimeSpan TimeFrom
        {
            get { return _timeFrom; }
            set
            {
                _timeFrom = value;
                OnPropertyChanged(nameof(TimeFrom));
            }
        }

        public TimeSpan TimeTo
        {
            get { return _timeTo; }
            set
            {
                _timeTo = value;
                OnPropertyChanged(nameof(TimeTo));
            }
        }

        public DateTime DateTimeFrom => DateFrom.Add(TimeFrom);
        public DateTime DateTimeTo => DateTo.Add(TimeTo);

        public IEnumerable<Selectable<User>> UsersGrid { get; set; }
        public IEnumerable<Selectable<UserType>> UserTypesGrid { get; set; }
        public IEnumerable<Selectable<OperationType>> OperationTypesGrid { get; set; }
        public IEnumerable<Selectable<Program>> ProgramsGrid { get; set; }
        public IEnumerable<Selectable<Department>> DepartmentsGrid { get; set; }

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

        public IEnumerable<Operation> Operations
            =>
                _service.GetAllWhere<Operation>(
                    operation =>
                        operation.DateTime >= DateTimeFrom &&
                        operation.DateTime <= DateTimeTo &&
                        _users.Single(user => user.Instanse.UserId == operation.User.UserId).IsSelected &&
                        _departments.Single(department => department.Instanse.DepartmentId == operation.Department.DepartmentId).IsSelected &&
                        _programs.Single(program => program.Instanse.ProgramId == operation.Program.ProgramId).IsSelected &&
                        _operationTypes.Single(operationType => operationType.Instanse.OperationTypeId == operation.OperationType.OperationTypeId).IsSelected &&
                        _userTypes.Single(userType => userType.Instanse.UserTypeId == operation.User.UserType.UserTypeId).IsSelected);

        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set
            {
                _seriesCollection = value;
                OnPropertyChanged(nameof(SeriesCollection));
            }
        }

        public Func<double, string> DateTimeFormatter => value => new DateTime((long)value).ToString("dd.MM.yyyy hh:mm:ss");

        public double YAxisMax => SeriesCollection.Count;

        public double YAxisMin { get; } = -1;

        public Func<double, string> YFormatter
            =>
                value =>
                    SeriesCollection.FirstOrDefault(
                        collection =>
                            collection.Values.Cast<OperationWithIndex>()
                                .Any(operation => Math.Abs(operation.Index - value) < 1e-9))?
                        .Values.Cast<OperationWithIndex>()
                        .First()
                        .Operation.User.UserName.Trim() ?? string.Empty;

        public BaseViewModel()
        {
            PrepareEventHandlers();

            _users = _service.GetAll<User>().Select(user => new Selectable<User>(user)).ToList();
            _operationTypes =
                _service.GetAll<OperationType>().Select(type => new Selectable<OperationType>(type)).ToList();
            _programs = _service.GetAll<Program>().Select(program => new Selectable<Program>(program)).ToList();
            _departments =
                _service.GetAll<Department>().Select(department => new Selectable<Department>(department)).ToList();
            _userTypes = _service.GetAll<UserType>().Select(type => new Selectable<UserType>(type)).ToList();

            GridOptions = GetGridOptions()
                .OrderBy(option => option.GridName)
                .ToDictionary(key => key.GridName, value => value);

            GridOptionSelected = GridOptions.FirstOrDefault();
            TextSearchQuery = string.Empty;
        }

        private SeriesCollection GetSeriesCollection()
        {
            var mapper = Mappers.Xy<OperationWithIndex>().X(x => x.Operation.DateTime.Ticks).Y(y => y.Index);
            var seriesCollection = new SeriesCollection(mapper);

            var operationsByUser =
                Operations
                    .OrderByDescending(operation => operation.User.UserName)
                    .ThenBy(operation => operation.DateTime)
                    .GroupBy(operation => operation.User.UserId)
                    .Select((group, index) => new { Group = group, Index = index });

            foreach (var operationGroup in operationsByUser)
            {
                var values = new ChartValues<OperationWithIndex>();
                foreach (var operation in operationGroup.Group)
                {
                    values.Add(new OperationWithIndex { Index = operationGroup.Index, Operation = operation });
                }
                seriesCollection.Add(new LineSeries
                {
                    Values = values,
                    PointDiameter = 20,
                    StrokeThickness = 3,
                    Fill = Brushes.Transparent
                });
            }
            return seriesCollection;
        }

        private IEnumerable<GridOption> GetGridOptions()
        {
            return new List<GridOption>
            {
                new GridOption
                {
                    GridName = "Пользователи",
                    SelectAllHandler = value =>
                    {
                        UsersGrid = UsersGrid.Select(user => new Selectable<User>(user.Instanse, value));
                        OnPropertyChanged(nameof(UsersGrid));
                    },
                    SearchHandler = new Command(_ =>
                    {
                        UsersGrid =
                            _users.Where(
                                user =>
                                    user.Instanse.UserName.StartsWith(TextSearchQuery,
                                        StringComparison.InvariantCultureIgnoreCase));
                        OnPropertyChanged(nameof(UsersGrid));
                    })
                },
                new GridOption
                {
                    GridName = "Уровни доступа",
                    SelectAllHandler = value =>
                    {
                        UserTypesGrid = UserTypesGrid.Select(type => new Selectable<UserType>(type.Instanse, value));
                        OnPropertyChanged(nameof(UserTypesGrid));
                    },
                    SearchHandler = new Command(_ =>
                    {
                        UserTypesGrid =
                            _userTypes.Where(
                                type =>
                                    type.Instanse.TypeName.StartsWith(TextSearchQuery,
                                        StringComparison.InvariantCultureIgnoreCase));
                        OnPropertyChanged(nameof(UserTypesGrid));
                    })
                },
                new GridOption
                {
                    GridName = "Программы",
                    SelectAllHandler = value =>
                    {
                        ProgramsGrid = ProgramsGrid.Select(program => new Selectable<Program>(program.Instanse, value));
                        OnPropertyChanged(nameof(ProgramsGrid));
                    },
                    SearchHandler = new Command(_ =>
                    {
                        ProgramsGrid =
                            _programs.Where(
                                program =>
                                    program.Instanse.ProgramName.StartsWith(TextSearchQuery,
                                        StringComparison.InvariantCultureIgnoreCase));
                        OnPropertyChanged(nameof(ProgramsGrid));
                    })
                },
                new GridOption
                {
                    GridName = "Филиалы",
                    SelectAllHandler = value =>
                    {
                        DepartmentsGrid =
                            DepartmentsGrid.Select(department => new Selectable<Department>(department.Instanse, value));
                        OnPropertyChanged(nameof(DepartmentsGrid));
                    },
                    SearchHandler = new Command(_ =>
                    {
                        DepartmentsGrid =
                            _departments.Where(
                                department =>
                                    department.Instanse.DepartmentName.StartsWith(TextSearchQuery,
                                        StringComparison.InvariantCultureIgnoreCase));
                        OnPropertyChanged(nameof(DepartmentsGrid));
                    })
                },
                new GridOption
                {
                    GridName = "Типы операций",
                    SelectAllHandler = value =>
                    {
                        OperationTypesGrid = OperationTypesGrid.Select(type => new Selectable<OperationType>(type.Instanse, value));
                        OnPropertyChanged(nameof(OperationTypesGrid));
                    },
                    SearchHandler = new Command(_ =>
                    {
                        OperationTypesGrid =
                            _operationTypes.Where(
                                type =>
                                    type.Instanse.TypeName.StartsWith(TextSearchQuery,
                                        StringComparison.InvariantCultureIgnoreCase));
                        OnPropertyChanged(nameof(OperationTypesGrid));
                    })
                },
            };
        }

        private void PrepareEventHandlers()
        {
            PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(DateFrom):
                        SeriesCollection = GetSeriesCollection();
                        break;
                    case nameof(TextSearchQuery):
                        GridOptionSelected.Value.SearchHandler.Execute(null);
                        break;
                    case nameof(GridOptionSelected):
                        foreach (var gridOption in GridOptions.Except(new[] { GridOptionSelected }))
                        {
                            gridOption.Value.Visibility = Visibility.Collapsed;
                        }
                        GridOptionSelected.Value.Visibility = Visibility.Visible;
                        TextSearchQuery = string.Empty;
                        break;
                }
            };
        }
    }
}