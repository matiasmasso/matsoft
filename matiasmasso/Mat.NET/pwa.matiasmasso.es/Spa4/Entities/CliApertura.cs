using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Potential new customer application form
    /// </summary>
    public partial class CliApertura
    {
        public CliApertura()
        {
            Brands = new HashSet<Tpa>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Contact person
        /// </summary>
        public string Nom { get; set; } = null!;
        /// <summary>
        /// Company name
        /// </summary>
        public string RaoSocial { get; set; } = null!;
        /// <summary>
        /// Commercial name
        /// </summary>
        public string NomComercial { get; set; } = null!;
        /// <summary>
        /// VAT number
        /// </summary>
        public string Nif { get; set; } = null!;
        /// <summary>
        /// Address (street and number)
        /// </summary>
        public string Adr { get; set; } = null!;
        /// <summary>
        /// ISO 3166 country code
        /// </summary>
        public string IsoPais { get; set; } = null!;
        /// <summary>
        /// Country zone; foreign key for Zona table
        /// </summary>
        public Guid? ZonaGuid { get; set; }
        /// <summary>
        /// Zip code
        /// </summary>
        public string Zip { get; set; } = null!;
        /// <summary>
        /// Location
        /// </summary>
        public string Cit { get; set; } = null!;
        /// <summary>
        /// Phone number
        /// </summary>
        public string Tel { get; set; } = null!;
        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// Website url
        /// </summary>
        public string Web { get; set; } = null!;
        /// <summary>
        /// Activity; foreign key for ContactClass table
        /// </summary>
        public Guid? ContactClass { get; set; }
        /// <summary>
        /// Commercial surface available; enumerable DTOCliApertura.CodsSuperficie
        /// </summary>
        public int CodSuperficie { get; set; }
        /// <summary>
        /// Turnover; enumerable DTOCliApertura.CodsVolumen
        /// </summary>
        public int CodVolumen { get; set; }
        /// <summary>
        /// Child care bussiness share within turnover
        /// </summary>
        public int SharePuericultura { get; set; }
        /// <summary>
        /// Other activities to complete turnover, if  any
        /// </summary>
        public string? OtherShares { get; set; }
        /// <summary>
        /// Number of sale points; enumerable DTOCliApertura.CodsSalePoints
        /// </summary>
        public int CodSalePoints { get; set; }
        /// <summary>
        /// Commercial associations which may be member of, if any
        /// </summary>
        public string? Associacions { get; set; }
        /// <summary>
        /// Antiquity; enumerable DTOCliApertura.CodsAntiguedad
        /// </summary>
        public int CodAntiguedad { get; set; }
        /// <summary>
        /// Launch date
        /// </summary>
        public DateTime? FchApertura { get; set; }
        /// <summary>
        /// Experience; enumerable DTOCliApertura.CodsExperience
        /// </summary>
        public int CodExperiencia { get; set; }
        /// <summary>
        /// Otherchild care brands which may be found in this commerce
        /// </summary>
        public string? OtherBrands { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string? Obs { get; set; }
        /// <summary>
        /// Status; enumerable DTOCliApertura.CodsTancament (1.Visited, 2.Placed first order, 3.Cancelled)
        /// </summary>
        public int CodTancament { get; set; }
        /// <summary>
        /// Comments from commercial agent
        /// </summary>
        public string? RepObs { get; set; }
        /// <summary>
        /// Inquiry date
        /// </summary>
        public DateTime FchCreated { get; set; }

        public virtual ContactClass? ContactClassNavigation { get; set; }
        public virtual Zona? ZonaGu { get; set; }

        public virtual ICollection<Tpa> Brands { get; set; }
    }
}
