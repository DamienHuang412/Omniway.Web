using Omniway.Web.Core.Interfaces;

namespace Omniway.Web.App.Hosting;

public class PrePreparationHostedService : IHostedService
{
    private readonly IDataInitialService _dataInitialService;
    
    public PrePreparationHostedService(IDataInitialService dataInitialService)
    {
        _dataInitialService = dataInitialService;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _dataInitialService.Initialize(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}