using DTO;

namespace Api.Helpers
{
    public class FileUploadHelper
    {
        public static async Task LoadFileStream(DocfileModel? docfile, IFormFileCollection files)
        {
            if (docfile != null)
            {

                var file = files[docfile.HashFilename()];
                if (file != null)
                {
                    Stream stream = file.OpenReadStream();
                    MemoryStream ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    stream.Close();
                    docfile.Document = new Media(file.ContentType, ms.ToArray());
                }

                var thumbnail = files[docfile.HashThumbnailname()];
                if (thumbnail != null)
                {
                    Stream stream = thumbnail.OpenReadStream();
                    MemoryStream ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    stream.Close();
                    docfile.Thumbnail = new Media(thumbnail.ContentType, ms.ToArray());
                }
            }
        }
    }
}
