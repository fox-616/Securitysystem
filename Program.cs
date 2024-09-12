using Microsoft.EntityFrameworkCore;
using WebAPI_3.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//註冊 Session
builder.Services.AddDistributedMemoryCache(); // 使用內存中的分佈式緩存
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // 設定 session 過期時間
    //options.Cookie.HttpOnly = true; // 使 cookie 只可由 HTTP 請求訪問
    //options.Cookie.IsEssential = true; // 確保在要求無跟踪請求時也會發送 cookie
});


//builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<SecuritiesSystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SecuritiesSystemConnection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseAuthorization();

//MVC 補啟動
app.UseRouting();

//使用 session
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
