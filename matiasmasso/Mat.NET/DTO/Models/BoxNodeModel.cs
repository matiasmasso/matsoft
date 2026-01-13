using System.Collections.Generic;

namespace DTO
{
    public class BoxNodeModel : BoxViewModel
    {
        public BoxNodeModel.Collection Children { get; set; }

        public BoxNodeModel() : base()
        {
            Children = new BoxNodeModel.Collection();
        }

        public static BoxNodeModel Factory(string title, string navigateTo = "", string tag = "")
        {
            BoxNodeModel retval = new BoxNodeModel();
            {
                var withBlock = retval;
                withBlock.Title = title;
                withBlock.NavigateTo = navigateTo;
                withBlock.Tag = tag;
            }
            return retval;
        }

        public override string ToString()
        {
            return string.Format("BoxNodeModel: {0} -> {1}", Title ?? "", NavigateTo ?? "");
        }

        public new class Collection : List<BoxNodeModel>
        {
            public DTOLang Lang { get; set; }
            public int Cod { get; set; }


            public static BoxNodeModel.Collection Factory(DTOLang oLang, int cod = 0)
            {
                BoxNodeModel.Collection retval = new BoxNodeModel.Collection();
                retval.Lang = oLang;
                retval.Cod = cod;
                return retval;
            }
            public BoxNodeModel Add(string title, string navigateTo = "", string tag = "")
            {
                BoxNodeModel retval = BoxNodeModel.Factory(title, navigateTo, tag);
                this.Add(retval);
                return retval;
            }
        }
    }
}
