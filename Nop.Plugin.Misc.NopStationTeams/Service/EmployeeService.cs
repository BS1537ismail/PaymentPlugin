using Nop.Core;
using Nop.Data;
using Nop.Plugin.Misc.NopStationTeams.Domain;

namespace Nop.Plugin.Misc.NopStationTeams.Service;
public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee> _employeeRepository;
   
    public EmployeeService(IRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
      
    }

    public virtual async Task InsertEmployeeAsync(Employee employee)
    {
        await _employeeRepository.InsertAsync(employee);
    }

    public virtual async Task UpdateEmployeeAsync(Employee employee)
    {
        await _employeeRepository.UpdateAsync(employee);
    }

    public virtual async Task DeleteEmployeeAsync(Employee employee)
    {
        await _employeeRepository.DeleteAsync(employee);
    }

    public virtual async Task<Employee> GetEmployeeByIdAsync(int employeeId)
    {
        return await _employeeRepository.GetByIdAsync(employeeId);
    }

    public virtual async Task<IPagedList<Employee>> SearchEmployeesAsync(string name, int statusId,
        int pageIndex = 0, int pageSize = int.MaxValue)
    {
        var query = from e in _employeeRepository.Table
                    select e;

        if (!string.IsNullOrEmpty(name))
            query = query.Where(e => e.Name.Contains(name));

        if (statusId > 0)
        {
            query = query.Where(e => e.EmployeeStatusId == statusId);
        }

        query = query.OrderBy(e => e.Name);

        return await query.ToPagedListAsync(pageIndex, pageSize);
    }


    

}
