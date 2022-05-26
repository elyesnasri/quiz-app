using Microsoft.EntityFrameworkCore;
using QuizApp.Web.Data;
using QuizApp.Web.GraphQL;

var builder = WebApplication.CreateBuilder (args);

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

var app = builder.Build ();

app.UseHttpsRedirection ();
app.UseStaticFiles ();

app.MapControllers ();
app.MapFallbackToFile ("index.html");

app.MapGraphQL ();

app.Run ();

