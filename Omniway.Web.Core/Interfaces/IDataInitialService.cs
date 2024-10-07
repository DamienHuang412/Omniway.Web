namespace Omniway.Web.Core.Interfaces;

public interface IDataInitialService
{
    Task Initialize(CancellationToken cancellationToken);
}