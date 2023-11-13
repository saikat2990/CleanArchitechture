using CleanArchitechture.Api.Middlewares;
using CleanArchitechture.API.ServiceRegister;
using CleanArchitechture.Core.Utilities;
using CleanArchitechture.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog.Events;
using Serilog;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Logs");
var filePath = pathBuilt + $"\\log{DateTime.UtcNow.ToString("yyyy-MM-dd")}.txt";

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(filePath)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
    .CreateLogger();

Log.Information("Application starting...");

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.Converters.Add(new StringEnumConverter
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    });
    o.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
    o.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
}).ConfigureApiBehaviorOptions(o =>
{
    o.InvalidModelStateResponseFactory = context =>
    {
        var result = new Utility().GetErrorResponse(context.ModelState.Values.SelectMany(x => x.Errors), "Bad Request");
        return new BadRequestObjectResult(result);
    };
});

builder.Services.RegisterDependency();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
builder.AddIdentity();
builder.AddAuthentication();
builder.RegisterCors();
builder.RegisterSwagger();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

await app.ExecuteScopedActions();

await app.DataSeeding();

app.UseCors("CorsPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseGlobalErrorHandlingMiddleware();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();