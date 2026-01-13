using Client.Shared;
using DTO;

namespace Client.ViewModels
{
    public class NavItemsViewModel
    {
        private AppState AppState;
        public List<MenuItemModel> MenuItems { get; set; }
        public List<ExpandableItem> Items { get; set; } = new List<ExpandableItem>();

        public ExpandableItem? SelectedItem { get; set; }


        public NavItemsViewModel(AppState appstate, List<MenuItemModel> menuItems)
        {
            AppState = appstate;
            MenuItems = menuItems;
            var rootMenuItems = new List<MenuItemModel>();
            rootMenuItems.AddRange(menuItems.Where(x => x.Parent == null));
            Items = ExpandableItems(rootMenuItems); 
        }

        public void ItemClicked(ExpandableItem item)
        {
            SelectedItem = item;

            if (item.IsExpanded())
            {
                item.ExpandCod = Shared.ExpandableItem.ExpandCods.Collapsed;
                Items.RemoveAll(x => x.Level > item.Level);
            }
            else if (item.IsCollapsed())
            {
                item.ExpandCod = Shared.ExpandableItem.ExpandCods.Expanded;
                var parentMenu = item.Tag as MenuItemModel;
                var childMenus = MenuItems.Where(x => x.Parent.Equals(parentMenu!.Guid)).ToList();
                childMenus.Reverse();
                var idx = Items.IndexOf(SelectedItem);
                foreach(var childMenu in childMenus)
                {
                    Items.Insert(idx + 1, ExpandableItem(childMenu, item.Level + 1));
                }
            }
            else
            {
                var menuItem = item.Tag as MenuItemModel;
                if (menuItem != null)
                {
                    if (menuItem.Mode == MenuItemModel.Modes.Action)
                    {
                        //....
                    }
                }
            }
        }

        public bool IsRequestToLogout(ExpandableItem item)
        {
            var menuItem = item.Tag as MenuItemModel;
            if (menuItem != null)
                return (menuItem.Mode == MenuItemModel.Modes.Action && menuItem.Action == "Logout");
            return false;
        }

        public string ExpandClass(ExpandableItem item)
        {
            var retval = string.Empty;
            if (item.IsExpanded())
                retval = "Expanded";
            else if (item.IsCollapsed())
                retval = "Collapsed";
            return retval;
        }

        public List<ExpandableItem> ExpandableItems(List<MenuItemModel> menuItems, int level = 0)
        {
            return menuItems.Select(x => ExpandableItem(x, level)).ToList();
        }

        public ExpandableItem ExpandableItem(MenuItemModel menuItem, int level = 0)
        {
            var hasChildren = MenuItems.Any(x => x.Parent?.Equals(menuItem.Guid) ?? false);
            var expandCod = hasChildren ? Shared.ExpandableItem.ExpandCods.Collapsed : Shared.ExpandableItem.ExpandCods.None;
            var retval = Shared.ExpandableItem.Factory(menuItem, menuItem.Caption?.Tradueix(AppState.Lang!) ?? "", level, expandCod);
            retval.FontAwesome = menuItem.Ico;
            return retval;
        }

        public string Link(ExpandableItem item)
        {
            var retval = string.Empty;
            var menuItem = item.Tag as MenuItemModel;
            if (menuItem!.Mode == MenuItemModel.Modes.Navigation)
                retval = menuItem?.Action ?? "";
            return retval;
        }

        public bool HasLink(ExpandableItem item)
        {
            return !string.IsNullOrEmpty(Link(item));
        }

        public bool IsSelected(ExpandableItem item)
        {
            return item.Equals(SelectedItem);
        }


        public bool IsToggle(ExpandableItem item)
        {
            var retval = string.Empty;
            var menuItem = item.Tag as MenuItemModel;
            return (menuItem!.Mode == MenuItemModel.Modes.Toggle);
        }



        private List<MenuItemModel> Children(Guid? parent = null)
        {
            var retval = new List<MenuItemModel>();
            retval.AddRange(MenuItems?.Where(x => x.Parent == parent).ToList() ?? new List<MenuItemModel>());
            return retval;
        }

    }
}
