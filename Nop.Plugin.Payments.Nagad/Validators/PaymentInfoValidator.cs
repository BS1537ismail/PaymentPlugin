using FluentValidation;
using Nop.Plugin.Payments.Nagad.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Payments.Nagad.Validators;

public class PaymentInfoValidator : BaseNopValidator<PaymentInfoModel>
{
    public PaymentInfoValidator(ILocalizationService localizationService)
    {

        RuleFor(x => x.MobileNumber).Matches(@"^(?:(?:\+|00)88|01)?\d{11}$").WithMessageAwait(localizationService.GetResourceAsync("Payment.MobileNumber.Wrong"));
        RuleFor(x => x.TransactionId).Matches(@"^[0-9]{3,4}$").WithMessageAwait(localizationService.GetResourceAsync("Payment.TransactionId.Wrong"));
    }
}