using Ardalis.ListStartupServices;
using Modulith.Persistence.Interceptors;
using Modulith.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<AuditableEntityInterceptor>();

builder.Services.Configure<ServiceConfig>(config => config.Services = [.. builder.Services]);

builder.AddRateLimiting();

builder.AddModules();

builder.AddCustomCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseShowAllServicesMiddleware();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseCustomCors();

app.UseRateLimiter();

app.UseInfrastructureService();

app.UseHttpsRedirection();

app.MapSpecialEndpoints();

app.Run();

public partial class Program
{
    protected Program()
    {
    }
}