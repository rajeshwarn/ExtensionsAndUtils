using System;
using System.ComponentModel.DataAnnotations;
using Shared;

namespace Model.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TimeRequiredAttribute: RequiredAttribute { }
}
