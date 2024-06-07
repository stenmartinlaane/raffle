using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using App.DAL.EF;
using App.Domain.Identity;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using backend.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// clear default claims
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services
    .AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultUI()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services
    .AddAuthentication(o =>
    {
        o.DefaultScheme = "MyCookieScheme";
        o.DefaultAuthenticateScheme = "MyCookieScheme";
    })
    .AddCookie("MyCookieScheme",  o =>
    {
        o.Cookie.Name = "jwt";
        o.Cookie.HttpOnly = true;
        o.Cookie.SecurePolicy = CookieSecurePolicy.None;
        o.Cookie.SameSite = SameSiteMode.Strict;
        //o.Cookie.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        o.Cookie.IsEssential = true;
        o.Cookie.Path = "/";
    });

builder.Services.AddAuthorization(
    b =>
    {
        b.AddPolicy("id_policy", pb => pb
            .RequireAuthenticatedUser()
            .AddAuthenticationSchemes("MyCookieScheme")
            .RequireClaim(ClaimTypes.NameIdentifier)
        );
    }
);

builder.Services.AddAutoMapper(
    typeof(App.DAL.EF.AutoMapperProfile)
);

var apiVersioningBuilder = builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});
apiVersioningBuilder.AddApiExplorer(options =>
{
    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
    // note: the specified format code will format the version as "'v'major[.minor][-status]"
    options.GroupNameFormat = "'v'VVV";

    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    // can also be used to control the format of the API version in route templates
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => { policy.WithOrigins(builder.Configuration.GetValue<string>("AllowedFrontendDomain"))
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Allow credentials (cookies)
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllersWithViews();

// ===================================================
var app = builder.Build();
// ===================================================

SetupAppData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// app.UseCors("CorsAllowAll");
app.UseCors("AllowSpecificOrigin");

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();



app.UseRequestLocalization(options:
    app.Services.GetService<IOptions<RequestLocalizationOptions>>()?.Value!
);

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant()
        );
    }
    // serve from root
    // options.RoutePrefix = string.Empty;
});

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); //.RequireAuthorization("cookie");

app.Run();

static void SetupAppData(WebApplication app)
{
    using var serviceScope = ((IApplicationBuilder)app).ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope();
    using var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    if (!context.Database.ProviderName!.Contains("InMemory"))
    {
        context.Database.Migrate();
    }
    
    using var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    using var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
    
    var res = roleManager.CreateAsync(new AppRole()
    {
        Name = "Admin"
    }).Result;

    if (!res.Succeeded)
    {
        Console.WriteLine(res.ToString());
    }

    var user = new AppUser()
    {
        Email = "admin@eesti.ee",
        UserName = "admin@eesti.ee",
        FirstName = "Admin",
        LastName = "Eesti"
    };
    res = userManager.CreateAsync(user, "Kala.maja1").Result;
    if (!res.Succeeded)
    {
        Console.WriteLine(res.ToString());
    }

    res = userManager.AddToRoleAsync(user, "Admin").Result;
    if (!res.Succeeded)
    {
        Console.WriteLine(res.ToString());
    }
}