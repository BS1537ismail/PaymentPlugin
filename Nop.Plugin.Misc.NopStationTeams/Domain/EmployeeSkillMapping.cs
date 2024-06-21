using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;

namespace Nop.Plugin.Misc.NopStationTeams.Domain;
public class EmployeeSkillMapping : BaseEntity
{
    public int EmployeeId { get; set; }
    public int SkillId { get; set; }
}
