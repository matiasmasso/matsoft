using Api.Services;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly AlbumsService _albumsService;
        private readonly AlbumCacheService _cache;

        public AlbumsController(AlbumsService albumsService, AlbumCacheService cache)
        {
            _albumsService = albumsService;
            _cache = cache;
        }

        // ------------------------------------------------------------
        // ÁLBUMES
        // ------------------------------------------------------------

        [HttpGet]
        public IActionResult GetAllAlbums()
        {
            try
            {
                var albums = _albumsService.GetAllAlbums();
                return Ok(albums);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error loading albums: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult UpdateAlbum([FromBody] AlbumModel album)
        {
            try
            {
                _albumsService.UpdateAlbum(album);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating album {album.FolderName()}: {ex.Message}");
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAlbum([FromBody] AlbumModel album)
        {
            try
            {
                await _albumsService.DeleteAlbumAsync(album);
                _cache.RemoveAlbum(album.Guid);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting album {album.FolderName()}: {ex.Message}");
            }
        }

        // ------------------------------------------------------------
        // ITEMS
        // ------------------------------------------------------------

        [HttpPost("GetAlbumItems")]
        public IActionResult GetAlbumItems([FromBody] AlbumModel album)
        {
            try
            {
                _cache.WarmAlbum(album.Guid);
                var items = _cache.GetItemsByAlbum(album.Guid);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error loading items: {ex.Message}");
            }
        }

        [HttpPost("item")]
        public async Task<IActionResult> SaveItem([FromBody] AlbumModel.Item item)
        {
            try
            {
                await _albumsService.UpdateItemAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving item: {ex.Message}");
            }
        }

        [HttpPost("items")]
        public async Task<IActionResult> SaveItems([FromBody] List<AlbumModel.Item> items)
        {
            try
            {
                await _albumsService.UpdateItemsAsync(items);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving items: {ex.Message}");
            }
        }

        [HttpPost("SaveSortedItems")]
        public async Task<IActionResult> SaveSortedItems([FromBody] Dictionary<Guid, int> sortedDictionary)
        {
            try
            {
                await _albumsService.SaveSortedItemsAsync(sortedDictionary);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error sorting items: {ex.Message}");
            }
        }

        [HttpPost("item/Delete")]
        public async Task<IActionResult> DeleteItem([FromBody] AlbumModel.Item item)
        {
            try
            {
                await _albumsService.DeleteItemAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting item: {ex.Message}");
            }
        }

        [HttpPost("items/Delete")]
        public async Task<IActionResult> DeleteItems([FromBody] List<AlbumModel.Item> items)
        {
            try
            {
                await _albumsService.DeleteItemsAsync(items);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting items: {ex.Message}");
            }
        }

        // ------------------------------------------------------------
        // MEDIA / THUMBNAILS (sin EF, solo caché)
        // ------------------------------------------------------------

        [HttpGet("Media/{guid}")]
        public async Task<IActionResult> Media(Guid guid)
        {
            var item = _cache.GetItem(guid);
            if (item == null)
                return NotFound();

            if (item.IsVideo())
                return await VideoStream(item);

            return PhysicalFile(item.FullPath, item.ContentType!);
        }

        [HttpGet("Thumb/{guid}")]
        public IActionResult Thumb(Guid guid)
        {
            var item = _cache.GetItem(guid);
            if (item == null)
                return NotFound();

            return PhysicalFile(item.ThumbPath, item.ContentType!);
        }

        // ------------------------------------------------------------
        // VIDEO STREAMING
        // ------------------------------------------------------------

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
                var range = rangeHeader.Replace("bytes=", "").Split('-');
                start = long.Parse(range[0]);
                if (range.Length > 1 && !string.IsNullOrEmpty(range[1]))
                    end = long.Parse(range[1]);

                response.StatusCode = StatusCodes.Status206PartialContent;
                response.Headers.Append("Content-Range", $"bytes {start}-{end}/{totalSize}");
            }

            var length = end - start + 1;
            response.ContentType = mimeType;
            response.ContentLength = length;

            using var fs = new FileStream(item.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            fs.Seek(start, SeekOrigin.Begin);
            var buffer = new byte[64 * 1024];
            long remaining = length;

            while (remaining > 0)
            {
                var read = await fs.ReadAsync(buffer, 0, (int)Math.Min(buffer.Length, remaining));
                if (read == 0) break;
                await response.Body.WriteAsync(buffer.AsMemory(0, read));
                remaining -= read;
            }

            return new EmptyResult();
        }

        // ------------------------------------------------------------
        // UPLOAD
        // ------------------------------------------------------------

        [Authorize]
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

                var tempDir = Path.Combine(AlbumModel.TmpUploadsFullPath(), fileName);
                Directory.CreateDirectory(tempDir);

                var tempPath = Path.Combine(tempDir, $"{fileName}.part{chunkIndex}");
                using var stream = new FileStream(tempPath, FileMode.Create);
                await file.CopyToAsync(stream);

                var usrId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
                if (!DateTimeOffset.TryParse(lastModified, out var fchCreated))
                    return BadRequest("Invalid date");

                var metaPath = Path.Combine(tempDir, "metadata.json");
                var metadata = new UploadedFileInfo
                {
                    FileName = fileName,
                    ContentType = contentType,
                    UsrCreated = new Guid(usrId),
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
        public async Task<IActionResult> UploadComplete()
        {
            try
            {
                var jsonPart = Request.Form["album"];
                var album = JsonConvert.DeserializeObject<AlbumModel>(jsonPart!);
                await _albumsService.CompleteUploadAsync(album!);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error completing upload: {ex.Message}");
            }
        }


        [HttpGet("Durations")]
        public async Task<IActionResult> Durations()
        {
            try
            {
                await _albumsService.SetVideosDurationAsync();
                return Ok("Success!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error setting video durations: {ex.Message}");

            }

        }
    }
}