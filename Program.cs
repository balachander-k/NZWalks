using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injecting the NZWalksDbContext into the application
builder.Services.AddDbContext<NZWalksDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionStrings")));

//Injecting the NZWalksAuthDbContext into the application
builder.Services.AddDbContext<NZWalksAuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksAuthConnectionStrings")));


builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();

//AutoMapper Configuration (Injecting to Services)
builder.Services.AddAutoMapper(typeof(AutoMappingProfiles));

//Registering Identity Services
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    .AddEntityFrameworkStores<NZWalksAuthDbContext>()
    .AddDefaultTokenProviders();

//Configuring Password Policiies 
builder.Services.Configure<IdentityOptions>(Options=>
{
    Options.Password.RequireDigit = false;  //If you want to require a digit in the password, set this to true (Rquirees at least one digit from 0-9)
    Options.Password.RequireLowercase = false; // If you want to require a lowercase letter in the password, set this to true (Requires at least one lowercase letter from a-z)
    Options.Password.RequireNonAlphanumeric = false; // If you want to require a non-alphanumeric character in the password, set this to true (Requires at least one non-alphanumeric character)
    Options.Password.RequireUppercase = false;// If you want to require an uppercase letter in the password, set this to true (Requires at least one uppercase letter from A-Z)
    Options.Password.RequiredLength = 6; // Minimum password length
    Options.Password.RequiredUniqueChars = 1; // Requires a unique character in the password
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });
 
  
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
