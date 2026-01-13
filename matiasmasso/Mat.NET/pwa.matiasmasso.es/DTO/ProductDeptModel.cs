using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ProductDeptModel:ProductModel
    {
        public Guid Brand { get; set; }
        public ProductDeptModel() : base(SourceCods.Dept) { }
        public ProductDeptModel(Guid guid) : base(guid, SourceCods.Dept) { }

        public new string ThumbnailUrl() => Globals.RemoteApiUrl("productDept/thumbnail", string.Format("{0}.jpg", Guid.ToString()));
        public static string ThumbnailUrl(Guid guid) => Globals.RemoteApiUrl("productDept/thumbnail", string.Format("{0}.jpg", guid.ToString()));
        public const int THUMBNAIL_WIDTH = 700;
        public const int THUMBNAIL_HEIGHT = 700;

        public override string ToString()
        {
            return String.Format("Dept: {0}", Nom.Esp ?? "?");
        }

    }
}
