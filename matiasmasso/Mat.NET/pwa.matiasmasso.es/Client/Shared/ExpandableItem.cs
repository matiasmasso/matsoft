namespace Client.Shared
{
    public class ExpandableItem
    {
        public string Nom { get; set; }
        public object? Tag { get; set; }
        public int Level { get; set; }
        public ExpandCods ExpandCod { get; set; }
        public int LinCod { get; set; }
        public CheckStates CheckState { get; set; } = CheckStates.None;
        public string? Ico { get; set; }
        public string? FontAwesome { get; set; }

        public enum ExpandCods
        {
            None,
            Collapsed,
            Expanded,
            Checkbox
        }

        public enum CheckStates : int
        {
            None,
            Checked,
            Unchecked,
            Indeterminate
        }

        public static ExpandableItem Factory(object tag, string nom, int level = 0, ExpandCods expandCod = ExpandCods.Collapsed)
        {
            var retval = new ExpandableItem();
            retval.Tag = tag;
            retval.Nom = nom;
            retval.Level = level;
            retval.ExpandCod = expandCod;
            return retval;
        }

        public bool IsExpanded()
        {
            return ExpandCod == ExpandCods.Expanded;
        }
        public bool IsCollapsed()
        {
            return ExpandCod == ExpandCods.Collapsed;
        }

        public string ExpansionClass()
        {
            var retval = "";
            if (IsExpanded())
                retval = "Expanded";
            else if (IsCollapsed())
                retval = "Collapsed";
            return retval;
        }

        public bool IsChecked()
        {
            return CheckState == CheckStates.Checked;
        }

        public override string ToString()
        {
            return Nom;
        }
    }

}
