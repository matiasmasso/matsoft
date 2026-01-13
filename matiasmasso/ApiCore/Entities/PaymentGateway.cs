using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Bank online gateways to manage secure payments from customers credit cards
/// </summary>
public partial class PaymentGateway
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Friendly name
    /// </summary>
    public string? Nom { get; set; }

    /// <summary>
    /// Code assigned by the gateway service (currently Redsys) to this account
    /// </summary>
    public string? MerchantCode { get; set; }

    /// <summary>
    /// Key to sign messages to and from the gateway
    /// </summary>
    public string? SignatureKey { get; set; }

    /// <summary>
    /// Url to address customers for new payments
    /// </summary>
    public string? SermepaUrl { get; set; }

    /// <summary>
    /// Url from our website the gateway service will send logs of operations
    /// </summary>
    public string? MerchantUrl { get; set; }

    /// <summary>
    /// Url the gateway will address our customers after a successful payment
    /// </summary>
    public string? UrlOk { get; set; }

    /// <summary>
    /// Url the gateway will address our customers after a failed payment
    /// </summary>
    public string? UrlKo { get; set; }

    /// <summary>
    /// Admin user name to manage our account on gateway service
    /// </summary>
    public string? UserAdmin { get; set; }

    /// <summary>
    /// Admin password to manage our account on gateway service
    /// </summary>
    public string? Pwd { get; set; }

    /// <summary>
    /// Date the gateway is active from
    /// </summary>
    public DateOnly FchFrom { get; set; }

    /// <summary>
    /// Date the gateway was closed
    /// </summary>
    public DateOnly? FchTo { get; set; }
}
