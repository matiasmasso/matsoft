using MatHelperCFwk;
using System.Drawing;

public static class ImageExtensions
{
    public static byte[] Bytes(this Image value)
    {
        return ImageHelper.GetByteArrayFromImg(value);
    }
}


