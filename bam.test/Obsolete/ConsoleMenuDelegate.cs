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
    public delegate void ConsoleMenuDelegate(Assembly assemblyToAnalyze, ConsoleMenu[] otherMenus, string header);
    //public delegate void ConsoleMenuDelegate<T>(ConsoleMenu[] otherMenus, string header) where T: Attribute, new();
}
