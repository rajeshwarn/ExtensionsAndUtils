using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WSAnswer
/// </summary>
namespace BLL.Communications
{
    public class WSAnswer<T>
    {
        public ResultEnum ResultEnum { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}