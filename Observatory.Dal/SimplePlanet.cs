using Observatory.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Observatory.Dal
{
    [DataContract]
    class SimplePlanet : SimpleOrb
    {
        [DataMember]
        public float Mass { get; set; }

        [DataMember]
        public float Diameter { get; set; }

        [DataMember]
        public float MeanDistance { get; set; }

        [DataMember]
        public float SiderealPeriod { get; set; }
    
        [DataMember]
        public float SynodicPeriod { get; set; }

        [DataMember]
        public int OwnerOrbId { get; set; }

        [DataMember]
        public int OwnerGalaxyId { get; set; }

        public SimplePlanet(Planet planet)
        {
            Id = planet.Id;
            Name = planet.Name;
            Diameter = planet.Diameter;
            Mass = planet.Mass;
            MeanDistance = planet.MeanDistance;
            SiderealPeriod = planet.SiderealPeriod;
            SynodicPeriod = planet.SynodicPeriod;
            OwnerGalaxyId = planet.OwnerGalaxy.Id;
            var s = planet.OwnerOrb as Star;
            if (s != null)
            {
                OwnerOrbId = s.Id;
            }
            else
            {
                var p = planet.OwnerOrb as Planet;
                if (p != null)
                {
                    OwnerOrbId = p.Id;
                }
            }

            HasOrbs = planet.HasSecondaryPlanets;
        }

        public void CopyToPlanet(Planet planet)
        {
            planet.Id = Id;
            planet.Name = Name;
            planet.Diameter = Diameter;
            planet.Mass = Mass;
            planet.MeanDistance = MeanDistance;
            planet.SiderealPeriod = SiderealPeriod;
            planet.SynodicPeriod = SynodicPeriod;
        }
    }
}
