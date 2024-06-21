using Nop.Core;
using Nop.Plugin.Misc.NopStationTeams.Domain;

namespace Nop.Plugin.Misc.NopStationTeams.Service;
public interface IEmployeeSkillService
{
    Task InsertEmployeeAsync(EmployeeSkill skill);

    Task UpdateEmployeeAsync(EmployeeSkill skill);

    Task DeleteEmployeeAsync(EmployeeSkill skill);

    Task<EmployeeSkill> GetEmployeeByIdAsync(int skillId);

    Task<IPagedList<EmployeeSkill>> SearchEmployeeSkillAsync(string name,
            int pageIndex = 0, int pageSize = int.MaxValue);

    Task<IList<EmployeeSkill>> GetSkillByIdsAsync(int[] skillIds);

    Task InsertDeveloperSkillMappingAsync(EmployeeSkillMapping developerSkillMapping);

    Task UpdateDeveloperSkillMappingAsync(EmployeeSkillMapping developerSkillMapping);

    Task DeleteDeveloperSkillMappingAsync(EmployeeSkillMapping developerSkillMapping);

    Task<IList<EmployeeSkillMapping>> GetDeveloperSkillMappingsByDeveloperIdAsync(int developerId);

    Task<EmployeeSkillMapping> GetDeveloperSkillMappingByIdAsync(int developerSkillMappingId);

    Task<EmployeeSkillMapping> FindDeveloperSkillMappingAsync(int developerId, int skillId);

    Task<IList<EmployeeSkill>> GetAllSkillsAsync();
}
