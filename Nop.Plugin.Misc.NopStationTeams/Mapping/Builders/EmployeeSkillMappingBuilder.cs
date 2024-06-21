using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Builders.Create.Table;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.NopStationTeams.Domain;

namespace Nop.Plugin.Misc.NopStationTeams.Mapping.Builders;
public class EmployeeSkillMappingBuilder : NopEntityBuilder<EmployeeSkillMapping>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table.WithColumn(nameof(EmployeeSkillMapping.EmployeeId)).AsInt32().ForeignKey<Employee>()
             .WithColumn(nameof(EmployeeSkillMapping.SkillId)).AsInt32().ForeignKey<EmployeeSkill>();
    }
}
