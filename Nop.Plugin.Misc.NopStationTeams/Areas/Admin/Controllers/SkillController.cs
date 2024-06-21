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

public class SkillController : BasePluginController
{
    private readonly IEmployeeSkillService _employeeSkillService;
    private readonly IEmployeeSkillModelFactory _employeeSkillModelFactory;

    public SkillController(IEmployeeSkillService employeeSkillService, IEmployeeSkillModelFactory employeeSkillModelFactory)
    {
        _employeeSkillService = employeeSkillService;
        _employeeSkillModelFactory = employeeSkillModelFactory;
    }

    public async Task<IActionResult> List()
    {
        var searchModel = await _employeeSkillModelFactory.PrepareEmployeeSkillSearchModelAsync(new EmployeeSkillSearchModel());
        return View("~/Plugins/Misc.NopStationTeams/Areas/Admin/Views/Skill/List.cshtml", searchModel);
    }

    [HttpPost]
    public async Task<IActionResult> List(EmployeeSkillSearchModel searchModel)
    {
        var model = await _employeeSkillModelFactory.PrepareEmployeeSkillListModelAsync(searchModel);
        return Json(model);
    }

    public async Task<IActionResult> Create()
    {
        var model = await _employeeSkillModelFactory.PrepareEmployeeSkillModelAsync(new EmployeeSkillModel(), null);
        return View("~/Plugins/Misc.NopStationTeams/Areas/Admin/Views/Skill/Create.cshtml", model);
    }

    [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
    public async Task<IActionResult> Create(EmployeeSkillModel model, bool continueEditing)
    {
        if (ModelState.IsValid)
        {
            var skill = new EmployeeSkill
            {
                SkillName = model.SkillName,
            };

            await _employeeSkillService.InsertEmployeeAsync(skill);

            return continueEditing ? RedirectToAction("Edit", new { id = skill.Id }) : RedirectToAction("List");
        }

        model = await _employeeSkillModelFactory.PrepareEmployeeSkillModelAsync(model, null);
        return View("~/Plugins/Misc.NopStationTeams/Areas/Admin/Views/Skill/Create.cshtml", model);

    }
    public async Task<IActionResult> Edit(int id)
    {
        var skill = await _employeeSkillService.GetEmployeeByIdAsync(id);
        if (skill == null)
            return RedirectToAction("List");

        var model = await _employeeSkillModelFactory.PrepareEmployeeSkillModelAsync(null, skill);
        return View("~/Plugins/Misc.NopStationTeams/Areas/Admin/Views/Skill/Edit.cshtml", model);
    }

    [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
    public async Task<IActionResult> Edit(EmployeeSkillModel model, bool continueEditing)
    {
        var skill = await _employeeSkillService.GetEmployeeByIdAsync(model.Id);
        if (skill == null)
            return RedirectToAction("List");

        if (ModelState.IsValid)
        {
            skill.SkillName = model.SkillName;


            await _employeeSkillService.UpdateEmployeeAsync(skill);

            return continueEditing ? RedirectToAction("Edit", new { id = skill.Id }) : RedirectToAction("List");
        }

        model = await _employeeSkillModelFactory.PrepareEmployeeSkillModelAsync(model, skill);
        return View("~/Plugins/Misc.NopStationTeams/Areas/Admin/Views/Skill/Edit.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EmployeeSkillModel model)
    {
        var skill = await _employeeSkillService.GetEmployeeByIdAsync(model.Id);
        if (skill == null)
            return RedirectToAction("List");

        await _employeeSkillService.DeleteEmployeeAsync(skill);
        return RedirectToAction("List");
    }
}
