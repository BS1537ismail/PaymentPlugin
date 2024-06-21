using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Payments.Nagad.Models;

public record PaymentInfoModel : BaseNopModel
{
    public PaymentInfoModel()
    {
        AccountTypes = new List<SelectListItem>();
       
    }

    [NopResourceDisplayName("Payment.SelectAccount")]
    public string AccountType { get; set; }

    [NopResourceDisplayName("Payment.SelectAccount")]
    public IList<SelectListItem> AccountTypes { get; set; }

    

    [NopResourceDisplayName("Payment.MobileNumber")]
    public string MobileNumber { get; set; }

    [NopResourceDisplayName("Payment.Transaction_Id")]
    public string TransactionId { get; set; }
}