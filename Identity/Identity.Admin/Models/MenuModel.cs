namespace Identity.Admin.Models
{
    public class MenuModel
    {
        public List<Item> Items { get; set; } = new List<Item>();

        public Item? SelectedItem { get; set; }

        public void AddItem(string title, string url)
        {
            Items.Add(new Item
            {
                Title = title,
                Url = url
            });
        }
        public class Item
        {
            public string Title { get; set; } = string.Empty;
            public string Url { get; set; } = string.Empty;
        }
    }
}
