using Products.WebAPI;
using Products.Persistence;
using Products.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureWebAPI();

// Add services to the container.
#region IoC Container
builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.ConfigureWebAPI();

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
