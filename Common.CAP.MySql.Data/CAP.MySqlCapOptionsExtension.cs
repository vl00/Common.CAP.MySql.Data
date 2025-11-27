using System;
using DotNetCore.CAP.MySql;
using DotNetCore.CAP.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace DotNetCore.CAP;

internal class MySqlCapOptionsExtension : ICapOptionsExtension
{
    private readonly Action<MySqlOptions> _configure;

    public MySqlCapOptionsExtension(Action<MySqlOptions> configure)
    {
        _configure = configure;
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddSingleton(new CapStorageMarkerService("MySql"));
        services.AddSingleton<IDataStorage, MySqlDataStorage>();

        services.TryAddSingleton<IStorageInitializer, MySqlStorageInitializer>();

        //Add MySqlOptions
        services.Configure(_configure);
        services.AddSingleton<IConfigureOptions<MySqlOptions>, ConfigureMySqlOptions>();
    }
}