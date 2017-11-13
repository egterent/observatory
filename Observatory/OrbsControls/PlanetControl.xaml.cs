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

using Observatory.ObjectModel;

namespace Observatory.OrbsControls
{
    /// <summary>
    /// Логика взаимодействия для PlanetControl.xaml
    /// </summary>
    public partial class PlanetControl : UserControl
    {
        public object AddingPlanetOwner { get; set; }

        private static readonly RoutedEvent AddSecondaryPlanetEvent;
        public event RoutedEventHandler AddSecondaryPlanet
        {
            add { AddHandler(AddSecondaryPlanetEvent, value); }
            remove { RemoveHandler(AddSecondaryPlanetEvent, value); }
        }

        private static readonly RoutedEvent ChangeSecondaryPlanetEvent;
        public event RoutedEventHandler ChangeSecondaryPlanet
        {
            add { AddHandler(ChangeSecondaryPlanetEvent, value); }
            remove { RemoveHandler(ChangeSecondaryPlanetEvent, value); }
        }

        private static readonly RoutedEvent DeleteSecondaryPlanetEvent;
        public event RoutedEventHandler DeleteSecondaryPlanet
        {
            add { AddHandler(DeleteSecondaryPlanetEvent, value); }
            remove { RemoveHandler(DeleteSecondaryPlanetEvent, value); }
        }

        private static readonly RoutedEvent AddPlanetEvent;
        public event RoutedEventHandler AddPlanet
        {
            add { AddHandler(AddPlanetEvent, value); }
            remove { RemoveHandler(AddPlanetEvent, value); }
        }

        private static readonly RoutedEvent ChangePlanetEvent;
        public event RoutedEventHandler ChangePlanet
        {
            add { AddHandler(ChangePlanetEvent, value); }
            remove { RemoveHandler(ChangePlanetEvent, value); }
        }

        private static readonly RoutedEvent DeletePlanetEvent;
        public event RoutedEventHandler DeletePlanet
        {
            add { AddHandler(DeletePlanetEvent, value); }
            remove { RemoveHandler(DeletePlanetEvent, value); }
        }

        private static readonly RoutedEvent ChangePlanetTypeEvent;
        public event RoutedEventHandler ChangePlanetType
        {
            add { AddHandler(ChangePlanetTypeEvent, value); }
            remove { RemoveHandler(ChangePlanetTypeEvent, value); }
        }

        static PlanetControl()
        {
            AddSecondaryPlanetEvent = EventManager.RegisterRoutedEvent("AddSecondaryPlanet",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PlanetControl));
            ChangeSecondaryPlanetEvent =
                EventManager.RegisterRoutedEvent("ChangeSecondaryPlanet",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PlanetControl));
            DeleteSecondaryPlanetEvent =
                EventManager.RegisterRoutedEvent("DeleteSecondaryPlanet",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PlanetControl));
            AddPlanetEvent = EventManager.RegisterRoutedEvent("AddPlanet",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PlanetControl));
            ChangePlanetEvent = EventManager.RegisterRoutedEvent("ChangePlanet",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PlanetControl));
            DeletePlanetEvent = EventManager.RegisterRoutedEvent("DeletePlanet",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PlanetControl));
            ChangePlanetTypeEvent = EventManager.RegisterRoutedEvent("ChangePlanetType",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PlanetControl));
        }

        public PlanetControl()
        {
            InitializeComponent();
        }

        private void OnAddPlanet(object sender, RoutedEventArgs e)
        {
            if (AddingPlanetOwner.GetType() == typeof(Star))
                RaiseEvent(new RoutedEventArgs(AddPlanetEvent));
            else
                RaiseEvent(new RoutedEventArgs(AddSecondaryPlanetEvent));
        }

        private bool IsSecondaryPlanet()
        {
            var aPlanet = DataContext as Planet;
            return aPlanet.OwnerOrb.GetType() == typeof(Planet);
        }

        private void OnChangePlanet(object sender, RoutedEventArgs e)
        {
            if (IsSecondaryPlanet())
                RaiseEvent(new RoutedEventArgs(ChangeSecondaryPlanetEvent));
            else
                RaiseEvent(new RoutedEventArgs(ChangePlanetEvent));
        }

        private void OnDeletePlanet(object sender, RoutedEventArgs e)
        {
            if (IsSecondaryPlanet())
                RaiseEvent(new RoutedEventArgs(DeleteSecondaryPlanetEvent));
            else
                RaiseEvent(new RoutedEventArgs(DeletePlanetEvent));
        }

        private void OnChangePlanetType(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ChangePlanetTypeEvent));
        }
    }
}
