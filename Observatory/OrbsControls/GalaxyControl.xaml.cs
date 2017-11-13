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
    /// Логика взаимодействия для GalaxyControl.xaml
    /// </summary>
    public partial class GalaxyControl : UserControl
    {
        private static readonly RoutedEvent AddGalaxyEvent;
        public event RoutedEventHandler AddGalaxy
        {
            add { AddHandler(AddGalaxyEvent, value); }
            remove { RemoveHandler(AddGalaxyEvent, value); }
        }

        private static readonly RoutedEvent ChangeGalaxyEvent;
        public event RoutedEventHandler ChangeGalaxy
        {
            add { AddHandler(ChangeGalaxyEvent, value); }
            remove { RemoveHandler(ChangeGalaxyEvent, value); }
        }

        private static readonly RoutedEvent DeleteGalaxyEvent;
        public event RoutedEventHandler DeleteGalaxy
        {
            add { AddHandler(DeleteGalaxyEvent, value); }
            remove { RemoveHandler(DeleteGalaxyEvent, value); }
        }

        private static readonly RoutedEvent ClearStarsEvent;
        public event RoutedEventHandler ClearStars
        {
            add { AddHandler(ClearStarsEvent, value); }
            remove { RemoveHandler(ClearStarsEvent, value); }
        }

        private static readonly RoutedEvent ClearGalaxiesEvent;
        public event RoutedEventHandler ClearGalaxies
        {
            add { AddHandler(ClearGalaxiesEvent, value); }
            remove { RemoveHandler(ClearGalaxiesEvent, value); }
        }

        static GalaxyControl()
        {
            AddGalaxyEvent = EventManager.RegisterRoutedEvent("AddGalaxy",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(GalaxyControl));
            ChangeGalaxyEvent = EventManager.RegisterRoutedEvent("ChangeGalaxy",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(GalaxyControl));
            DeleteGalaxyEvent = EventManager.RegisterRoutedEvent("DeleteGalaxy",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(GalaxyControl));
            ClearStarsEvent = EventManager.RegisterRoutedEvent("ClearStars",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(GalaxyControl));
            ClearGalaxiesEvent = EventManager.RegisterRoutedEvent("ClearGalaxies",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(GalaxyControl));
        }

        public GalaxyControl()
        {
            InitializeComponent();
        }

        private void OnAddGalaxy(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(AddGalaxyEvent));
        }

        private void OnChangeGalaxy(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ChangeGalaxyEvent));
        }

        private void OnDeleteGalaxy(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(DeleteGalaxyEvent));
        }

        private void OnClearStars(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ClearStarsEvent));
        }

        private void OnClearGalaxies(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ClearGalaxiesEvent));
        }
    }
}
