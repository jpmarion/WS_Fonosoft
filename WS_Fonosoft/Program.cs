using WS_Fonosoft.Src.Auth.Infraestructura.Interface;
using WS_Fonosoft.Src.Auth.Infraestructura;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.Auth.Dominio.Interface;
using CompartidoPE.Modelo;
using WS_Fonosoft.Src.Auth.Dominio.Entidades;
using CifradoPE.Infraestructura.Interface;
using CifradoPE.Infraestructura;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WS_Fonosoft.Src.ObraSocial.Dominio.Entidades;
using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;
using WS_Fonosoft.Src.ObraSocial.Infraestructura;
using WS_Fonosoft.Src.ObraSocial.Infraestructura.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
        ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]))
    };
});

#region SWAGGER
var securityScheme = new OpenApiSecurityScheme()
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JSON Web Token based securit"
};

var securityReq = new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }
};

var contact = new OpenApiContact()
{
    Name = "Juan Pablo Marión",
    Email = "totomarion@gmail.com"
    //Url = new Uri("http://www.mohamadlawand.com")
};

var license = new OpenApiLicense()
{
    Name = "Free License"
    //Url = new Uri("http://www.mohamadlawand.com")
};

var info = new OpenApiInfo()
{
    Version = "v1",
    Title = "API WS_Fonosoft",
    Description = "API para FonosoftPWA",
    //TermsOfService = new Uri("http://www.example.com"),
    Contact = contact,
    License = license
};

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", info);
    o.AddSecurityDefinition("Bearer", securityScheme);
    o.AddSecurityRequirement(securityReq);
});

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
#endregion


builder.Services.AddScoped<IUsuario, Usuario>();
builder.Services.AddScoped<IObraSocial, ObraSocial>();
builder.Services.AddScoped<IResponse<IUsuario>, Response<IUsuario>>();
builder.Services.AddScoped<IResponse<IObraSocial>, Response<IObraSocial>>();
builder.Services.AddScoped<IError, Error>();
builder.Services.AddScoped<IResponse<IError>, Response<IError>>();



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
builder.Services.AddScoped<IMysqlRepositorioAuth>(_ => new FonosoftAuthRepo(serverMySql, userMySql, databaseMysql, portMysql, passwordMysql));
builder.Services.AddScoped<IMysqlRepositorioObraSocial>(_ => new FonosoftObraSocialRepo(serverMySql, userMySql, databaseMysql, portMysql, passwordMysql));
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
#region CORS
var MyAllowSpecificOrigins = "_MyAllowSubdomainPolicy";
var origenes = builder.Configuration["CORS:MyAllowSpecificOrigins"];

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins(origenes)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
//app.UseAuthentication();

app.MapControllers();


app.Run();
