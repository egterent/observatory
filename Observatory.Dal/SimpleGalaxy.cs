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
    class SimpleGalaxy : SimpleOrb
    {
        public SimpleGalaxy(Galaxy galaxy)
        {
            Id = galaxy.Id;
            Name = galaxy.Name;
            HasOrbs = galaxy.HasStars;
        }

        public void CopyToGalaxy(Galaxy galaxy)
        {
            galaxy.Id = Id;
            galaxy.Name = Name;
        }
    }
}
