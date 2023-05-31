using MVCLab2.Middlewares;

namespace MVCLab2.Extentions
{

  // Bir sınıfı veya interface'i genişletme o sınıfa yeni özellikler kazandırmak istediğimizde kullanırız. IApplicationBuilder sınıfına yeni bir özellik kazandırmak istediğimiz için this keyword ile extension tanımı yaptık
  public static class AppExtension
  {
    public static IApplicationBuilder UseClientCrendentials(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<ClientCredentialRequestMiddleware>();
    }

 
  }

 
}
