using System.Text.Json.Serialization;
using AccountsApi.DataContext;
using AccountsApi.Helpers;
using AccountsApi.Repositories;
using AccountsApi.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

{
    var services = builder.Services;
    var environment = builder.Environment;


    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddControllers().AddJsonOptions(x => 
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
      
        // ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });   

    services.AddAutoMapper(typeof(AutoMapperProfile));

    //Confifure our strongly typed settings object
    services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

    //Configre DI for our application Services
    services.AddSingleton<ApplicationDbContext>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IUserService, UserSerive>();
}



var app = builder.Build();

 //ensure datasase and tables exist
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Init();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.Run();

