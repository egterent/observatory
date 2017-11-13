using Observatory.ObjectModel;
using Observatory.ObjectModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Observatory.Dal
{
    [DataContract]
    class SimpleStar : SimpleOrb
    {
        [DataMember]
        public SpectralType SpectralType{ get; set; }

        [DataMember]
        public float Magnitude{ get; set; }

        [DataMember]
        public float Rectascension{ get; set; }

        [DataMember]
        public float DeclinationAngle{ get; set; }

        [DataMember]
        public float Temperature{ get; set; }

        [DataMember]
        public float Distance{ get; set; }

        [DataMember]
        public int OwnerGalaxyId { get; set; }

        public SimpleStar(Star star)
        {
            Id = star.Id;
            Name = star.Name;
            Magnitude = star.Magnitude;
            Rectascension = star.Rectascension;
            DeclinationAngle = star.DeclinationAngle;
            Temperature = star.Temperature;
            Distance = star.Distance;
            OwnerGalaxyId = star.OwnerGalaxy.Id;
            HasOrbs = star.HasPlanets;
        }

        public void CopyToStar(Star star)
        {
            star.Id = Id;
            star.Name = Name;
            star.Magnitude = Magnitude;
            star.Rectascension = Rectascension;
            star.SpectralType = SpectralType;
            star.Temperature = Temperature;
            star.DeclinationAngle = DeclinationAngle;
            star.Distance = Distance;
        }
    }
}
