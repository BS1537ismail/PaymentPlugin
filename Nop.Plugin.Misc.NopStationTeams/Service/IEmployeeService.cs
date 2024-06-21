using Nop.Core;
using Nop.Plugin.Misc.NopStationTeams.Domain;

namespace Nop.Plugin.Misc.NopStationTeams.Service;
public interface IEmployeeService
{
    Task InsertEmployeeAsync(Employee employee);

    Task UpdateEmployeeAsync(Employee employee);

    Task DeleteEmployeeAsync(Employee employee);

    Task<Employee> GetEmployeeByIdAsync(int employeeId);

    Task<IPagedList<Employee>> SearchEmployeesAsync(string name, int statusId,
            int pageIndex = 0, int pageSize = int.MaxValue);

    
   
}
