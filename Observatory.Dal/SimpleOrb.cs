using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Observatory.Dal
{
    [DataContract]
    class SimpleOrb
    {
        [DataMember]
        public int Id{ get; set; }

        [DataMember]
        public string Name{ get; set; }

        [DataMember]
        public bool HasOrbs { get; set; }
    }
}
