using BlazorApp.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddScoped<CounterState>();

//dependency Injection
builder.Services.AddSingleton<IFoodService, FastFoodService>();
//생성자에서 알아서 연결해줌
builder.Services.AddSingleton<PaymentService>();

//3가지 모드
builder.Services.AddSingleton<SingletonService>(); //처음 한번
builder.Services.AddTransient<TransientService>(); //매번 갱신
builder.Services.AddScoped<ScopeService>();        //페이지 새로고침시 갱신


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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
