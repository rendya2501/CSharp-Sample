using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Json1
{
    [DataContract]
    public class Person
    {
        [DataMember(Name = "name")]
        //[IgnoreDataMember]でシリアライズの対象外にすることができる。
        public string Name { get; set; }
        [DataMember(Name = "age")]
        public int Age { get; set; }
    }
}
