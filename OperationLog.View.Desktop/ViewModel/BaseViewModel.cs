using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Ninject;
using OperationLog.BusinessLogic.Services;
using OperationLog.Presentation.Desktop.Infrastructure;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Presentation.Desktop.ViewModel
{
    public class BaseViewModel : ObservableObject
    {
        public IEnumerable<User> Users
        {
            get
            {
                using (var service = NinjectKernel.Kernel.Get<IService<User>>())
                {
                    return service.GetAll();
                }
            }
        }

        public SeriesCollection SeriesCollection => new SeriesCollection
        {
            new LineSeries
            {
                Values = new ChartValues<double> { double.NaN, 3, 3, 3, 3, 3, double.NaN},
                PointDiameter = 20,
                Fill = Brushes.Transparent
            },
            new LineSeries
            {
                Values = new ChartValues<double> { double.NaN, 2, 2, double.NaN, 2, 2, double.NaN},
                PointDiameter = 20,
                Fill = Brushes.Transparent
            },
        };

        public async Task SyncAll()
        {
            var progressAlert =
                await (Application.Current.MainWindow as MetroWindow).ShowProgressAsync("Please wait...", "Sync....");
            progressAlert.SetIndeterminate();

            await Task.Run(() => Thread.Sleep(5000));
            await progressAlert.CloseAsync();
        }

        public Command Command => new Command(async data => await SyncAll());
    }
}