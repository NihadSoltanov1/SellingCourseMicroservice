using FreeCourses.Service.Catalog.Service;
using FreeCourses.Service.Catalog.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

// Configure the DatabaseSettings using the configuration
builder.Services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<ICourseService, CourseService>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddControllers();
builder.Services.Configure <DatabaseSettings> (configuration.GetSection("DataseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
builder.Services.AddAutoMapper(typeof(Program));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
