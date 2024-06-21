using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.NopStationTeams.Domain;

namespace Nop.Plugin.Misc.NopStationTeams.Mapping.Builders;
public class SkillBuilder: NopEntityBuilder<EmployeeSkill>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table.WithColumn(nameof(EmployeeSkill.SkillName)).AsString(100).NotNullable();
             

    }
}
