using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Windows desktop ERP menu items
    /// </summary>
    public partial class WinMenuItem
    {
        public WinMenuItem()
        {
            InverseParentNavigation = new HashSet<WinMenuItem>();
            WinMenuItemRols = new HashSet<WinMenuItemRol>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Parent menu item (or nothing for root items)
        /// </summary>
        public Guid? Parent { get; set; }
        /// <summary>
        /// Sort order
        /// </summary>
        public int Ord { get; set; }
        /// <summary>
        /// Caption, Spanish language
        /// </summary>
        public string NomEsp { get; set; } = null!;
        /// <summary>
        /// Caption, Catalan language
        /// </summary>
        public string? NomCat { get; set; }
        /// <summary>
        /// Caption, English language
        /// </summary>
        public string? NomEng { get; set; }
        /// <summary>
        /// Caption, Portuguese language
        /// </summary>
        public string? NomPor { get; set; }
        /// <summary>
        /// Enumerable DTOWinMenuItem.Cods (1.folder, has children menu items 2.item, launches action)
        /// </summary>
        public int Cod { get; set; }
        /// <summary>
        /// Function name from main form Frm__Idx.vb to launch when clicked
        /// </summary>
        public string ActionProcedure { get; set; } = null!;
        /// <summary>
        /// 489x48px image
        /// </summary>
        public byte[]? Icon { get; set; }
        public int Mime { get; set; }
        /// <summary>
        /// Enumerable DTOWinMenuItem.CustomTargets to display custom items when clicked (1.banks, 2.Staff, 3.Reps...)
        /// </summary>
        public int CustomTarget { get; set; }
        /// <summary>
        /// Hyphen divided string with Company ids (foreign key to Emp table) where this menuitem should be used
        /// </summary>
        public string Emps { get; set; } = null!;
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }

        public virtual WinMenuItem? ParentNavigation { get; set; }
        public virtual ICollection<WinMenuItem> InverseParentNavigation { get; set; }
        public virtual ICollection<WinMenuItemRol> WinMenuItemRols { get; set; }
    }
}
