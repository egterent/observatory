using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;

using Observatory.ObjectModel;
using Observatory.ViewModel;
using Observatory.Dal.Repositories;
using Observatory.ObjectModel.Enums;

namespace Observatory
{
    /// <summary>
    /// Логика взаимодействия для ViewCatalog.xaml
    /// </summary>
    public partial class ViewCatalog : Page
    {
        public GalaxiesRepository GalaxiesRepository { get; set; }

        public SortedCollectionView OrbsSortedCollectionView;

        public ViewCatalog()
        {
            InitializeComponent();
            InitializeData();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void InitializeData()
        {
            GalaxiesRepository = new GalaxiesRepository(ConfigurationManager.
                AppSettings["ComputerObservatory"].ToString());           
            CatalogTreeView.DataContext = GalaxiesRepository.GetGalaxiesCollection();
        }


        private void CatalogTreeView_SelectedItemChanged(object sender, 
            RoutedPropertyChangedEventArgs<object> e)
        {            
            var SelectedOrb = CatalogTreeView.SelectedItem as IHaveOrbs;
            var SelectedOrbType = CatalogTreeView.SelectedItem.GetType();
            if (SelectedOrb.OrbsCollection != null)
            {
                OrbsSortedCollectionView = 
                    new SortedCollectionView(SelectedOrb.OrbsCollection);
                CatalogListView.DataContext = OrbsSortedCollectionView;               
                if (SelectedOrbType == typeof(GalaxiesCollection))
                {
                    CatalogListView.View =
                    CatalogListView.FindResource("GalaxyGridView") as ViewBase;
                }
                else
                {
                    if (SelectedOrbType == typeof(Galaxy))
                    {
                        CatalogListView.View =
                        CatalogListView.FindResource("StarGridView") as ViewBase;                        
                    }
                    else
                    {
                        CatalogListView.View =
                        CatalogListView.FindResource("PlanetGridView") as ViewBase;
                    }
                }
            }
            if (SelectedOrbType == typeof(Galaxy))
            {
                AGalaxyControl.DataContext = CatalogTreeView.SelectedItem;
            }
            else
            {
                if (SelectedOrbType == typeof(Star))
                {
                    AStarControl.DataContext = CatalogTreeView.SelectedItem;
                    APlanetControl.AddingPlanetOwner = CatalogTreeView.SelectedItem;
                }
                else
                {
                    if (SelectedOrbType == typeof(Planet))
                    {
                        APlanetControl.DataContext = CatalogTreeView.SelectedItem;
                        APlanetControl.AddingPlanetOwner = CatalogTreeView.SelectedItem;
                    }
                }
            }                
        }

        private void OnSaveToFile(object sender, RoutedEventArgs e)
        {
            GalaxiesRepository.SaveToFile();
        }

        private void OnQuit(object sender, RoutedEventArgs e)
        {
            if (GalaxiesRepository.IsChanged)
            {
                string message = "В каталог были внесены изменения. Сохранить изменения перед выходом?";
                var mb = MessageBox.Show(message,"Observatory", MessageBoxButton.YesNo);
                if (mb == MessageBoxResult.Yes)
                {
                    GalaxiesRepository.SaveToFile();
                }
            }
            App.Current.Exit += delegate { MessageBox.Show("Приложение будет закрыто."); };
            App.Current.Shutdown();          
        }

        private void OnAddGalaxy(object sender, RoutedEventArgs e)
        {
            var newGalaxy = new Galaxy(AGalaxyControl.GalaxyNameTB.Text);
            GalaxiesRepository.AddGalaxy(newGalaxy);
        }

        private void OnChangeGalaxy(object sender, RoutedEventArgs e)
        {
            var newGalaxy = new Galaxy(AGalaxyControl.GalaxyNameTB.Text);
            var g = CatalogTreeView.SelectedItem as Galaxy;
            if (g != null)
                GalaxiesRepository.ChangeGalaxy(g.Id, newGalaxy);
        }

        private void OnDeleteGalaxy(object sender, RoutedEventArgs e)
        {
            var g = CatalogTreeView.SelectedItem as Galaxy;
            if (g != null)
                GalaxiesRepository.DeleteGalaxy(g.Id);
        }

        private void OnAddStar(object sender, RoutedEventArgs e)
        {
            var newStar = new Star(AStarControl.StarNameTB.Text,
                (SpectralType)Enum.Parse
                (typeof(SpectralType), AStarControl.SpectralTypeCB.Text),
                float.Parse(AStarControl.MagnitudeTB.Text),
                float.Parse(AStarControl.RectascentionTB.Text),
                float.Parse(AStarControl.DeclinationAngelTB.Text),
                float.Parse(AStarControl.TemperatureTB.Text),
                float.Parse(AStarControl.DistanceTB.Text));
            var g = CatalogTreeView.SelectedItem as Galaxy;
            if (g != null)
                GalaxiesRepository.AddStar(g.Id, newStar);
        }

        private void OnChangeStar(object sender, RoutedEventArgs e)
        {
            var newStar = new Star(AStarControl.StarNameTB.Text,
                (SpectralType)Enum.Parse
                (typeof(SpectralType), AStarControl.SpectralTypeCB.Text),
                float.Parse(AStarControl.MagnitudeTB.Text),
                float.Parse(AStarControl.RectascentionTB.Text),
                float.Parse(AStarControl.DeclinationAngelTB.Text),
                float.Parse(AStarControl.TemperatureTB.Text),
                float.Parse(AStarControl.DistanceTB.Text));
            var s = CatalogTreeView.SelectedItem as Star;
            if (s != null)
                GalaxiesRepository.ChangeStar(s.Id, s.OwnerGalaxy.Id, newStar);
        }

        private void OnDeleteStar(object sender, RoutedEventArgs e)
        {
            var s = CatalogTreeView.SelectedItem as Star;
            if (s != null)
                GalaxiesRepository.DeleteStar(s.Id, s.OwnerGalaxy.Id);
        }

        private void OnAddPlanet(object sender, RoutedEventArgs e)
        {
            var newPlanet = new Planet(APlanetControl.PlanetNameTB.Text,
                float.Parse(APlanetControl.MassTB.Text),
                float.Parse(APlanetControl.DiameterTB.Text),
                float.Parse(APlanetControl.MeanDistanceTB.Text),
                float.Parse(APlanetControl.SiderealPeriodTB.Text),
                float.Parse(APlanetControl.SynodicPeriodTB.Text));
            var s = CatalogTreeView.SelectedItem as Star;
            if (s != null)
                GalaxiesRepository.AddPlanet(s.OwnerGalaxy.Id, s.Id, newPlanet);
        }

        private void OnChangePlanet(object sender, RoutedEventArgs e)
        {
            var newPlanet = new Planet(APlanetControl.PlanetNameTB.Text,
                float.Parse(APlanetControl.MassTB.Text),
                float.Parse(APlanetControl.DiameterTB.Text),
                float.Parse(APlanetControl.MeanDistanceTB.Text),
                float.Parse(APlanetControl.SiderealPeriodTB.Text),
                float.Parse(APlanetControl.SynodicPeriodTB.Text));
            var p = CatalogTreeView.SelectedItem as Planet;
            if (p != null)
            {
                var s = p.OwnerOrb as Star;
                GalaxiesRepository.ChangePlanet
                    (p.Id, p.OwnerGalaxy.Id, s.Id, newPlanet);
            }
        }

        private void OnDeletePlanet(object sender, RoutedEventArgs e)
        {
            var p = CatalogTreeView.SelectedItem as Planet;
            if (p != null)
            {
                var s = p.OwnerOrb as Star;
                GalaxiesRepository.DeletePlanet
                    (p.Id, p.OwnerGalaxy.Id, s.Id);
            }
        }

        private void OnAddSecondaryPlanet(object sender, RoutedEventArgs e)
        {
            var newPlanet = new Planet(APlanetControl.PlanetNameTB.Text,
                float.Parse(APlanetControl.MassTB.Text),
                float.Parse(APlanetControl.DiameterTB.Text),
                float.Parse(APlanetControl.MeanDistanceTB.Text),
                float.Parse(APlanetControl.SiderealPeriodTB.Text),
                float.Parse(APlanetControl.SynodicPeriodTB.Text));
            var p = CatalogTreeView.SelectedItem as Planet;
            if (p != null)
            {
                var s = p.OwnerOrb as Star;
                GalaxiesRepository.AddSecondaryPlanet
                    (p.OwnerGalaxy.Id, s.Id, p.Id, newPlanet);
            }
        }

        private void OnChangeSecondaryPlanet(object sender, RoutedEventArgs e)
        {
            var newPlanet = new Planet(APlanetControl.PlanetNameTB.Text,
                float.Parse(APlanetControl.MassTB.Text),
                float.Parse(APlanetControl.DiameterTB.Text),
                float.Parse(APlanetControl.MeanDistanceTB.Text),
                float.Parse(APlanetControl.SiderealPeriodTB.Text),
                float.Parse(APlanetControl.SynodicPeriodTB.Text));
            var sp = CatalogTreeView.SelectedItem as Planet;
            if (sp != null)
            {
                var p = sp.OwnerOrb as Planet;
                var s = p.OwnerOrb as Star;
                GalaxiesRepository.ChangeSecondaryPlanet
                    (sp.Id, sp.OwnerGalaxy.Id, s.Id, p.Id, newPlanet);
            }
        }

        private void OnDeleteSecondaryPlanet(object sender, RoutedEventArgs e)
        {
            var sp = CatalogTreeView.SelectedItem as Planet;
            if (sp != null)
            {
                var p = sp.OwnerOrb as Planet;
                var s = p.OwnerOrb as Star;
                GalaxiesRepository.DeleteSecondaryPlanet
                    (sp.Id, sp.OwnerGalaxy.Id, s.Id, p.Id);
            }
        }

        private void OnChangePlanetType(object sender, RoutedEventArgs e)
        {
            APlanetControl.DataContext = null;
        }

    }
}
