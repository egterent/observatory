using System;
using System.Windows;

namespace Observatory.Dal.Repositories
{
    public class MessageBoxShow
    {
        public MessageBoxShow(Exception ex)
        {
            MessageBox.Show(ex.Message, "Observatory", MessageBoxButton.OK);
        }
    }
}
