using Microsoft.Extensions.Configuration;
using SkymeyLibs;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Configuration.GetSection("ConfigPath").Value)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();
builder.Services.Configure<MainSettings>(builder.Configuration.GetSection("MainSettings"));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();


app.UseAuthorization();


app.MapControllers();


app.Run();
