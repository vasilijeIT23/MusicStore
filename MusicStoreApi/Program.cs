using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MusicStoreInfrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MusicStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MusicStore")));




builder.Services.AddControllers();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
