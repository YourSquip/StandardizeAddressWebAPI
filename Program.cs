using Microsoft.Extensions.DependencyInjection;
using Dadata;
using Dadata.Model;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using StandardizeAddressWebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//------
builder.Services.AddCors();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHttpClient<Service>(client =>
{
    client.BaseAddress = new Uri("https://cleaner.dadata.ru/api/v1/clean/address");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder.AllowAnyOrigin());

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseRouting();

app.Run();
