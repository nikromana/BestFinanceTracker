using BestFinanceTracker.Application.Features.Categories.Commands.CreateCategory;
using BestFinanceTracker.Application.Interfaces;
using BestFinanceTracker.Infrastructure.Data;
using BestFinanceTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString!));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCategoryCommand).Assembly));
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
