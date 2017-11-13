using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Observatory.ObjectModel;
using Observatory.Dal.DC;

namespace Observatory.Dal.Repositories
{
    public class GalaxiesRepository
    {
        private DataContext dc;

        public bool IsChanged { get; internal set; }

        public GalaxiesRepository(string connectionString)
        {
            IsChanged = false;
            try
            {
                dc = new DataContext(connectionString);
            }
            catch (Exception ex)
            {
                var mb = new MessageBoxShow(ex);
            }           
        }

        public void SaveToFile()
        {
            try
            {
                dc.SaveToFile();
            }
            catch (Exception ex)
            {
                var mb = new MessageBoxShow(ex);
            }            
        }

        public void AddGalaxy(Galaxy newGalaxy)
        {
            try
            {
                dc.GalaxiesCollection[0].Add(dc.NewId, newGalaxy);
                IsChanged = true;
            }
            catch (Exception ex)
            {
                dc.IdBack();
                var mb = new MessageBoxShow(ex);
            }            
        }

        public void AddStar(int ownerGalaxyId, Star newStar)
        {
            try
            {
                dc.GalaxiesCollection[0].Galaxies[ownerGalaxyId].AddStar(dc.NewId, newStar);
                IsChanged = true;
            }
            catch (Exception ex)
            {
                dc.IdBack();
                var mb = new MessageBoxShow(ex);
            }           
        }

        public void AddSecondaryPlanet(int ownerGalaxyId, int ownerStarId,
            int ownerPlanetId, Planet newSecondaryPlanet)
        {
            try
            {
                dc.GalaxiesCollection[0].Galaxies[ownerGalaxyId].Stars[ownerStarId].
                    Planets[ownerPlanetId].AddSecondaryPlanet(dc.NewId, newSecondaryPlanet);
                IsChanged = true;
            }
            catch (Exception ex)
            {
                dc.IdBack();
                var mb = new MessageBoxShow(ex);
            }          
        }

        public void AddPlanet(int ownerGalaxyId, int ownerStarId, Planet newPlanet)
        {
            try
            {
                dc.GalaxiesCollection[0].Galaxies[ownerGalaxyId].Stars[ownerStarId].AddPlanet
                    (dc.NewId, newPlanet);
                IsChanged = true;
            }
            catch (Exception ex)
            {
                dc.IdBack();
                var mb = new MessageBoxShow(ex);                
            }            
        }

        public void ChangeSecondaryPlanet(int id, int ownerGalaxyId, int ownerStarId,
            int ownerPlanetId, Planet newSecondaryPlanet)
        {
            try
            {
                dc.GalaxiesCollection[0].Galaxies[ownerGalaxyId].Stars[ownerStarId].
                    Planets[ownerPlanetId].ChangeSecondaryPlanet(id, newSecondaryPlanet);
                IsChanged = true;
            }
            catch (Exception ex)
            {
                var mb = new MessageBoxShow(ex);
            }
        }

        public void ChangePlanet(int id, int ownerGalaxyId, int ownerStarId, Planet newPlanet)
        {
            try
            {
                dc.GalaxiesCollection[0].Galaxies[ownerGalaxyId].Stars[ownerStarId].
                    ChangePlanet(id, newPlanet);
                IsChanged = true;
            }
            catch (Exception ex)
            {
                var mb = new MessageBoxShow(ex);
            }
        }

        public void ChangeStar(int id, int ownerGalaxyId, Star newStar)
        {
            try
            {
                dc.GalaxiesCollection[0].Galaxies[ownerGalaxyId].
                    ChangeStar(id, newStar);
                IsChanged = true;
            }
            catch (Exception ex)
            {
                var mb = new MessageBoxShow(ex);
            }
        }

        public void ChangeGalaxy(int id, Galaxy newGalaxy)
        {
            try
            {
                dc.GalaxiesCollection[0].ChangeGalaxy(id, newGalaxy);
                IsChanged = true;
            }
            catch (Exception ex)
            {
                var mb = new MessageBoxShow(ex);
            }
        }

        public void DeleteSecondaryPlanet(int id, int ownerGalaxyId, int ownerStarId,
            int ownerPlanetId)
        {
            try
            {
                dc.GalaxiesCollection[0].Galaxies[ownerGalaxyId].Stars[ownerStarId].
                    Planets[ownerPlanetId].DeleteSecondaryPlanet(id);
                IsChanged = true;
            }
            catch (Exception ex)
            {
                var mb = new MessageBoxShow(ex);
            }            
        }

        public void DeletePlanet(int id, int ownerGalaxyId, int ownerStarId)
        {
            try
            {
                dc.GalaxiesCollection[0].Galaxies[ownerGalaxyId].Stars[ownerStarId].
                    DeletePlanet(id);
                IsChanged = true;
            }
            catch (Exception ex)
            {
                var mb = new MessageBoxShow(ex);
            }
        }

        public void DeleteStar(int id, int ownerGalaxyId)
        {
            try
            {
                dc.GalaxiesCollection[0].Galaxies[ownerGalaxyId].DeleteStar(id);
                IsChanged = true;
            }
            catch (Exception ex)
            {
                var mb = new MessageBoxShow(ex);
            }
        }

        public void DeleteGalaxy(int id)
        {
            try
            {
                dc.GalaxiesCollection[0].DeleteGalaxy(id);
                IsChanged = true;
            }
            catch (Exception ex)
            {
                var mb = new MessageBoxShow(ex);
            }
        }

        public ObservableDictionary<int, GalaxiesCollection> GetGalaxiesCollection()
        {
            return dc.GalaxiesCollection;
        }
    }
}
