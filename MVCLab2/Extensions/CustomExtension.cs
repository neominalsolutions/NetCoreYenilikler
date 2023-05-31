namespace MVCLab2.Extensions
{
  // extension sınfıları static olur. static sınıfların içerisinde zaten static üyeler yazabiliriz.
  // Bir sınıfı genişletme o sınıfa yeni özellikler kazandırmak istediğimizde kullanırız.
  public static class CustomExtension
  {
    public static string ToUpperCase(this string value)
    {
      return value.ToUpper();
    }

    public static string GetPrettyDateToString(this DateTime date)
    {
      return date.ToShortDateString();

    }
  }

  
}
