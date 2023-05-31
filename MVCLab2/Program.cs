using MVCLab2.DIServices;
using MVCLab2.Extentions;
using MVCLab2.Middlewares;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // hem mvc hemde razor page desteklesin
builder.Services.AddControllers();

// 3 farkl� lifetimescope sahip servisleri tan�tt�k.
// Controller �zerinden hepsini test edece�iz.

// Microsoft.Extensions.DependencyInjection a�a��daki servisler bu paket i�erisinde gelir.

// Transient her istekte instance al�r
// Session, Validation 
builder.Services.AddTransient<TransientService>();

// her bir controller request de birden fazla kez �a��rlsa dahi tek bir instance al�r
// Web Request bazl�, Her bir web iste�inde controller bazl� instance al�r.
// EF Repository Servisler i�in kullan�r�z. Kendi Domain Service.
builder.Services.AddScoped<ScopeService>();

// singleton ise uygulama genelinde tek bir instance ile �al���r
// Redis Ba�lant�s�, uzak sunucu ba�lant�s� socket ba�lant�s�, Email Service
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


// Use middleware Bu y�ntem next ad�nda bir delegate sunar. Bununla bir sonraki middleware s�reci aktar�r . 2 middleware nesnesinin aras�na girmek ama�l� kullan�labilir. Genelde en �ok tercih edilen middlewaredir. A�a��daki app.UseHttpsRedirection(); app.UseStaticFiles(); hepsi use middleware olarak tan�mlanm��t�r.

//  public delegate Task RequestDelegate(HttpContext context);

// net core ortam�nda gelen isteklerin execute edilmesini �al��t�r�lmas�n� bu delegate sa�lar. yani gelen istekleri asenkton olarak yakalar, i�erisinde HttpContext ile web uygulamas�na ait t�m nesneleri bar�nd�r�r. HttpContext i�erisinde temelde iki �nemli Nesne bulunmaktad�r. Request di�eri ise Response.  Request Client taraftan web sayfas�na gelen iste�i sunucu (web serverda) yakalamam�z� sa�lar. Response ise web sunucusunun Web Client'a nas�l bir istek d�nd�relece�i ile ilgilenir. Html Response, Json Response, Text Response gibi farkl� tipte Responselar suncuya d�nd�r�lebilir.


app.Use(async (context, next) =>
{

  // context.Request
  //context.Response
  // context.User.HasClaim(); 
  //context.Session

  // request,response,user,session bilgilerini HttpContext �zerinden kullan�r�z.
 
  Debug.WriteLine("Use Middleware (Start)"); 
  // Routing Middleware ge�
  await next.Invoke(); // burada s�re� ba�ka bir middleware next methodu ile aktar�l�r.
  // sonra geri d�n�p i�leme devam et
  Debug.WriteLine("Use Middleware (End)");

});

// Map Middleware ile ad�ndan da anla��laca�� �zere bir path yakalamak istedi�imiz zaman kullan�r�z. A�a��daki gibi branch isimli map i�in bir i� mant��� yerle�tirmek istedi�imizde a�a��daki gibi bir yol izleyebiliriz.

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
  endpoints.MapRazorPages(); // razor page y�nlendirmelerini tan�mla.

  // areas her zaman default mvc route �zerine yaz�l�r
  endpoints.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
  );

  endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});




// Custom Middleware

// 1. Y�ntem Not: (Kulland�ktan sonra kapat, TagHelper �rne�i i�in)
// app.UseMiddleware<ClientCredentialRequestMiddleware>();

// 2. Y�ntem
// app.UseClientCrendentials();





// RUN middleware Run ile pipeline �zerinde short circuit(k�sa devre) yapmak m�mk�nd�r. Request bu metoda d��t�kten sonra pipeline �zerindeki di�er i�lemler devam etmez ve di�er middlewarelar �zerinden ge�mez. a�a��daki app.Run() �rnektir.

app.Run();
