using PathPilot.Shared.Infrastructure;
using PathPilot.Shared.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureModules();

var assemblies = ModuleLoader.LoadAssemblies(builder.Configuration);
var modules = ModuleLoader.LoadModules(assemblies);

builder.Services.AddInfrastructure(assemblies, modules);

foreach (var module in modules)
{
    module.Register(builder.Services);
}

var app = builder.Build();

app.UseInfrastructure();

app.MapControllers();
app.MapModuleInfo();

app.Run();


namespace PathPilot.Bootstrapper
{
    public partial class Program { }
}