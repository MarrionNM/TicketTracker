using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api;
using TaskTracker.Api.Contracts;
using TaskTracker.Api.Data.Seeding;
using TaskTracker.Api.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------------
// SERVICES
// ------------------------------------------------------------

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core - InMemory persistence
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TaskTrackerDb"));

// Repository DI
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithOrigins("http://localhost:4200",
                           "http://localhost:5173",
                           "http://localhost:3000");
    });
});

// Model Validation Response (RFC 7807)
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/validation-error",
            Title = "Validation failed",
            Detail = "One or more validation errors occurred."
        };

        return new BadRequestObjectResult(problemDetails);
    };
});

// Logging
builder.Services.AddHttpLogging(o => { });

var app = builder.Build();

// ------------------------------------------------------------
// MIDDLEWARE
// ------------------------------------------------------------

// HTTP Logging
app.UseHttpLogging();

// Global Exception Handling
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        var problem = new ProblemDetails
        {
            Title = "An unexpected error occurred.",
            Detail = exception?.Message,
            Status = (int)HttpStatusCode.InternalServerError,
            Type = "https://example.com/errors/internal-server-error"
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problem.Status.Value;

        await context.Response.WriteAsJsonAsync(problem);
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.MapControllers();

// ------------------------------------------------------------
// SEED DATABASE
// ------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData.Seed(db);
}

app.Run();
