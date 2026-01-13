using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Models
{
    public class ImageCache
    {
        public List<Item> Items { get; set; }

        public enum Cods
        {
            Banner,
            CategorySkuColorsSprite,
            ProductSku
        }

        public ImageCache()
        {
            Items = new List<Item>();
        }
        public Item Find(Cods cod, Guid guid)
        {
            return Items.FirstOrDefault(x => x.Cod == cod && x.Guid.Equals(guid));
        }
        public Item Add(Cods cod, Guid guid, Byte[] image)
        {
            Item retval = new Item();
            retval.Cod = cod;
            retval.Guid = guid;
            retval.Image = image;
            Items.Add(retval);
            return retval;
        }
        public void Reset(Cods cod, Guid? guid = null)
        {
            if (guid == null)
                Items.RemoveAll(x => x.Cod == cod);
            else
                Items.RemoveAll(x => x.Cod == cod & x.Guid == guid);
        }

        public class Item
        {
            public Cods Cod { get; set; }
            public Guid Guid { get; set; }
            public byte[] Image { get; set; }
        }
    }
}
