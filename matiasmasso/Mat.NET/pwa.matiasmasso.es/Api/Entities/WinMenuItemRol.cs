using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Menu items per user rol
    /// </summary>
    public partial class WinMenuItemRol
    {
        /// <summary>
        /// foreign key for parent table WinMenuItem
        /// </summary>
        public Guid MenuItem { get; set; }
        /// <summary>
        /// Rol id authorized to activate this menu item
        /// </summary>
        public int Rol { get; set; }

        public virtual WinMenuItem MenuItemNavigation { get; set; } = null!;
    }
}
