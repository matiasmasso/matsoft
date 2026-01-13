using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Automatic tasks run at specific schedules by MatSched Windows service
    /// </summary>
    public partial class Task
    {
        public Task()
        {
            TaskLogs = new HashSet<TaskLog>();
            TaskSchedules = new HashSet<TaskSchedule>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Enumerable DTOTask.Cods to call a task programmatically
        /// </summary>
        public int? Cod { get; set; }
        /// <summary>
        /// Task name
        /// </summary>
        public string? Nom { get; set; }
        /// <summary>
        /// Task description
        /// </summary>
        public string? Dsc { get; set; }
        /// <summary>
        /// If false, the task will not run
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// If not null, the task won&apos;t run until this date
        /// </summary>
        public DateTimeOffset? NotBefore { get; set; }
        /// <summary>
        /// If not null, the task will no longer run after this date
        /// </summary>
        public DateTimeOffset? NotAfter { get; set; }

        public virtual ICollection<TaskLog> TaskLogs { get; set; }
        public virtual ICollection<TaskSchedule> TaskSchedules { get; set; }
    }
}
