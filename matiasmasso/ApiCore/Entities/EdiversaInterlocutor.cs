using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// GLN EAN 13 Codes of the entities whom we have agreed to exchange Edi documents
/// </summary>
public partial class EdiversaInterlocutor
{
    /// <summary>
    /// EAN 13 code of the counterpart
    /// </summary>
    public string Ean { get; set; } = null!;
}
