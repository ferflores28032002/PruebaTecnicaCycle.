using PruebaTecnicaCycle.Application.services;
using PruebaTecnicaCycle.Domain.interfaces.repositories;
using PruebaTecnicaCycle.Infrastructure.Data.context;
using PruebaTecnicaCycle.Infrastructure.Data.repositories;
using Amazon.S3;
using PruebaTecnicaCycle.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<PruebaTecnicaCycleContext>();
// Register PruebaTecnicaCycleContext
builder.Services.AddScoped<PruebaTecnicaCycleContext>();

// Register IRepositorioBase and its implementation
builder.Services.AddScoped<IRepositorioBase<Product, Guid>, ProductRepository>();

// Registra el servicio de subida de imágenes a S3
builder.Services.AddScoped<S3ImageUploaderServices>();

builder.Services.AddScoped<ProductServices>();

// Configura el cliente de S3
builder.Services.AddAWSService<IAmazonS3>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
}
   
) ;

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Use CORS with the default policy
app.UseCors();

app.UseRouting();

app.UseAuthorization();
app.MapIdentityApi<IdentityUser>();
app.MapControllers();

app.Run();
