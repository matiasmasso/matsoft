using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwStoreLocator2
{
    public Guid Brand { get; set; }

    public Guid Category { get; set; }

    public Guid? Country { get; set; }

    public Guid? AreaGuid { get; set; }

    public Guid? Location { get; set; }

    public Guid Client { get; set; }

    public bool Raffles { get; set; }

    public bool Impagat { get; set; }

    public bool Blocked { get; set; }

    public bool Obsoleto { get; set; }

    public int? ConsumerPriority { get; set; }

    public int SalePointsCount { get; set; }

    public Guid? PremiumLine { get; set; }

    public DateTime? LastFch { get; set; }

    public decimal? CcxVal { get; set; }

    public decimal? Val { get; set; }

    public decimal? ValHistoric { get; set; }
}
