using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Eci
{
    public string? TipoDeDocumento { get; set; }

    public double? Numero { get; set; }

    public string? Descripcion { get; set; }

    public string? Observaciones { get; set; }

    public string? Referencias { get; set; }

    public double? Proveedor { get; set; }

    public string? FechaDocumento { get; set; }

    public string? FechaContable { get; set; }

    public string? FechaVencimiento { get; set; }

    public string? FechaPrevistaPago { get; set; }

    public string? Importe { get; set; }

    public string? Divisa { get; set; }

    public double? DocumentoCompensación { get; set; }

    public double? DocumentoPago { get; set; }

    public string? VíaPago { get; set; }

    public string? ReceptorPago { get; set; }

    public string? Sucursal { get; set; }

    public double? DptoCompraUneco { get; set; }

    public double? Pedido { get; set; }

    public string? Albarán { get; set; }

    public string? CódigoCargoAbono { get; set; }

    public string? DenominaciónBanco { get; set; }

    public string? Divisa1 { get; set; }
}
