using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OopEksamen.Utilities
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
    internal class RegexMatchAttribute : Attribute
    {
        Regex Regex { get; set; }
        Exception Exception { get; set; }

        public RegexMatchAttribute(Regex regex, Exception exception)
        {
            Regex = regex;
            Exception = exception;
        }

        private string _value;
        public string Prop
        {
            get { return _value; }
            set
            {
                if(Regex.IsMatch(value))
                {
                    _value = value;
                }
                else
                {
                    throw Exception;
                }
            }
        }

    }
}
