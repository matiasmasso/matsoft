using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Models
{
    public class CachedImages
    {
        List<Item> Items { get; set; }

        public CachedImages()
        {
            Items = new List<Item>();
        }

        public void Add(Guid guid, Defaults.ImgTypes cod, MatHelperStd.ImageMime imageMime)
        {
            Item item = new Item();
            item.Guid = guid;
            item.Cod = cod;
            item.ImageMime = imageMime;
            Items.Add(item);
        }

        public MatHelperStd.ImageMime ImageMime(Guid guid, Defaults.ImgTypes cod)
        {
            MatHelperStd.ImageMime retval = null;
            Item value = Items.FirstOrDefault(x => x.Guid.Equals(guid) & x.Cod == cod);
            if (value != null)
                retval = value.ImageMime;
            return retval;
        }

        public void Reset(Guid guid)
        {
            Items.RemoveAll(x => x.Guid.Equals(guid));
        }

        public class Item
        {
            public Guid Guid { get; set; }
            public DTO.Defaults.ImgTypes Cod { get; set; }
            public MatHelperStd.ImageMime ImageMime { get; set; }
        }
    }
}
