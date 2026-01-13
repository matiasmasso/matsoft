using DTO;
using FFMpegCore;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Api.Helpers
{
    public class FFMpegHelper
    {
        private readonly IWebHostEnvironment _env;
        public FFMpegHelper(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task SaveVideoThumbnail(AlbumModel.Item item, int timestamp = 2)
        {
            // 1. Extract duration using FFProbe
            item.Duration = await GetVideoDurationAsync(item.FullPath);

            // 2. Extract thumbnail at given timestamp
            await FFMpegArguments
                .FromFileInput(item.FullPath)
                .OutputToFile(item.ThumbPath, overwrite: true, o => o
                    .Seek(TimeSpan.FromSeconds(timestamp))
                    .WithVideoCodec("mjpeg")
                    .WithFrameOutputCount(1)
                    .ForceFormat("image2")
                    .WithVideoFilters(f => f.Scale(200, -1)))
                .ProcessAsynchronously();
        }

        public async Task<TimeSpan?> GetVideoDurationAsync(string path)
        {
            var info = await FFProbe.AnalyseAsync(path);
            var retval = info.Duration;
            return retval;
        }


    }
}
