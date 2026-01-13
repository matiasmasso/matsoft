using Api.Data;
using Api.Entities;
using Api.Helpers;
using Api.Services;
using DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static DTO.AlbumModel;

public class AlbumsService
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment _env;
    private readonly AlbumCacheService _cache;

    public AlbumsService(IWebHostEnvironment env, AppDbContext db, AlbumCacheService cache)
    {
        _db = db;
        _env = env;
        _cache = cache;
    }

    // ------------------------------------------------------------
    // LECTURAS (solo desde la caché)
    // ------------------------------------------------------------

    public AlbumModel.Item? GetAlbumItem(Guid guid)
        => _cache.GetItem(guid);

    public List<AlbumModel.Item> GetAlbumItems(Guid albumGuid)
        => _cache.GetItemsByAlbum(albumGuid);

    public List<AlbumModel> GetAllAlbums()
    {
        // Preload aggregated item info
        var summaries = _db.AlbumItems
            .GroupBy(i => i.Album)
            .Select(g => new
            {
                Album = g.Key,
                Images = g.Count(i => !i.ContentType!.StartsWith("video")),
                Videos = g.Count(i => i.ContentType!.StartsWith("video")),
                Size = g.Sum(i => (long?)i.Size ?? 0)
            })
            .ToDictionary(x => x.Album, x => x);

        return _db.Albums
            .AsNoTracking()
            .OrderByDescending(x => x.Year)
            .ThenByDescending(x => x.Month)
            .ThenByDescending(x => x.Day)
            .ThenBy(x => x.Name)
            .Select(x => new AlbumModel
            {
                Guid = x.Guid,
                Name = x.Name,
                Year = x.Year,
                Month = x.Month,
                Day = x.Day,
                UserCreated = new UserModel(x.UsrCreated),
                FchCreated = x.FchCreated,

                ImagesCount = summaries.ContainsKey(x.Guid) ? summaries[x.Guid].Images : 0,
                VideosCount = summaries.ContainsKey(x.Guid) ? summaries[x.Guid].Videos : 0,
                Size = summaries.ContainsKey(x.Guid) ? summaries[x.Guid].Size : 0
            })
            .ToList();
    }


    public AlbumModel? GetAlbum(Guid guid)
            => _db.Albums
                .AsNoTracking()
                .Where(x => x.Guid == guid)
                .Select(x => new AlbumModel
                {
                    Guid = x.Guid,
                    Name = x.Name,
                    Year = x.Year,
                    Month = x.Month,
                    Day = x.Day,
                    UserCreated = new UserModel(x.UsrCreated),
                    FchCreated = x.FchCreated
                })
                .FirstOrDefault();

    // ------------------------------------------------------------
    // ESCRITURAS (BD + invalidación de caché)
    // ------------------------------------------------------------

    public async Task CompleteUploadAsync(AlbumModel album)
    {
        var tempDir = AlbumModel.TmpUploadsFullPath();
        var uploadDir = album.FolderFullPath();

        Directory.CreateDirectory(uploadDir);

        var items = new List<AlbumModel.Item>();

        foreach (var fileDir in Directory.GetDirectories(tempDir))
        {
            var metaPath = Path.Combine(fileDir, "metadata.json");
            if (!File.Exists(metaPath))
                continue;

            var metadata = System.Text.Json.JsonSerializer.Deserialize<UploadedFileInfo>(
                await File.ReadAllTextAsync(metaPath));

            if (metadata == null)
                continue;

            var fileName = Path.GetFileName(fileDir);
            var finalPath = Path.Combine(uploadDir, fileName);
            var partFiles = Directory.GetFiles(fileDir, fileName + ".part*")
                .OrderBy(f => int.Parse(f.Split(".part").Last()));

            // Rebuild file
            using (var finalStream = new FileStream(finalPath, FileMode.Create))
            {
                foreach (var part in partFiles)
                {
                    using var partStream = new FileStream(part, FileMode.Open);
                    await partStream.CopyToAsync(finalStream);
                }
            }

            // Compute hash
            var fileBytes = await File.ReadAllBytesAsync(finalPath);
            var hash = DTO.Helpers.CryptoHelper.Sha256(fileBytes);

            // Skip duplicates
            var existingItems = _cache.GetItemsByAlbum(album.Guid);
            if (existingItems.Any(i => i.Hash == hash))
            {
                File.Delete(finalPath);
                Directory.Delete(fileDir, true);
                continue;
            }

            // Create DTO
            var sortOrder = existingItems.Count > 0
                ? existingItems.Max(i => i.SortOrder ?? 0) + 1
                : 1;

            var item = new AlbumModel.Item()
            {
                Album = album,
                Size = fileBytes.Length,
                SortOrder = sortOrder,
                Hash = hash,
                ContentType = metadata.ContentType!,
                UserCreated = new UserModel(metadata.UsrCreated),
                FchCreated = metadata.FchCreated
            };

            // Move file to final GUID-based name
            var targetPath = item.FullPath;
            File.Move(finalPath, targetPath);

            // Thumbnail
            if (item.IsVideo())
            {
                var helper = new FFMpegHelper(_env);
                await helper.SaveVideoThumbnail(item);
            }
            else
            {
                var thumbBytes = CreateThumbnailFromDisk(
                    item.FullPath,
                    AlbumModel.ThumbnailMaxWidth,
                    AlbumModel.ThumbnailMaxHeight,
                    item.ContentType!);

                File.WriteAllBytes(item.ThumbPath, thumbBytes);
            }

            // Save to DB
            await UpdateItemAsync(item);

            // Update cache
            _cache.Update(item);

            // Cleanup
            Directory.Delete(fileDir, true);

            items.Add(item);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public byte[] CreateThumbnailFromDisk(string filePath, int maxWidth, int maxHeight, string contentType)
    {
        using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var original = System.Drawing.Image.FromStream(fs);

        // Calculate proportional size
        var ratioX = (double)maxWidth / original.Width;
        var ratioY = (double)maxHeight / original.Height;
        var ratio = Math.Min(ratioX, ratioY);

        var newWidth = (int)(original.Width * ratio);
        var newHeight = (int)(original.Height * ratio);

        using var thumbnail = new System.Drawing.Bitmap(newWidth, newHeight);
        using (var graphics = System.Drawing.Graphics.FromImage(thumbnail))
        {
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            graphics.DrawImage(original, 0, 0, newWidth, newHeight);
        }

        System.Drawing.Imaging.ImageFormat format = contentType.ToLowerInvariant() switch
        {
            "image/png" => System.Drawing.Imaging.ImageFormat.Png,
            "image/jpeg" => System.Drawing.Imaging.ImageFormat.Jpeg,
            "image/jpg" => System.Drawing.Imaging.ImageFormat.Jpeg,
            "image/bmp" => System.Drawing.Imaging.ImageFormat.Bmp,
            "image/gif" => System.Drawing.Imaging.ImageFormat.Gif,
            "image/tiff" => System.Drawing.Imaging.ImageFormat.Tiff,
            "image/x-icon" => System.Drawing.Imaging.ImageFormat.Icon,
            _ => System.Drawing.Imaging.ImageFormat.Png
        };

        using var ms = new MemoryStream();
        thumbnail.Save(ms, format);
        return ms.ToArray();
    }
    public async Task UpdateItemAsync(AlbumModel.Item value)
    {
        var entity = await _db.AlbumItems.FirstOrDefaultAsync(x => x.Guid == value.Guid);

        if (entity == null)
        {
            entity = new Api.Entities.AlbumItem();
            _db.AlbumItems.Add(entity);
            entity.Guid = value.Guid;
        }

        entity.Album = value.Album!.Guid;
        entity.SortOrder = value.SortOrder ?? 0;
        entity.Hash = value.Hash!;
        entity.ContentType = value.ContentType!;
        entity.Size = value.Size;
        entity.DurationSeconds = value.Duration.HasValue
    ? (long?)value.Duration.Value.TotalSeconds
    : null;

        entity.Title = value.Title;
        entity.Description = value.Description;
        entity.UsrCreated = value.UserCreated!.Guid;
        entity.FchCreated = (DateTimeOffset)value.FchCreated!;

        await _db.SaveChangesAsync();

        // Actualizar caché
        _cache.Update(value);
    }

    public async Task DeleteItemAsync(AlbumModel.Item item)
    {
        var entity = await _db.AlbumItems.FirstOrDefaultAsync(x => x.Guid == item.Guid);
        if (entity != null)
            _db.AlbumItems.Remove(entity);

        if (File.Exists(item.FullPath))
            File.Delete(item.FullPath);

        if (File.Exists(item.ThumbPath))
            File.Delete(item.ThumbPath);

        await _db.SaveChangesAsync();

        // Invalidar caché
        _cache.Remove(item.Guid);
    }

    public async Task DeleteItemsAsync(List<AlbumModel.Item> items)
    {
        var guids = items.Select(i => i.Guid).ToList();

        var entities = await _db.AlbumItems
            .Where(x => guids.Contains(x.Guid))
            .ToListAsync();

        _db.AlbumItems.RemoveRange(entities);

        foreach (var item in items)
        {
            if (File.Exists(item.FullPath))
                File.Delete(item.FullPath);

            if (File.Exists(item.ThumbPath))
                File.Delete(item.ThumbPath);

            _cache.Remove(item.Guid);
        }

        await _db.SaveChangesAsync();
    }

    public async Task UpdateItemsAsync(List<AlbumModel.Item> items)
    {
        // 1. Preload entities
        var guids = items.Select(i => i.Guid).ToList();

        var entities = await _db.AlbumItems
            .Where(x => guids.Contains(x.Guid))
            .ToDictionaryAsync(x => x.Guid);

        // 2. Preparar staging de movimientos (solo rutas)
        var stagedMoves = new List<(string Original, string Temp, string Final)>();

        foreach (var item in items)
        {
            if (!entities.TryGetValue(item.Guid, out var entity))
                continue;

            var previous = LoadAlbumItem(entity);

            bool albumChanged = item.Album!.Guid != previous.Album!.Guid;

            if (albumChanged)
            {
                // Rutas finales
                var finalFull = item.FullPath;
                var finalThumb = item.ThumbPath;

                Directory.CreateDirectory(Path.GetDirectoryName(finalFull)!);
                Directory.CreateDirectory(Path.GetDirectoryName(finalThumb)!);

                // Rutas temporales
                var tempFull = finalFull + ".tmp-" + Guid.NewGuid();
                var tempThumb = finalThumb + ".tmp-" + Guid.NewGuid();

                // Registrar staging
                stagedMoves.Add((previous.FullPath, tempFull, finalFull));
                stagedMoves.Add((previous.ThumbPath, tempThumb, finalThumb));
            }

            // Actualizar EF
            entity.Album = item.Album.Guid;
            entity.Title = item.Title;
            entity.Description = item.Description;
        }

        // 3. Iniciar transacción EF
        using var tx = await _db.Database.BeginTransactionAsync();

        try
        {
            // Guardar cambios EF
            await _db.SaveChangesAsync();

            // Update cache for each modified item
            foreach (var item in items)
            {
                _cache.Update(item);
            }

            // Invalidate affected albums
            var affectedAlbums = items.Select(i => i.Album!.Guid).Distinct();
            foreach (var albumGuid in affectedAlbums)
            {
                _cache.RemoveAlbum(albumGuid);
            }


            // 4. Mover archivos a temporales
            foreach (var (original, temp, _) in stagedMoves)
            {
                File.Move(original, temp, overwrite: true);
            }

            // 5. Confirmar transacción EF
            await tx.CommitAsync();
        }
        catch
        {
            // Rollback EF
            await tx.RollbackAsync();

            // Restaurar archivos originales si se movieron
            foreach (var (original, temp, _) in stagedMoves)
            {
                if (File.Exists(temp))
                    File.Move(temp, original, overwrite: true);
            }

            throw;
        }

        // 6. Commit físico: temp → final
        foreach (var (_, temp, final) in stagedMoves)
        {
            if (File.Exists(temp))
            {
                if (File.Exists(final))
                    File.Delete(final);

                File.Move(temp, final);
            }
        }
    }

    private AlbumModel.Item LoadAlbumItem(AlbumItem entity)
    {
        // Cargamos el álbum de EF por su Guid (campo entity.Album)
        var albumEntity = _db.Albums.First(a => a.Guid == entity.Album);

        // Proyectamos EF → DTO AlbumModel
        var albumDto = new AlbumModel(albumEntity.Guid)
        {
            Name = albumEntity.Name,
            Year = albumEntity.Year,
            Month = albumEntity.Month,
            Day = albumEntity.Day,
            // Si tienes más campos que afecten a FolderName(), añádelos aquí
        };

        // Proyectamos EF → DTO AlbumModel.Item
        var itemDto = new AlbumModel.Item(entity.Guid)
        {
            Album = albumDto,
            SortOrder = entity.SortOrder,
            Hash = entity.Hash,
            ContentType = entity.ContentType,   // IMPORTANTÍSIMO para FileExtension/Filename
            Size = entity.Size,
            Duration = TimeSpan.FromSeconds((double?)entity.DurationSeconds ?? 0),
            Title = entity.Title,
            Description = entity.Description,

            UserCreated = new UserModel(entity.UsrCreated),
            FchCreated = entity.FchCreated
        };

        // A partir de aquí, itemDto.FullPath / ThumbPath / TempPath
        // usan RootFolder + Album.FolderName() + Filename/Thumbname calculados en el DTO.

        return itemDto;
    }

    public async Task SaveSortedItemsAsync(Dictionary<Guid, int> sortedDictionary)
    {
        var guids = sortedDictionary.Keys.ToList();
        var entities = await _db.AlbumItems
            .Where(i => guids.Contains(i.Guid))
            .ToListAsync();

        foreach (var entity in entities)
            entity.SortOrder = sortedDictionary[entity.Guid];

        await _db.SaveChangesAsync();

        // ------------------------------------------------------------
        // FIX: invalidate album cache so next read reloads correct order
        // ------------------------------------------------------------
        var albumGuid = entities.First().Album;
        _cache.RemoveAlbum(albumGuid);
    }

    // ------------------------------------------------------------
    // ÁLBUMES
    // ------------------------------------------------------------

    public bool UpdateAlbum(AlbumModel value)
    {
        var entity = _db.Albums.FirstOrDefault(x => x.Guid == value.Guid);

        if (entity == null)
        {
            entity = new Api.Entities.Album();
            _db.Albums.Add(entity);
            entity.Guid = value.Guid;
        } else
        {
            // Comprobar si el cambio de nombre implica mover la carpeta física
            var previousAlbum = new AlbumModel(entity.Guid)
            {
                Name = entity.Name,
                Year = entity.Year,
                Month = entity.Month,
                Day = entity.Day
            };
            var previousFolder = previousAlbum.FolderFullPath();
            var newAlbum = new AlbumModel(value.Guid)
            {
                Name = value.Name,
                Year = value.Year,
                Month = value.Month,
                Day = value.Day
            };
            var newFolder = newAlbum.FolderFullPath();
            if (previousFolder != newFolder)
            {
                // Mover carpeta física
                if (Directory.Exists(previousFolder))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(newFolder)!);
                    Directory.Move(previousFolder, newFolder);
                }
            }
        }

        entity.Name = value.Name;
        entity.Year = value.Year;
        entity.Month = value.Month;
        entity.Day = value.Day;
        entity.UsrCreated = value.UserCreated!.Guid;
        entity.FchCreated = (DateTimeOffset)value.FchCreated!;

        _db.SaveChanges();

        _cache.RemoveAlbum(value.Guid);

        return true;
    }

    public async Task DeleteAlbumAsync(AlbumModel album)
    {
        var items = _cache.GetItemsByAlbum(album.Guid);

        await DeleteItemsAsync(items);

        var entity = await _db.Albums.FirstOrDefaultAsync(x => x.Guid == album.Guid);
        if (entity != null)
            _db.Albums.Remove(entity);

        var folder = album.FolderFullPath();
        if (Directory.Exists(folder))
            Directory.Delete(folder, true);

        await _db.SaveChangesAsync();

        _cache.RemoveAlbum(album.Guid);

    }

    public async Task SetVideosDurationAsync()
    {
        var items = new List<AlbumModel.Item>();
        var entities = await _db.AlbumItems 
            .AsNoTracking()
            .Where(x => x.ContentType.StartsWith("video") && x.DurationSeconds == null).ToListAsync();

        foreach(var entity in entities)
        {
            var item = LoadAlbumItem(entity);
            items.Add(item);
        }

        var helper = new FFMpegHelper(_env);
        foreach(var item in items)
        {
            if (item.Duration != null)
                continue;
            item.Duration = await helper.GetVideoDurationAsync(item.FullPath);
            await UpdateItemAsync(item);
        }



    }
}