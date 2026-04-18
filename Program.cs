using Microsoft.EntityFrameworkCore;
using WebProjectAPIs.Database;
using WebProjectAPIs.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerAuth();
builder.Services.AddDbContext<DB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConString")));

// Configure JWT authentication -------------------------------------------------------------------------------------------------------------
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSetting:SecretKey"] ?? string.Empty)),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSetting:Issuer"] ?? string.Empty,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSetting:Audience"] ?? string.Empty,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });
// ------------------------------------------------------------------------------------------------------------------------------------------

builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<AuthServices>();
var app = builder.Build();
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html");
        return;
    }
    await next();
});
if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
