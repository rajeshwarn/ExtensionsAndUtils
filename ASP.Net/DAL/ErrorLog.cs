using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ErrorLog
    {
        public string Logger { get; set; }
        public string Tracking { get; set; }
        public bool IsError { get; set; }
    }
}
