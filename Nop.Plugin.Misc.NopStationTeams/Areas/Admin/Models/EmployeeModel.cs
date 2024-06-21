using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Models;
public record EmployeeModel : BaseNopEntityModel
{
    public EmployeeModel()
    {
        AvailableEmployeeStatusOptions = new List<SelectListItem>();
        AvailableEmployeeSkillOptions = new List<SelectListItem>();
        SelectedSkills = new List<int>();
        Skills = new List<string>();
    }

    [NopResourceDisplayName("Admin.Misc.Employee.Fields.Skills")]
    public IList<int> SelectedSkills { get; set; }
    [NopResourceDisplayName("Admin.Misc.Employee.Fields.Skills")]
    public List<string> Skills { get; set; }

    [NopResourceDisplayName("Admin.Misc.Employee.Fields.Name")]
    public string Name { get; set; }
    [NopResourceDisplayName("Admin.Misc.Employee.Fields.Designation")]
    public string Designation { get; set; }
    //[NopResourceDisplayName("Admin.Misc.Employee.Fields.skills")]
    //public string skills { get; set; }
    [NopResourceDisplayName("Admin.Misc.Employee.Fields.PictureThumbnailUrl")]
    public string PictureThumbnailUrl { get; set; }

    [UIHint("Picture")]
    [NopResourceDisplayName("Admin.Misc.Employee.Fields.Picture")]
    public int PictureId { get; set; }
    [NopResourceDisplayName("Admin.Misc.Employee.Fields.IsMVP")]
    public bool IsMVP { get; set; }
    [NopResourceDisplayName("Admin.Misc.Employee.Fields.IsNopCommerceCertified")]
    public bool IsNopCommerceCertified { get; set; }
    [NopResourceDisplayName("Admin.Misc.Employee.Fields.EmployeeStatus")]
    public int EmployeeStatusId { get; set; }
    [NopResourceDisplayName("Admin.Misc.Employee.Fields.EmployeeStatus")]
    public string EmployeeStatusStr { get; set; }
    public IList<SelectListItem> AvailableEmployeeStatusOptions { get; set; }

    public IList<SelectListItem> AvailableEmployeeSkillOptions { get; set; }
}
