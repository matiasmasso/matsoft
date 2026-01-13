using Api.Data;
using Api.Entities;
using Api.Services;
using DTO;
using Microsoft.EntityFrameworkCore;

public class AlbumCacheService
{
    private readonly AppDbContext _db;
    private readonly AlbumCacheStore _store;

    private const int MaxHotAlbums = 20;

    public AlbumCacheService(AppDbContext db, AlbumCacheStore store)
    {
        _db = db;
        _store = store;
    }

    // ------------------------------------------------------------
    // PRELOAD COMPLETO (arranque o refresco)
    // ------------------------------------------------------------
    public void Preload()
    {
        var items = _db.AlbumItems
            .Include(x => x.AlbumNavigation)
            .Include(x => x.UsrCreatedNavigation)
            .AsNoTracking()
            .ToList();

        lock (_store.Lock)
        {
            _store.Items.Clear();
            _store.Albums.Clear();
            _store.Lru.Clear();

            foreach (var x in items)
            {
                var dto = Map(x);

                _store.Items[dto.Guid] = dto;

                if (!_store.Albums.ContainsKey(dto.Album.Guid))
                    _store.Albums[dto.Album.Guid] = new List<AlbumModel.Item>();

                _store.Albums[dto.Album.Guid].Add(dto);
            }
        }
    }

    // ------------------------------------------------------------
    // WARM ALBUM (carga bajo demanda)
    // ------------------------------------------------------------
    public void WarmAlbum(Guid albumGuid)
    {
        lock (_store.Lock)
        {
            if (_store.Albums.ContainsKey(albumGuid))
            {
                TouchLRU(albumGuid);
                return;
            }
        }

        var items = _db.AlbumItems
            .Where(x => x.Album == albumGuid)
            .OrderBy(x => x.SortOrder)
            .Include(x => x.AlbumNavigation)
            .Include(x => x.UsrCreatedNavigation)
            .AsNoTracking()
            .ToList()
            .Select(Map)
            .ToList();

        lock (_store.Lock)
        {
            _store.Albums[albumGuid] = items;

            foreach (var item in items)
                _store.Items[item.Guid] = item;

            TouchLRU(albumGuid);
            EnforceLRU();
        }
    }

    // ------------------------------------------------------------
    // LECTURAS
    // ------------------------------------------------------------
    public AlbumModel.Item? GetItem(Guid guid)
    {
        lock (_store.Lock)
        {
            return _store.Items.TryGetValue(guid, out var item)
                ? item
                : null;
        }
    }

    public List<AlbumModel.Item> GetItemsByAlbum(Guid albumGuid)
    {
        lock (_store.Lock)
        {
            if (_store.Albums.TryGetValue(albumGuid, out var list))
            {
                TouchLRU(albumGuid);
                return list;
            }
        }

        WarmAlbum(albumGuid);

        lock (_store.Lock)
        {
            return _store.Albums[albumGuid];
        }
    }

    // ------------------------------------------------------------
    // INVALIDACIÓN
    // ------------------------------------------------------------
    public void Update(AlbumModel.Item item)
    {
        lock (_store.Lock)
        {
            _store.Items[item.Guid] = item;

            if (!_store.Albums.ContainsKey(item.Album.Guid))
                _store.Albums[item.Album.Guid] = new List<AlbumModel.Item>();

            var list = _store.Albums[item.Album.Guid];
            var idx = list.FindIndex(x => x.Guid == item.Guid);

            if (idx >= 0)
                list[idx] = item;
            else
                list.Add(item);

            TouchLRU(item.Album.Guid);
        }
    }

    public void Remove(Guid itemGuid)
    {
        lock (_store.Lock)
        {
            if (_store.Items.TryGetValue(itemGuid, out var item))
            {
                _store.Items.Remove(itemGuid);

                if (_store.Albums.TryGetValue(item.Album.Guid, out var list))
                    list.RemoveAll(x => x.Guid == itemGuid);
            }
        }
    }

    public void RemoveAlbum(Guid albumGuid)
    {
        lock (_store.Lock)
        {
            if (_store.Albums.TryGetValue(albumGuid, out var items))
            {
                foreach (var item in items)
                    _store.Items.Remove(item.Guid);

                _store.Albums.Remove(albumGuid);
            }

            _store.Lru.Remove(albumGuid);
        }
    }

    // ------------------------------------------------------------
    // LRU
    // ------------------------------------------------------------
    private void TouchLRU(Guid albumGuid)
    {
        _store.Lru.Remove(albumGuid);
        _store.Lru.AddFirst(albumGuid);
    }

    private void EnforceLRU()
    {
        while (_store.Lru.Count > MaxHotAlbums)
        {
            var last = _store.Lru.Last!.Value;
            _store.Lru.RemoveLast();

            if (_store.Albums.TryGetValue(last, out var items))
            {
                foreach (var item in items)
                    _store.Items.Remove(item.Guid);

                _store.Albums.Remove(last);
            }
        }
    }

    // ------------------------------------------------------------
    // MAPPER
    // ------------------------------------------------------------
    private AlbumModel.Item Map(AlbumItem x)
    {
        return new AlbumModel.Item(x.Guid)
        {
            SortOrder = x.SortOrder,
            Hash = x.Hash,
            ContentType = x.ContentType,
            Size = x.Size,
            Duration = TimeSpan.FromSeconds((double?)x.DurationSeconds ?? 0),
            Title = x.Title,
            Description = x.Description,
            UserCreated = new UserModel(x.UsrCreated!)
            {
                Nickname = x.UsrCreatedNavigation.Nickname
            },
            FchCreated = x.FchCreated!,
            Album = new AlbumModel(x.AlbumNavigation.Guid)
            {
                Name = x.AlbumNavigation.Name,
                Year = x.AlbumNavigation.Year,
                Month = x.AlbumNavigation.Month,
                Day = x.AlbumNavigation.Day
            }
        };
    }
}