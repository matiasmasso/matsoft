using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class MenuItemModel:BaseGuid
    {
        public LangTextModel? Caption { get; set; }
        public LangTextModel? Url { get; set; }
        public bool BlankTarget { get; set; }
        public Action? Action { get; set; }
        public Guid? Parent { get; set; }
        public Globals.Pages Page { get; set; } = Globals.Pages.None;
        public int? Ord { get; set; }
        public bool Private { get; set; } = false;
        public Modes Mode { get; set; } = Modes.Navigation;
        public string? Ico { get; set; }



        public enum Modes : int
        {
            None,
            Title,
            Navigation,
            Toggle,
            Action,
            CustomAction
        }

        public MenuItemModel() : base()
        {
            Caption = new LangTextModel(Guid, LangTextModel.Srcs.MenuItem);
            Mode = Modes.Navigation;
        }
        public MenuItemModel(Guid guid) : base(guid)
        {
            Caption = new LangTextModel(Guid, LangTextModel.Srcs.MenuItem);
            Mode = Modes.Navigation;
        }
        public MenuItemModel(Modes mode, string? esp=null, string? cat=null, string? eng=null, string? por=null, bool blankTarget = false) : base()
        {
            Caption = new LangTextModel(Guid, LangTextModel.Srcs.MenuItem)
            {
                Esp = esp,
                Cat = cat,
                Eng = eng,
                Por = por
            };
            Mode = mode;
            BlankTarget= blankTarget;
        }

        public static MenuItemModel Spacer()
        {
            return new MenuItemModel(Modes.None);
        }

        public new string ToString() => string.Format("{LangTextModel: {0}", Caption?.Esp ?? "");

    }


    public class MenuItemToggleModel : MenuItemModel
    {
        public bool Value { get; set; }
        public Action? TrueAction { get; set; }
        public Action? FalseAction { get; set; }
        public MenuItemToggleModel(LangTextModel? caption, bool? value = null, Action? trueAction=null, Action? falseAction = null) {
            Caption = caption;
            Value = value ?? false;
            TrueAction = trueAction;
            FalseAction = falseAction;
            Mode = Modes.Toggle;
        }

        public void Toggle()
        {
            Value = !Value;
            if (Value & TrueAction != null) TrueAction!();
            else if (!Value & FalseAction != null) FalseAction!();
        }
    }

}
