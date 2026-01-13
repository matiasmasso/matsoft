using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace DTO
{
    public class AlbumModel : BaseGuid
    {
        public string? Name { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public UserModel? UserCreated { get; set; }
        public DateTimeOffset? FchCreated { get; set; } = DateTimeOffset.Now;
        public int ImagesCount { get; set; }
        public int VideosCount { get; set; }
        public long Size { get; set; }

        public List<AlbumModel.Item> Items { get; set; } = new();

        public const string RootFolder = "C:\\Public\\Albums\\";
        public const string TmpUploadsFolder = "tmpUploads";
        public const string ApiBaseUrl = "https://genapi.matiasmasso.es";
        public const int ThumbnailMaxWidth = 200;
        public const int ThumbnailMaxHeight = 200;

        public AlbumModel() : base() { }
        public AlbumModel(Guid guid) : base(guid) { }

        public string FolderFullPath() => Path.Combine(AlbumModel.RootFolder, FolderName());
        public static string TmpUploadsFullPath() => Path.Combine(AlbumModel.RootFolder, TmpUploadsFolder);
        
        public string DatePart()
        {
            var sb = new StringBuilder();
            if (Year.HasValue)
            {
                sb.Append(Year.Value.ToString("D4"));
                if (Month.HasValue)
                {
                    sb.Append(".").Append(Month.Value.ToString("D2"));
                    if (Day.HasValue)
                    {
                        sb.Append(".").Append(Day.Value.ToString("D2"));
                    }
                }
            }
            return sb.ToString();
        }
        public string FolderName()
        {
            var sb = new StringBuilder(DatePart());

            if (!string.IsNullOrEmpty(Name))
            {
                if (sb.Length > 0) sb.Append(" ");
                sb.Append(Name);
            }
            return sb.ToString();
        }

        public string Features()
        {
            var sb = new StringBuilder();
            if (ImagesCount > 0) sb.Append($"{ImagesCount}i");
            if (VideosCount > 0)
            {
                sb.Append(sb.Length == 0 ? "" : ", ");
                sb.Append($"{VideosCount}v");
            }
            if (ImagesCount == 0 && VideosCount == 0)
                sb.Append("(buit)");
            else
            {
                sb.Append(sb.Length == 0 ? "" : ", ");
            sb.Append(Media.FormattedSize(Size));
            }
            return sb.ToString();
        }

        public bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Name ?? "";
                var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
                var options = CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreNonSpace;
                retval = searchTerms.All(x => compareInfo.IndexOf(searchTarget, x, options) >= 0);
            }
            return retval;
        }

        public class Item : BaseGuid, IHasId
        {
            public AlbumModel? Album { get; set; }
            public int? SortOrder { get; set; }
            public string? Hash { get; set; }
            public string? ContentType { get; set; }
            public string? Base64Thumbnail { get; set; }
            //public byte[]? Bytes { get; set; }
            public int? Size { get; set; }
            public TimeSpan? Duration { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }


            public UserModel? UserCreated { get; set; }
            public DateTimeOffset? FchCreated { get; set; } = DateTimeOffset.Now;



            public Item() : base() { }
            public Item(Guid guid) : base(guid) { }

            public string DownloadUrl => $"Albums/Media/{Guid}";
            public string ThumbUrl => $"Albums/Thumb/{Guid}";
            public string ThumbnailSrc => $"data:image/jpeg;base64,{Base64Thumbnail}";
            public string FileExtension => Media.GetExtension(ContentType!);
            public string Filename => $"{Guid.ToString()}{FileExtension}";
            public string Thumbname => $"{Guid.ToString()}.thumb{(IsVideo() ? ".jpg" : FileExtension)}";
            public string Tempname => $"{Guid.ToString()}.temp{(IsVideo() ? ".jpg" : FileExtension)}";
            public string FullPath => System.IO.Path.Combine(RootFolder, Album!.FolderName(), Filename);
            public string ThumbPath => System.IO.Path.Combine(RootFolder, Album!.FolderName(), Thumbname);
            public string TempPath => System.IO.Path.Combine(RootFolder, Album!.FolderName(), Tempname);

            public string Id => Guid.ToString();

            public bool IsImage() => ContentType?.StartsWith("image") ?? false;
            public bool IsVideo() => ContentType?.StartsWith("video") ?? false;
        }

    }
}
