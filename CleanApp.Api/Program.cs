using AddBus;
using App.Application.Contracts.Caching;
using App.Application.Extensions;
using App.Caching;
using App.Persistance.Extensions;
using CleanApp.Api.ExceptionHandler;
using CleanApp.Api.Extension;
using CleanApp.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle




builder.Services.AddControllerWithFilters().AddSwaggerGen().AddExceptionHandler().AddCaching()
.AddBussExt(builder.Configuration);


builder.Services.AddRepository(builder.Configuration).AddServices(builder.Configuration);

var app = builder.Build();

app.UseConfigurePipeLine();


app.MapControllers();

app.Run();
