using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TAM2.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<TAM2Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TAM2Context") ?? throw new InvalidOperationException("Connection string 'TAM2Context' not found.")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{

}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
