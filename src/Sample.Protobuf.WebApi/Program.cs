using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.Protobuf.WebApi.Core.Extensions;
using Sample.Protobuf.WebApi.Core.Middleware;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.AddSerilog(builder.Configuration, "API Protobuf");
    Log.Information("Getting the motors running...");

    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    builder.Services.AddSwaggerApi(builder.Configuration);
    builder.Services.AddControllers();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseSwaggerDocApi();
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}