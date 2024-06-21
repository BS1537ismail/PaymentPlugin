using Nop.Plugin.Payments.Nagad.Domain;

namespace Nop.Plugin.Payments.Nagad.Service;
public interface INagadPaymentService
{
    Task InsertPaymentInfoAsync(PaymentInfo employee);
}
