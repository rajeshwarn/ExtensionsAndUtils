using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework
{
    public class RequestResult<T>
    {
        public ResultEnum ResultOperation { get; set; }

        public T ResultObject { get; set; }

        public string VersionNumber { get; set; }
    }
}
