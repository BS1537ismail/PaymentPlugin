﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Payments.Nagad.Models;

public record ConfigurationModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; set; }

    [NopResourceDisplayName("Plugins.Payments.Nagad.Fields.AdditionalFeePercentage")]
    public bool AdditionalFeePercentage { get; set; }
    public bool AdditionalFeePercentage_OverrideForStore { get; set; }

    [NopResourceDisplayName("Plugins.Payments.Nagad.Fields.AdditionalFee")]
    public decimal AdditionalFee { get; set; }
    public bool AdditionalFee_OverrideForStore { get; set; }

    public int TransactModeId { get; set; }
    [NopResourceDisplayName("Plugins.Payments.Nagad.Fields.TransactMode")]
    public SelectList TransactModeValues { get; set; }
    public bool TransactModeId_OverrideForStore { get; set; }
}