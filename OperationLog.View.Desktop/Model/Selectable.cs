using OperationLog.Presentation.Desktop.Infrastructure.Mvvm;

namespace OperationLog.Presentation.Desktop.Model
{
    /// <summary>
    /// Класс «обертка» над обьектом типа T, добавляющая свойство выбран/не выбран.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ObservableObject" />
    public class Selectable<T> : ObservableObject
    {
        /// <summary>
        /// Выбран ли объект.
        /// </summary>
        private bool _isSelected = true;
        /// <summary>
        /// Выбран ли объект.
        /// </summary>
        /// <value><c>true</c> если выбран; иначе, <c>false</c>.</value>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        /// <summary>
        /// Объект типа Т.
        /// </summary>
        public T Instanse { get; set; }

        /// <summary>
        /// Конструктор <see cref="Selectable{T}"/>. По умолчанию объект выбран.
        /// </summary>
        /// <param name="instanse">Объект типа Т.</param>
        /// <param name="selected"><c>true</c> если выбран, <c>false</c> иначе.</param>
        public Selectable(T instanse, bool selected) : this(instanse)
        {
            IsSelected = selected;
        }

        /// <summary>
        /// Конструктор <see cref="Selectable{T}"/>. По умолчанию объект выбран.
        /// </summary>
        /// <param name="instanse">Объект типа Т.</param>
        public Selectable(T instanse)
        {
            Instanse = instanse;
        }
    }
}