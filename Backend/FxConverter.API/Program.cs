using FxConverter.Application.Mappings;
using FxConverter.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "FxConverter API", Version = "v1" });
});

// Add Infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);

// Add Application services
builder.Services.AddScoped<FxConverter.Application.Interfaces.IConversionService, FxConverter.Application.Services.ConversionService>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthorization();
app.MapControllers();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FxConverter.Infrastructure.Data.FxConverterDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
