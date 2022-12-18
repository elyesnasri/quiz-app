using Microsoft.EntityFrameworkCore;
using QuizApp.Web.Data;
using QuizApp.Web.GraphQL;

var builder = WebApplication.CreateBuilder (args);
const string _allowedOrigins = "_allowedOrigins";

builder.Services.AddControllers ();

builder.Services.AddDbContext<ApplicationDbContext> (options =>
{
    options.UseSqlite (builder.Configuration.GetConnectionString ("QuizApp"));
});

builder.Services
    .AddGraphQLServer ()
    .AddQueryType<Queries> ()
    .AddMutationType<Mutations> ()
    .AddProjections ();

builder.Services.AddCors (options =>
{
    options.AddPolicy (name: _allowedOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:44498")
                  .AllowAnyOrigin ()
                  .AllowAnyHeader();
        });
});

var app = builder.Build ();

app.UseHttpsRedirection ();
app.UseStaticFiles ();

app.UseCors (_allowedOrigins);

app.MapControllers ();
app.MapFallbackToFile ("index.html");

app.MapGraphQL ();

app.Run ();

