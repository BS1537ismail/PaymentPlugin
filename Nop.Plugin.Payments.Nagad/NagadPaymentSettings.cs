using Nop.Core.Configuration;
namespace Nop.Plugin.Payments.Nagad;

public class NagadPaymentSettings : ISettings
{
 
    public TransactMode TransactMode { get; set; }
    public bool AdditionalFeePercentage { get; set; }
    public decimal AdditionalFee { get; set; }
}