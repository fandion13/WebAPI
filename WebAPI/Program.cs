using System.Data.SqlClient;
using System.Data;
using WebAPI.Camadas.Dados;
using WebAPI.Camadas.Negocios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Adicione suas camadas aqui:
builder.Services.AddScoped<DTask>();
builder.Services.AddScoped<NTask>();

// Exemplo de configura��o da string de conex�o com o banco de dados
//string connectionString = builder.Configuration.GetConnectionString("BancoSQL");
//builder.Services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));

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
