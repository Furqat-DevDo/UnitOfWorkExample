using Microsoft.EntityFrameworkCore;
using UnitOfWorkExample.Data;
using UnitOfWorkExample.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLogging(log =>
{
    log.AddConsole();
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
