using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Observatory.Dal
{
    [DataContract]
    class OrbsCollection
    {
        [DataMember]
        public ICollection<SimpleGalaxy> GalaxiesCollection{ get; set; }

        [DataMember]
        public ICollection<SimpleStar> StarsCollection{ get; set; }

        [DataMember]
        public ICollection<SimplePlanet> PlanetsCollection{ get; set; }

        [DataMember]
        public ICollection<SimplePlanet> SecondaryPlanetsCollection{ get; set; }

        public OrbsCollection()
        {
            GalaxiesCollection = new Collection<SimpleGalaxy>();
            StarsCollection = new Collection<SimpleStar>();
            PlanetsCollection = new Collection<SimplePlanet>();
            SecondaryPlanetsCollection = new Collection<SimplePlanet>();
        }

    }
}
