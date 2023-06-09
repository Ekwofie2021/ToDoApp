using FluentValidation;
using FluentValidation.AspNetCore;
using ToDoApp.Models;
using ToDoApp.Repository;
using ToDoApp.Services;
using ToDoApp.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
  .AddNewtonsoftJson()
 .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ToDoDataRequestValidator>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IToDoRepository, ToDoRepository>();
builder.Services.AddSingleton<IToDoService, ToDoService>();
builder.Services.AddScoped<IValidator<ToDoDataRequest>, ToDoDataRequestValidator>();

var app = builder.Build();

app.UseCors(opt => opt.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());
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
