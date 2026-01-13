using DTO;
namespace Api.Shared
{
    public class Cache
    {
        private static List<Img> imgList = new List<Img>();
        public static List<UserModel> Users = new List<UserModel>();

        public static Byte[]? GetImg(Guid guid, Img.Cods cod = Img.Cods.NotSet)
        {
            return imgList.FirstOrDefault(x => x.Guid.Equals(guid) && x.Cod == cod)?.Stream;
        }
        public static ImageMime? GetImg(string id, Img.Cods cod = Img.Cods.NotSet)
        {
            return imgList.FirstOrDefault(x => x.Id == id && x.Cod == cod)?.ImageMime;
        }

        public static void SetImg(Byte[] stream, Guid guid, Img.Cods cod = Img.Cods.NotSet)
        {
            imgList.Add(new Img
            {
                Guid = guid,
                Cod = cod,
                Stream = stream
            });
        }

        public static void RemoveImg(string id)
        {
            var item = imgList.FirstOrDefault(x => x.Id == id);
            if (item != null) imgList.Remove(item);
        }

        public static void SetImgMime(ImageMime value, string id, Img.Cods cod = Img.Cods.NotSet)
        {
            imgList.Add(new Img
            {
                Id = id,
                Cod = cod,
                ImageMime = value
            });
        }

        public static void AddUserIfMissing(UserModel? user)
        {
            if (user != null)
            {
                if (!Users.Any(x => x.Guid.Equals(user.Guid)))
                    Users.Add(user);
            }
        }

        public class Img
        {
            public Guid? Guid { get; set; }
            public string? Id { get; set; }
            public Cods Cod { get; set; } = Cods.NotSet;
            public Byte[]? Stream { get; set; }
            public ImageMime? ImageMime { get; set; }

            public enum Cods
            {
                NotSet,
                RaffleImgBanner600,
                RaffleThumbnail,
                SkuThumbnail,
                SkuImage,
                RaffleWinnerImg
            }

        }
    }
}
