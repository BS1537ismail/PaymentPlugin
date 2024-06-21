using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.NopStationTeams.Domain;
using Nop.Plugin.Misc.NopStationTeams.Factories;
using Nop.Plugin.Misc.NopStationTeams.Models;
using Nop.Plugin.Misc.NopStationTeams.Service;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Misc.NopStationTeams.Components;

public class EmployeeCategoryViewComponent : NopViewComponent
{
    private readonly IEmployeeService _employeeService;
    private readonly IEmployeeModelFactory _employeeModelFactory;

    public EmployeeCategoryViewComponent(IEmployeeService employeeService,
        IEmployeeModelFactory employeeModelFactory)
    {
        _employeeService = employeeService;
        _employeeModelFactory = employeeModelFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var employeeModel = additionalData as CategoryModel;

        int employeeId = employeeModel.Id;
        var employees = await _employeeService.SearchEmployeesAsync("", (int)EmployeeStatus.Active);

       
        if (employees.Count() == 0)
            return Content("");
        

        var model = await _employeeModelFactory.PrepareEmployeeListModel(employees, widgetZone, employeeId);

        return View("~/Plugins/Misc.NopStationTeams/Views/Default.cshtml", model);
    }
}
