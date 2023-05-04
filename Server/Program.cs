using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Data;
using Server.Models;
using System.Net.Sockets;
using System.Text.Json.Serialization;
using System.Xml.Linq;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DockerConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<IquraWordsDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<MyIdentityDbContext>(option =>
    option.UseNpgsql(connectionString));

builder.Services.AddDbContext<WordsDbContext>(options =>
    options.UseNpgsql(connectionString.Replace("IquraWords", "IquraWords_Admin")));


builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MyIdentityDbContext>().AddDefaultTokenProviders();

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
builder.Services.AddCors(options =>
{
    options.AddPolicy("corsapp", builder =>
    {
        builder.WithOrigins("http://localhost:3000", "https://localhost")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("corsapp");
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corsapp");
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

app.MapFallback(async context =>
{
    context.Response.StatusCode = 404;
    await context.Response.WriteAsync("Oops! The page you are looking for cannot be found.");
});

app.Run();


