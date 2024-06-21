using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Models;
using Nop.Plugin.Misc.NopStationTeams.Domain;

namespace Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Factories;
public interface IEmployeeSkillModelFactory
{
    Task<EmployeeSkillListModel> PrepareEmployeeSkillListModelAsync(EmployeeSkillSearchModel searchModel);
    Task<EmployeeSkillSearchModel> PrepareEmployeeSkillSearchModelAsync(EmployeeSkillSearchModel searchModel);
    Task<EmployeeSkillModel> PrepareEmployeeSkillModelAsync(EmployeeSkillModel model, EmployeeSkill skill, bool excludeProperties = false);
}
