using ApiPersonajesAWS.Data;
using ApiPersonajesAWS.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("AWS");

builder.Services.AddTransient<RepositorieTelevision>();

builder.Services.AddDbContext<TelevisionContext>(options => options.UseMySQL(connectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapControllers();
app.MapOpenApi();
app.MapScalarApiReference();

app.MapGet(
    "/",
    context =>
    {
        context.Response.Redirect("/scalar");
        return Task.CompletedTask;
    }
);

app.UseHttpsRedirection();

app.Run();
