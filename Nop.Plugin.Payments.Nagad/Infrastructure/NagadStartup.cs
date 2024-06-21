using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Payments.Nagad.Service;

namespace Nop.Plugin.Payments.Nagad.Infrastructure;
public class NagadStartup : INopStartup
{
    public int Order => 1000;

    public void Configure(IApplicationBuilder application)
    {
        
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INagadPaymentService, NagadPaymentService>();
    }
}
