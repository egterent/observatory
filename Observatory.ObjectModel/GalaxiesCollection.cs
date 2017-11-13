using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Observatory.ObjectModel.OrbExceptions;

namespace Observatory.ObjectModel
{
    public class GalaxiesCollection : NotifyChanges, IHaveOrbs,
        IEnumerable<KeyValuePair<int, Galaxy>>
    {
        private string _name;

        public string Name
        {
            get { return _name; }

            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private ObservableDictionary<int, Galaxy> _galaxies;

        public ObservableDictionary<int, Galaxy> Galaxies
        {
            get { return _galaxies; }

            set
            {
                if (value != null)
                {
                    _galaxies = value;
                    NotifyPropertyChanged("Galaxies");
                }
            }
        }

        public int GalaxiesCount
        {
            get { return _galaxies != null ? _galaxies.Count : 0; }
        }

        public int StarsCount
        {
            get { return _galaxies != null ? 
                    _galaxies.Select(s => s.Value.StarsCount).Sum() : 0; }
        }

        public int PlanetsCount
        {
            get { return _galaxies != null ? 
                    _galaxies.Select(s => s.Value.PlanetsCount).Sum() : 0; }
        }

        public int SecondaryPlanetsCount
        {
            get { return _galaxies != null ? _galaxies.SelectMany
                    (s => s.Value.Stars.SelectMany(z => z.Value.Planets.Select
                    (v => v.Value.SecondaryPlanetsCount))).Sum() : 0; }
        }

        public bool NotEmpty
        {
            get { return (GalaxiesCount + StarsCount + PlanetsCount + 
                    SecondaryPlanetsCount) == 0 ? false : true; } 
        }


        public ICollection OrbsCollection
        {
            get
            {
                return _galaxies != null ?
                    (ICollection)_galaxies.Values : null;
            }
        }

        public GalaxiesCollection(string galaxiesCollectionName)
        {
            _name = galaxiesCollectionName;
            _galaxies = new ObservableDictionary<int, Galaxy>();
        }

        public GalaxiesCollection(int id, string galaxiesCollectionName) : 
            this(galaxiesCollectionName)
        {
            Id = id;
        }

        public void Add(int id, Galaxy galaxy)
        {
            if (_galaxies == null)
                _galaxies = new ObservableDictionary<int, Galaxy>();            
            OrbNullArgumentException(galaxy, "Galaxy");
            OrbIdDetectedArgumentException(_galaxies, id);
            OrbNameDetectedArgumentException(_galaxies, galaxy.Name);
            galaxy.Id = id;
            _galaxies.Add(id, galaxy);
            NotifyPropertyChanged("Galaxies");
        }

        public void ChangeGalaxy(int id, Galaxy newGalaxy)
        {
            OrbFaildArgumentException(_galaxies, id);                     
            _galaxies.Remove(id);
            _galaxies.Add(id, newGalaxy);
            NotifyPropertyChanged("Galaxies");
        }

        public void DeleteGalaxy(int id)
        {
            OrbFaildArgumentException(_galaxies, id);
            var RemovedGalaxy = _galaxies[id];
            _galaxies.Remove(id);
            if (_galaxies.Count == 0)
                _galaxies = null;
            NotifyPropertyChanged("Galaxies");
        }

        public void ClearGalaxies()
        {
            if (_galaxies != null)
            {                
                _galaxies.Clear();
                _galaxies = null;
                NotifyPropertyChanged("Galaxies");
            }
        }

        public IEnumerator<KeyValuePair<int, Galaxy>> GetEnumerator()
        { return _galaxies.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}
