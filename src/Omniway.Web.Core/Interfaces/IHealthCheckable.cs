namespace Omniway.Web.Core.Interfaces;

public interface IHealthCheckable
{
    Task<bool> Check(CancellationToken cancellationToken);
}