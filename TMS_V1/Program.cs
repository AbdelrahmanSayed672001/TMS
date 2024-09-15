

var builder = WebApplication.CreateBuilder(args);

// Add Google External Login
var configurationSecret =builder.Configuration;


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "Google";

}).AddCookie()
.AddGoogle(googleOptions =>
{

    googleOptions.ClientId = configurationSecret["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configurationSecret["Authentication:Google:ClientSecret"];
});

// Inject Servces
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<IAttachment, AttachmentService>();
builder.Services.AddTransient<IFileValidationService, FileValidationService>();
builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepoService<>));


// Add Razor Pages services
builder.Services.AddRazorPages();

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/", async context =>
{
    if (!context.User.Identity.IsAuthenticated)
    {
        context.Response.Redirect("/Identity/Account/Login");
    }
    else
    {
        context.Response.Redirect("/RegUserRoles/Index");
    }
    await Task.CompletedTask;
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//   name: "backend",
//   pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}",
//   defaults: new { area = "Backend" }
//   );

app.MapRazorPages();

app.Run();
