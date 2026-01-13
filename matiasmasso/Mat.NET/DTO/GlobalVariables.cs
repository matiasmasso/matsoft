using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class GlobalVariables
    {
        public static DateTime Now() { return DateTime.Now; } // add days here in order to test past or future dates
        public static DateTime Today() { return Now().Date; }
    }
}
