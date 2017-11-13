using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Observatory
{
    /// <summary>
    /// Логика взаимодействия для EnterToObservatory.xaml
    /// </summary>
    public partial class EnterToObservatory : Page
    {
        public EnterToObservatory()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (loadingTypesListBox.SelectedIndex)
            {
                case 0: 
                    var viewCatalog = new ViewCatalog();
                    NavigationService.Navigate(viewCatalog);
                    break;
                case 1:
                    var authorizationPage = new AuthorizationPage();
                    NavigationService.Navigate(authorizationPage);
                    break;
            }
            loadingTypesListBox.SelectedIndex = -1;
        }
    }
}
