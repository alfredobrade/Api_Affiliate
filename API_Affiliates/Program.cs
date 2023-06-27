
using API_Affiliates.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;



using API_Affiliates.ServiceInterfaces;
using API_Affiliates.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using API_Affiliates.Repository;

var builder = WebApplication.CreateBuilder(args);

//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var corsSettings = builder.Configuration.GetSection("CorsSettings").Get<CorsSettings>();
builder.Services.Configure<CorsSettings>(builder.Configuration.GetSection("CorsSettings"));


builder.Services.AddCors(options => {
    options.AddPolicy("AllowOrigins",
    policy => {
        policy.WithOrigins(corsSettings.AllowedOrigins)
        .AllowAnyHeader().AllowAnyMethod();
    });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Inyections
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAffiliateService, AffilliateService>();


//DB connection
builder.Services.AddSqlServer<ProjectDbContext>(builder.Configuration.GetConnectionString("DbConnection"));


var key = builder.Configuration.GetValue<string>("JwtSettings:key");
var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,

    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//adding CORS
app.UseCors("AllowOrigins");

app.UseHttpsRedirection();

//adding authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
