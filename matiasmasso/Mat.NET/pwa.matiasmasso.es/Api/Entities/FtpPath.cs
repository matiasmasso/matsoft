using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Partner ftp server paths
    /// </summary>
    public partial class FtpPath
    {
        /// <summary>
        /// Ftp server owner, foreign key for CliGral table
        /// </summary>
        public Guid Owner { get; set; }
        /// <summary>
        /// Path purpose; enumerable DTOFtpserver.path.cods
        /// </summary>
        public int Cod { get; set; }
        /// <summary>
        /// Path from ftp server root
        /// </summary>
        public string? Path { get; set; }

        public virtual CliGral OwnerNavigation { get; set; } = null!;
    }
}
