using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ProductModel:BaseGuid, IModel
    {
        public SourceCods Src { get; set; }
        public LangTextModel Nom { get; set; }
        public LangTextModel Excerpt { get; set; }
        public LangTextModel Content { get; set; }

        public bool HasImage { get; set; }
        public bool Obsoleto { get; set; }

        public enum SourceCods
        {
            NotSet,
            Catalog,
            Brand,
            Category,
            Sku,
            Dept
        }

        // important en minuscules:
        public enum Tabs
        {
            general,
            coleccion,
            distribuidores,
            galeria,
            descargas,
            accesorios,
            videos,
            bloggerposts,
            descripcion,
            imagen
        }
        public ProductModel() : base()
        {
            Src = SourceCods.NotSet;
            Nom = new LangTextModel(base.Guid, LangTextModel.Srcs.ProductNom);
            Excerpt = new LangTextModel(base.Guid, LangTextModel.Srcs.ProductExcerpt);
            Content = new LangTextModel(base.Guid, LangTextModel.Srcs.ProductText);
        }
        public ProductModel(SourceCods src) : base()
        {
            Src = src;
            Nom = new LangTextModel(base.Guid, LangTextModel.Srcs.ProductNom);
            Excerpt = new LangTextModel(base.Guid, LangTextModel.Srcs.ProductExcerpt);
            Content = new LangTextModel(base.Guid, LangTextModel.Srcs.ProductText);
        }
        public ProductModel(Guid guid, SourceCods? src = null) : base(guid) {
            Src = src ?? SourceCods.NotSet;
            Nom = new LangTextModel(base.Guid, LangTextModel.Srcs.ProductNom);
            Excerpt = new LangTextModel(base.Guid, LangTextModel.Srcs.ProductExcerpt);
            Content = new LangTextModel(base.Guid, LangTextModel.Srcs.ProductText);
        }


        public string ThumbnailUrl() => Globals.RemoteApiUrl("product/thumbnail", string.Format("{0}.jpg", Guid.ToString()));

        public string PropertyPageUrl() => Globals.PageUrl("to do", Guid.ToString());

        public override bool Matches(string? searchTerm)
        {
            return Nom.Contains(searchTerm);
        }

        public class Filter:BaseGuid
        {
            public List<Guid> Items { get; set; } = new();

            public Filter() : base() {}

            public Filter(Guid oGuid) : base(oGuid) {}
        }
    }
}
