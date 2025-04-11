//var builder = WebApplication.CreateBuilder(args);

//// GEREKL� SERV�SLER� EKLE
//builder.Services.AddControllersWithViews();
//builder.Services.AddAuthorization(); // <--- Bu sat�r eksikti!

//// ?? CORS POL�T�KASI EKLE
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowLocalhost", builder =>
//    {
//        builder.WithOrigins("https://localhost:7176") // senin frontend portun
//               .AllowAnyHeader()
//               .AllowAnyMethod();
//    });
//});

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//// ?? CORS'U ROUTING'DEN SONRA KOY
//app.UseCors("AllowLocalhost");

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();



var builder = WebApplication.CreateBuilder(args);

// GEREKL� SERV�SLER� EKLE
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();

// ?? CORS POL�T�KASI EKLE
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("https://localhost:7176") // senin frontend portun
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// ?? TEHL�KEL� SERT�F�KA DO�RULAMA DEVRE DI�I (sadece geli�tirme ortam� i�in)
builder.Services.AddHttpClient("UnsafeClient")
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
