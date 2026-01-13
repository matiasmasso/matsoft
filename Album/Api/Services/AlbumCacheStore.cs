using DTO;

namespace Api.Services
{
    public class AlbumCacheStore
    {
        public Dictionary<Guid, AlbumModel.Item> Items = new();
        public Dictionary<Guid, List<AlbumModel.Item>> Albums = new();
        public LinkedList<Guid> Lru = new();
        public object Lock = new();
    }
}
