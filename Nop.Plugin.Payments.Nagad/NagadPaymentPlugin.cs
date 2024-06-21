using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Plugin.Payments.Nagad.Validators;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Plugins;
using Nop.Plugin.Payments.Nagad.Models;
using FluentValidation;
using Nop.Plugin.Payments.Nagad.Components;
using Nop.Plugin.Payments.Nagad.Service;
using Nop.Plugin.Payments.Nagad.Domain;
namespace Nop.Plugin.Payments.Nagad;


public class NagadPaymentPlugin : BasePlugin, IPaymentMethod
{
    #region Fields

    protected readonly ILocalizationService _localizationService;
    protected readonly IOrderTotalCalculationService _orderTotalCalculationService;
    protected readonly ISettingService _settingService;
    protected readonly IWebHelper _webHelper;
    protected readonly NagadPaymentSettings _nagadPaymentSettings;
    private readonly INagadPaymentService _nagadPayment;

    #endregion

    #region Ctor

    public NagadPaymentPlugin(ILocalizationService localizationService,
        IOrderTotalCalculationService orderTotalCalculationService,
        ISettingService settingService,
        IWebHelper webHelper,
        NagadPaymentSettings nagadPaymentSettings, INagadPaymentService nagadPayment)
    {
        _localizationService = localizationService;
        _orderTotalCalculationService = orderTotalCalculationService;
        _settingService = settingService;
        _webHelper = webHelper;
        _nagadPaymentSettings = nagadPaymentSettings;
        _nagadPayment = nagadPayment;
    }

    #endregion

    #region Methods

    public Task<ProcessPaymentResult> ProcessPaymentAsync(ProcessPaymentRequest processPaymentRequest)
    {
        var result = new ProcessPaymentResult
        {
            AllowStoringCreditCardNumber = true
        };
        switch (_nagadPaymentSettings.TransactMode)
        {
            case TransactMode.Pending:
                result.NewPaymentStatus = PaymentStatus.Pending;
                break;
            case TransactMode.Authorize:
                result.NewPaymentStatus = PaymentStatus.Authorized;
                break;
            case TransactMode.AuthorizeAndCapture:
                result.NewPaymentStatus = PaymentStatus.Paid;
                break;
            default:
                result.AddError("Not supported transaction type");
                break;
        }

        return Task.FromResult(result);
    }

    public Task PostProcessPaymentAsync(PostProcessPaymentRequest postProcessPaymentRequest)
    {
        //nothing
        return Task.CompletedTask;
    }

    public Task<bool> HidePaymentMethodAsync(IList<ShoppingCartItem> cart)
    {
        return Task.FromResult(false);
    }

    public async Task<decimal> GetAdditionalHandlingFeeAsync(IList<ShoppingCartItem> cart)
    {
        return await _orderTotalCalculationService.CalculatePaymentAdditionalFeeAsync(cart,
            _nagadPaymentSettings.AdditionalFee, _nagadPaymentSettings.AdditionalFeePercentage);
    }

    public Task<CapturePaymentResult> CaptureAsync(CapturePaymentRequest capturePaymentRequest)
    {
        return Task.FromResult(new CapturePaymentResult { Errors = new[] { "Capture method not supported" } });
    }

    public Task<RefundPaymentResult> RefundAsync(RefundPaymentRequest refundPaymentRequest)
    {
        return Task.FromResult(new RefundPaymentResult { Errors = new[] { "Refund method not supported" } });
    }

    public Task<VoidPaymentResult> VoidAsync(VoidPaymentRequest voidPaymentRequest)
    {
        return Task.FromResult(new VoidPaymentResult { Errors = new[] { "Void method not supported" } });
    }

    public Task<ProcessPaymentResult> ProcessRecurringPaymentAsync(ProcessPaymentRequest processPaymentRequest)
    {
        var result = new ProcessPaymentResult
        {
            AllowStoringCreditCardNumber = true
        };
        switch (_nagadPaymentSettings.TransactMode)
        {
            case TransactMode.Pending:
                result.NewPaymentStatus = PaymentStatus.Pending;
                break;
            case TransactMode.Authorize:
                result.NewPaymentStatus = PaymentStatus.Authorized;
                break;
            case TransactMode.AuthorizeAndCapture:
                result.NewPaymentStatus = PaymentStatus.Paid;
                break;
            default:
                result.AddError("Not supported transaction type");
                break;
        }

        return Task.FromResult(result);
    }

    public Task<CancelRecurringPaymentResult> CancelRecurringPaymentAsync(CancelRecurringPaymentRequest cancelPaymentRequest)
    {
        return Task.FromResult(new CancelRecurringPaymentResult());
    }

   
    public Task<bool> CanRePostProcessPaymentAsync(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        return Task.FromResult(false);
    }

    
    public async Task<IList<string>> ValidatePaymentFormAsync(IFormCollection form)
    {
        var warnings = new List<string>();

        //validate
        var validator = new PaymentInfoValidator(_localizationService);
        var model = new PaymentInfoModel
        {
            MobileNumber = form[nameof(PaymentInfoModel.MobileNumber)].ToString(),
            AccountType = form[nameof(PaymentInfoModel.AccountType)].ToString(),
            TransactionId = form[nameof(PaymentInfoModel.TransactionId)].ToString()
        };
        var validationResult = validator.Validate(model);

        var paymentModel = new PaymentInfo
        {
            MobileNumber = form[nameof(PaymentInfo.MobileNumber)].ToString(),
            AccountType = form[nameof(PaymentInfo.AccountType)].ToString(),
            TransactionId = form[nameof(PaymentInfo.TransactionId)].ToString()
        };

        if (validationResult.IsValid)
        {
            await _nagadPayment.InsertPaymentInfoAsync(paymentModel);
        }

        if (!validationResult.IsValid)
            warnings.AddRange(validationResult.Errors.Select(error => error.ErrorMessage));

        return warnings;
    }

   
    public Task<ProcessPaymentRequest> GetPaymentInfoAsync(IFormCollection form)
    {
        var request = new ProcessPaymentRequest();
        request.CustomValues["Account type"] = form[nameof(PaymentInfoModel.AccountType)].ToString();
        request.CustomValues["Number"] = form[nameof(PaymentInfoModel.MobileNumber)].ToString();
        request.CustomValues["Txn ID"] = form[nameof(PaymentInfoModel.TransactionId)].ToString();

        return Task.FromResult(request);
    }

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/PaymentNagad/Configure";
    }

  
    public Type GetPublicViewComponent()
    {
        return typeof(PaymentNagadViewComponent);
    }

   
    public override async Task InstallAsync()
    {
        //settings
        var settings = new NagadPaymentSettings
        {
            TransactMode = TransactMode.Pending
        };
        await _settingService.SaveSettingAsync(settings);

        //locales
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Payments.Nagad.Instructions"] = "This payment method stores credit card information in database (it's not sent to any third-party processor). In order to store credit card information, you must be PCI compliant.",
            ["Plugins.Payments.Nagad.Fields.AdditionalFee"] = "Additional fee",
            ["Plugins.Payments.Nagad.Fields.AdditionalFee.Hint"] = "Enter additional fee to charge your customers.",
            ["Plugins.Payments.Nagad.Fields.AdditionalFeePercentage"] = "Additional fee. Use percentage",
            ["Plugins.Payments.Nagad.Fields.AdditionalFeePercentage.Hint"] = "Determines whether to apply a percentage additional fee to the order total. If not enabled, a fixed value is used.",
            ["Plugins.Payments.Nagad.Fields.TransactMode"] = "After checkout mark payment as",
            ["Plugins.Payments.Nagad.Fields.TransactMode.Hint"] = "Specify transaction mode.",
            ["Plugins.Payments.Nagad.PaymentMethodDescription"] = "Pay by credit / debit card",
            ["payment.selectaccount"] = "Account",
            ["payment.selectaccount.Hint"] = "Select Account",
            ["payment.mobilenumber"] = "Mobile Number",
            ["payment.mobilenumber.Hint"] = "Set Mobile Number",
            ["payment.transactionid"] = "Transaction Id",
            ["payment.transactionid.Hint"] = "Set Transaction Id"

        });

        await base.InstallAsync();
    }

 
    public override async Task UninstallAsync()
    {
        //settings
        await _settingService.DeleteSettingAsync<NagadPaymentSettings>();

        //locales
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Payments.Nagad");

        await base.UninstallAsync();
    }

    
    public async Task<string> GetPaymentMethodDescriptionAsync()
    {
        return await _localizationService.GetResourceAsync("Plugins.Payments.Nagad.PaymentMethodDescription");
    }

    #endregion

    #region Properties

   
    public bool SupportCapture => false;

   
    public bool SupportPartiallyRefund => false;

    
    public bool SupportRefund => false;

    
    public bool SupportVoid => false;

    public RecurringPaymentType RecurringPaymentType => RecurringPaymentType.Manual;

    
    public PaymentMethodType PaymentMethodType => PaymentMethodType.Standard;

   
    public bool SkipPaymentInfo => false;

    #endregion
}
