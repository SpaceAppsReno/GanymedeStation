using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ganymede.Communications
{
    [DataContract]
    internal class NamesJson
    {
        [DataMember(Name = "name")]
        public string Results { get; set; }
    }
}
