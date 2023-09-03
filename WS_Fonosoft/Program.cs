using WS_Fonosoft.Src.Auth.Infraestructura.Interface;
using WS_Fonosoft.Src.Auth.Infraestructura;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.Auth.Dominio.Interface;
using CompartidoPE.Modelo;
using WS_Fonosoft.Src.Auth.Dominio.Entidades;
using CifradoPE.Infraestructura.Interface;
using CifradoPE.Infraestructura;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsuario, Usuario>();
builder.Services.AddScoped<IResponse<IUsuario>, Response<IUsuario>>();

#region MYSQL
ConfigurationBuilder builderConfig = new ConfigurationBuilder();
builderConfig.SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", false);
IConfigurationRoot configuration = builderConfig.Build();
//ConnectionStrings connectionStrings = new ConnectionStrings();
//IConfigurationSection configurationSection = configuration.GetSection("ConnectionStrings");
//configurationSection.Bind(connectionStrings);

string serverMySql = configuration.GetValue<string>("ConnectionStrings:ServerMysql");
string userMySql = configuration.GetValue<string>("ConnectionStrings:UserMysql");
string databaseMysql = configuration.GetValue<string>("ConnectionStrings:DatabaseMysql");
string portMysql = configuration.GetValue<string>("ConnectionStrings:PortMySql");
string passwordMysql = configuration.GetValue<string>("ConnectionStrings:PasswordMysql");
builder.Services.AddScoped<IMysqlRepositorio>(_ => new FonosoftAuthRepo(serverMySql, userMySql, databaseMysql, portMysql, passwordMysql));
#endregion
#region ENCRIPTACION
string key = configuration.GetValue<string>("Encriptar:Key");
string iv = configuration.GetValue<string>("Encriptar:iv");
builder.Services.AddScoped<IAes>(_ => new AesAsimetrico(key, iv));
#endregion
#region EMAIL
string host = configuration.GetValue<string>("Email:host");
int port = configuration.GetValue<int>("Email:port");
string usuario = configuration.GetValue<string>("Email:usuario");
string contrasenia = configuration.GetValue<string>("Email:contrasenia");
bool habilitaSSL = configuration.GetValue<bool>("Email:habilitaSSL");
builder.Services.AddScoped<IEmailRepo>(_ => new EmailRepo(host, port, usuario, contrasenia, habilitaSSL));
#endregion

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
