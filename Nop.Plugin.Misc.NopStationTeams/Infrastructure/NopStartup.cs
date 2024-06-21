using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.NopStationTeams.Areas.Admin.Factories;
using Nop.Plugin.Misc.NopStationTeams.Service;

namespace Nop.Plugin.Misc.NopStationTeams.Infrastructure;
public class NopStartup : INopStartup
{
    public int Order => 3000;
    public void Configure(IApplicationBuilder application)
    {
    }
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IEmployeeModelFactory, EmployeeModelFactory>();
        services.AddScoped<IEmployeeSkillService, EmployeeSkillService>();
        services.AddScoped<IEmployeeSkillModelFactory, EmployeeSkillModelFactory>();

        services.AddScoped<Factories.IEmployeeModelFactory, Factories.EmployeeModelFactory>();
    }
}
