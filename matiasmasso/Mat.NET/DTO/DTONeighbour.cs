using System;

namespace DTO
{
    public class DTONeighbour : DTOContact
    {
        public int Distance { get; set; } // metres
        public DTOAmt Amt { get; set; }

        public DTONeighbour() : base()
        {
        }

        public DTONeighbour(Guid oGuid) : base(oGuid)
        {
        }

        public static string FormattedDistance(decimal src)
        {
            string retval = "";
            switch (src)
            {
                case object _ when src < 100:
                    {
                        retval = string.Format("{0:0} m", src);
                        break;
                    }

                case object _ when src < 500:
                    {
                        retval = string.Format("{0:0} m", 10 * Math.Round(src / 10, 0, MidpointRounding.AwayFromZero));
                        break;
                    }

                case object _ when src < 1000:
                    {
                        retval = string.Format("{0:0} m", 100 * Math.Round(src / 100, 0, MidpointRounding.AwayFromZero));
                        break;
                    }

                case object _ when src < 5000:
                    {
                        retval = string.Format("{0:N1} Km", src / 1000);
                        break;
                    }

                default:
                    {
                        retval = string.Format("{0:N0} Km", Math.Truncate(src / 1000));
                        break;
                    }
            }
            return retval;
        }
    }
}
