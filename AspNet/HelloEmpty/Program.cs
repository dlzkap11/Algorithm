var builder = WebApplication.CreateBuilder(args);

//MVC 기반
//builder.Services.AddControllersWithViews();

//Razor 기반
//builder.Services.AddRazorPages();


// WebAPI 기반
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// M (Model)        데이터 (원자재)
// V (View)         UI (인테리어)
// C (Controller)   컨트롤러 (액션)


// Razor 구성
// M
// VC
// M - V - VM


// WebAPI 구성
// M
// C


//app.MapGet("/", () => "Hello World!");


// MVC
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.UseRouting();

// Razor
//app.MapRazorPages();


// Web API
app.MapControllers();

app.Run();
