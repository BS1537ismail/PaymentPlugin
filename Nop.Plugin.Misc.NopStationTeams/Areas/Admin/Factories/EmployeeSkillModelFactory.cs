using Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Models;
using Nop.Plugin.Misc.NopStationTeams.Domain;
using Nop.Plugin.Misc.NopStationTeams.Service;
using Nop.Services;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Factories;
public class EmployeeSkillModelFactory : IEmployeeSkillModelFactory
{
    private readonly IEmployeeSkillService _employeeSkillservice;
    private readonly ILocalizationService _localizationService;


    public EmployeeSkillModelFactory(IEmployeeSkillService employeeSkillservice, ILocalizationService localizationService)
    {
        _employeeSkillservice = employeeSkillservice;
        _localizationService = localizationService;

    }

    public async Task<EmployeeSkillListModel> PrepareEmployeeSkillListModelAsync(EmployeeSkillSearchModel searchModel)
    {


        ArgumentException.ThrowIfNullOrEmpty(nameof(searchModel));
        var skill = await _employeeSkillservice.SearchEmployeeSkillAsync(searchModel.SkillName, pageIndex: searchModel.Page - 1,
            pageSize: searchModel.PageSize);

        var model = await new EmployeeSkillListModel().PrepareToGridAsync(searchModel, skill, () =>
        {

            return skill.SelectAwait(async skill =>
            {
                return await PrepareEmployeeSkillModelAsync(null, skill, true);
            });
        });

        return model;
    }

    public async Task<EmployeeSkillModel> PrepareEmployeeSkillModelAsync(EmployeeSkillModel model, EmployeeSkill skill, bool excludeProperties = false)
    {
        if (skill != null)
            if (model == null)
                //fill in model values from the entity
                model = new EmployeeSkillModel()
                {
                    Id = skill.Id,
                    SkillName = skill.SkillName
                };

        return model;
    }

    public async Task<EmployeeSkillSearchModel> PrepareEmployeeSkillSearchModelAsync(EmployeeSkillSearchModel searchModel)
    {
        ArgumentNullException.ThrowIfNull(nameof(searchModel));

        //prepare page parameters
        searchModel.SetGridPageSize();

        return searchModel;
    }




}
