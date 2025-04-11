//var builder = WebApplication.CreateBuilder(args);

//// GEREKLÝ SERVÝSLERÝ EKLE
//builder.Services.AddControllersWithViews();
//builder.Services.AddAuthorization(); // <--- Bu satýr eksikti!

//// ?? CORS POLÝTÝKASI EKLE
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

// GEREKLÝ SERVÝSLERÝ EKLE
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();

// ?? CORS POLÝTÝKASI EKLE
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("https://localhost:7176") // senin frontend portun
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// ?? TEHLÝKELÝ SERTÝFÝKA DOÐRULAMA DEVRE DIÞI (sadece geliþtirme ortamý için)
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
