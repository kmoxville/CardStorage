using CardStorage.Data;
using CardStorage.Data.UnitOfWork;
using CardStorage.Utils;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MySQLOptions>(
                builder.Configuration.GetSection(MySQLOptions.Position));

builder.Services.AddDatabaseContext(builder.Configuration, builder.Environment.IsDevelopment());

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>();
builder.Services.AddUnitOfWork();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddAuthService();

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
