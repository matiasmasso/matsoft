using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatHelper.Excel
{
    public class Rasterizer
    {
        public static byte[] GetExcelThumbnail(byte[] oStream, List<Exception> exs)
        {
            string filename = MatHelper.Excel.ClosedXml.SaveExcelStream(exs, oStream);
            string sXpsFilename = AppHelper.GetXpsFileNameFromExcelFileName(exs, filename);
            XPSHelper oXPSHelper = XPSHelper.FromFilename(sXpsFilename);
            byte[] retval = oXPSHelper.GenerateThumbnail();
            return retval;
        }
    }
}
