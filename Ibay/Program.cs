using IbayApi.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<IbayContext>();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Ibay API",
        Description = "iBay Ltd est une entreprise qui vise à produire la meilleure expérience pour votre magasin en ligne. Cette API développée en C# / .net Core vous permet d'accéder à des informations sur les produits et à d'autres données pertinentes, ainsi que de devenir vendeur et de proposer vos propres produits. Toutes les données sont stockées dans une base de données SQL Server à l'aide de Entity Framework Core et sont conformes aux normes REST (méthodes, endpoint, etc.). En plus de cela, une application de console .net simple est fournie pour interroger l'API.",
            
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddControllers();

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
