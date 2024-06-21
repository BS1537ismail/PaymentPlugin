using Nop.Plugin.Misc.NopStationTeams.Domain;
using Nop.Web.Framework.Models;
using Nop.Web.Models.Media;

namespace Nop.Plugin.Misc.NopStationTeams.Models;

public record EmployeeModel : BaseNopEntityModel
{
    public EmployeeModel()
    {
        Skills = new List<string>();
        Picture = new PictureModel();
    }

    public IList<string> Skills { get; set; }

    public string Name { get; set; }

    public string Designation { get; set; }

    public PictureModel Picture { get; set; }
    
    public bool IsMVP { get; set; }
    
    public bool IsNopCommerceCertified { get; set; }

    public EmployeeStatus EmployeeStatus { get; set; }
    
    public string EmployeeStatusStr { get; set; }
}
