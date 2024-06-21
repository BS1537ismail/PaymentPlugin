using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Models;
public record EmployeeSkillModel : BaseNopEntityModel
{

    [NopResourceDisplayName("Admin.Misc.Employee.Fields.SkillName")]
    public string SkillName { get; set; }

}
