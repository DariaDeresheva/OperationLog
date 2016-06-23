using OperationLog.Presentation.Desktop.Infrastructure.Mvvm;

namespace OperationLog.Presentation.Desktop.Model
{
    public class Selectable<T> : ObservableObject
    {
        private bool _isSelected = true;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public T Instanse { get; set; }

        public Selectable(T instanse, bool selected) : this(instanse)
        {
            IsSelected = selected;
        }

        public Selectable(T instanse)
        {
            Instanse = instanse;
        }
    }
}