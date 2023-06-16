using Azure;
using chrep.core.park.uof;
using chrep.data.park.SqlServer;
using chrep.data.park.uof;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddDbContext<AppDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DB")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUnitofworks, Unitofworks>();
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(cors => cors.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().WithOrigins("*"));
app.MapControllers();

app.Run();
