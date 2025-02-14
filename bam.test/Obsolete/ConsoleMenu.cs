/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bam.CommandLine;

namespace Bam.CommandLine
{
    [Obsolete("This class is obsolete. Use BamContext and related classes from the namespaces Bam.Console, Bam.Shell.")]
    public class ConsoleMenu
    {
        public ConsoleMenu()
        {
        }
        public string Name { get; set; }
        public char MenuKey { get; set; }
        public ConsoleMenuDelegate MenuWriter { get; set; }
        public Assembly AssemblyToAnalyze { get; set; }
        public string HeaderText { get; set; }
        public string FooterText { get; set; }

    }


}
