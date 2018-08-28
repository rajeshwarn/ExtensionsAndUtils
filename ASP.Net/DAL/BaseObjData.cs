using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DAL
{
    [DataContract]
	public class BaseObjData
    {
        public int Id { get; set; }

        public int IdType { get; set; }

        [DataMember]
        public string Type { get; set; }

        public int IdParent { get; set; }

        [DataMember]
        public string Parent { get; set; }

        [DataMember]
        public string ParentCode { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Code { get; set; }
    }
}
