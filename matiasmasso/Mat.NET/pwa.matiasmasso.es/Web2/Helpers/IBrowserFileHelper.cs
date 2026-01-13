using Microsoft.AspNetCore.Components.Forms;

namespace Web.Helpers
{
    public class IBrowserFileHelper
    {
        public static async Task<Byte[]> FileBytes(IBrowserFile file)
        {
            Stream stream = file.OpenReadStream(maxAllowedSize: 100000000);
            MemoryStream ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            stream.Close();
            return ms.ToArray();
        }
    }
}
