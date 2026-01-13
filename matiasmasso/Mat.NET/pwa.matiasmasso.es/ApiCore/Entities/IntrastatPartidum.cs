using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Intrastat declaration items
/// </summary>
public partial class IntrastatPartidum
{
    /// <summary>
    /// Foreign key to parent table Intrastat
    /// </summary>
    public Guid Intrastat { get; set; }

    /// <summary>
    /// item line number
    /// </summary>
    public int Lin { get; set; }

    /// <summary>
    /// Document id, foreign key to either an invoice or a delivery note
    /// </summary>
    public Guid Tag { get; set; }

    /// <summary>
    /// Destination country or origin, foreign key to Country table
    /// </summary>
    public Guid Country { get; set; }

    /// <summary>
    /// Foreign key to destination province
    /// </summary>
    public Guid? Provincia { get; set; }

    /// <summary>
    /// pre-defined commercial terms published by the International Chamber of Commerce (ICC). Foreign key for Incoterms table
    /// </summary>
    public string Incoterm { get; set; } = null!;

    /// <summary>
    /// Enumerable DTOIntrastat.Partida.NaturalezasTransaccion. 11.Compra en firme
    /// </summary>
    public int NaturalezaTransaccion { get; set; }

    /// <summary>
    /// Enumerable DTOIntrastat.Partida.CodisTransport. 1.maritimo, 2.ferrocarril, 3.carretera....
    /// </summary>
    public int CodiTransport { get; set; }

    /// <summary>
    /// Customs code. Foreign key for CodisMercancia table
    /// </summary>
    public string CodiMercancia { get; set; } = null!;

    /// <summary>
    /// Country of manufacture. Foreign key to Country table
    /// </summary>
    public Guid? MadeIn { get; set; }

    /// <summary>
    /// Enumerable DTOIntrastat.Partida.RegimenesEstadisticos 1.destinoFinalEuropa
    /// </summary>
    public int RegimenEstadistico { get; set; }

    /// <summary>
    /// Total number of units of product
    /// </summary>
    public int? UnidadesSuplementarias { get; set; }

    /// <summary>
    /// Total weight of product
    /// </summary>
    public decimal Kg { get; set; }

    /// <summary>
    /// Total invoiced value
    /// </summary>
    public decimal ImporteFacturado { get; set; }

    /// <summary>
    /// Total products value
    /// </summary>
    public decimal ValorEstadistico { get; set; }

    public virtual CodisMercancium CodiMercanciaNavigation { get; set; } = null!;

    public virtual Country CountryNavigation { get; set; } = null!;

    public virtual Incoterm IncotermNavigation { get; set; } = null!;

    public virtual Intrastat IntrastatNavigation { get; set; } = null!;

    public virtual Country? MadeInNavigation { get; set; }

    public virtual Provincium? ProvinciaNavigation { get; set; }
}
