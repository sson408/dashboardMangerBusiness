using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using dashboardManger.Data;
using dashboardManger.DTOs;
using dashboardManger.Extensions;
using System.Security.Claims;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

// setup CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader() 
            .AllowCredentials();
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{ 
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = false,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            //get token from header
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            Console.WriteLine($"Received Token: {token}");

            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            //if authentication failed
            Console.WriteLine("Authentication failed:");
            Console.WriteLine($"Exception: {context.Exception.Message}");

            if (context.Exception is SecurityTokenException tokenException)
            {
                Console.WriteLine($"Token exception: {tokenException.Message}");
            }

 
            Console.WriteLine(context.Exception.ToString());

            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            //if token is validated
            Console.WriteLine("Token validated successfully");

            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            Console.WriteLine("User Claims:");
            foreach (var claim in claimsIdentity.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            //if token validation challenge
            Console.WriteLine("Token validation challenge:");
            Console.WriteLine($"Error: {context.Error}");
            Console.WriteLine($"ErrorDescription: {context.ErrorDescription}");

            if (context.AuthenticateFailure != null)
            {
                Console.WriteLine($"AuthenticateFailure: {context.AuthenticateFailure.Message}");
            }

            //we can handle the response here
            // context.HandleResponse();

            return Task.CompletedTask;
        }
    };


});

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// add auto mapper Program
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCustomServices();

var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "File", "Image");
var uoloadPropertyFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "File", "Property");

var app = builder.Build();

app.UseStaticFiles();
// setup static file
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), uploadFolderPath)),
    RequestPath = "/File/Image"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), uoloadPropertyFolderPath)),
    RequestPath = "/File/Property"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//start cors
app.UseCors();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
