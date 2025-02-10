using Microsoft.OpenApi.Models;
using CustomerManagement.Domain.Interfaces;
using CustomerManagement.Infrastructure.Repositories;
using MediatR;
using CustomerManagement.Application.Commands;
using CustomerManagement.Application.Queries;
using System.Reflection;
using CustomerManagement.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer Management API", Version = "v1" });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.AddScoped<ICustomerRepository>(provider =>
    new CustomerRepository(connectionString));

builder.Services.AddScoped<IRequestHandler<CreateCustomerCommand, int>, CreateCustomerHandler>();
builder.Services.AddScoped<IRequestHandler<GetCustomerByIdQuery, Customer>, GetCustomerByIdHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Management API v1"));
}

app.UseAuthorization();
app.MapControllers();
app.Run();