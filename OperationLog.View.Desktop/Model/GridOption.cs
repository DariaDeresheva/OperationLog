using System;
using System.Windows;
using System.Windows.Input;
using OperationLog.Presentation.Desktop.Infrastructure;

namespace OperationLog.Presentation.Desktop.Model
{
    public class GridOption : ObservableObject
    {
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
    }
}
