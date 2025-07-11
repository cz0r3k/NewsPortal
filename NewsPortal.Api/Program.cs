using System.Text.Json.Serialization;
using NewsPortal.Application;
using NewsPortal.Domain.Services;
using NewsPortal.Infrastructure.Context;
using NewsPortal.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigurePersistence();
builder.Services.ConfigureApplication();
builder.Services.AddScoped<IArticleStatsService, ArticleStatsService>();

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var serviceScope = app.Services.CreateScope();
var dataContext = serviceScope.ServiceProvider.GetService<NewsContext>();
dataContext?.Database.EnsureCreated();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();