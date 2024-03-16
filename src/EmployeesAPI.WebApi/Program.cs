using FluentValidation;
using FluentValidation.AspNetCore;
using EmployeesAPI.WebApi;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddJsonFile("appsettings.local.json", optional: true)
    .AddEnvironmentVariables();

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddSwaggerGen();

builder.Services
    .Configurate(builder.Configuration)
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
    .AddControllers();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("employees", p =>
    {
        p.WithOrigins(
            "http://localhost:3000",
            "http://localhost:3001")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("employees");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
