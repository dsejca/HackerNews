using AppSettings;
using Core;
using Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHackerStories, HackerStories>();

//Add Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Get Section from appsettings.json
var appSettings = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<ConfigAppSettings>(appSettings);

//Configure CORS
var allowedOrigins = "AllowOrigins";
builder.Services.AddCors(options => {
    options.AddPolicy(name: allowedOrigins,
        policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.SetIsOriginAllowed((Host) => true);
            policy.AllowCredentials();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
