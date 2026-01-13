using IronBarCode;
using iText.Barcodes.Qrcode;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatHelper
{
    public class BarcodeHelper
    {
        public static Image EAN13Image(string value, int width, int height, BarcodeWriterEncoding encoding = BarcodeWriterEncoding.EAN13)
        {
            // Dim oBarcode
            // oBarcode.ResizeTo(width, height)

            Bitmap retval;
            byte[] bytes;
            try
            {
                bytes = BarcodeWriter.CreateBarcode(value, BarcodeWriterEncoding.EAN13).ResizeTo(width, height).SetMargins(100).ToJpegBinaryData();
                retval = (Bitmap)ImageHelper.FromBytes(bytes);
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al redactar el codi de barres: " + ex.Message);
                retval = new Bitmap(1, 1);
            }
            return retval;
        }

    }
}
