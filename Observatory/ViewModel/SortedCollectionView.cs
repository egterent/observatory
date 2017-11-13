using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

using Observatory.ObjectModel;
using System.Collections;

namespace Observatory.ViewModel
{
    public class SortedCollectionView
    {
        private ICollection _firstCollection;
        private CollectionViewSource _firstCollectionView;
        private string _sortColumn;
        private ListSortDirection _sortDirection;

        public ICommand SortCommand
        {
            get;
            private set;
        }

        public SortedCollectionView(ICollection firstCollection)
        {
            SortCommand = new RelayCommand(Sort);
            FirstCollection = firstCollection;                        
        }

        public CollectionView FirstCollectionView
        {
            get
            {
                return (CollectionView)_firstCollectionView.View;
            }
        }

        public ICollection FirstCollection
        {
            get
            {
                return _firstCollection;
            }

            set
            {
                _firstCollection = value;
                _firstCollectionView = new CollectionViewSource();
                _firstCollectionView.Source = _firstCollection;
            }
        }

        public void Sort(object parameter)
        {
            string column = parameter as string;
            if (_sortColumn == column)
            {
                // Toggle sorting direction 
                _sortDirection = _sortDirection == ListSortDirection.Ascending ?
                    ListSortDirection.Descending : ListSortDirection.Ascending;
            }
            else
            {
                _sortColumn = column;
                _sortDirection = ListSortDirection.Ascending;
            }

            _firstCollectionView.SortDescriptions.Clear();
            _firstCollectionView.SortDescriptions.Add
                (new SortDescription(_sortColumn, _sortDirection));
        }

    }
}
