using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Models;
public record EmployeeSkillSearchModel : BaseSearchModel
{
    public EmployeeSkillSearchModel()
    {
        AvailableEmployeeStatusOptions = new List<SelectListItem>();
    }
    [NopResourceDisplayName("Admin.Misc.Skill.List.SkillName")]
    public string SkillName { get; set; }
    public IList<SelectListItem> AvailableEmployeeStatusOptions { get; set; }
}
