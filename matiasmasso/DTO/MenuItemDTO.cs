using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class MenuItemDTO
    {
        public MenuItemModel? Model { get; set; }
        public List<GuidNom> ParentCandidates { get; set; } = new();


        public MenuItemDTO() { }


        public bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
                retval = Model?.Caption == null ? false : Model.Caption.Contains(searchTerm);
            return retval;
        }

        public new string ToString() => string.Format("{MenuItemDTO: {0}}", Model?.Caption?.Esp ?? "");
    }
}
