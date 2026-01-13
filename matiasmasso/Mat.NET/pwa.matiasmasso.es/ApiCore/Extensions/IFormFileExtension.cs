using iText.Commons.Actions.Data;
using Newtonsoft.Json;

namespace Api.Extensions
{
    public static class IFormFileExtension
    {
        public static byte[] ToByteArray(this IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                return stream.ToByteArray();
            }
        }
        public static byte[] ToByteArray(this Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
