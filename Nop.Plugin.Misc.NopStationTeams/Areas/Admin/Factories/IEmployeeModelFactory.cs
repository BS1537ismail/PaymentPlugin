using Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Models;
using Nop.Plugin.Misc.NopStationTeams.Domain;

namespace Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Factories;

public interface IEmployeeModelFactory
{
    Task<EmployeeListModel> PrepareEmployeeListModelAsync(EmployeeSearchModel searchModel);
    Task<EmployeeSearchModel> PrepareEmployeeSearchModelAsync(EmployeeSearchModel searchModel);
    Task<EmployeeModel> PrepareEmployeeModelAsync(EmployeeModel model, Employee employee, bool excludeProperties = false);
}