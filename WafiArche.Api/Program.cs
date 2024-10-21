using Microsoft.EntityFrameworkCore;
using Serilog;
using WafiArche.Api.Conveerters;
using WafiArche.Application.Mappings;
using WafiArche.Application.Products;
using WafiArche.Application.PublicHolidays;
using WafiArche.EntityFrameworkCore.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register DbContext with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
);
// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure Serilog for logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddScoped<IProductAppService, ProductAppService>();
builder.Services.AddScoped<IPublicHolidayService, PublicHolidayService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
