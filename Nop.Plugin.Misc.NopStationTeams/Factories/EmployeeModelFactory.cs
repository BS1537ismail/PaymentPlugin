using ExCSS;
using Nop.Plugin.Misc.NopStationTeams.Domain;
using Nop.Plugin.Misc.NopStationTeams.Models;
using Nop.Plugin.Misc.NopStationTeams.Service;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Models.Media;

namespace Nop.Plugin.Misc.NopStationTeams.Factories;

public class EmployeeModelFactory : IEmployeeModelFactory
{
    private readonly ILocalizationService _localizationService;
    private readonly IEmployeeSkillService _employeeSkillService;
    private readonly IPictureService _pictureService;

    public EmployeeModelFactory(ILocalizationService localizationService,
        IEmployeeSkillService employeeSkillService,
        IPictureService pictureService)
    {
        _localizationService = localizationService;
        _employeeSkillService = employeeSkillService;
        _pictureService = pictureService;
    }

    public async Task<IList<EmployeeModel>> PrepareEmployeeListModel(IList<Employee> employees, string widgetZone, int employeeId)
    {
        
        var model = new List<EmployeeModel>();

        foreach (var employee in employees)
        {
            if (widgetZone == PublicWidgetZones.CategoryDetailsBottom)
            {
                if (employeeId == 1 && (employee.IsNopCommerceCertified == true))
                {
                        model.Add(await PrepareEmployeeModel(employee));
                        continue;

                }
                else if (employeeId == 2 && employee.IsMVP == true)
                {
                        model.Add(await PrepareEmployeeModel(employee));
                        continue;
                }
                else if (employeeId == 3 && employee.IsNopCommerceCertified == false && employee.IsMVP == false)
                {
                        model.Add(await PrepareEmployeeModel(employee));
                        continue;
                }
               
            }
           else  model.Add(await PrepareEmployeeModel(employee));
        }

        return model;
    }

    public async Task<EmployeeModel> PrepareEmployeeModel(Employee employee)
    { 
        var skillMappings = await _employeeSkillService.GetDeveloperSkillMappingsByDeveloperIdAsync(employee.Id);
        var skills = await _employeeSkillService.GetSkillByIdsAsync(skillMappings.Select(sm => sm.SkillId).ToArray());

        var picture = await _pictureService.GetPictureByIdAsync(employee.PictureId);

        var pictureModel = new PictureModel
        {
            AlternateText = "Picture of " + employee.Name,
            Title = "Picture of " + employee.Name,
            ThumbImageUrl = (await _pictureService.GetPictureUrlAsync(picture, 200)).Url,
            FullSizeImageUrl = (await _pictureService.GetPictureUrlAsync(picture)).Url,
            Id = employee.PictureId
        };

        return new EmployeeModel()
        { 
            Designation = employee.Designation,
            EmployeeStatus = employee.EmployeeStatus,
            EmployeeStatusStr = await _localizationService.GetLocalizedEnumAsync(employee.EmployeeStatus),
            Id = employee.Id,
            IsMVP = employee.IsMVP,
            IsNopCommerceCertified = employee.IsNopCommerceCertified,
            Name = employee.Name,
            Skills  = skills.Select(s => s.SkillName).ToList(),
            Picture = pictureModel
        };
    }
}
