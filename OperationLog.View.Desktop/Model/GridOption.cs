using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LiveCharts.Helpers;
using Ninject;
using OperationLog.Presentation.Desktop.Infrastructure;

namespace OperationLog.Presentation.Desktop.Model
{
    public class GridOption : ObservableObject
    {
        private static readonly ITextSearchRule TextSearchRule = NinjectKernel.Kernel.Get<ITextSearchRule>();

        private Visibility _visibility = Visibility.Collapsed;
        private bool _allSelected = true;

        public string GridName { get; set; }
        public ICommand SearchHandler { get; set; }
        public Action<bool> SelectAllHandler { get; set; }

        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public bool AllSelected
        {
            get { return _allSelected; }
            set
            {
                _allSelected = value;
                SelectAllHandler(_allSelected);
                OnPropertyChanged(nameof(AllSelected));
            }
        }

        private static Action<bool> CommonSelectAllHandler<T>(IEnumerable<Selectable<T>> gridCollection)
        {
            return value => gridCollection.ForEach(element => element.IsSelected = value);
        }

        private static ICommand CommonSearchHandler<T>(ICollection<Selectable<T>> gridCollection,
            IEnumerable<Selectable<T>> searchCollection, 
            Func<T, string> searchProperty)
        {
            return new Command(searchQuery =>
            {
                var found =
                    searchCollection.Where(
                        element =>
                            TextSearchRule.SearchSuccesful(searchProperty(element.Instanse), (string) searchQuery))
                        .ToList();
                gridCollection.Clear();
                found.ForEach(gridCollection.Add);
            });
        }

        public static GridOption Create<T>(ICollection<Selectable<T>> gridCollection,
            IEnumerable<Selectable<T>> searchCollection,
            Func<T, string> searchProperty)
        {
            return new GridOption
            {
                SelectAllHandler = CommonSelectAllHandler(gridCollection),
                SearchHandler = CommonSearchHandler(gridCollection, searchCollection, searchProperty)
            };
        }
    }
}
