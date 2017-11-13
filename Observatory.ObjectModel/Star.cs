using System;
using System.Collections;
using static Observatory.ObjectModel.OrbExceptions;

using Observatory.ObjectModel.Enums;

namespace Observatory.ObjectModel
{
    public class Star : NotifyChanges, IHaveOrbs, IEquatable<Star>
    {
        private string _name;

        private SpectralType _spectralType;

        private float _magnitude;

        private float _rectascension;

        private float _declinationAngle;

        private float _temperature;

        private float _distance;

        private ObservableDictionary<int, Planet> _planets;

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

        public SpectralType SpectralType
        {
            get { return _spectralType; }
            set
            {
                OrbCharsArgumentException(value);
                if (value != _spectralType)
                {                   
                    _spectralType = value;
                    NotifyPropertyChanged("SpectralType");
                }
            }
        }

        public float Magnitude
        {
            get { return _magnitude; }
            set
            {
                if (value != _magnitude)
                {                    
                    _magnitude = value;
                    NotifyPropertyChanged("Magnitude");
                }
            }
        }

        public float Rectascension
        {
            get { return _rectascension; }
            set
            {
                OrbCharsArgumentException(value, "прямого восхождения звезды");
                if (value != _rectascension)
                {                    
                    _rectascension = value;
                    NotifyPropertyChanged("Rectascention");
                }
            }
        }

        public float DeclinationAngle
        {
            get { return _declinationAngle; }
            set
            {
                if (value != _declinationAngle)
                {                    
                    _declinationAngle = value;
                    NotifyPropertyChanged("DeclinationAngel");
                }
            }
        }

        public float Temperature
        {
            get { return _temperature; }
            set
            {
                OrbCharsArgumentException(value, "температуры звезды");
                if (value != _temperature)
                {                    
                    _temperature = value;
                    NotifyPropertyChanged("Temperature");
                }
            }

        }

        public float Distance
        {
            get { return _distance; }
            set
            {
                OrbCharsArgumentException(value, "расстояния до звезды");
                if (value != _distance)
                {                    
                    _distance = value;
                    NotifyPropertyChanged("Distance");
                }
            }
        }

        public int PlanetsCount
        {
            get { return _planets != null ? _planets.Count : 0; }
        }


        public ObservableDictionary<int, Planet> Planets
        {
            get { return _planets; }

            set
            {
                if (value != null)
                {
                    _planets = value;
                    NotifyPropertyChanged("Planets");
                }
            }
        }

        public ICollection OrbsCollection
        {
            get
            {
                return _planets != null ?
                    (ICollection)_planets.Values : null;
            }
        }

        public Star()
        {
            _planets = new ObservableDictionary<int, Planet>();
        }

        public Star(string starName, SpectralType spectralType, float magnitude, 
            float rectascension, float declinationAngle, 
            float temperature, float distance) : this()
        {
            OrbNameArgumentException(starName, "планеты");
            OrbCharsArgumentException(spectralType);
            OrbCharsArgumentException(rectascension, "прямого восхождения звезды");
            OrbCharsArgumentException(temperature, "температуры звезды");
            OrbCharsArgumentException(distance, "расстояния до звезды");            
            _name = starName;
            NotifyPropertyChanged("Name");
            _spectralType = spectralType;
            NotifyPropertyChanged("SpectralType");
            _magnitude = magnitude;
            NotifyPropertyChanged("Magnitude");
            _rectascension = rectascension;
            NotifyPropertyChanged("Rectascension");
            _declinationAngle = declinationAngle;
            NotifyPropertyChanged("DeclinationAngle");
            _temperature = temperature;
            NotifyPropertyChanged("Temperature");
            _distance = distance;
            NotifyPropertyChanged("Distance");
        }

        public Star(int id, string starName, SpectralType spectralType,
            float magnitude, float rectascension, float declinationAngle,
            float temperature, float distance) : this(starName, spectralType, 
                magnitude, rectascension, declinationAngle, temperature, distance)
        {
            Id = id;
            NotifyPropertyChanged("Id");
        }

        public Star(int id, string starName, SpectralType spectralType, float magnitude, 
            float rectascension, float declinationAngle, float temperature, 
            float distance, ObservableDictionary<int, Planet> planets) : 
            this(id, starName, spectralType, magnitude, rectascension, declinationAngle, 
                temperature, distance)
        {
            _planets = planets;
            NotifyPropertyChanged("Planets");
        }

        public bool HasPlanets
        { get { return _planets != null ? _planets.Count > 0 : false; } } 

        public void AddPlanet(int id, Planet newPlanet)
        {
            if (_planets == null)
                _planets = new ObservableDictionary<int, Planet>();
            OrbNullArgumentException(newPlanet, "Planet");
            OrbIdDetectedArgumentException(_planets, id, "Планета");
            OrbNameDetectedArgumentException(_planets, newPlanet.Name, "Планета");
            newPlanet.Id = id;
            _planets.Add(id, newPlanet);
            newPlanet.OwnerOrb = this;
            newPlanet.OwnerGalaxy = OwnerGalaxy;
            NotifyPropertyChanged("Planets");
        }

        public void ChangePlanet(int id, Planet newPlanet)
        {
            OrbFaildArgumentException(_planets, id, "Планета");
            if (_planets[id] != newPlanet)
            {             
                var ChangedPlanet = _planets[id];
                _planets[id] = newPlanet;
                newPlanet.OwnerOrb = this;
                newPlanet.OwnerGalaxy = OwnerGalaxy;
                NotifyPropertyChanged("Planets");
            }
        }

        public void DeletePlanet(int id)
        {
            OrbFaildArgumentException(_planets, id, "Планета");            
            _planets.Remove(id);
            if (_planets.Count == 0)
                _planets = null;
            NotifyPropertyChanged("Planets");
        }

        public bool Equals(Star other)
        { return _name == other.Name && _spectralType == other.SpectralType 
                && _magnitude == other.Magnitude && _rectascension == 
                other.Rectascension && _declinationAngle == other.DeclinationAngle && 
                _temperature == other.Temperature && _distance == other.Distance &&
                _planets == other.Planets;
        }
     
        public override bool Equals(object obj)
        {
            if (obj is Star)
                return Equals((Star)obj);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return (int)(_magnitude + _rectascension + _temperature + _distance);
        }

        public static bool operator ==(Star star1, Star star2)
        {
            if ((object)star1 == null || (object)star2 == null)
                return Equals(star1, star2);
            else
                return star1.Equals(star2);
        }

        public static bool operator !=(Star star1, Star star2)
        {
            if ((object)star1 == null || (object)star2 == null)
                return !Equals(star1, star2);
            else
                return !star1.Equals(star2);
        }
    }
}
