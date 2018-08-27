using System;

namespace Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class DbValueAttribute : Attribute
    {
        private readonly string _value;

        public DbValueAttribute(string value)
        {
            this._value = value;
        }

        public string Value
        {
            get { return this._value; }
        }
    }
}
