using Api.Controllers;
using Api.Entities;
using Api.Helpers;
using Azure.Core;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
//using FFMpegCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Api.Services
{
    public class AlbumService_Old
    {
        private readonly MatGenContext _db;
        private readonly IWebHostEnvironment _env;

        public AlbumService_Old(IWebHostEnvironment env, MatGenContext db)
        {
            _db = db;
            _env = env;
        }

        public AlbumModel? GetAlbum(Guid guid)
        {
            return _db.Albums
                .Where(x => x.Guid == guid)
                .Select(x => new AlbumModel
                {
                    Guid = x.Guid,
                    Name = x.Name,
                    Year = x.Year,
                    Month = x.Month,
                    Day = x.Day
                }).FirstOrDefault()!;
        }

        public bool Update(AlbumModel value)
        {
            Entities.Album? entity = _db.Albums.Where(x => x.Guid == value.Guid).FirstOrDefault();
            if (entity == null)
            {
                if (!CreateFolder(value)) throw new Exception($"Could not create folder {value.FolderName()}");
                entity = new Entities.Album();
                _db.Albums.Add(entity);
                entity.Guid = value.Guid;
            }
            else
            {
                if (!RenameFolder(value)) throw new Exception($"Could not rename folder {value.FolderName()}");
                //entity = _db.Albums.Where(x => x.Guid == value.Guid).FirstOrDefault();
            }

            //if (entity == null) throw new Exception($"Album '{value.Name ?? value.Guid.ToString()}' not found");

            entity.Name = value.Name;
            entity.Year = value.Year;
            entity.Month = value.Month;
            entity.Day = value.Day;

            _db.SaveChanges();
            return true;
        }
        public async Task Delete(AlbumModel value)
        {
            // Load all items for that album
            var itemsToDelete = await _db.AlbumItems
                .Where(x => x.Album == value.Guid)
                .ToListAsync();

            // Remove them
            _db.AlbumItems.RemoveRange(itemsToDelete);

            // Load the album 
            var albumToDelete = _db.Albums.FirstOrDefault(x => x.Guid.Equals(value.Guid));

            // Remove it
            if (albumToDelete != null)
                _db.Albums.Remove(albumToDelete);

            // Delete the folder and its contents
            var folderPath = value.FolderFullPath();
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, recursive: true);
            }

            // Commit
            await _db.SaveChangesAsync();
        }

        public bool CreateFolder(AlbumModel value)
        {
            var folderPath = Path.Combine(AlbumModel.RootFolder, value.FolderName());
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return true;
        }

        public bool RenameFolder(AlbumModel newValue)
        {
            var oldValue = GetAlbum(newValue.Guid)!;
            var oldFolderPath = Path.Combine(AlbumModel.RootFolder, oldValue.FolderName());
            var newFolderPath = Path.Combine(AlbumModel.RootFolder, newValue.FolderName());
            if (Directory.Exists(oldFolderPath) && !Directory.Exists(newFolderPath))
            {
                Directory.Move(oldFolderPath, newFolderPath);
            }
            return true;
        }

        public AlbumModel.Item GetAlbumItem(Guid guid)
        {
            return _db.AlbumItems
                .Where(x => x.Guid == guid)
                .Select(x => new AlbumModel.Item(x.Guid)
                {
                    Ord = x.Ord,
                    Hash = x.Hash,
                    ContentType = x.ContentType,
                    Size = x.Size,
                    Album = new AlbumModel(x.AlbumNavigation.Guid)
                    {
                        Name = x.AlbumNavigation.Name,
                        Year = x.AlbumNavigation.Year,
                        Month = x.AlbumNavigation.Month,
                        Day = x.AlbumNavigation.Day
                    }
                }).FirstOrDefault()!;
        }

        public List<AlbumModel.Item> GetAlbumItems(AlbumModel album)
        {
            var retval = _db.AlbumItems
                .Where(x => x.Album == album.Guid)
                .Select(x => new AlbumModel.Item(x.Guid)
                {
                    Album = album,
                    Ord = x.Ord,
                    Hash = x.Hash,
                    ContentType = x.ContentType,
                    Size = x.Size
                }).OrderBy(x => x.Ord)
            .ToList();

            return retval;
        }


        public async Task CompleteUploadAsync(AlbumModel album)
        {
            album!.Items = GetAlbumItems(album!);
            var tempDir = AlbumModel.TmpUploadsFullPath(); // where all temp chunks and uploads are stored
            var uploadDir = album!.FolderFullPath(); // final destination folder

            var items = new List<AlbumModel.Item>();

            foreach (var fileDir in Directory.GetDirectories(tempDir))
            {
                // Recuperem metadata
                var metaPath = System.IO.Path.Combine(fileDir, "metadata.json");
                var metadata = System.Text.Json.JsonSerializer.Deserialize<UploadedFileInfo>(
                    await System.IO.File.ReadAllTextAsync(metaPath));

                if (metadata!.ContentType!.StartsWith("image") | metadata.ContentType.StartsWith("video"))
                {
                    var fileName = System.IO.Path.GetFileName(fileDir);
                    var partFiles = Directory.GetFiles(fileDir, fileName + ".part*").OrderBy(f => f);
                    var finalPath = System.IO.Path.Combine(uploadDir, fileName);
                    //using var finalStream = new FileStream(finalPath, FileMode.Create);

                    //foreach (var partFile in partFiles)
                    //{
                    //    using var partStream = new FileStream(partFile, FileMode.Open);
                    //    await partStream.CopyToAsync(finalStream);
                    //}

                    byte[] fileBytes;
                    string hash;
                    using (var finalStream = new FileStream(finalPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                    {
                        foreach (var partFile in partFiles)
                        {
                            using var partStream = new FileStream(partFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                            await partStream.CopyToAsync(finalStream);
                        }

                        finalStream.Position = 0;
                        using var ms = new MemoryStream();
                        await finalStream.CopyToAsync(ms);
                        fileBytes = ms.ToArray();
                        hash = DTO.Helpers.CryptoHelper.Sha256(fileBytes);
                    }

                    //check if file already exists in album by hash
                    //var hash = DTO.Helpers.CryptoHelper.Sha256(finalPath);
                    if (album.Items!.Any(i => i.Hash == hash))
                    {
                        // delete the final file as it's a duplicate
                        System.IO.File.Delete(finalPath);
                        continue; // skip adding this item
                    }

                    var ord = (album.Items != null && album.Items.Count > 0) ? album.Items.Max(i => i.Ord ?? 0) + 1 : 1;
                    var item = new AlbumModel.Item()
                    {
                        Album = album,
                        Size = (int)new FileInfo(finalPath).Length,
                        Ord = ord,
                        Hash = hash,
                        ContentType = metadata?.ContentType ?? "application/octet-stream"
                    };

                    //rename file to include guid
                    var sourcePath = finalPath;
                    var targetPath = item.FullPath;

                    // This renames the file
                    System.IO.File.Move(sourcePath, targetPath);

                    try
                    {
                        if (item.IsVideo())
                        {
                            var helper = new FFMpegHelper(_env);
                            await helper.SaveVideoThumbnail(item);
                        }
                        else
                        {
                            var thumbnailBytes = CreateThumbnailFromDisk(item.FullPath, AlbumModel.ThumbnailMaxWidth, AlbumModel.ThumbnailMaxHeight, item.ContentType!);
                            File.WriteAllBytes(item.ThumbPath, thumbnailBytes);
                        }

                        UpdateItem(item);
                        items.Add(item);
                    }
                    catch (Exception ex)
                    {
                        if (System.IO.File.Exists(item.FullPath))
                            System.IO.File.Delete(item.FullPath);

                        if (System.IO.File.Exists(item.TempPath))
                            System.IO.File.Delete(item.TempPath);

                        if (System.IO.File.Exists(item.ThumbPath))
                            System.IO.File.Delete(item.ThumbPath);

                        throw new Exception($"Uploaded failed; file has been deleted: {ex.Message}");
                    }
                }

                //clean up temp files
                if (System.IO.Directory.Exists(fileDir))
                    Directory.Delete(fileDir, recursive: true);
            }
        }





        public async Task GenerateThumbnailsForAllImagesAsync()
        {
            var albums = GetAllAlbums();
            var sortedAlbums = albums.OrderBy(x => x.Year).ThenBy(x => x.Month).ThenBy(x => x.Day).ToList();
            foreach (var album in sortedAlbums)
            {
                var items = GetAlbumItems(album);
                foreach (var item in items)
                {
                    if (item.IsImage())
                    {
                        var thumbnailBytes = CreateThumbnailFromDisk(item.FullPath, AlbumModel.ThumbnailMaxWidth, AlbumModel.ThumbnailMaxHeight, item.ContentType!);
                        File.WriteAllBytes(item.ThumbPath, thumbnailBytes);
                    }
                }
            }

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public byte[] CreateThumbnailFromDisk(string filePath, int maxWidth, int maxHeight, string contentType)
        {
            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var original = Image.FromStream(fs);


            // Calculate proportional size
            var ratioX = (double)maxWidth / original.Width;
            var ratioY = (double)maxHeight / original.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(original.Width * ratio);
            var newHeight = (int)(original.Height * ratio);

            using var thumbnail = new Bitmap(newWidth, newHeight);
            using (var graphics = Graphics.FromImage(thumbnail))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                graphics.DrawImage(original, 0, 0, newWidth, newHeight);
            }

            ImageFormat format = contentType.ToLowerInvariant() switch
            {
                "image/png" => ImageFormat.Png,
                "image/jpeg" => ImageFormat.Jpeg,
                "image/jpg" => ImageFormat.Jpeg,
                "image/bmp" => ImageFormat.Bmp,
                "image/gif" => ImageFormat.Gif,
                "image/tiff" => ImageFormat.Tiff,
                "image/x-icon" => ImageFormat.Icon,
                _ => ImageFormat.Png // default fallback
            };

            using var ms = new MemoryStream();
            thumbnail.Save(ms, format);
            return ms.ToArray();
        }


        public bool UpdateItem(AlbumModel.Item value)
        {
            if (_db.AlbumItems.Any(x => x.Album == value.Album!.Guid && x.Hash == value.Hash))
                throw new Exception($"Item with same hash already exists in album {value.Album!.Name}");

            Entities.AlbumItem? entity;
            if (value.IsNew)
            {
                entity = new Entities.AlbumItem();
                _db.AlbumItems.Add(entity);
                entity.Guid = value.Guid;
            }
            else
            {
                throw new Exception($"update file not implemented {value.FullPath}");
            }


            entity.Album = value.Album!.Guid;
            entity.Ord = (int)value.Ord!;
            entity.Hash = value.Hash!;
            entity.ContentType = value.ContentType!;
            entity.Size = value.Size;

            _db.SaveChanges();
            return true;
        }

        public List<AlbumModel> GetAllAlbums()
        {
            return _db.Albums.Select(x => new AlbumModel
            {
                Guid = x.Guid,
                Name = x.Name,
                Year = x.Year,
                Month = x.Month,
                Day = x.Day
            }).OrderByDescending(x => x.Year)
            .ThenByDescending(x => x.Month)
            .ThenByDescending(x => x.Day)
            .ThenBy(x => x.Name)
            .ToList();
        }

        public void Rebuild()
        {
            var rootPath = AlbumModel.RootFolder;
            Directory.CreateDirectory(rootPath);
            var albums = GetAllAlbums();
            foreach (var album in albums)
            {
                var folderPath = Path.Combine(rootPath, album.FolderName());
                Directory.CreateDirectory(folderPath);

            }
        }

        public void Sort()
        {
            var albums = GetAllAlbums();
            foreach (var album in albums)
            {
                var idx = 0;
                foreach (var item in _db.AlbumItems.Where(x => x.Album == album.Guid))
                {
                    idx += 1;
                    item.Ord = idx;
                }
                _db.SaveChanges();
            }
        }
    }
}


