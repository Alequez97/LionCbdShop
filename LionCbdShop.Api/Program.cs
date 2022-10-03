using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Mapper.Profiles;
using LionCbdShop.Domain.Services;
using LionCbdShop.Persistence.Interfaces;
using LionCbdShop.Persistence.Repositories.Data;
using LionCbdShop.Persistence.Repositories.Files;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(ProductProfile));

builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IProductRepository, AzureCosmosDbProductRepository>();
builder.Services.AddSingleton<IProductImagesRepository, AzureBlobStorageProductImagesRepository>();

builder.Services.AddSingleton<IOrderService, OrderService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CBD Lion Api", Version = "v1" });
    c.EnableAnnotations();
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
