using Limon.Hive.E.Bazar.Application;
using Limon.Hive.E.Bazar.Infrastractures;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Appointment Management API",
        Description = "Appointment Management API",
    });
});

builder.Services.AddDbContext<LimonHiveDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("LimonHiveDbConnection"));
    if (builder.Environment.IsDevelopment())
    {
        o.EnableDetailedErrors();
        o.EnableSensitiveDataLogging();
    }
});

builder.Services.AddScoped<ILimonHiveDbContext, LimonHiveDbContext>();

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AppoinmentCreateHandler).GetTypeInfo().Assembly));
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DoctorQueryHandler).GetTypeInfo().Assembly));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

