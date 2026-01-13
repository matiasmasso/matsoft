using System;

namespace DTO
{
    public class DTOwellknown
    {
        public Guid Guid { get; set; }
        public Modes Mode { get; set; }
        public int Id { get; set; }

        public enum Modes
        {
            NotSet,
            Customer
        }
    }
}
