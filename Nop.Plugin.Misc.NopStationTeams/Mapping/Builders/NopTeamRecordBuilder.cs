using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.NopStationTeams.Domain;

namespace Nop.Plugin.Misc.NopStationTeams.Mapping.Builders;
public class NopTeamRecordBuilder : NopEntityBuilder<Employee>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table.WithColumn(nameof(Employee.Name)).AsString(100).NotNullable()
             .WithColumn(nameof(Employee.Designation)).AsString(100).NotNullable()
             .WithColumn(nameof(Employee.PictureId)).AsInt32().NotNullable()
             .WithColumn(nameof(Employee.IsMVP)).AsBoolean()
             .WithColumn(nameof(Employee.IsNopCommerceCertified)).AsBoolean();

    }
}
