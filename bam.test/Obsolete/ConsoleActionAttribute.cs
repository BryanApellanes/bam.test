/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bam.Test;

namespace Bam.CommandLine
{
    /// <summary>
    /// An attribute used to designate a method as runnable from a command line
    /// menu interface
    /// </summary>
    [Obsolete("This class is obsolete. Use BamContext and related classes from the namespaces Bam.Console, Bam.Shell.")]
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ConsoleActionAttribute : Attribute, IInfoAttribute
    {
        /// <summary>
        /// Instantiate a ConsoleAction attribute
        /// </summary>
        public ConsoleActionAttribute()
        {

        }

        public ConsoleActionAttribute(string information)
        {
            Information = information;
        }

        public ConsoleActionAttribute(string commandLineSwitch, string information)
            : this(information)
        {
            CommandLineSwitch = commandLineSwitch;
        }

        public ConsoleActionAttribute(string commandLineSwitch, string valueExample, string information)
            : this(commandLineSwitch, information)
        {
            ValueExample = valueExample;
        }

        /// <summary>
        /// A string representing an example of a valid value, for example [filename]
        /// </summary>
        public string ValueExample
        {
            get;
            private set;
        }

        public string CommandLineSwitch
        {
            get;
            private set;
        }

        public string Information { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Information))
                return Information;

            return base.ToString();
        }
    }
}
