using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOGallery
    {
        public String SpriteUrl { get; set; }
        public List<Item> Items { get; set; }

        public int ItemWidth { get; set; }
        public int ItemHeight { get; set; }

        internal const int MAXCOLUMNS = 10;

        public int ItemsCount = 0;
        public int PageIdx = 0;
        public PaginationClass Pagination { get; set; }

        public static DTOGallery Factory(int itemWidth, int itemHeight, string spriteUrl = "", int itemsCount = 0, int pageIndex = 0)
        {
            DTOGallery retval = new DTOGallery();
            retval.ItemWidth = itemWidth;
            retval.ItemHeight = itemHeight;
            retval.SpriteUrl = spriteUrl;
            retval.Items = new List<Item>();
            retval.Pagination = new PaginationClass(itemsCount, 12, 5, pageIndex);
            return retval;
        }

        public Item AddItem(String caption = "", String imageUrl = "", String navigateTo = "", String tag = "")
        {
            Item retval = new Item();
            retval.Parent = this;
            retval.Caption = caption;
            retval.ImageUrl = imageUrl;
            retval.NavigateTo = navigateTo;
            retval.Tag = tag;
            Items.Add(retval);
            return retval;
        }


        public class Item
        {
            public String Caption { get; set; }
            public String NavigateTo { get; set; }

            public String ImageUrl { get; set; }
            public String Tag { get; set; }
            internal DTOGallery Parent { get; set; }

            public int OffsetX()
            {
                return ColIdx() * Parent.ItemWidth;
            }

            public int OffsetY()
            {
                return RowIdx() * Parent.ItemHeight;
            }

            public int ColIdx()
            {
                int idx = this.Idx();
                int retval = idx % DTOGallery.MAXCOLUMNS;
                return retval;
            }

            public int RowIdx()
            {
                int retval = Parent.Items.Count == 0 ? 0 : (int)Math.Floor((decimal)Idx() / Parent.Items.Count);
                return retval;
            }

            public int Idx()
            {
                return Parent.Items.IndexOf(this);
            }

        }

        public class PaginationClass
        {
            public int ItemsCount { get; set; }

            public int ItemsXPage { get; set; }
            public int ButtonsCount { get; set; }
            public int PageIdx { get; set; }

            public int PagesCount { get; set; }
            public int FirstVisiblePage { get; set; }
            public int LastVisiblePage { get; set; }

            public int PageFirstItem { get; set; }
            public int PageLastItem { get; set; }
            public string NavigateUrl { get; set; }


            public PaginationClass(int itemsCount = 0, int itemsXPage = 12, int buttonsCount = 5, int pageIdx = 0)
            {
                ItemsCount = itemsCount;
                ItemsXPage = itemsXPage;
                ButtonsCount = buttonsCount;
                PagesCount = (int)Math.Ceiling((decimal)itemsCount / itemsXPage);

                PageIdx = pageIdx;
                FirstVisiblePage = 0;
                LastVisiblePage = buttonsCount - 1;
                if (pageIdx > ButtonsCount / 2 - 1)
                    FirstVisiblePage = pageIdx - ButtonsCount / 2;
                if (pageIdx > PagesCount - ButtonsCount / 2 - 1)
                    FirstVisiblePage = PagesCount - ButtonsCount - 1;
                LastVisiblePage = FirstVisiblePage + ButtonsCount - 1;
                if (LastVisiblePage > PagesCount - 1)
                    LastVisiblePage = PagesCount - 1;

                PageFirstItem = PageIdx * ItemsXPage;
                PageLastItem = PageFirstItem + ItemsXPage - 1;
                if (PageLastItem > itemsCount - 1)
                    PageLastItem = itemsCount - 1;
            }

            public bool Enabled()
            {
                bool retval = PagesCount > 1;
                return retval;
            }

            public List<Button> Buttons()
            {
                List<Button> retval = new List<Button>();
                for (int i = FirstVisiblePage; i <= LastVisiblePage; i++)
                {
                    Button button = new Button(i, PageIdx);
                    retval.Add(button);
                }
                return retval;
            }

            public class Button
            {
                public string Caption { get; set; }
                public int Value { get; set; }
                public bool Active { get; set; }

                public Button(int value, int pageIdx)
                {
                    Value = value;
                    Caption = (value + 1).ToString();
                    Active = (value == pageIdx);
                }
            }

        }


    }
}
