using Microsoft.EntityFrameworkCore;
using QuizApp.Web.Data;

var builder = WebApplication.CreateBuilder (args);

builder.Services.AddControllers ();

builder.Services.AddDbContext<ApplicationDbContext> (options =>
{
    options.UseSqlite (builder.Configuration.GetConnectionString ("QuizApp"));
});

var app = builder.Build ();

app.UseHttpsRedirection ();
app.UseStaticFiles ();

app.MapControllers ();
app.MapFallbackToFile ("index.html");

app.Run ();

