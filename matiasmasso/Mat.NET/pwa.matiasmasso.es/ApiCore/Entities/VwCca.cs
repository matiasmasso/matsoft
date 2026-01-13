using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwCca
{
    public int Emp { get; set; }

    public Guid CcaGuid { get; set; }

    public int CcaId { get; set; }

    public DateOnly? Fch { get; set; }

    public string? Txt { get; set; }

    public int? Ccd { get; set; }

    public int? Cdn { get; set; }

    public Guid CcbGuid { get; set; }

    public decimal Eur { get; set; }

    public string Cur { get; set; } = null!;

    public decimal Pts { get; set; }

    public byte Dh { get; set; }

    public Guid? PndGuid { get; set; }

    public Guid CtaGuid { get; set; }

    public int CtaCod { get; set; }

    public byte CtaAct { get; set; }

    public string CtaEsp { get; set; } = null!;

    public string? CtaCat { get; set; }

    public string? CtaEng { get; set; }

    public string CtaId { get; set; } = null!;

    public Guid? ContactGuid { get; set; }

    public string? FullNom { get; set; }

    public Guid? UsrCreatedGuid { get; set; }

    public string? UsrCreatedAdr { get; set; }

    public string? UsrCreatedNickname { get; set; }

    public DateTime FchCreated { get; set; }

    public string? Hash { get; set; }
}
