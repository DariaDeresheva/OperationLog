using OperationLog.Presentation.Desktop.Infrastructure;

namespace OperationLog.Presentation.Desktop.Model
{
    public class Selectable<T> : ObservableObject where T : class, new() 
    {
        private bool _isSelected = true;

        public T Instanse { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

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