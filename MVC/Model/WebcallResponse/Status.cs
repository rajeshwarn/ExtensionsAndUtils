using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Model.WebcallResponse
{
    ////[DataContract(Namespace = "http://spms.min-saude.pt/PDS")]
    public enum Status
    {
        //[EnumMember]
        SUCCESS,
        //[EnumMember]
        FAIL
    }
}
