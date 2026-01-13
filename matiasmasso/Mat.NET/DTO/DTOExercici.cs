using System;

namespace DTO
{
    public class DTOExercici
    {
        public Guid Guid { get; set; }
        public DTOEmp Emp { get; set; }
        public int Year { get; set; }

        public DTOExercici(DTOEmp oEmp, int iYear) : base()
        {
            Emp = oEmp;
            Year = iYear;
        }


        public DTOExercici Trimmed()
        {
            DTOExercici retval = new DTOExercici(Emp.Trimmed(), Year);
            return retval;
        }

        public new bool Equals(object oExercici)
        {
            bool retval = false;
            if (oExercici != null)
            {
                if (oExercici is DTOExercici)
                {
                    if (((DTOExercici)oExercici).Emp.Id == Emp.Id)
                        retval = ((DTOExercici)oExercici).Year == Year;
                }
            }
            return retval;
        }

        public static DTOExercici Current(DTOEmp oEmp)
        {
            DTOExercici retval = new DTOExercici(oEmp, DTO.GlobalVariables.Today().Year);
            return retval;
        }

        public static DTOExercici Past(DTOEmp oEmp)
        {
            DTOExercici retval = FromYear(oEmp, DTO.GlobalVariables.Today().Year - 1);
            return retval;
        }

        public static DTOExercici FromYear(DTOEmp oEmp, int iYear)
        {
            DTOExercici retval = new DTOExercici(oEmp, iYear);
            return retval;
        }

        public DTOExercici Previous()
        {
            DTOExercici retval = new DTOExercici(Emp, Year - 1);
            return retval;
        }

        public DTOExercici Next()
        {
            DTOExercici retval = new DTOExercici(Emp, Year + 1);
            return retval;
        }

        public DateTime FirstFch()
        {
            DateTime retval = new DateTime(Year, 1, 1);
            return retval;
        }

        public DateTime LastFch()
        {
            DateTime retval = new DateTime(Year, 12, 31);
            return retval;
        }

        public DateTime LastFch1stQuarterNextYear()
        {
            DateTime dtfch = new DateTime(Year + 1, 3, 31);
            return dtfch;
        }

        public DateTime LastDayOrToday()
        {
            DateTime retval = Year == DTO.GlobalVariables.Today().Year ? DTO.GlobalVariables.Today() : new DateTime(Year, 12, 31);
            return retval;
        }
    }
}
