using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Factories;
using Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Models;
using Nop.Plugin.Misc.NopStationTeams.Domain;
using Nop.Plugin.Misc.NopStationTeams.Service;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class EmployeeController : BasePluginController
{
    private readonly IEmployeeService _employeeService;
    private readonly IEmployeeModelFactory _employeeModelFactory;
    private readonly IEmployeeSkillService _employeeSkillService;

    public EmployeeController(IEmployeeService employeeService, 
        IEmployeeModelFactory employeeModelFactory, 
        IEmployeeSkillService employeeSkillService)
    {
        _employeeService = employeeService;
        _employeeModelFactory = employeeModelFactory;
        _employeeSkillService = employeeSkillService;
    }

    #region utils
    protected virtual async Task SaveSkillMappingsAsync(Employee employee, EmployeeModel model)
    {
        var existingDeveloperSkils = await _employeeSkillService.GetDeveloperSkillMappingsByDeveloperIdAsync(employee.Id);

        //delete skills
        foreach (var existingDeveloperSkil in existingDeveloperSkils)
            if (!model.SelectedSkills.Contains(existingDeveloperSkil.SkillId))
                await _employeeSkillService.DeleteDeveloperSkillMappingAsync(existingDeveloperSkil);

        var validSkills = await _employeeSkillService.GetSkillByIdsAsync(model.SelectedSkills.ToArray());
        //add skill
        foreach (var skillId in model.SelectedSkills)
        {
            if (validSkills.FirstOrDefault(s => s.Id == skillId) is null)
                continue;

            if (await _employeeSkillService.FindDeveloperSkillMappingAsync(model.Id, skillId) == null)
                await _employeeSkillService.InsertDeveloperSkillMappingAsync(new EmployeeSkillMapping
                {
                    EmployeeId = employee.Id,
                    SkillId = skillId
                });
        }
    }
    #endregion

    public async Task<IActionResult> List()
    {
        var searchModel = await _employeeModelFactory.PrepareEmployeeSearchModelAsync(new EmployeeSearchModel());
        return View("~/Plugins/Misc.NopStationTeams/Areas/Admin/Views/Employee/List.cshtml", searchModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> List(EmployeeSearchModel searchModel)
    {
        var model = await _employeeModelFactory.PrepareEmployeeListModelAsync(searchModel);
        return Json(model);
    }

    public async Task<IActionResult> Create()
    {
        var model = await _employeeModelFactory.PrepareEmployeeModelAsync(new EmployeeModel(), null);
        return View("~/Plugins/Misc.NopStationTeams/Areas/Admin/Views/Employee/Create.cshtml", model);
    }

    [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
    public async Task<IActionResult> Create(EmployeeModel model, bool continueEditing)
    {
        if (ModelState.IsValid)
        {
            var employee = new Employee
            {
                Designation = model.Designation,
                EmployeeStatusId = model.EmployeeStatusId,
                IsMVP = model.IsMVP,
                IsNopCommerceCertified = model.IsNopCommerceCertified,
                Name = model.Name,
                PictureId = model.PictureId,
            };

            await _employeeService.InsertEmployeeAsync(employee);
            await SaveSkillMappingsAsync(employee, model);

            return continueEditing ? RedirectToAction("Edit", new { id = employee.Id }) : RedirectToAction("List");
        }

        model = await _employeeModelFactory.PrepareEmployeeModelAsync(model, null);
        return View("~/Plugins/Misc.NopStationTeams/Areas/Admin/Views/Employee/Create.cshtml", model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee == null)
            return RedirectToAction("List");

        var model = await _employeeModelFactory.PrepareEmployeeModelAsync(null, employee);
        return View("~/Plugins/Misc.NopStationTeams/Areas/Admin/Views/Employee/Edit.cshtml", model);
    }

    [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
    public async Task<IActionResult> Edit(EmployeeModel model, bool continueEditing)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(model.Id);
        if (employee == null)
            return RedirectToAction("List");

        if (ModelState.IsValid)
        {
            employee.Designation = model.Designation;
            employee.EmployeeStatusId = model.EmployeeStatusId;
            employee.IsMVP = model.IsMVP;
            employee.IsNopCommerceCertified = model.IsNopCommerceCertified;
            employee.Name = model.Name;
            employee.PictureId = model.PictureId;

            await _employeeService.UpdateEmployeeAsync(employee);
            await SaveSkillMappingsAsync(employee, model);

            return continueEditing ? RedirectToAction("Edit", new { id = employee.Id }) : RedirectToAction("List");
        }

        model = await _employeeModelFactory.PrepareEmployeeModelAsync(model, employee);
        return View("~/Plugins/Misc.NopStationTeams/Areas/Admin/Views/Employee/Edit.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EmployeeModel model)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(model.Id);
        if (employee == null)
            return RedirectToAction("List");

        await _employeeService.DeleteEmployeeAsync(employee);
        return RedirectToAction("List");
    }
}
