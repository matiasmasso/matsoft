using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Tpv
{
    public Guid Guid { get; set; }

    public int Emp { get; set; }

    /// <summary>
    /// Friendly name
    /// </summary>
    public string? Nom { get; set; }

    public string? MerchantCode { get; set; }

    public string? GatewayUrlDevelopment { get; set; }

    public string? GatewayUrlProduction { get; set; }

    public int MerchantTerminal { get; set; }

    public string? MerchantUrl { get; set; }

    public string? UrlOk { get; set; }

    public string? UrlKo { get; set; }

    public string? PrivateKeyDevelopment { get; set; }

    public string? PrivateKeyProduction { get; set; }

    public Guid? Banc { get; set; }

    public DateTime FchCreated { get; set; }

    public string SignatureVersion { get; set; } = null!;

    public int Environment { get; set; }

    public string? AdminUrlProduction { get; set; }

    public string? AdminUrlDevelopment { get; set; }

    public virtual CliBnc? BancNavigation { get; set; }
}
