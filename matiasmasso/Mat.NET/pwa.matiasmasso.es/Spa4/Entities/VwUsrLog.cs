using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// For each target shows creation date and user, and last edition date and user
    /// </summary>
    public partial class VwUsrLog
    {
        public Guid UsrCreated { get; set; }
        public string UsrCreatedEmailAddress { get; set; } = null!;
        public string? UsrCreatedNickname { get; set; }
        public Guid UsrLastEdited { get; set; }
        public string UsrLastEditedEmailAddress { get; set; } = null!;
        public string? UsrLastEditedNickname { get; set; }
    }
}
