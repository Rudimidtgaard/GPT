using GPT.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddLogging(configure => configure.AddDebug().AddConsole());
        services.AddHttpClient();
        services.AddScoped<IIntegrationService, GPTSample>();
    }).Build();

try
{

    var logger = host.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Host created");

    await host.Services.GetRequiredService<IIntegrationService>().RunAsync();
}
catch (Exception generalException)
{
    var logger = host.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(generalException, "An exception happened while running the integration service");
}

Console.ReadKey();