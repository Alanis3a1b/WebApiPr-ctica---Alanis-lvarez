using Microsoft.EntityFrameworkCore;
using WebApiPr�ctica___Alanis_�lvarez.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Inyecci�n por dependencia de string de conexi�n al contexto
builder.Services.AddDbContext<equiposContext>(options =>
         options.UseSqlServer(
             builder.Configuration.GetConnectionString("equiposDbConnection")
             ));

builder.Services.AddDbContext<marcasContext>(options =>
         options.UseSqlServer(
             builder.Configuration.GetConnectionString("equiposDbConnection")
             ));

builder.Services.AddDbContext<estados_equipoContext>(options =>
         options.UseSqlServer(
             builder.Configuration.GetConnectionString("equiposDbConnection")
             ));

builder.Services.AddDbContext<tipo_equipoContext>(options =>
         options.UseSqlServer(
             builder.Configuration.GetConnectionString("equiposDbConnection")
             ));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
