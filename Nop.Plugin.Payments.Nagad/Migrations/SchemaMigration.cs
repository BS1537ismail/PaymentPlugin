using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Payments.Nagad.Domain;

namespace Nop.Plugin.Payments.Nagad.Migrations;
[NopMigration("2024/06/21 08:20:55:1687541", "Nagad.PaymentInfo base schema", MigrationProcessType.Installation)]
public class SchemaMigration : Migration
{
    public override void Up()
    {
        Create.TableFor<PaymentInfo>();
       
    }
    public override void Down()
    {
        Delete.Table(nameof(PaymentInfo));
    }
}
