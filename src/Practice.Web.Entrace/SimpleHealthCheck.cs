using Microsoft.Extensions.Diagnostics.HealthChecks;
using Practice.Web.Core.Interfaces;

namespace Omniway.Web.App;

public class SimpleHealthCheck : IHealthCheck
{
    private readonly IHealthCheckable[] _healthCheckServices;
    
    public SimpleHealthCheck(IEnumerable<IHealthCheckable> healthChecks)
    {
        _healthCheckServices = healthChecks.ToArray();
    }
    
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var healthCheckService in _healthCheckServices)
        {
            if (!await healthCheckService.Check(cancellationToken))
            {
                return HealthCheckResult.Unhealthy("Check failed");
            }
        }
        
        return HealthCheckResult.Healthy("Check healthy");
    }
}