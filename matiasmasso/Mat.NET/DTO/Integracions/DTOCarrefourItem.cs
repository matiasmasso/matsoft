namespace DTO.Integracions
{

    public class DTOCarrefourItem
    {
        public string SupplierCode { get; set; } = "MATIA";
        public int Section { get; set; } = 61; // seccion de bebé
        public string Implantation { get; set; } = "610087";
        public string MadeIn { get; set; }
        public string SkuDsc { get; set; }
        public string SkuCustomRef { get; set; }
        public string SkuColor { get; set; }
        public int UnitsPerMasterBox { get; set; }
        public int UnitsPerInnerBox { get; set; }
        public string MasterBarCode { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
        public int Weight { get; set; }
        public string Albaran { get; set; }
        public int Linea { get; set; }

        public string Dimensions()
        {
            string retval = string.Format("{0} x {1} x {2} cm", Length / (double)10, Width / (double)10, Height / (double)10);
            return retval;
        }
    }

}
