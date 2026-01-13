using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Json Schemas for partners iontegration with our Api
    /// </summary>
    public partial class JsonSchema
    {
        public JsonSchema()
        {
            JsonLogs = new HashSet<JsonLog>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Schema friendly name
        /// </summary>
        public string? Nom { get; set; }
        /// <summary>
        /// Json code
        /// </summary>
        public string? Json { get; set; }
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// Date this entry was last edited
        /// </summary>
        public DateTime FchLastEdited { get; set; }
        /// <summary>
        /// User who created this entry; foreign key for Email table
        /// </summary>
        public Guid UsrCreated { get; set; }
        /// <summary>
        /// User who edited this entry for last time; foreign key for Email table
        /// </summary>
        public Guid? UsrLastEdited { get; set; }

        public virtual Email UsrCreatedNavigation { get; set; } = null!;
        public virtual Email? UsrLastEditedNavigation { get; set; }
        public virtual ICollection<JsonLog> JsonLogs { get; set; }
    }
}
