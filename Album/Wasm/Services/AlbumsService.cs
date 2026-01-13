using DTO;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using static DTO.AlbumModel;

namespace Wasm.Services
{
    public class AlbumsService
    {
        private readonly ApiHttpClient _api;

        public AlbumsService(ApiHttpClient api)
        {
            _api = api;
        }

        private HttpClient CreateClient()
        {
            var http = _api.CreateAuthenticated();   // IMPORTANT
            Console.WriteLine($"[AlbumsService] Using BaseAddress: {http.BaseAddress}");
            return http;
        }

        // ---------------------------------------------------------
        // GET SINGLE ALBUM
        // ---------------------------------------------------------
        public async Task<AlbumModel?> GetAlbumAsync(Guid guid)
        {
            var http = CreateClient();
            var url = $"albums/{guid}";

            Console.WriteLine($"[AlbumsService] GET {http.BaseAddress}{url}");

            try
            {
                var response = await http.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[AlbumsService] Failed GET {url}: {response.StatusCode}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<AlbumModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AlbumsService] ERROR in GetAlbumAsync: {ex}");
                return null;
            }
        }

        // ---------------------------------------------------------
        // SAVE ALBUM
        // ---------------------------------------------------------
        public async Task SaveAsync(AlbumModel album)
        {
            var http = CreateClient();
            var url = "albums";

            Console.WriteLine($"[AlbumsService] POST {http.BaseAddress}{url}");

            var response = await http.PostAsJsonAsync(url, album);

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error saving album: {msg}");
            }
        }

        // ---------------------------------------------------------
        // DELETE ALBUM
        // ---------------------------------------------------------
        public async Task DeleteAsync(AlbumModel album)
        {
            var http = CreateClient();
            var url = "albums/delete";

            Console.WriteLine($"[AlbumsService] POST {http.BaseAddress}{url}");

            var response = await http.PostAsJsonAsync(url, album);

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error deleting album: {msg}");
            }
        }

        // ---------------------------------------------------------
        // SAVE ALBUM.ITEM
        // ---------------------------------------------------------
        public async Task SaveAsync(AlbumModel.Item args)
        {
            var http = CreateClient();
            var url = "albums/item";

            var response = await http.PostAsJsonAsync(url, args);

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                var err = response.ReasonPhrase;
                throw new Exception($"Error saving album item: {msg ?? err}");
            }
        }
        public async Task SaveAsync(List<AlbumModel.Item> args)
        {
            var http = CreateClient();
            var url = "albums/items";

            var response = await http.PostAsJsonAsync(url, args);

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                var err = response.ReasonPhrase;
                throw new Exception($"Error saving album items: {msg ?? err}");
            }
        }

        // ---------------------------------------------------------
        // DELETE ALBUM.ITEM
        // ---------------------------------------------------------
        public async Task DeleteAsync(AlbumModel.Item item)
        {
            var http = CreateClient();
            var url = "albums/item/delete";

            var response = await http.PostAsJsonAsync(url, item);

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error deleting album.Item: {msg}");
            }
        }

        public async Task DeleteItemsAsync(List<AlbumModel.Item> items)
        {
            var http = CreateClient();
            var url = "albums/items/delete";

            var response = await http.PostAsJsonAsync(url, items);

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error deleting album.Items: {msg}");
            }
        }

        // ---------------------------------------------------------
        // SAVE ALBUM.ITEMS
        // ---------------------------------------------------------

        public async Task UpdateItemOrderAsync(Dictionary<Guid, int?> sortedDictionary)
        {
            var http = CreateClient();
            var url = "albums/saveSortedItems";

            var response = await http.PostAsJsonAsync(url, sortedDictionary);

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error saving sorted items: {msg}");
            }
        }

        // ---------------------------------------------------------
        // GET ALL ALBUMS
        // ---------------------------------------------------------
        public async Task<List<AlbumModel>?> GetAllAlbums()
        {
            var http = CreateClient();
            var url = "albums";

            Console.WriteLine($"[AlbumsService] GET {http.BaseAddress}{url}");

            try
            {
                return await http.GetFromJsonAsync<List<AlbumModel>>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AlbumsService] ERROR in GetAllAlbums: {ex}");
                return null;
            }
        }

        // ---------------------------------------------------------
        // GET ALBUM ITEMS
        // ---------------------------------------------------------
        public async Task<List<AlbumModel.Item>?> GetAlbumItems(AlbumModel parent)
        {
            var http = CreateClient();
            var url = "albums/GetAlbumItems";

            Console.WriteLine($"[AlbumsService] POST {http.BaseAddress}{url}");

            var response = await http.PostAsJsonAsync(url, parent);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<AlbumModel.Item>>();

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }

        // ---------------------------------------------------------
        // CHUNKED FILE UPLOAD
        // ---------------------------------------------------------

        public async Task UploadFilesToAlbumAsync(
    AlbumModel album,
    List<IBrowserFile> files,
    Action<int, long, long> progressCallback,
    HeicService heic)
        {
            var http = CreateClient();
            var albumJson = JsonConvert.SerializeObject(album);

            for (int fileIndex = 0; fileIndex < files.Count; fileIndex++)
            {
                var file = files[fileIndex];

                byte[] fileBytes;
                string uploadFileName = file.Name;
                string uploadContentType = file.ContentType;

                // ---------------------------------------------------------
                // HEIC conversion
                // ---------------------------------------------------------
                if (heic.IsHeic(file))
                {
                    try
                    {
                        var base64 = await heic.ConvertHeicAsync(file);

                        // Strip prefix
                        var commaIndex = base64.IndexOf(',');
                        var pureBase64 = base64.Substring(commaIndex + 1);

                        fileBytes = Convert.FromBase64String(pureBase64);
                    }
                    catch (Exception ex)
                    {
                        // maybe it was a Jpg with .heic extension
                        using var stream = file.OpenReadStream(long.MaxValue);
                        using var ms = new MemoryStream();
                        await stream.CopyToAsync(ms);
                        fileBytes = ms.ToArray();
                    }

                    uploadFileName = Path.ChangeExtension(file.Name, ".jpg");
                    uploadContentType = "image/jpeg";
                }
                else
                {
                    using var stream = file.OpenReadStream(long.MaxValue);
                    using var ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }

                long totalBytes = fileBytes.Length;
                long bytesUploaded = 0;

                // ---------------------------------------------------------
                // Chunk upload
                // ---------------------------------------------------------
                const int chunkSize = 1 * 1024 * 1024;
                var totalChunks = (int)Math.Ceiling((double)totalBytes / chunkSize);

                for (int chunkIndex = 0; chunkIndex < totalChunks; chunkIndex++)
                {
                    var offset = chunkIndex * chunkSize;
                    var size = Math.Min(chunkSize, fileBytes.Length - offset);
                    var chunkData = new byte[size];
                    Buffer.BlockCopy(fileBytes, offset, chunkData, 0, size);

                    var chunkContent = new ByteArrayContent(chunkData);

                    var form = new MultipartFormDataContent
            {
                { chunkContent, "file", $"{uploadFileName}.part{chunkIndex}" },
                { new StringContent(chunkIndex.ToString()), "chunkIndex" },
                { new StringContent(uploadFileName), "fileName" },
                { new StringContent(uploadContentType), "contentType" },
                { new StringContent(file.LastModified.ToString("o")), "lastModified" },
                { new StringContent(albumJson, Encoding.UTF8, "application/json"), "album" }
            };

                    var response = await http.PostAsync("albums/upload/chunk", form);

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Upload failed at chunk {chunkIndex}: {error}");
                    }

                    bytesUploaded += size;
                    progressCallback?.Invoke(fileIndex, bytesUploaded, totalBytes);
                }

                // ---------------------------------------------------------
                // Finalize
                // ---------------------------------------------------------
                var finalizeForm = new MultipartFormDataContent
        {
            { new StringContent(albumJson, Encoding.UTF8, "application/json"), "album" },
            { new StringContent(uploadFileName), "fileName" }
        };

                var finalizeResponse = await http.PostAsync("albums/upload/complete", finalizeForm);

                if (!finalizeResponse.IsSuccessStatusCode)
                {
                    var error = await finalizeResponse.Content.ReadAsStringAsync();
                    throw new Exception($"Finalizing upload failed for {uploadFileName}: {error}");
                }

                // Ensure 100%
                progressCallback?.Invoke(fileIndex, totalBytes, totalBytes);
            }
        }




        public async Task UpdateItemOrderAsync(AlbumModel selectedAlbum, List<AlbumModel.Item> items)
        {

        }

    }
}
