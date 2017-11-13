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

using Observatory.Authentication;

namespace Observatory
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var userPrincipal = new ObservatoryPrincipal(LoginTB.Text,PasswordB.Password);
            if (userPrincipal.Identity.IsAuthenticated)
            {
                var viewCatalog = new ViewCatalog();
                NavigationService.Navigate(viewCatalog);
                viewCatalog.AGalaxyControl.ButtonsPanel.Visibility = Visibility.Visible;
                viewCatalog.AStarControl.ButtonsPanel.Visibility = Visibility.Visible;
                viewCatalog.APlanetControl.ButtonsPanel.Visibility = Visibility.Visible;
            }
        }
    }
}
