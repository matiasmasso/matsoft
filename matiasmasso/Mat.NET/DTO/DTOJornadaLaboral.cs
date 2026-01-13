using System;

namespace DTO
{
    public class DTOJornadaLaboral : DTOBaseGuid
    {
        public DTOStaff Staff { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }

        public enum Modes
        {
            notset,
            entrada,
            sortida
        }

        public enum Status
        {
            NotSet,
            ReadyToEnter,
            ReadyToExit,
            MissingExit,
            NoStaff
        }


        public DTOJornadaLaboral() : base() { }
        public DTOJornadaLaboral(Guid guid) : base(guid) { }

        public static DTOJornadaLaboral Factory(DTOStaff staff)
        {
            DTOJornadaLaboral retval = new DTOJornadaLaboral();
            retval.Staff = staff;
            retval.FchFrom = DTO.GlobalVariables.Now();
            return retval;
        }

        public bool IsOpen()
        {
            return FchTo == DateTime.MinValue;
        }
    }
}
