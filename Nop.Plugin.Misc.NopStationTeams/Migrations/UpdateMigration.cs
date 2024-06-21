using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.NopStationTeams.Domain;

namespace Nop.Plugin.Misc.NopStationTeams.Migrations;
[NopSchemaMigration("2024/06/06 08:20:55:1687541", "Nopstation.Employee update migration", MigrationProcessType.Update)]
public class UpdateMigration : Migration
{
    public override void Up()
    {
        Create.TableFor<EmployeeSkillMapping>();
    }

    public override void Down()
    {
        Delete.Table(nameof(EmployeeSkillMapping));
    }
}
