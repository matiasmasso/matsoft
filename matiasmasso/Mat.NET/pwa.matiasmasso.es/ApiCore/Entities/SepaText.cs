using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Texts used to composed Sepa bank mandate in the different languages supported
/// </summary>
public partial class SepaText
{
    /// <summary>
    /// ISO 639-3 language code
    /// </summary>
    public string Lang { get; set; } = null!;

    /// <summary>
    /// Code refers to text position within the mandate
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Translated text
    /// </summary>
    public string? Text { get; set; }
}
