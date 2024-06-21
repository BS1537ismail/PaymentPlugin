using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;

namespace Nop.Plugin.Payments.Nagad.Domain;
public class PaymentInfo : BaseEntity
{
    public string AccountType { get; set; }
    public string MobileNumber { get; set; }
    public string TransactionId { get; set; }
}
