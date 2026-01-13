
namespace DTO
{
    public class DTODefaultImage
    {
        public Defaults.ImgTypes Id { get; set; }
        public byte[] Image { get; set; }

        public static DTODefaultImage Factory(Defaults.ImgTypes oId, byte[] oImage = null/* TODO Change to default(_) if this is not a reference type */)
        {
            DTODefaultImage retval = new DTODefaultImage();
            {
                var withBlock = retval;
                withBlock.Id = oId;
                withBlock.Image = oImage;
            }
            return retval;
        }
    }
}
