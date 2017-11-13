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

namespace Observatory.OrbsControls
{
    /// <summary>
    /// Логика взаимодействия для StarControl.xaml
    /// </summary>
    public partial class StarControl : UserControl
    {
        private static readonly RoutedEvent AddStarEvent;
        public event RoutedEventHandler AddStar
        {
            add { AddHandler(AddStarEvent, value); }
            remove { RemoveHandler(AddStarEvent, value); }
        }

        private static readonly RoutedEvent ChangeStarEvent;
        public event RoutedEventHandler ChangeStar
        {
            add { AddHandler(ChangeStarEvent, value); }
            remove { RemoveHandler(ChangeStarEvent, value); }
        }

        private static readonly RoutedEvent DeleteStarEvent;
        public event RoutedEventHandler DeleteStar
        {
            add { AddHandler(DeleteStarEvent, value); }
            remove { RemoveHandler(DeleteStarEvent, value); }
        }

        private static readonly RoutedEvent ClearPlanetsEvent;
        public event RoutedEventHandler ClearPlanets
        {
            add { AddHandler(ClearPlanetsEvent, value); }
            remove { RemoveHandler(ClearPlanetsEvent, value); }
        }

        static StarControl()
        {
            AddStarEvent = EventManager.RegisterRoutedEvent("AddStar",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(StarControl));
            ChangeStarEvent = EventManager.RegisterRoutedEvent("ChangeStar",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(StarControl));
            DeleteStarEvent = EventManager.RegisterRoutedEvent("DeleteStar",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(StarControl));
            ClearPlanetsEvent = EventManager.RegisterRoutedEvent("ClearStars",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(StarControl));
        }

        public StarControl()
        {
            InitializeComponent();
        }

        private void OnAddStar(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(AddStarEvent));
        }

        private void OnChangeStar(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ChangeStarEvent));
        }

        private void OnDeleteStar(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(DeleteStarEvent));
        }

        private void OnClearPlanets(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ClearPlanetsEvent));
        }

    }
}
