using ProductCrudApplication.Data;
using Microsoft.EntityFrameworkCore;
using ProductCrudApplication.Repository;
using ProductCrudApplication.Service;
using Microsoft.Extensions.Options;
using System.Security.AccessControl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
//builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var dbConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
//Database
builder.Services.AddDbContext<AppDbContext>(o => 
            o.UseMySql(dbConnectionStr, ServerVersion.AutoDetect(dbConnectionStr)));

builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IProductService,ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }