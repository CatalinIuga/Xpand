using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Xpand.Api.Data;
using Xpand.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<FileService>();

// I love cors. :D
builder
    .Services
    .AddCors(options =>
    {
        options.AddPolicy(
            "CORS",
            builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
        );
    });

builder
    .Services
    .AddDbContext<XpandDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"))
    );

builder
    .Services
    .AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition(
            "Bearer",
            new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description =
                    "WARNING! Add the Bearer Authorization string as following: `Bearer <generated-JWT-token>`. For testing, use the signin endpoint to get a token. Credentials: test / test.",
            }
        );
        options.AddSecurityRequirement(
            new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            }
        );
        options.SwaggerDoc("v1", new() { Title = "Xpand Api", Version = "v1" });
        options.UseInlineDefinitionsForEnums();
    });

// JWT Authentication
builder
    .Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(
        options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // For production, set to true.
                ValidateAudience = false, // For production, set to true.
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)
                ),
                ClockSkew = TimeSpan.Zero
            }
    );

builder.Services.AddAuthorization();

// This makes enums serialize as strings instead of integers.
builder
    .Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CORS");

// Serving static files from the Uploads folder.
app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "Uploads")
        ),
        RequestPath = "/static"
    }
);

app.MapControllers();

app.Run();
