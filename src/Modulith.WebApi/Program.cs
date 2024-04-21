using Ardalis.ListStartupServices;
using Modulith.Infrastructure.RateLimiter;
using Modulith.Persistence.Interceptors;
using Modulith.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<AuditableEntityInterceptor>();

builder.Services.Configure<ServiceConfig>(config => config.Services = [.. builder.Services]);

builder.Services.AddInfrastructureService(builder);

builder.AddRateLimiting();

builder.AddModuleServices();

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

app.MapEndpoints();

app.MapSpecialEndpoints();

app.Run();

public partial class Program
{
    protected Program()
    {
    }
}