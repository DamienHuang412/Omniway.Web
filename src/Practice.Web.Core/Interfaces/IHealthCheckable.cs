namespace Practice.Web.Core.Interfaces;

public interface IHealthCheckable
{
    Task<bool> Check(CancellationToken cancellationToken);
}