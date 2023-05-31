namespace MVCLab2.Middlewares
{

  // gelen request header da Client_id ve Client_Secret değeri var mı kontrolü yapar. Client Credential yöntemi ile istekleri gelen isteğin header bilgisini kontrol eder.
  public class ClientCredentialRequestMiddleware:IMiddleware
  {

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      // postman olabilir istek atarken header üzerinden client_id client_secret vs client_credentials bilgilerini kontrol ediyoruz
      var clientId = context.Request.Headers.Keys.Contains("client_id");
      var clientSecret = context.Request.Headers.Keys.Contains("client_secret");
      var grantType = context.Request.Headers.Keys.Contains("grant_types");

      if (!clientId || !clientSecret || !grantType)
      {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Client Credentials bilgileri eksik!");
        return;
      }
      else
      {
        // client id client secret bilgileri dbden kontrol edilip bir işlem yapılacak ona göre bir kontrol sağlanabilir.
        //todo
      }
      await next.Invoke(context);
    }
  }
}
