public class AlbumCacheRefresher : BackgroundService
{
    private readonly IServiceProvider _provider;

    public AlbumCacheRefresher(IServiceProvider provider)
    {
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _provider.CreateScope();
            var cache = scope.ServiceProvider.GetRequiredService<AlbumCacheService>();

            cache.Preload();

            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
        }
    }
}

