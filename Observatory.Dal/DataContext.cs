using System.Collections.Generic;
using System.Linq;
using System.Data;
using Observatory.ObjectModel;
using System.Runtime.Serialization;
using System.IO;

namespace Observatory.Dal.DC
{
    class DataContext
    {
        private OrbsCollection _orbsCollection;

        private int _lastId;

        public int NewId
        { get
            {
                _lastId += 1;
                return _lastId;
            }
        }

        public void IdBack()
        { _lastId -= 1; }

        public ObservableDictionary<int, GalaxiesCollection> GalaxiesCollection
        { get; set; }

        public string ConnectionString
        { get; set; }

        private void NewGalaxiesCollection()
        {
            GalaxiesCollection = new ObservableDictionary<int, GalaxiesCollection>()
            { new KeyValuePair<int, GalaxiesCollection>
            (0, new GalaxiesCollection(0, "Галактики")) };
        }

        public DataContext(string connectionString)
        {
            ConnectionString = connectionString;
            _lastId = 0;
            try
            {
                ReadFromFile();
            }
            catch
            {
                NewGalaxiesCollection();
            }
        }

        public void ReadFromFile()
        {
            var dcs = new DataContractSerializer(typeof(OrbsCollection));

            var fi = new FileInfo(ConnectionString);

            _orbsCollection = new OrbsCollection();

            using (Stream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.Read, 
                FileShare.None))
            {
                _orbsCollection = (OrbsCollection)dcs.ReadObject(fs);
            }
            CopyOrbsCollectionToGalaxiesCollection();
        }

        public void SaveToFile()
        {
            if (GalaxiesCollection[0].NotEmpty)
            {
                CopyGalaxiesCollectionToOrbsCollection();

                var dcs = new DataContractSerializer(typeof(OrbsCollection));

                var fi = new FileInfo(ConnectionString);

                using (Stream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.Write,
                    FileShare.None))
                {
                    dcs.WriteObject(fs, _orbsCollection);
                }
            }
        }

        private void CopyOrbsCollectionToGalaxiesCollection()
        {
            NewGalaxiesCollection();
            foreach (SimpleGalaxy simpleGalaxy in _orbsCollection.GalaxiesCollection)
            {
                var galaxy = new Galaxy();
                simpleGalaxy.CopyToGalaxy(galaxy);
                GalaxiesCollection[0].Add(galaxy.Id, galaxy);
                if (simpleGalaxy.HasOrbs)
                {
                    var simpleStarsCollection =
                        _orbsCollection.StarsCollection.Where
                        (s => s.OwnerGalaxyId == simpleGalaxy.Id);
                    foreach (SimpleStar simpleStar in simpleStarsCollection)
                    {
                        var star = new Star();
                        simpleStar.CopyToStar(star);
                        GalaxiesCollection[0].Galaxies[galaxy.Id].
                            AddStar(star.Id, star);
                        star.OwnerGalaxy = galaxy;
                        if (simpleStar.HasOrbs)
                        {
                            var simplePlanetsCollection = _orbsCollection.
                                PlanetsCollection.Where(s => s.OwnerOrbId == simpleStar.Id);
                            foreach (SimplePlanet simplePlanet in
                                simplePlanetsCollection)
                            {
                                var planet = new Planet();
                                simplePlanet.CopyToPlanet(planet);
                                GalaxiesCollection[0].Galaxies[galaxy.Id].
                                    Stars[star.Id].AddPlanet(planet.Id, planet);
                                planet.OwnerGalaxy = galaxy;
                                planet.OwnerOrb = star;
                                if (simplePlanet.HasOrbs)
                                {
                                    var simpleSecondaryPlanetsCollection =
                                        _orbsCollection.SecondaryPlanetsCollection.
                                        Where(s => s.OwnerOrbId == simplePlanet.Id);
                                    foreach (SimplePlanet simpleSecondaryPlanet
                                        in simpleSecondaryPlanetsCollection)
                                    {
                                        var secondaryPlanet = new Planet();
                                        simpleSecondaryPlanet.CopyToPlanet
                                            (secondaryPlanet);
                                        GalaxiesCollection[0].Galaxies
                                            [galaxy.Id].
                                            Stars[star.Id].Planets
                                            [planet.Id].AddSecondaryPlanet
                                            (secondaryPlanet.Id, secondaryPlanet);
                                        secondaryPlanet.OwnerGalaxy = galaxy;
                                        secondaryPlanet.OwnerOrb = planet;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CopyGalaxiesCollectionToOrbsCollection()
        {
            _orbsCollection = new OrbsCollection();
            foreach (Galaxy galaxy in GalaxiesCollection[0].Galaxies.Values)
            {
                _orbsCollection.GalaxiesCollection.Add(new SimpleGalaxy(galaxy));
                if (galaxy.HasStars)
                {
                    foreach (Star star in galaxy.Stars.Values)
                    {
                        _orbsCollection.StarsCollection.Add(new SimpleStar(star));
                        if (star.HasPlanets)
                        {
                            foreach (Planet planet in star.Planets.Values)
                            {
                                _orbsCollection.PlanetsCollection.
                                    Add(new SimplePlanet(planet));
                                if (planet.HasSecondaryPlanets)
                                {
                                    foreach (Planet secondaryPlanet in 
                                        planet.SecondaryPlanets.Values)
                                    {
                                        _orbsCollection.SecondaryPlanetsCollection.
                                            Add(new SimplePlanet(secondaryPlanet));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
