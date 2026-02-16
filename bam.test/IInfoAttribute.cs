/*
	Copyright © Bryan Apellanes 2015  
*/

namespace Bam.Test
{
    /// <summary>
    /// Defines an attribute that carries descriptive information about a test or command.
    /// </summary>
    public interface IInfoAttribute
    {
        /// <summary>
        /// Gets or sets the descriptive information string.
        /// </summary>
        string Information { get; set; }
    }
}
