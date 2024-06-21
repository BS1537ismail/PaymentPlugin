using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using FluentValidation;
using Nop.Plugin.Misc.NopStationTeams.Domain;
using Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Models;

namespace Nop.Plugin.Misc.NopStationTeams.Validator
{
    public partial class EmployeeValidator : BaseNopValidator<EmployeeModel>
    {
        public EmployeeValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Admin.Misc.Employee.Fields.Name.Required"));
            RuleFor(x => x.Designation).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Admin.Misc.Employee.Fields.Designation.Required"));

            SetDatabaseValidationRules<Employee>();
        }
    }
}