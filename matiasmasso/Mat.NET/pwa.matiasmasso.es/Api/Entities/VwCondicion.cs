using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Terms and conditions contents
    /// </summary>
    public partial class VwCondicion
    {
        /// <summary>
        /// Terms id, foreign key for Cond table
        /// </summary>
        public Guid CondGuid { get; set; }
        /// <summary>
        /// Chapter id, foreign key for CondCapitol table
        /// </summary>
        public Guid? CapitolGuid { get; set; }
        /// <summary>
        /// Chapter order
        /// </summary>
        public int? Ord { get; set; }
        /// <summary>
        /// Terms title, Spanish language
        /// </summary>
        public string? TitleEsp { get; set; }
        /// <summary>
        /// Terms title, Catalan language
        /// </summary>
        public string? TitleCat { get; set; }
        /// <summary>
        /// Terms title, English language
        /// </summary>
        public string? TitleEng { get; set; }
        /// <summary>
        /// Terms title, Portuguese language
        /// </summary>
        public string? TitlePor { get; set; }
        /// <summary>
        /// Terms excerpt, Spanish language
        /// </summary>
        public string? ExcerptEsp { get; set; }
        /// <summary>
        /// Terms excerpt, Catalan language
        /// </summary>
        public string? ExcerptCat { get; set; }
        /// <summary>
        /// Terms excerpt, English language
        /// </summary>
        public string? ExcerptEng { get; set; }
        /// <summary>
        /// Terms excerpt, Portuguese language
        /// </summary>
        public string? ExcerptPor { get; set; }
        /// <summary>
        /// Chapter title, Spanish language
        /// </summary>
        public string? CapitolEsp { get; set; }
        /// <summary>
        /// Chapter title, Catalan language
        /// </summary>
        public string? CapitolCat { get; set; }
        /// <summary>
        /// Chapter title, English language
        /// </summary>
        public string? CapitolEng { get; set; }
        /// <summary>
        /// Chapter title, Portuguese language
        /// </summary>
        public string? CapitolPor { get; set; }
        /// <summary>
        /// Chapter content, Spanish language
        /// </summary>
        public string? TxtEsp { get; set; }
        /// <summary>
        /// Chapter content, Catalan language
        /// </summary>
        public string? TxtCat { get; set; }
        /// <summary>
        /// Chapter content, English language
        /// </summary>
        public string? TxtEng { get; set; }
        /// <summary>
        /// Chapter content, Portuguese language
        /// </summary>
        public string? TxtPor { get; set; }
        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime? FchCreated { get; set; }
        /// <summary>
        /// User who created these terms
        /// </summary>
        public Guid? UsrCreated { get; set; }
        /// <summary>
        /// Creation user nickname
        /// </summary>
        public string? UsrCreatedNickname { get; set; }
        /// <summary>
        /// Date this record was last edited
        /// </summary>
        public DateTime? FchLastEdited { get; set; }
        /// <summary>
        /// User who edited this record for last time
        /// </summary>
        public Guid? UsrLastEdited { get; set; }
        /// <summary>
        /// Nickname of the user who edited this record for last time
        /// </summary>
        public string? UsrLastEditedNickname { get; set; }
    }
}
