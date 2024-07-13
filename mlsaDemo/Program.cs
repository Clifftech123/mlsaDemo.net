using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using mlsaDemo.Context;
using mlsaDemo.Interface;
using mlsaDemo.Middleware;
using mlsaDemo.Models;
using mlsaDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


{
 //  Adding automapper 
  builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}

//  getting the connection string from appsettings.json
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));



//  adding the db context
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var dbSettings = serviceProvider.GetRequiredService<IOptions<DbSettings>>().Value;
    options.UseSqlServer(dbSettings.ConnectionString);
});

// Adding services 

builder.Services.AddScoped<IServices, ItemServices>();


// Adding Global Exception Handler
{
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();


    builder.Services.AddLogging();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() { Title = "mlsademo", Version = "v1" });
    });
}

var app = builder.Build();



{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}



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
