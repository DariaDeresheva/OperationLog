using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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
using OperationLog.BusinessLogic.Services;
using OperationLog.ExcelProvider;
using OperationLog.Presentation.Desktop.Helpers;
using OperationLog.Presentation.Desktop.Infrastructure;
using OperationLog.Presentation.Desktop.Infrastructure.Mvvm;
using OperationLog.Presentation.Desktop.Model;
using OperationLog.Presentation.Desktop.Model.Dto;
using OperationLog.Presentation.Desktop.Model.Dto.ValuesToExcel;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Presentation.Desktop.ViewModel
{
    public class StartWindowViewModel : ObservableObject, IDisposable
    {
        private readonly IService _service = DependencyResolver.Get<IService>();
        private readonly IExcelProvider _excel = DependencyResolver.Get<IExcelProvider>();

        private List<Operation> _latestSelectedOperations = new List<Operation>();

        private List<Selectable<User>> _users;
        private List<Selectable<UserType>> _userTypes;
        private List<Selectable<OperationType>> _operationTypes;
        private List<Selectable<Program>> _programs;
        private List<Selectable<Department>> _departments;

        private readonly DateTime _initialDateFrom = DateTime.Now.AddMonths(-1);
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

        private static Func<ChartPoint, string> TooltipLabelPoint
            => point => TooltipInfo(((OperationWithIndex) point.Instance).Operation);

        private KeyValuePair<string, GridOption> _gridOptionSelected;

        private SeriesCollection _seriesCollection;

        private readonly CartesianMapper<OperationWithIndex> _cartesianMapper =
            Mappers.Xy<OperationWithIndex>().X(x => x.Operation.DateTime.Ticks).Y(y => y.Index);

        private DateTime DateTimeFromQuery => DateFrom.Date.Add(TimeFrom);
        private DateTime DateTimeToQuery => DateTo.Date.Add(TimeTo);

        private ICommand InitializeChart => new Command(async _ =>
        {
            SeriesCollection = GetSeriesCollection();
            await OnSeriesResultAsync(WaitChartUpdateAsync);
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

        public IDictionary<string, GridOption> GridOptions { get; set; }

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

        public double YAxisMax => _latestSelectedOperations.DistinctBy(operation => operation.User.UserId).Count();

        public double YAxisMin { get; } = -1;

        public Func<double, string> DateTimeFormatter
            => value => new DateTime((long)value).ToString("dd.MM.yyyy HH:mm:ss");

        public Func<double, string> NameFormatter
            =>
                value =>
                    ((OperationWithIndex)
                        SeriesCollection.FirstOrDefault(
                            collection =>
                                Math.Abs(((OperationWithIndex) collection.Values.Points.First().Instance).Index - value) <
                                double.Epsilon)
                            ?.Values.Points.First().Instance)
                        ?.Operation.User.UserName ?? string.Empty;

        public ICommand ApplyFilter => new Command(async _ =>
        {
            SeriesCollection = GetSeriesCollection();
            await OnSeriesResultAsync(WaitChartUpdateAsync, NothingFoundAsync);
        });

        public ICommand PrepareApplicationData => new Command(async _ =>
        {
            if (await PullDatabaseAsync())
            {
                PrepareEventHandlers();
                PrepareDateTimeLimits();
                PrepareGridOptions();
                InitializeChart.Execute(null);
            }
            else
            {
                await MessageDialog("Ошибка подключения к базе данных!",
                    "Проверьте строку подключения в конфигурационном файле.");
                Application.Current.Shutdown();
            }
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
            ApplyFilter.Execute(null);
        });

        public ICommand SaveToExcel => new Command(async _ =>
        {
            var filename = ExportToExcel();
            await
                MessageDialog($@"Файл ""{filename}"" успешно сохранен!",
                    $"Путь: {Path.Combine(Path.GetFullPath(filename))}");
        });

        private void PrepareGridOptions()
        {
            GridOptions = GetGridOptions();
            GridOptionSelected = GridOptions.FirstOrDefault();
            OnPropertyChanged(nameof(GridOptions));
        }

        private void PrepareDateTimeLimits()
        {
            DateFrom = DateTimeFromAxesLimit = _initialDateFrom;
            DateTo = DateTimeToAxesLimit = _initialDateTo;
            TimeFrom = DateFrom.TimeOfDay;
            TimeTo = DateTo.TimeOfDay;
            OnPropertyChanged(nameof(DateFrom));
            OnPropertyChanged(nameof(DateTo));
            OnPropertyChanged(nameof(TimeFrom));
            OnPropertyChanged(nameof(TimeTo));
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
            _departments =
                _service.GetAll<Department>()
                    .Select(department => new Selectable<Department>(department))
                    .OrderBy(department => department.Instanse.DepartmentName)
                    .ToList();
            _operationTypes =
                _service.GetAll<OperationType>()
                    .Select(type => new Selectable<OperationType>(type))
                    .OrderBy(type => type.Instanse.TypeName)
                    .ToList();
        }

        private async Task OnSeriesResultAsync(Func<Task> onNotEmptySeries = null, Func<Task> onEmptySeries = null)
        {
            if (_latestSelectedOperations.Any())
            {
                DateTimeFromAxesLimit = _latestSelectedOperations.Min(value => value.DateTime);
                DateTimeToAxesLimit = _latestSelectedOperations.Max(value => value.DateTime);
                if (onNotEmptySeries != null)
                {
                    await onNotEmptySeries();
                }
            }
            else
            {
                DateTimeFromAxesLimit = DateTimeFromQuery;
                DateTimeToAxesLimit = DateTimeToQuery;
                if (onEmptySeries != null)
                {
                    await onEmptySeries();
                }
            }
            OnPropertyChanged(nameof(DateTimeFromAxesLimit));
            OnPropertyChanged(nameof(DateTimeToAxesLimit));
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

        private static Task<ProgressDialogController> ProgressDialog(string title, string message)
        {
            return (Application.Current.MainWindow as MetroWindow).ShowProgressAsync(title, message);
        }

        private static Task<MessageDialogResult> MessageDialog(string title, string message)
        {
            return (Application.Current.MainWindow as MetroWindow).ShowMessageAsync(title, message);
        }

        private static async Task WaitSeriesRefreshAsync() => await Task.Delay(TimeSpan.FromSeconds(1));

        private static async Task WaitChartUpdateAsync()
        {
            var progressAlert = await ProgressDialog("Применение фильтров...", "Пожалуйста подождите...");
            progressAlert.SetIndeterminate();
            await WaitSeriesRefreshAsync();
            await progressAlert.CloseAsync();
        }

        private async Task<bool> PullDatabaseAsync()
        {
            var progressAlert = await ProgressDialog("Подключение к базе данных...", "Пожалуйста подождите...");
            progressAlert.SetIndeterminate();
            Exception catched = null;
            await Task.Run(() =>
            {
                try
                {
                    PrepareCollectionsFromDatabase();
                }
                catch (SqlException exception)
                {
                    catched = exception;
                }
            });
            await progressAlert.CloseAsync();
            return catched == null;
        }

        private static async Task NothingFoundAsync()
            => await MessageDialog("По вашему запросу ничего не найдено!", "Попробуйте изменить параметры поиска.");

        private SeriesCollection NewSeriesCollection() => new SeriesCollection(_cartesianMapper);

        private SeriesCollection GetSeriesCollection()
        {
            var seriesValues = GetSeriesChartValues().ToList();

            var chartSeries =
                seriesValues.Select(
                    values => NewLineSeries(values, values.First().Operation.User.UserName, 20, Brushes.DeepSkyBlue));

            var beginChartSeries =
                seriesValues.SelectMany(
                    values => values.Where(value => value.Operation.OperationType.OperationTypeId == 1002))
                    .Select(
                        value =>
                            NewLineSeries(new[] { value }.AsChartValues(), value.Operation.User.UserName, 30,
                                Brushes.Green));

            var endChartSeries =
                seriesValues.SelectMany(
                    values => values.Where(value => value.Operation.OperationType.OperationTypeId == 1003))
                    .Select(
                        value =>
                            NewLineSeries(new[] { value }.AsChartValues(), value.Operation.User.UserName, 10,
                                Brushes.Red));

            var seriesCollection = NewSeriesCollection();
            seriesCollection.AddRange(beginChartSeries.Concat(chartSeries).Concat(endChartSeries));
            return seriesCollection;
        }

        private IEnumerable<ChartValues<OperationWithIndex>> GetSeriesChartValues()
        {
            var operationsFiltered = SelectOperations();

            var operationsOrdered = operationsFiltered
                .OrderByDescending(operation => operation.User.UserName)
                .ThenBy(operation => operation.DateTime);

            var operationsByUser = operationsOrdered
                .GroupBy(operation => operation.User.UserId)
                .Select((group, index) => new { Group = @group, Index = index })
                .ToList();

            foreach (var user in operationsByUser)
            {
                var remainOperations = user.Group.ToList();
                while (remainOperations.Any())
                {
                    var enterExitGroup =
                        remainOperations.TakeWhile(operation => operation.OperationType.OperationTypeId != 1001)
                            .ToList();
                    var exitOperation =
                        remainOperations.FirstOrDefault(operation => operation.OperationType.OperationTypeId == 1001);
                    if (exitOperation != null)
                    {
                        enterExitGroup.Add(exitOperation);
                    }
                    yield return enterExitGroup
                        .Select(operation => new OperationWithIndex { Index = user.Index, Operation = operation })
                        .AsChartValues();
                    remainOperations = remainOperations.Except(enterExitGroup).ToList();
                }
            }
        }

        private static LineSeries NewLineSeries(IChartValues values, string title, double pointDiameter, Brush stroke)
            => new LineSeries
            {
                Values = values,
                PointDiameter = pointDiameter,
                StrokeThickness = 2,
                Fill = Brushes.Transparent,
                LabelPoint = TooltipLabelPoint,
                Title = title,
                Stroke = stroke
            };

        private static string TooltipInfo(Operation operation)
        {
            var info = new[]
            {
                $"Операция: {operation.OperationType.TypeName}",
                $"Программа: {operation.Program.ProgramName}",
                $"IP-адрес: {DatabaseValuesConverter.GetIpString(operation.StationIpAddress)}",
                $"MAC-адрес: {DatabaseValuesConverter.GetMacString(operation.StationAddress.Select(symbol => (byte) symbol))}"
            };
            return string.Join(Environment.NewLine, info);
        }

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

        private IEnumerable<ValueToExcelWorksheet> ValuesToExcel()
            =>
                _latestSelectedOperations.OrderBy(operation => operation.OperationType.TypeName)
                    .GroupBy(operation => operation.Program.ProgramId)
                    .Select(byProgram =>
                        new ValueToExcelWorksheet
                        {
                            ProgramName = byProgram.First().Program.ProgramName,
                            Users =
                                byProgram.GroupBy(operation => operation.User.UserId)
                                    .Select(byUser =>
                                        new ValueToExcelUser
                                        {
                                            UserName = byUser.First().User.UserName,
                                            OperationTypes =
                                                byUser.GroupBy(operation => operation.OperationType.OperationTypeId)
                                                    .Select(operations =>
                                                        new ValueToExcelOperationType
                                                        {
                                                            TypeName =
                                                                operations.First().OperationType.TypeName,
                                                            Id = operations.Key,
                                                            Count = operations.Count()
                                                        })
                                        })
                        });

        private string ExportToExcel()
        {
            using (var book = _excel.CreateBook($"Отчет {DateTime.Now.ToString(@"dd.MM.yyyy HH-mm-ss")}.xlsx"))
            {
                var types = _operationTypes.Select(type => type.Instanse).ToList();
                foreach (var value in ValuesToExcel())
                {
                    var header = new[] {"ФИО"}.Concat(types.Select(type => type.TypeName));
                    var content =
                        value.Users.Select(man => new {man.UserName, man.OperationTypes})
                            .Select(user => new[] {user.UserName}
                                .Concat(types.GroupJoin(user.OperationTypes, a => a.OperationTypeId, b => b.Id,
                                    (a, b) => b.Sum(c => c.Count)).Cast<object>()));
                    var summary =
                        new[] {"Итого"}.Concat(
                            types.GroupJoin(value.Users.SelectMany(user => user.OperationTypes), a => a.OperationTypeId,
                                b => b.Id, (a, b) => b.Sum(c => c.Count)).Cast<object>());

                    var table = new[] {header}.Concat(content).Concat(new[] {summary}).ToList();

                    var worksheet = book.CreateWorksheet(value.ProgramName);
                    worksheet.GetCell(1, 1).SetFromTable(table);
                    worksheet.AddPieChart(position: worksheet.GetCell(table.Count + 2, 2),
                        name: "Типы операций",
                        size: 600,
                        valuesRange:
                            worksheet.GetRange(worksheet.GetCell(table.Count, 2),
                                worksheet.GetCell(table.Count, table.First().Count())),
                        axesRange:
                            worksheet.GetRange(worksheet.GetCell(1, 2), worksheet.GetCell(1, table.First().Count())));
                }
                return book.FileName;
            }
        }

        public void Dispose()
        {
            _service?.Dispose();
        }
    }
}