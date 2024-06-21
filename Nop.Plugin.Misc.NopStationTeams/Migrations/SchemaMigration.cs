using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.NopStationTeams.Domain;

namespace Nop.Plugin.Misc.NopStationTeams.Migrations
{
    [NopMigration("2024/05/23 08:20:55:1687541", "NopStationTeams.NopTeamModel base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : Migration
    {
        public override void Up()
        {
            Create.TableFor<Employee>();
            Create.TableFor<EmployeeSkill>();
        }

        public override void Down()
        {
            Delete.Table(nameof(Employee));
            Delete.Table(nameof(EmployeeSkill));
           
        }
    }
}
