using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Data;
using Nop.Plugin.Misc.NopStationTeams.Domain;

namespace Nop.Plugin.Misc.NopStationTeams.Service;
public class EmployeeSkillService:IEmployeeSkillService
{
    private readonly IRepository<EmployeeSkill> _employeeRepository;
    private readonly IRepository<EmployeeSkillMapping> _developerSkillMappingRepository;
    public EmployeeSkillService(IRepository<EmployeeSkill> employeeRepository, IRepository<EmployeeSkillMapping> developerSkillMappingRepository)
    {
        _employeeRepository = employeeRepository;
        _developerSkillMappingRepository = developerSkillMappingRepository;
    }

    public virtual async Task InsertEmployeeAsync(EmployeeSkill skill)
    {
        await _employeeRepository.InsertAsync(skill);
    }

    public virtual async Task UpdateEmployeeAsync(EmployeeSkill skill)
    {
        await _employeeRepository.UpdateAsync(skill);
    }

    public virtual async Task DeleteEmployeeAsync(EmployeeSkill skill)
    {
        await _employeeRepository.DeleteAsync(skill);
    }

    public virtual async Task<EmployeeSkill> GetEmployeeByIdAsync(int skillId)
    {
        return await _employeeRepository.GetByIdAsync(skillId);
    }

    public virtual async Task<IPagedList<EmployeeSkill>> SearchEmployeeSkillAsync(string name,
        int pageIndex = 0, int pageSize = int.MaxValue)
    {
        var query = from e in _employeeRepository.Table
                    select e;

        if (!string.IsNullOrEmpty(name))
            query = query.Where(e => e.SkillName.Contains(name));

        query = query.OrderBy(e => e.SkillName);

        return await query.ToPagedListAsync(pageIndex, pageSize);
    }

    public virtual async Task<IList<EmployeeSkill>> GetSkillByIdsAsync(int[] skillIds)
    {
        return await _employeeRepository.GetByIdsAsync(skillIds);
    }




    public virtual async Task DeleteDeveloperSkillMappingAsync(EmployeeSkillMapping developerSkillMapping)
    {
        await _developerSkillMappingRepository.DeleteAsync(developerSkillMapping);
    }

    public virtual async Task<IList<EmployeeSkillMapping>> GetDeveloperSkillMappingsByDeveloperIdAsync(int employeeId)
    {
        return await _developerSkillMappingRepository.Table
            .Where(es => es.EmployeeId == employeeId)
            .ToListAsync();
    }

    public virtual async Task<EmployeeSkillMapping> GetDeveloperSkillMappingByIdAsync(int developerSkillMappingId)
    {
        return await _developerSkillMappingRepository.GetByIdAsync(developerSkillMappingId, cache => default);
    }

    public virtual async Task InsertDeveloperSkillMappingAsync(EmployeeSkillMapping developerSkillMapping)
    {
        await _developerSkillMappingRepository.InsertAsync(developerSkillMapping);
    }

    public virtual async Task UpdateDeveloperSkillMappingAsync(EmployeeSkillMapping developerSkillMapping)
    {
        await _developerSkillMappingRepository.UpdateAsync(developerSkillMapping);
    }

    public virtual async Task<EmployeeSkillMapping> FindDeveloperSkillMappingAsync(int developerId, int skillId)
    {
        return await _developerSkillMappingRepository.Table
            .FirstOrDefaultAsync(ds => ds.SkillId == skillId && ds.EmployeeId == developerId);
    }


    public async Task<IList<EmployeeSkill>> GetAllSkillsAsync()
    {
        return await _employeeRepository.Table.ToListAsync();
    }
}
