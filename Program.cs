using DirectoryOfIndividuals.Api.Data;
using DirectoryOfIndividuals.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IndividualsDirectoryDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddControllers();
builder.Services.AddScoped<IIndividualsService, IndividualsService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<IndividualsDirectoryDbContext>();
    context.Database.Migrate();
}

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
