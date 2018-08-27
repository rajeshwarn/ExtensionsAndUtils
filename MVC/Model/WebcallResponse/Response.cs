using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Model.WebcallResponse
{
    ////[DataContract(Namespace = "http://spms.min-saude.pt/PDS")]
    public class Response<TResult>
    {
        //[DataMember(Name = "Result")]
        public TResult Result { get; set; }

        //[DataMember(Name = "Messages")]
        public ICollection<String> Messages { get; set; }

        //[DataMember(Name = "Status")]
        public Status Status { get; set; }

    }
}
