using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Framework
{
    [DataContract]
    [Flags]
    public enum ResultEnum
    {
        [EnumMember]
        Error = -1,
		
        [EnumMember]
        InvalidToken = 0,
		
        [EnumMember]
        Success = 1,
		
        [EnumMember]
        NoData = 2,
		
        [EnumMember]
        NoNewData = 3,
		
        [EnumMember]
        DuplicatedData = 4
    }
}
