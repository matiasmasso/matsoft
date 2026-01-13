using Api.Entities;
using Api.Services;
using Azure;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Vml;
using DTO;
using ImageMagick.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly AlbumService _albumService;
        private readonly IWebHostEnvironment _env;
        public AlbumController(IWebHostEnvironment env, AlbumService albumService)
        {
            _albumService = albumService;
            _env = env;
        }



        [HttpGet("GetAllAlbums")]
        public IActionResult GetAllAlbums()
        {
            try
            {
                var values = _albumService.GetAllAlbums();
                return Ok(values);

            }
            catch (Exception ex)
            {
                return BadRequest($"error on Api.GetAllAlbums: {ex.Message}");
            }
        }

        [HttpGet("Sort")]
        public IActionResult Sort()
        {
            try
            {
                _albumService.Sort();
                return Ok("sorted Ok");

            }
            catch (Exception ex)
            {
                return BadRequest($"error on sorting: {ex.Message}");
            }
        }


        [HttpPost]
        public IActionResult Update([FromBody] AlbumModel value)
        {

            try
            {
                _albumService.Update(value);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating album {value.FolderName}: {ex.Message}");
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] AlbumModel value)
        {

            try
            {
                await _albumService.Delete(value);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating album {value.FolderName}: {ex.Message}");
            }
        }



        [HttpGet("Media/{guid}")]
        public async Task<IActionResult> Media(Guid guid)
        {
            var item = _albumService.GetAlbumItem(guid);
            if (item == null)
                return NotFound();

            if (item.IsVideo())
                return await VideoStream(item);
            else
                return PhysicalFile(item.FullPath, item.ContentType!);
        }

        [HttpGet("Thumb/{guid}")]
        public async Task<IActionResult> Thumb(Guid guid)
        {
            var item = _albumService.GetAlbumItem(guid);
            if (item == null)
                return NotFound();

            return PhysicalFile(item.ThumbPath, item.ContentType!);
        }

        private async Task<IActionResult> VideoStream(AlbumModel.Item item)
        {
            var fileInfo = new FileInfo(item.FullPath);
            var mimeType = item.ContentType;

            var request = HttpContext.Request;
            var response = HttpContext.Response;

            response.Headers.Append("Accept-Ranges", "bytes");

            long totalSize = fileInfo.Length;
            long start = 0;
            long end = totalSize - 1;

            if (request.Headers.ContainsKey("Range"))
            {
                var rangeHeader = request.Headers["Range"].ToString();
                // Example: "bytes=1000-"
                var range = rangeHeader.Replace("bytes=", "").Split('-');
                start = long.Parse(range[0]);
                if (range.Length > 1 && !string.IsNullOrEmpty(range[1]))
                {
                    end = long.Parse(range[1]);
                }

                response.StatusCode = StatusCodes.Status206PartialContent;
                response.Headers.Append("Content-Range", $"bytes {start}-{end}/{totalSize}");
            }

            var length = end - start + 1;
            response.ContentType = mimeType;
            response.ContentLength = length;

            using var fs = new FileStream(item.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            fs.Seek(start, SeekOrigin.Begin);
            var buffer = new byte[64 * 1024]; // 64KB chunks
            long remaining = length;

            while (remaining > 0)
            {
                var read = await fs.ReadAsync(buffer, 0, (int)Math.Min(buffer.Length, remaining));
                if (read == 0) break;
                await response.Body.WriteAsync(buffer.AsMemory(0, read));
                remaining -= read;
            }

            return new EmptyResult(); // response already written
        }

        [HttpPost("GetAlbumItems")]
        public IActionResult GetAlbumItems([FromBody] AlbumModel album)
        {

            try
            {
                var values = _albumService.GetAlbumItems(album);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling items from album {album.FolderName}: {ex.Message}");
            }
        }


        [HttpPost("upload/chunk")]
        public async Task<IActionResult> UploadChunk(
    IFormFile file,
    [FromForm] int chunkIndex,
    [FromForm] string fileName,
    [FromForm] string contentType,
    [FromForm] string lastModified)

        {
            try
            {
                var jsonPart = Request.Form["album"];
                var album = JsonConvert.DeserializeObject<AlbumModel>(jsonPart!);

                var tempDir = System.IO.Path.Combine(AlbumModel.TmpUploadsFullPath(), fileName);
                Directory.CreateDirectory(tempDir);

                var tempPath = System.IO.Path.Combine(tempDir, $"{fileName}.part{chunkIndex}");
                using var stream = new FileStream(tempPath, FileMode.Create);
                await file.CopyToAsync(stream);

                long unixMs = long.Parse(lastModified);
                DateTimeOffset fchCreated = DateTimeOffset.FromUnixTimeMilliseconds(unixMs);

                // Guardamos metadatos en un JSON por archivo
                var metaPath = System.IO.Path.Combine(tempDir, "metadata.json");
                var metadata = new
                {
                    FileName = fileName,
                    ContentType = contentType,
                    UsrCreated = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier),
                    FchCreated = fchCreated
                };

                await System.IO.File.WriteAllTextAsync(metaPath, System.Text.Json.JsonSerializer.Serialize(metadata));

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error uploading chunk: {ex.Message}");
            }
        }



        [HttpPost("upload/complete")]
        public async Task<IActionResult> Complete()
        {
            try
            {
                var jsonPart = Request.Form["album"];
                var album = JsonConvert.DeserializeObject<AlbumModel>(jsonPart!);
                await _albumService.CompleteUploadAsync(album!);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error completing upload: {ex.Message}");
            }

            return Ok(); // new { UploadId = albumGuid.ToString(), Files = resultFiles });
        }



        //======================================================================================


        [HttpGet("Rebuild")] //creates folders tree from database if they don't exists
        public async Task<IActionResult> Rebuild()
        {
            _albumService.Rebuild();
            return Ok(new { message = $"Album rebuild initiated successfully" });
        }

        [HttpGet("Test")]
        public async Task<IActionResult> Test()
        {
            try
            {
                await _albumService.GenerateThumbnailsForAllImagesAsync();
                return Ok("Thumbs generated Ok");

            }
            catch (Exception ex)
            {
                return BadRequest($"error on Api.GetAllAlbums: {ex.Message}");
            }
        }
    }

    public class UploadedFileInfo
    {
        public string? FileName { get; set; }
        public string? ContentType { get; set; }

        public Guid UsrCreated { get; set; }
        public DateTime FchCreated { get; set; }
    }

}


