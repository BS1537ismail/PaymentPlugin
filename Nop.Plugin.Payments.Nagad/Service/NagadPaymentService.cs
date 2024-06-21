using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Data;
using Nop.Plugin.Payments.Nagad.Domain;

namespace Nop.Plugin.Payments.Nagad.Service;
public class NagadPaymentService : INagadPaymentService
{
    private readonly IRepository<PaymentInfo> _paymentRepository;

    public NagadPaymentService(IRepository<PaymentInfo> paymentRepository)
    {
        _paymentRepository = paymentRepository;

    }

    public virtual async Task InsertPaymentInfoAsync(PaymentInfo payment)
    {
        await _paymentRepository.InsertAsync(payment);
    }
}
