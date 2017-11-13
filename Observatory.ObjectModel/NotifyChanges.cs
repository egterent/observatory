using System.ComponentModel;

namespace Observatory.ObjectModel
{
    public class NotifyChanges: INotifyPropertyChanged
    {
        public int Id
        { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
