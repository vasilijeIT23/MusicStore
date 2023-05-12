using FluentAssertions.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;
using MusicStoreInfrastructure;
using System.Text.Json.Serialization;
using ToDoApi.Infrastructure;

var AllowCors = "_allowCors";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MusicStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MusicStore")));


builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<ProductType>, ProductTypeRepository>();
builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<IRepository<Cart>, CartRepository>();
builder.Services.AddScoped<IRepository<CartItem>, CartItemRepository>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IRepository<OrderItem>, OrderItemRepository>();
builder.Services.AddScoped<IRepository<Warehouse>, WarehouseRepository>();
builder.Services.AddScoped<IRepository<Stock>, StockRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    //TODO remove this
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowCors,
    builder =>
    {
        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
    });
});

builder.Services.AddOpenApiDocument(config =>
{
    config.SchemaNameGenerator = new CustomSwaggerSchemaNameGenerator();
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

app.UseCors(AllowCors);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseOpenApi();
app.UseSwaggerUi3();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<MusicStoreContext>();
    context.Database.Migrate();
}


app.Run();
