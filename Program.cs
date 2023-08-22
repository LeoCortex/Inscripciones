using GestionLogistica.DTOs;
using InscripcionesApi.src.Core;
using InscripcionesApi.src.Data;
using InscripcionesApi.src.Data.Models;
using InscripcionesApi.src.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DbContext
var conectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlServer<InscripcionesContext>(conectionString);

//Mapper
builder.Services.AddAutoMapper(typeof(Automapping)); 

//Class
builder.Services.AddScoped(typeof(ICoreFactory<>), typeof(FactoryCore<>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
