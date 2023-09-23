using Microsoft.AspNetCore.Authentication.Cookies;
using PortalGalaxy.WebMvc.Services.Implementaciones;
using PortalGalaxy.WebMvc.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Patron singleton para el objeto HttpClient
builder.Services.AddSingleton(_ => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetValue<string>("Backend:ApiRestUrl")!)
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IUserProxy, UserProxy>();
builder.Services.AddScoped<IUbigeoProxy, UbigeoProxy>();
builder.Services.AddScoped<ICategoriaProxy, CategoriaProxy>();
builder.Services.AddScoped<ITallerProxy, TallerProxy>();
builder.Services.AddScoped<IInstructorProxy, InstructorProxy>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120); // 2 horas
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "PortalGalaxy";
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
        options.LoginPath = "/User/Login";
        options.AccessDeniedPath = "/User/AccesoDenegado";
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
