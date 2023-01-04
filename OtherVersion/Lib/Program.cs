using Microsoft.EntityFrameworkCore;
using Lib.Models;
using Microsoft.AspNetCore.Identity;
using Lib;
using Lib.Configuration;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
 })
 .AddJwtBearer(jwt => {

    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, // this will validate the 3rd part of the jwt token using the secret that we added in the appsettings and verify we have generated the jwt token
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true
    };
 });









var connectionString = builder.Configuration.GetConnectionString("SqlDefault");

builder.Services.AddDbContext<LibContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddSingleton<IEncDec , encryption>();









// var key = Encoding.ASCII.GetBytes("MY_BIG_SECRET_KEY_LKSHDJFLSDKFW@#($)(#)32234");




builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();



builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
    builder
    .WithMethods("GET", "POST")
    .AllowAnyHeader()
    .AllowAnyOrigin();
}));


builder.Services.AddAuthorization(b => b.AddPolicy("Policy", options=>options.RequireClaim("UserId")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();


/*new Seeder(app.Services.GetService<LibContext>())
    .Seedbook();

new Seeder(app.Services.GetService<LibContext>())
    .SeedBookShelfs();
new Seeder(app.Services.GetService<LibContext>())
    .SeedUser();
new Seeder(app.Services.GetService<LibContext>())
    .SeedShelf();*/
/*
public class DBSeeder
{
    protected LibContext LibContext { get; set; }
    public DBSeeder(LibContext Lib)
    {
        LibContext = Lib;
    }

    public DBSeeder SeedStudents()
    {
        return this;
    }


    public DBSeeder SeedLibrary()
    {
        return this;
    }
}*/

