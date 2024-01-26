using BrightStarMovies.Application.Dtos;
using BrightStarMovies.Application.Extensions;
using BrightStarMovies.Infrastructure.Contexts;
using BrightStarMovies.Infrastructure.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    // Other options can be set here
});
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<MovieDto>().AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddApplicationService();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MigrateDatabase<AppDbContext>((context, services) =>
{
    var logger = services.GetService<ILogger<AppDbContext>>();

});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
