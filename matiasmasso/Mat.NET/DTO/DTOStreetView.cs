namespace DTO
{
    public class DTOStreetView
    {
        public DTOAddress Address { get; set; }
        public int Width { get; set; }  = 600;
        public int Height { get; set; } = 300;
        public int Heading { get; set; } = 90;
        public int Pitch { get; set; } = 0;
        public int Zoom { get; set; } = 90;
        public string Key { get; set; } = "AIzaSyC3O2n2r1p1w-9JkC-f-yI7HWQfkst053I";

        public static DTOStreetView Factory(DTOAddress oAddress, int width, int height)
        {
            DTOStreetView retval = new DTOStreetView();
            {
                retval.Address = oAddress;
                retval.Width = width;
                retval.Height = height;
            }
            return retval;
        }

        public string Url()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("https://maps.googleapis.com/maps/api/streetview");
            sb.Append("?size=" + Width + "x" + Height);
            sb.Append("&location=" + DTOAddress.GoogleNormalized(Address));
            if (Heading != -1)
                sb.Append("&heading=" + Heading);
            sb.Append("&pitch=" + Pitch);
            sb.Append("&fov=" + Zoom);
            sb.Append("&key=" + Key);
            string retval = sb.ToString();
            return retval;
        }
    }
}
