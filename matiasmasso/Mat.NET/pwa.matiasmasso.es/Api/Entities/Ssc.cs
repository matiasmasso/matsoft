using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Subscriptions. Automated tasks from Windows service Matsched (table Task) send email notifications to subscribed users
    /// </summary>
    public partial class Ssc
    {
        public Ssc()
        {
            SscEmails = new HashSet<SscEmail>();
            SscLogs = new HashSet<SscLog>();
            SscRols = new HashSet<SscRol>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company; foreign key for Emp table
        /// </summary>
        public int Emp { get; set; }
        /// <summary>
        /// Sort order in which this subscription should appear
        /// </summary>
        public int Ord { get; set; }
        /// <summary>
        /// True if all users are subscribed except for those listed, false if only users listed are subscribed
        /// </summary>
        public bool Reverse { get; set; }

        public virtual Emp EmpNavigation { get; set; } = null!;
        public virtual ICollection<SscEmail> SscEmails { get; set; }
        public virtual ICollection<SscLog> SscLogs { get; set; }
        public virtual ICollection<SscRol> SscRols { get; set; }
    }
}
