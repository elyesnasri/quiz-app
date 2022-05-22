var builder = WebApplication.CreateBuilder (args);

builder.Services.AddControllersWithViews ();

var app = builder.Build ();

app.UseHttpsRedirection ();
app.UseStaticFiles ();

app.MapControllers ();
app.MapFallbackToFile ("index.html");

app.Run ();

