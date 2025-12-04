using Comandas.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ComandasDbContext>(options =>
    options.UseSqlite("DataSource=comandas.db")
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// criar o banco de dados
// criar um escopo usado para obter instancias de variaveis
using (var scope = app.Services.CreateScope())
{
    // obtem um objeto do banco de dados
    var db = scope.ServiceProvider.GetRequiredService<ComandasDbContext>();
    // executa as migrations no banco de dados
    await db.Database.MigrateAsync();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
