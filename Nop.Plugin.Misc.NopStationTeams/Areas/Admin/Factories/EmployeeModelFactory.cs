using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Models;
using Nop.Plugin.Misc.NopStationTeams.Domain;
using Nop.Plugin.Misc.NopStationTeams.Service;
using Nop.Services;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Factories;
public class EmployeeModelFactory : IEmployeeModelFactory
{
    private readonly IEmployeeService _employeeeService;
    private readonly ILocalizationService _localizationService;
    private readonly IPictureService _pictureService;
    private readonly IEmployeeSkillService _employeeSkillService;

    public EmployeeModelFactory(IEmployeeService employeeeService, ILocalizationService localizationService,
        IPictureService pictureService, IEmployeeSkillService employeeSkillService)
    {
        _employeeeService = employeeeService;
        _localizationService = localizationService;
        _pictureService = pictureService;
        _employeeSkillService = employeeSkillService;
    }

    public async Task<EmployeeListModel> PrepareEmployeeListModelAsync(EmployeeSearchModel searchModel)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(searchModel));
        var employees = await _employeeeService.SearchEmployeesAsync(searchModel.Name, searchModel.EmployeeStatusId,
            pageIndex: searchModel.Page - 1,
            pageSize: searchModel.PageSize);

        var model = await new EmployeeListModel().PrepareToGridAsync(searchModel, employees, () =>
        {

            return employees.SelectAwait(async employee =>
            {
                return await PrepareEmployeeModelAsync(null, employee, true);
            });
        });

        return model;
    }

    public async Task<EmployeeModel> PrepareEmployeeModelAsync(EmployeeModel model, Employee employee, bool excludeProperties = false)
    {
        if (employee != null)
        {
            if (model == null)
                //fill in model values from the entity
                model = new EmployeeModel()
                {
                    Designation = employee.Designation,
                    EmployeeStatusId = employee.EmployeeStatusId,
                    Id = employee.Id,
                    IsMVP = employee.IsMVP,
                    IsNopCommerceCertified = employee.IsNopCommerceCertified,
                    Name = employee.Name,
                    PictureId = employee.PictureId,
                };
            model.EmployeeStatusStr = await _localizationService.GetLocalizedEnumAsync(employee.EmployeeStatus);
            var picture = await _pictureService.GetPictureByIdAsync(employee.PictureId);
            (model.PictureThumbnailUrl, _) = await _pictureService.GetPictureUrlAsync(picture, 75);

            var employeeSkills = await _employeeSkillService.GetDeveloperSkillMappingsByDeveloperIdAsync(employee.Id);
            model.SelectedSkills = employeeSkills.Select(ds => ds.SkillId).ToList();


            var displayEmployeeSkill = await _employeeSkillService.GetSkillByIdsAsync(model.SelectedSkills.ToArray());
            model.Skills = displayEmployeeSkill.Select(ds => ds.SkillName).ToList();
        }

        if (!excludeProperties)
        {
            model.AvailableEmployeeStatusOptions = (await EmployeeStatus.Active.ToSelectListAsync()).ToList();

            var allSkills = await _employeeSkillService.GetAllSkillsAsync();
            model.AvailableEmployeeSkillOptions = allSkills.Select(skill => new SelectListItem
            {
                Value = skill.Id.ToString(),
                Text = skill.SkillName
            }).ToList();
        }

        return model;
    }

    public async Task<EmployeeSearchModel> PrepareEmployeeSearchModelAsync(EmployeeSearchModel searchModel)
    {
        ArgumentNullException.ThrowIfNull(nameof(searchModel));

        searchModel.AvailableEmployeeStatusOptions = (await EmployeeStatus.Active.ToSelectListAsync()).ToList();
        searchModel.AvailableEmployeeStatusOptions.Insert(0,
            new SelectListItem
            {
                Text = "All",
                Value = "0"
            });

        //prepare page parameters
        searchModel.SetGridPageSize();

        return searchModel;
    }
}

