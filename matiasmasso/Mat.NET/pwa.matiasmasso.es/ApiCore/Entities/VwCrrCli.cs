using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwCrrCli
{
    public Guid CliGuid { get; set; }

    public Guid Guid { get; set; }

    public int Emp { get; set; }

    public int Crr { get; set; }

    public DateOnly Fch { get; set; }

    public byte Rt { get; set; }

    public string Dsc { get; set; } = null!;

    public Guid? UsrCreated { get; set; }

    public Guid? UsrLastEdited { get; set; }

    public DateTime FchCreated { get; set; }

    public DateTime FchLastEdited { get; set; }

    public string? UsrCreatedNickname { get; set; }

    public string? UsrCreatedEmailAddress { get; set; }

    public string? UsrLastEditedNickname { get; set; }

    public string? UsrLastEditedEmailAddress { get; set; }

    public string? Hash { get; set; }

    public int? Mime { get; set; }

    public int? Size { get; set; }

    public int? Pags { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }
}
