using ApiPersonajesAWS.Data;
using ApiPersonajesAWS.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("AWS");

// Repositorios
builder.Services.AddTransient<RepositorieTelevision>();

// DB
builder.Services.AddDbContext<TelevisionContext>(options =>
    options.UseMySQL(connectionString)
);

// Controllers + OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// CORS (permitir todo - SOLO desarrollo)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Pipeline
app.UseHttpsRedirection();

app.UseRouting();

// CORS aquí
app.UseCors("AllowAll");

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

app.Run();
