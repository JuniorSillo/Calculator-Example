using System.Reflection;
using CalculatorDomain.Logic;
using CalculatorDomain.Persistence;
using CalculatorDomainDemo.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Setup Data directory
var dataDirectory = Path.Combine(builder.Environment.ContentRootPath, "Data");
builder.Services.AddSingleton<ICalculationStore>(new FileCalculationStore(dataDirectory));

// Configure DbContext with SQLite
builder.Services.AddDbContext<CalculationDBContext>(options =>
    options.UseSqlite("Data Source=calculations.db")
);

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CalculationDBContext>()
    .AddDefaultTokenProviders();

// Add controllers and services
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<CalculatorService>();
builder.Services.AddAuthentication(options =>{
options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection("Jwt");
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwt["Key"]))
        };
    });
var app = builder.Build();

using (var scope = app.Services.CreateAsyncScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await IdentitySeeder.SeedAsync(userManager,roleManager);
}

app.UseAuthentication();
app.UseAuthorization();
// Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Enable Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Configure Swagger for development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// Run the app
app.Run();