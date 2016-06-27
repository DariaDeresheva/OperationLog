using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LiveCharts.Helpers;
using OperationLog.Presentation.Desktop.Helpers.TextSearchRule;
using OperationLog.Presentation.Desktop.Infrastructure;
using OperationLog.Presentation.Desktop.Infrastructure.Mvvm;

namespace OperationLog.Presentation.Desktop.Model
{
    /// <summary>
    /// Класс представляет модель для выбираемого списка объектов на графическом интерфейсе.
    /// </summary>
    /// <seealso cref="ObservableObject" />
    public class GridOption : ObservableObject
    {
        /// <summary>
        /// Правило текстового поиска.
        /// </summary>
        private static readonly ITextSearchRule TextSearchRule = DependencyResolver.Get<ITextSearchRule>();
        /// <summary>
        /// Видимость на графическом интерфейсе.
        /// </summary>
        private Visibility _visibility = Visibility.Collapsed;
        /// <summary>
        /// Выбраны ли все объекты в списке.
        /// </summary>
        /// <value><c>true</c> если все объекты выбраны; иначе, <c>false</c>.</value>
        private bool _allSelected = true;

        /// <summary>
        /// Название списка.
        /// </summary>
        public string GridName { get; set; }
        /// <summary>
        /// Обработчик запроса на текстовый поиск.
        /// </summary>
        public ICommand SearchHandler { get; set; }
        /// <summary>
        /// Обработчик запроса на выбор сразу всех объектов.
        /// </summary>
        public Action<bool> SelectAllHandler { get; set; }

        /// <summary>
        /// Видимость на графическом интерфейсе.
        /// </summary>
        /// <value>Уведомляет интерфейс об изменении.</value>
        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        /// <summary>
        /// Выбраны ли все объекты в списке.
        /// </summary>
        /// <value><c>true</c> если все объекты выбраны; иначе, <c>false</c>. 
        /// Уведомляет интерфейс об изменении.</value>
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

        /// <summary>
        /// Получить предустановленный обработчик запроса на выбор сразу всех объектов.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridCollection">Список объектов.</param>
        /// <returns>Action&lt;System.Boolean&gt;.</returns>
        private static Action<bool> CommonSelectAllHandler<T>(IEnumerable<Selectable<T>> gridCollection)
            => value => gridCollection.ForEach(element => element.IsSelected = value);

        /// <summary>
        /// Получить предустановленный обработчик запроса на текстовый поиск.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridCollection">Коллекция объектов, удовлетворяющих поиску.</param>
        /// <param name="searchCollection">Список объектов для поиска</param>
        /// <param name="searchProperty">Селектор значения для текстового поиска.</param>
        /// <returns>ICommand.</returns>
        private static ICommand CommonSearchHandler<T>(ICollection<Selectable<T>> gridCollection,
            IEnumerable<Selectable<T>> searchCollection, 
            Func<T, string> searchProperty) => new Command(searchQuery =>
            {
                var found =
                    searchCollection.Where(
                        element =>
                            TextSearchRule.SearchSuccesful(searchProperty(element.Instanse), (string) searchQuery))
                        .ToList();
                gridCollection.Clear();
                found.ForEach(gridCollection.Add);
            });

        /// <summary>
        /// Создать модель для выбираемого списка.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridCollection">Коллекция объектов, удовлетворяющих поиску.</param>
        /// <param name="searchCollection">Список объектов для поиска</param>
        /// <param name="searchProperty">Селектор значения для текстового поиска.</param>
        /// <returns>GridOption.</returns>
        public static GridOption Create<T>(ICollection<Selectable<T>> gridCollection,
            IEnumerable<Selectable<T>> searchCollection,
            Func<T, string> searchProperty) => new GridOption
            {
                SelectAllHandler = CommonSelectAllHandler(gridCollection),
                SearchHandler = CommonSearchHandler(gridCollection, searchCollection, searchProperty)
            };
    }
}