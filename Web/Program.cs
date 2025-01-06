using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//
// AddAuthentication adds authentication services to the Dependency Injection. The default scheme is cookie authentication
// AddCookie enables the cookie authentication scheme, where a cookie stored on a client browser will be used for authentication
//
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//
// configure the request pipeline to use authentication while processing requests
//
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
