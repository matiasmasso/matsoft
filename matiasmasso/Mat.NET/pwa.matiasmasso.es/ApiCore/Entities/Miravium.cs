using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Miravium
{
    public short RefMO { get; set; }

    public string RefFabricante { get; set; } = null!;

    public long EanProducto { get; set; }

    public string Marca { get; set; } = null!;

    public string CategorA { get; set; } = null!;

    public string Producto { get; set; } = null!;

    public int MiraviaCotegory { get; set; }

    public string MiraviaCotegoryName { get; set; } = null!;

    public string Venta { get; set; } = null!;

    public string? Largo { get; set; }

    public string? Ancho { get; set; }

    public string? Alto { get; set; }

    public string? Peso { get; set; }

    public string? MadeIn { get; set; }

    public int? CodigoArancelario { get; set; }

    public string? Imagen { get; set; }

    public string LandingPage { get; set; } = null!;
}
