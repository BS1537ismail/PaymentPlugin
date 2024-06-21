using Nop.Plugin.Misc.NopStationTeams.Models;
using Nop.Plugin.Misc.NopStationTeams.Domain;

namespace Nop.Plugin.Misc.NopStationTeams.Factories;

public interface IEmployeeModelFactory
{
    Task<IList<EmployeeModel>> PrepareEmployeeListModel(IList<Employee> employees, string widgetZone, int employeeId);

    Task<EmployeeModel> PrepareEmployeeModel(Employee employee);
}
