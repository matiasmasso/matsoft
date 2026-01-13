using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOStaffSched : DTOBaseGuid
    {
        public DTOEmp Emp { get; set; }
        public DTOStaff Staff { get; set; }
        public DateTime FchFrom { get; set; }
        public DateTime FchTo { get; set; }
        public string Obs { get; set; }

        public List<Item> Items { get; set; }

        public DTOStaffSched() : base()
        {
            Items = new List<Item>();
        }

        public DTOStaffSched(Guid oGuid) : base(oGuid)
        {
            Items = new List<Item>();
        }

        public static DTOStaffSched Factory(DTOEmp oEmp, DTOStaff oStaff)
        {
            DTOStaffSched retval = new DTOStaffSched();
            {
                var withBlock = retval;
                withBlock.Emp = oEmp;
                withBlock.Staff = oStaff;
                withBlock.FchFrom = DTO.GlobalVariables.Today();
            }
            return retval;
        }

        public string Range()
        {
            string retval = "";
            if (FchTo == default(DateTime))
                retval = "(vigent)";
            else
                retval = string.Format("{0:dd/MM/yy} - {1:dd/MM/yy}", FchFrom, FchTo);
            return retval;
        }

        public bool HasSpecificHourFrom()
        {
            return (FchFrom.Hour != 0 | FchFrom.Minute != 0);
        }

        public bool HasSpecificHourTo()
        {
            return (FchTo.Hour != 23 | FchTo.Minute != 59);
        }




        public class Item : DTOBaseGuid
        {
            public Cods Cod { get; set; }

            public DayOfWeek weekDay { get; set; }
            public int FromHour { get; set; }
            public int FromMinutes { get; set; }
            public int ToHour { get; set; }
            public int ToMinutes { get; set; }

            public enum Cods
            {
                Ordinari,
                Intensiu
            }


            public Item() : base()
            {
            }

            public Item(Guid oGuid) : base(oGuid)
            {
            }

            public static Item Factory(Cods oCod)
            {
                Item retval = new Item();
                {
                    var withBlock = retval;
                    withBlock.Cod = oCod;
                }
                return retval;
            }

            public bool HasValue()
            {
                bool retval = false;
                if (FromHour != ToHour)
                    retval = true;
                if (FromMinutes != ToMinutes)
                    retval = true;
                return retval;
            }

            public bool Matins()
            {
                return FromHour < 13;
            }
        }
    }
}
