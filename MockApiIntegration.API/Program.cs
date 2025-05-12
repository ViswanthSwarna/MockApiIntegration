using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MockApiIntegration.API.Middleware;
using MockApiIntegration.Application.Services.Classes;
using MockApiIntegration.Application.Services.Interfaces;
using MockApiIntegration.Application.Validators;
using MockApiIntegration.Infrastructure.Clients.Classes;
using MockApiIntegration.Infrastructure.Clients.Interfaces;
using MockApiIntegration.Infrastructure.Storage.Classes;
using MockApiIntegration.Infrastructure.Storage.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .Select(e => new { Field = e.Key, Error = e.Value.Errors.First().ErrorMessage });

        return new BadRequestObjectResult(errors);
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

    

builder.Services.AddValidatorsFromAssemblyContaining<ProductRequestDtoValidator>(ServiceLifetime.Transient);
builder.Services.AddSingleton<IProductIdStore, ProductIdStore>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddHttpClient<IMockApiClient, MockApiClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
