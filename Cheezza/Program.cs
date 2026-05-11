using Cheezza.Data;
using Cheezza.Models; // ApplicationUser ve Pizza modelleri için gerekli
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. ADIM: Veritabaný Baðlantýsý (SQLite Ayarý)
// .UseSqlServer yerine projenin þartý olan .UseSqlite kullanýldý.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 2. ADIM: Kimlik Doðrulama (Identity) ve 2FA
// IdentityUser yerine özelleþtirdiðimiz ApplicationUser kullanýldý.
// Admin yetkilendirmesi için .AddRoles<IdentityRole>() eklendi.
builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = true; // E-posta doðrulamasý (2FA için temel)
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// 3. ADIM: HTTP Ýstek Hattý (Middleware) Yapýlandýrmasý
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


// --- YENÝ EKLENEN ADMÝN YETKÝLENDÝRME KODU ---
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // 1. Veritabanýnda "Admin" rolü yoksa oluþtur
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // 2. Senin hesabýný bulup Admin yetkisini ata
    var adminUser = await userManager.FindByEmailAsync("yumuklusucurta6754@gmail.com");
    if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}
// ----------------------------------------------

app.Run();