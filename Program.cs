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
            // 捕获和输出接收到的令牌
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            Console.WriteLine($"Received Token: {token}");

            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            // 当身份验证失败时触发
            Console.WriteLine("Authentication failed:");
            Console.WriteLine($"Exception: {context.Exception.Message}");

            if (context.Exception is SecurityTokenException tokenException)
            {
                Console.WriteLine($"Token exception: {tokenException.Message}");
            }

            // 打印完整的异常堆栈跟踪
            Console.WriteLine(context.Exception.ToString());

            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            // 当令牌验证成功时触发
            Console.WriteLine("Token validated successfully");

            // 输出一些关于令牌的信息
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
            // 当身份验证被挑战时（即未能通过身份验证）触发
            Console.WriteLine("Token validation challenge:");
            Console.WriteLine($"Error: {context.Error}");
            Console.WriteLine($"ErrorDescription: {context.ErrorDescription}");

            // 详细描述挑战的原因
            if (context.AuthenticateFailure != null)
            {
                Console.WriteLine($"AuthenticateFailure: {context.AuthenticateFailure.Message}");
            }

            // 允许自定义响应或取消默认响应行为
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

var app = builder.Build();

app.UseStaticFiles();
// setup static file
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), uploadFolderPath)),
    RequestPath = "/File/Image"
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
