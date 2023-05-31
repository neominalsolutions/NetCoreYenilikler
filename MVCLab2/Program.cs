using MVCLab2.DIServices;
using MVCLab2.Extentions;
using MVCLab2.Middlewares;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // hem mvc hemde razor page desteklesin
builder.Services.AddControllers();

// 3 farklý lifetimescope sahip servisleri tanýttýk.
// Controller üzerinden hepsini test edeceðiz.

// Microsoft.Extensions.DependencyInjection aþaðýdaki servisler bu paket içerisinde gelir.

// Transient her istekte instance alýr
// Session, Validation 
builder.Services.AddTransient<TransientService>();

// her bir controller request de birden fazla kez çaðýrlsa dahi tek bir instance alýr
// Web Request bazlý, Her bir web isteðinde controller bazlý instance alýr.
// EF Repository Servisler için kullanýrýz. Kendi Domain Service.
builder.Services.AddScoped<ScopeService>();

// singleton ise uygulama genelinde tek bir instance ile çalýþýr
// Redis Baðlantýsý, uzak sunucu baðlantýsý socket baðlantýsý, Email Service
builder.Services.AddSingleton<SingletonService>();


// middleware service olarak ekledik
builder.Services.AddTransient<ClientCredentialRequestMiddleware>();

var app = builder.Build();

//app.Use(async (context, next) =>
//{
//  await next();
//});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}








app.UseHttpsRedirection();
app.UseStaticFiles();


// Use middleware Bu yöntem next adýnda bir delegate sunar. Bununla bir sonraki middleware süreci aktarýr . 2 middleware nesnesinin arasýna girmek amaçlý kullanýlabilir. Genelde en çok tercih edilen middlewaredir. Aþaðýdaki app.UseHttpsRedirection(); app.UseStaticFiles(); hepsi use middleware olarak tanýmlanmýþtýr.

//  public delegate Task RequestDelegate(HttpContext context);

// net core ortamýnda gelen isteklerin execute edilmesini çalýþtýrýlmasýný bu delegate saðlar. yani gelen istekleri asenkton olarak yakalar, içerisinde HttpContext ile web uygulamasýna ait tüm nesneleri barýndýrýr. HttpContext içerisinde temelde iki önemli Nesne bulunmaktadýr. Request diðeri ise Response.  Request Client taraftan web sayfasýna gelen isteði sunucu (web serverda) yakalamamýzý saðlar. Response ise web sunucusunun Web Client'a nasýl bir istek döndüreleceði ile ilgilenir. Html Response, Json Response, Text Response gibi farklý tipte Responselar suncuya döndürülebilir.


app.Use(async (context, next) =>
{

  // context.Request
  //context.Response
  // context.User.HasClaim(); 
  //context.Session

  // request,response,user,session bilgilerini HttpContext üzerinden kullanýrýz.
 
  Debug.WriteLine("Use Middleware (Start)"); 
  // Routing Middleware geç
  await next.Invoke(); // burada süreç baþka bir middleware next methodu ile aktarýlýr.
  // sonra geri dönüp iþleme devam et
  Debug.WriteLine("Use Middleware (End)");

});

// Map Middleware ile adýndan da anlaþýlacaðý üzere bir path yakalamak istediðimiz zaman kullanýrýz. Aþaðýdaki gibi branch isimli map için bir iþ mantýðý yerleþtirmek istediðimizde aþaðýdaki gibi bir yol izleyebiliriz.

app.Map(new PathString("/Branch"), _configuration =>
{
  _configuration.Run(async context =>
  {
    await context.Response.WriteAsync("Branch Page Request");

  });

});


app.UseRouting();
app.UseAuthorization();

//app.UseMiddleware<ClientCredentialRequestMiddleware>();
app.UseClientCrendentials();

app.UseEndpoints(endpoints =>
{
  endpoints.MapRazorPages(); // razor page yönlendirmelerini tanýmla.

  // areas her zaman default mvc route üzerine yazýlýr
  endpoints.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
  );

  endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});




// Custom Middleware

// 1. Yöntem Not: (Kullandýktan sonra kapat, TagHelper örneði için)
// app.UseMiddleware<ClientCredentialRequestMiddleware>();

// 2. Yöntem
// app.UseClientCrendentials();





// RUN middleware Run ile pipeline üzerinde short circuit(kýsa devre) yapmak mümkündür. Request bu metoda düþtükten sonra pipeline üzerindeki diðer iþlemler devam etmez ve diðer middlewarelar üzerinden geçmez. aþaðýdaki app.Run() örnektir.

app.Run();
