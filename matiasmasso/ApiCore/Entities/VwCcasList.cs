using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwCcasList
{
    public Guid Guid { get; set; }

    public int Emp { get; set; }

    public short Yea { get; set; }

    public int Cca { get; set; }

    public DateOnly? Fch { get; set; }

    public string? Txt { get; set; }

    public string? Hash { get; set; }

    public DateTime FchLastEdited { get; set; }

    public decimal? Eur { get; set; }

    public string? UsrNom { get; set; }
}
