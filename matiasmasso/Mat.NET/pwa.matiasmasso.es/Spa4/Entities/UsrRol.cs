using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// User rol names
    /// </summary>
    public partial class UsrRol
    {
        public UsrRol()
        {
            Credencials = new HashSet<Credencial>();
            WebMenuItems = new HashSet<WebMenuItem>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public int Rol { get; set; }
        /// <summary>
        /// Name, Spanish language
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Name, Catalan language
        /// </summary>
        public string? NomCat { get; set; }
        /// <summary>
        /// Name, English language
        /// </summary>
        public string? NomEng { get; set; }
        /// <summary>
        /// Name, Portuguese language
        /// </summary>
        public string? NomPor { get; set; }
        /// <summary>
        /// Rol features description
        /// </summary>
        public string? Dsc { get; set; }

        public virtual ICollection<Credencial> Credencials { get; set; }
        public virtual ICollection<WebMenuItem> WebMenuItems { get; set; }
    }
}
