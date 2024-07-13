using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebsiteBanThucAnNhanh18.Data;
using WebsiteBanThucAnNhanh18.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QLBHContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("QLBH"));
}
);
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option=>
    {
        option.LoginPath = "/KhachHang/DangNhap";
        option.AccessDeniedPath = "/AccessDenied";
       
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:7288") //Cho phép Web Admin truy cập file www root
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Cấu hình middleware để phục vụ tệp tĩnh và sử dụng CORS
app.UseStaticFiles();

app.UseHttpsRedirection();


app.UseRouting();

app.UseSession();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
