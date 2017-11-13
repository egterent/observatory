using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Observatory.ObjectModel.OrbExceptions;

namespace Observatory.ObjectModel
{
    public class Galaxy : NotifyChanges, IHaveOrbs, IEquatable<Galaxy>
    {
        private ObservableDictionary<int, Star> _stars;

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                OrbNameArgumentException(value, "галактики");
                if (value != _name)
                {                   
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public Galaxy()
        {
            _stars = new ObservableDictionary<int, Star>();
        }

        public Galaxy(string galaxyName) : this()
        {
            OrbNameArgumentException(galaxyName, "галактики");
            _name = galaxyName;
            NotifyPropertyChanged("Name");
        }

        public Galaxy(int id, string galaxyName) : this(galaxyName)
        {
            Id = id;
            NotifyPropertyChanged("Id");
        }

        public Galaxy(int id, string galaxyName, Star[] Stars) : this(id, galaxyName)
        {            
            foreach (Star S in Stars)
                _stars.Add(S.Name, S);
            NotifyPropertyChanged("Stars");
        }

        public bool HasStars
        {
            get { return _stars != null ? _stars.Count > 0 : false; }
        }

        public int StarsCount
        { get { return _stars != null ? _stars.Count : 0;  } }

        public ObservableDictionary<int, Star> Stars
        {
            get { return _stars; }

            set
            {
                if (value != null)
                {
                    _stars = value;
                    NotifyPropertyChanged("Stars");
                }
            }
        }

        public bool HasPlanets
        { get { return _stars.Any(s => s.Value.HasPlanets); } }

        public int PlanetsCount
        { get { return Planets.Count(); } }

        public IEnumerable<KeyValuePair<int, Planet>> Planets
        {
            get
            {
                return _stars.Where(s => s.Value.HasPlanets).
                    SelectMany(s => s.Value.Planets);
            }
        }

        public ICollection OrbsCollection
        {
            get
            {
                return _stars != null ? 
                    (ICollection)_stars.Values : null;
            }
        }

        public void AddStar(int id, Star newStar)
        {
            if (_stars == null)
                _stars = new ObservableDictionary<int, Star>();
            OrbNullArgumentException(newStar, "Star");
            OrbIdDetectedArgumentException(_stars, id);
            OrbNameDetectedArgumentException(_stars, newStar.Name);
            newStar.Id = id;
            _stars.Add(id, newStar);
            newStar.OwnerGalaxy = this;
            NotifyPropertyChanged("Stars");
        }

        public void ChangeStar(int id, Star newStar)
        {
            OrbFaildArgumentException(_stars, id);
            if (_stars[id] != newStar)
            {                
                _stars[id] = newStar;
                newStar.OwnerGalaxy = this;
                NotifyPropertyChanged("Stars");
            }
        }

        public void DeleteStar(int id)
        {
            OrbFaildArgumentException(_stars, id);
            _stars.Remove(id);
            if (_stars.Count == 0)
                _stars = null;
            NotifyPropertyChanged("Stars");
        }

        public bool Equals(Galaxy other)
        {
            return _name == other.Name && _stars == other.Stars;
        }

        public override bool Equals(object obj)
        {
            if (obj is Galaxy)
                return Equals((Galaxy)obj);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return _stars.Count + PlanetsCount + _name.GetHashCode();
        }

        public static bool operator ==(Galaxy galaxy1, Galaxy galaxy2)
        {
            if ((object)galaxy1 == null || (object)galaxy2 == null)
                return Equals(galaxy1, galaxy2);
            else
                return galaxy1.Equals(galaxy2);
        }

        public static bool operator !=(Galaxy galaxy1, Galaxy galaxy2)
        {
            if ((object)galaxy1 == null || (object)galaxy2 == null)
                return !Equals(galaxy1, galaxy2);
            else
                return !galaxy1.Equals(galaxy2);
        }
    }
}
