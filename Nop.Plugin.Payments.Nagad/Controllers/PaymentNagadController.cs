using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Payments.Nagad.Models;
using Nop.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Payments.Nagad.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class PaymentNagadController : BasePaymentController
{
    #region Fields

    protected readonly ILocalizationService _localizationService;
    protected readonly INotificationService _notificationService;
    protected readonly IPermissionService _permissionService;
    protected readonly ISettingService _settingService;
    protected readonly IStoreContext _storeContext;

    #endregion

    #region Ctor

    public PaymentNagadController(ILocalizationService localizationService,
        INotificationService notificationService,
        IPermissionService permissionService,
        ISettingService settingService,
        IStoreContext storeContext)
    {
        _localizationService = localizationService;
        _notificationService = notificationService;
        _permissionService = permissionService;
        _settingService = settingService;
        _storeContext = storeContext;
    }

    #endregion

    #region Methods

    public async Task<IActionResult> Configure()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePaymentMethods))
            return AccessDeniedView();

        //load settings for a chosen store scope
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var nagadPaymentSettings = await _settingService.LoadSettingAsync<NagadPaymentSettings>(storeScope);

        var model = new ConfigurationModel
        {
            TransactModeId = Convert.ToInt32(nagadPaymentSettings.TransactMode),
            AdditionalFee = nagadPaymentSettings.AdditionalFee,
            AdditionalFeePercentage = nagadPaymentSettings.AdditionalFeePercentage,
            TransactModeValues = await nagadPaymentSettings.TransactMode.ToSelectListAsync(),
            ActiveStoreScopeConfiguration = storeScope
        };
        if (storeScope > 0)
        {
            model.TransactModeId_OverrideForStore = await _settingService.SettingExistsAsync(nagadPaymentSettings, x => x.TransactMode, storeScope);
            model.AdditionalFee_OverrideForStore = await _settingService.SettingExistsAsync(nagadPaymentSettings, x => x.AdditionalFee, storeScope);
            model.AdditionalFeePercentage_OverrideForStore = await _settingService.SettingExistsAsync(nagadPaymentSettings, x => x.AdditionalFeePercentage, storeScope);
        }

        return View("~/Plugins/Payments.Nagad/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePaymentMethods))
            return AccessDeniedView();

        if (!ModelState.IsValid)
            return await Configure();

        //load settings for a chosen store scope
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var nagadPaymentSettings = await _settingService.LoadSettingAsync<NagadPaymentSettings>(storeScope);

        //save settings
        nagadPaymentSettings.TransactMode = (TransactMode)model.TransactModeId;
        nagadPaymentSettings.AdditionalFee = model.AdditionalFee;
        nagadPaymentSettings.AdditionalFeePercentage = model.AdditionalFeePercentage;

        

        await _settingService.SaveSettingOverridablePerStoreAsync(nagadPaymentSettings, x => x.TransactMode, model.TransactModeId_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(nagadPaymentSettings, x => x.AdditionalFee, model.AdditionalFee_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(nagadPaymentSettings, x => x.AdditionalFeePercentage, model.AdditionalFeePercentage_OverrideForStore, storeScope, false);

        //now clear settings cache
        await _settingService.ClearCacheAsync();

        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }

    #endregion
}