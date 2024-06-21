using Azure.Storage.Blobs.Models;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.Identity.Client;
using Nop.Core;
using Nop.Plugin.Misc.NopStationTeams.Components;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Infrastructure;
namespace Nop.Plugin.Misc.NopStationTeams;


public class NopStationPlugin : BasePlugin, IWidgetPlugin
{
    public bool HideInWidgetList => false;

    private readonly IWebHelper _webHelper;
    private readonly ILocalizationService _localizationService;

    public NopStationPlugin(IWebHelper webHelper, 
        ILocalizationService localizationService)
    {
        _webHelper = webHelper;
        _localizationService = localizationService;
    }

    public override string GetConfigurationPageUrl()
    {
        return _webHelper.GetStoreLocation() + "Admin/Employee/List";
    }

    public override async Task InstallAsync()
    {
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Admin.Misc.Employee"] = "Employee",
            ["Admin.Misc.Employee.AddNew"] = "Add New Employee",
            ["Admin.Misc.Employee.EditDetails"] = "Edit Employee Details",
            ["Admin.Misc.Employee.BackToList"] = "Back To Employee List",
            ["Admin.Misc.Employee.Fields.Name"] = "Name",
            ["Admin.Misc.Employee.Fields.Designation"] = "Designation",
            ["Admin.Misc.Employee.Fields.IsMVP"] = "Is MVP",
            ["Admin.Misc.Employee.Fields.IsNopCommerceCertified"] = "Is certified",
            ["Admin.Misc.Employee.Fields.EmployeeStatus"] = "Status",
            ["Admin.Misc.Employee.Fields.Name.Hint"] = "Enter employee name.",
            ["Admin.Misc.Employee.Fields.Designation.Hint"] = "Enter employee designation.",
            ["Admin.Misc.Employee.Fields.IsMVP.Hint"] = "Check if employee is MVP.",
            ["Admin.Misc.Employee.Fields.IsNopCommerceCertified.Hint"] = "Check if employee is certified.",
            ["Admin.Misc.Employee.Fields.EmployeeStatus.Hint"] = "Select employee status.",
            ["Admin.Misc.Employee.List.Name"] = "Name",
            ["Admin.Misc.Employee.List.EmployeeStatus"] = "Status",
            ["Admin.Misc.Employee.List.Name.Hint"] = "Search by employee name.",
            ["Admin.Misc.Employee.List.EmployeeStatus.Hint"] = "Search by employee status.",
            ["Admin.Misc.Skill.List.SkillName"] = "SkillName",
            ["Admin.Misc.Skill.List.SkillName.Hint"] = "Search by SkillName",
            ["Admin.Misc.EmployeeSkill.Fields.SkillName"] = "Skills",
            ["Admin.Misc.Employee.Fields.SkillName"] = "Skill Name",
            ["Admin.Misc.Employee.Fields.SkillName.Hint"] = "Add Skills",
            ["admin.misc.skills.addnew"] = "Add Skills",
            [" admin.misc.skills.backtolist"] = "Back to List",
            ["admin.misc.skills"] = "Employee Skills",
            ["Admin.Misc.Employee.Fields.skills"] = "Skills",
            ["Admin.Misc.Employee.Fields.skills.Hint"] = "Add Skill",
            ["admin.misc.employees.editdetails"] = "Edit",

        });

        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    { 
        await base.UninstallAsync();
    }

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(
            new List<string>
            {
                PublicWidgetZones.HomepageBottom,
                //PublicWidgetZones.WishlistBottom
                PublicWidgetZones.CategoryDetailsBottom
                
            });
    }

    public Type GetWidgetViewComponent(string widgetZone)
    {
        
        if (widgetZone == PublicWidgetZones.HomepageBottom)
        {
            return (typeof(EmployeeViewComponent));
        }
        else
        {
             return (typeof(EmployeeCategoryViewComponent));

        }
    }

    
}
