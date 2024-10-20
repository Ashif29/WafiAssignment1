using Microsoft.EntityFrameworkCore;
using WafiArche.Application.Mappings;
using WafiArche.Application.Products;
using WafiArche.EntityFrameworkCore.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register DbContext with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
);
// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IProductAppService, ProductAppService>();

builder.Services.AddControllers();
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
