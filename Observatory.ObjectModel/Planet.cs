using System;
using System.Collections;
using static Observatory.ObjectModel.OrbExceptions;

namespace Observatory.ObjectModel
{
    public class Planet : NotifyChanges, IHaveOrbs, IEquatable<Planet>
    {
        private string _name;

        private float _mass;

        private float _diameter;

        private float _meanDistance;

        private float _siderealPeriod;

        private float _synodicPeriod;

        private ObservableDictionary<int, Planet> _secondaryPlanets;

        public IHaveOrbs OwnerOrb { get; set; }

        public Galaxy OwnerGalaxy { get; set; }        

        public string Name
        {
            get { return _name; }
            set
            {
                OrbNameArgumentException(value, "планеты");
                if (value != _name)
                {                    
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public float Mass
        {
            get { return _mass; }
            set
            {
                OrbCharsArgumentException(value, "массы планеты");
                if (value != _mass)
                {                   
                    _mass = value;
                    NotifyPropertyChanged("Mass");
                }
            }
        }

        public float Diameter
        {
            get { return _diameter; }
            set
            {
                OrbCharsArgumentException(value, "диаметра планеты");
                if (value != _diameter)
                {                    
                    _diameter = value;
                    NotifyPropertyChanged("Diameter");
                }
            }
        }

        public float MeanDistance
        {
            get { return _meanDistance; }
            set
            {
                OrbCharsArgumentException(value, "среднего расстояния планеты");
                if (value != _meanDistance)
                {                   
                    _meanDistance = value;
                    NotifyPropertyChanged("MeanDistance");
                }
            }
        }

        public float SiderealPeriod
        {
            get { return _siderealPeriod; }
            set
            {
                OrbCharsArgumentException(value, "сидерального периода планеты");
                if (value != _siderealPeriod)
                {                  
                    _siderealPeriod = value;
                    NotifyPropertyChanged("SideralPeriod");
                }
            }
        }

        public float SynodicPeriod
        {
            get { return _synodicPeriod; }
            set
            {
                OrbCharsArgumentException(value, "синодического периода планеты");
                if (value != _synodicPeriod)
                {
                    
                    _synodicPeriod = value;
                    NotifyPropertyChanged("SynodicPeriod");
                }
            }
        }

        public int SecondaryPlanetsCount
        {
            get { return _secondaryPlanets != null ? _secondaryPlanets.Count : 0; }
        }

        public ObservableDictionary<int, Planet> SecondaryPlanets
        {
            get { return _secondaryPlanets; }

            set
            {
                if (value != _secondaryPlanets)
                {                    
                    _secondaryPlanets = value;
                    NotifyPropertyChanged("SecondaryPlanets");
                }                
            }
        }

        
        public ICollection OrbsCollection
        {
            get
            {
                return _secondaryPlanets != null ? 
                    (ICollection)_secondaryPlanets.Values : null;
            }
        }
      
        public Planet()
        {
            _secondaryPlanets = new ObservableDictionary<int, Planet>();
        }

        public Planet(string planetName, float mass,
            float diameter, float meanDistance,
            float siderealPeriod, float synodicPeriod) : this()
        {
            OrbNameArgumentException(planetName, "планеты");
            OrbCharsArgumentException(mass, "массы планеты");
            OrbCharsArgumentException(diameter, "диаметра планеты");
            OrbCharsArgumentException(meanDistance,
                "среднего расстояния планеты");
            OrbCharsArgumentException(siderealPeriod,
                "сидерального периода планеты");
            OrbCharsArgumentException(synodicPeriod,
                "синодического периода планеты");
            _name = planetName;
            NotifyPropertyChanged("Name");
            _mass = mass;
            NotifyPropertyChanged("Mass");
            _diameter = diameter;
            NotifyPropertyChanged("Diameter");
            _meanDistance = meanDistance;
            NotifyPropertyChanged("MeanDistance");
            _siderealPeriod = siderealPeriod;
            NotifyPropertyChanged("SiderealPeriod");
            _synodicPeriod = synodicPeriod;
            NotifyPropertyChanged("SynodicPeriod");
        }

        public Planet(int id, string planetName, float mass, 
            float diameter, float meanDistance,
            float siderealPeriod, float synodicPeriod) : 
            this(planetName, mass, diameter, meanDistance, siderealPeriod,
                synodicPeriod)
        {
            Id = id;
            NotifyPropertyChanged("Id");
        }

        public Planet(int id, string planetName, float mass, float diameter, 
            float meanDistance, float siderealPeriod, float synodicPeriod, 
            ObservableDictionary<int, Planet> secondaryPlanets) : 
            this(id, planetName, mass, diameter, meanDistance, siderealPeriod, 
                synodicPeriod)
        {
            _secondaryPlanets = secondaryPlanets;
            NotifyPropertyChanged("SecondaryPlanets");
        }

        public bool HasSecondaryPlanets
        {
            get
            {
                return _secondaryPlanets != null ?
                    _secondaryPlanets.Count > 0 : false;
            }
        }

        public void AddSecondaryPlanet(int id, Planet newSecondaryPlanet)
        {
            if (_secondaryPlanets == null)
                _secondaryPlanets = new ObservableDictionary<int, Planet>();
            OrbNullArgumentException(newSecondaryPlanet, "Secondary Planet");
            OrbIdDetectedArgumentException(_secondaryPlanets, id, "Планета-спутник");
            OrbNameDetectedArgumentException
                (_secondaryPlanets, newSecondaryPlanet.Name, "Планета-спутник");
            newSecondaryPlanet.Id = id;
            _secondaryPlanets.Add(id, newSecondaryPlanet);
            newSecondaryPlanet.OwnerOrb = this;
            newSecondaryPlanet.OwnerGalaxy = OwnerGalaxy;
            NotifyPropertyChanged("SecondaryPlanets");
        }

        public void ChangeSecondaryPlanet(int id, Planet newSecondaryPlanet)
        {
            OrbFaildArgumentException(_secondaryPlanets, id, "Планета-спутник");
            if (_secondaryPlanets[id] != newSecondaryPlanet)
            {                
                _secondaryPlanets[id] = newSecondaryPlanet;
                newSecondaryPlanet.OwnerOrb = this;
                newSecondaryPlanet.OwnerGalaxy = OwnerGalaxy;
                NotifyPropertyChanged("SecondaryPlanets");
            }
        }

        public void DeleteSecondaryPlanet(int id)
        {
            OrbFaildArgumentException(_secondaryPlanets, id, "Планета-спутник");            
            _secondaryPlanets.Remove(id);
            if (_secondaryPlanets.Count == 0)
                _secondaryPlanets = null;
            NotifyPropertyChanged("SecondaryPlanets");
        }

        public bool Equals(Planet other)
        {
            return _name == other.Name && _mass == other.Mass && 
                _diameter == other.Diameter && _meanDistance == other.MeanDistance &&
                _siderealPeriod == other._siderealPeriod && _synodicPeriod == 
                other.SynodicPeriod && _siderealPeriod == other.SiderealPeriod &&
                _secondaryPlanets == other.SecondaryPlanets;            
        }

        public override bool Equals(object obj)
        {
            if (obj is Planet)
                return Equals((Planet)obj);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return (int) (_mass + _diameter + _meanDistance + 
                _siderealPeriod + _synodicPeriod) ;
        }


        public static bool operator ==(Planet Planet1, Planet Planet2)
        {
            if ((object)Planet1 == null || (object)Planet2 == null)
                return Equals(Planet1, Planet2);
            else
                return Planet1.Equals(Planet2);
        }

        public static bool operator !=(Planet Planet1, Planet Planet2)
        {
            if ((object)Planet1 == null || (object)Planet2 == null)
                return !Equals(Planet1, Planet2);
            else
                return !Planet1.Equals(Planet2);
        }
    }
}
