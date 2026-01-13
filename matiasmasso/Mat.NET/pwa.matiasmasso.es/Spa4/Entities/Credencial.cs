using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// User credentials to sign in partners systems
    /// </summary>
    public partial class Credencial
    {
        public Credencial()
        {
            Owners = new HashSet<Email>();
            Rols = new HashSet<UsrRol>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Owner of the site; foreign key for CliGral
        /// </summary>
        public Guid? Contact { get; set; }
        /// <summary>
        /// Friendly caption
        /// </summary>
        public string? Referencia { get; set; }
        /// <summary>
        /// Landing page
        /// </summary>
        public string Url { get; set; } = null!;
        /// <summary>
        /// User name
        /// </summary>
        public string Usuari { get; set; } = null!;
        /// <summary>
        /// Password to sign in above site
        /// </summary>
        public string Password { get; set; } = null!;
        /// <summary>
        /// Comments
        /// </summary>
        public string? Obs { get; set; }
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// User who created this entry
        /// </summary>
        public Guid UsrCreated { get; set; }
        /// <summary>
        /// Date this entry was edited for last time
        /// </summary>
        public DateTime FchLastEdited { get; set; }
        /// <summary>
        /// User who edited this entry for last time
        /// </summary>
        public Guid UsrLastEdited { get; set; }

        public virtual CliGral? ContactNavigation { get; set; }
        public virtual Email UsrCreatedNavigation { get; set; } = null!;
        public virtual Email UsrLastEditedNavigation { get; set; } = null!;

        public virtual ICollection<Email> Owners { get; set; }
        public virtual ICollection<UsrRol> Rols { get; set; }
    }
}
