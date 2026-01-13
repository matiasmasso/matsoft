using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{

    public class NavDTO
    {
        public List<MenuItem> Items { get; set; } = new();
        //public List<KeyValuePair<Guid, UserModel.Rols>> ItemsRols { get; set; } = new();
        //public List<KeyValuePair<Guid, EmpModel.EmpIds>> ItemsEmps { get; set; } = new();


        //public List<Model> SortedItems()
        //{
        //    var retval = new List<Model>();
        //    var rootItems = Items.Where(x => x.Parent == null).OrderBy(y => y.Ord).ToList();
        //    foreach (var root in rootItems)
        //    {
        //        AddToSortedItems(root,retval);
        //    }
        //    return retval;
        //}

        //private void AddToSortedItems(NavDTO.Model parent, List<Model> list)
        //{
        //    list.Add(parent);
        //    var children = Items.Where(x => x.Parent == parent.Guid).OrderBy(y => y.Ord).ToList();
        //    foreach (var child in children)
        //    {
        //        child.Level = parent.Level + 1;
        //        AddToSortedItems(child, list);
        //    }
        //}

        public MenuItem AddItem(string url, string esp, string? cat = null, string? eng = null, string? por = null)
        {
            var retval = new MenuItem
            {
                Mode = MenuItem.Modes.Navigation,
                Ord = Items.Count == 0 ? 1 : Items.Max(x => x.Ord) + 1,
                Action = url,
                Nom = new LangTextModel { Esp = esp, Cat = cat, Eng = eng, Por = por }
            };
            Items.Add(retval);
            return retval;
        }

        public MenuItem AddAction(string action, string esp, string? cat = null, string? eng = null, string? por = null)
        {
            var retval = new MenuItem
            {
                Mode = MenuItem.Modes.Action,
                Ord = Items.Count == 0 ? 1 : Items.Max(x => x.Ord) + 1,
                Action = action,
                Nom = new LangTextModel { Esp = esp, Cat = cat, Eng = eng, Por = por }
            };
            Items.Add(retval);
            return retval;
        }

        public class MenuItem : BaseGuid, IModel
        {
            public Guid? Parent { get; set; }
            public Modes Mode { get; set; }
            public LangTextModel Nom { get; set; }
            public int Ord { get; set; }
            public int Level { get; set; }
            public string? Action { get; set; }
            public string? IcoSmall { get; set; }
            public string? IcoBig { get; set; }
            public List<Guid> Claims { get; set; } = new();
            public List<UserModel.Rols> Rols { get; set; } = new();
            public List<EmpModel.EmpIds> Emps { get; set; } = new();


            public enum Modes 
            {
                None,
                Title,
                Navigation,
                Toggle,
                Action,
                Login,
                Logout,
                Register,
                Spacer,
                Lang
            }

            public MenuItem() : base() {
                Nom = new LangTextModel(Guid, LangTextModel.Srcs.MenuItem);
            }
            public MenuItem(Guid guid) : base(guid) {
                Nom = new LangTextModel(Guid, LangTextModel.Srcs.MenuItem);
            }

            public string EditorUrl() => "/menuItem/" + Guid.ToString();
            public string CreateChildUrl() => "/menuItem/AddFromParent/" + Guid.ToString();
            public override string ToString() => Nom?.Esp ?? "{NavDTO.MenuItem " + Guid.ToString() +"}";

            public override bool Matches(string? searchTerm)
            {
                bool retval = true;
                if (!string.IsNullOrWhiteSpace(searchTerm))
                    retval = Nom?.Contains(searchTerm) ?? false;
                return retval;
            }

            public string PropertyPageUrl()
            {
                throw new NotImplementedException();
            }
        }
    }
}
