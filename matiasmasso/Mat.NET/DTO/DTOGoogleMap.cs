namespace DTO
{
    public class DTOGoogleMap
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public int Zoom { get; set; } = 12;
        public int MinZoom { get; set; } = 0;
        public int MaxZoom { get; set; } = 24;
    }
}
