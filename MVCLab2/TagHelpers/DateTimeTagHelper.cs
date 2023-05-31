using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVCLab2.TagHelpers
{

  // <asp-datetime value="1"></asp-datetime>
  [HtmlTargetElement("asp-datetime", Attributes = "value")]
  public class DateTimeTagHelper : TagHelper
  {
    // default şuanı verir
    public DateTime Value { get; set; } = DateTime.Now;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {

      output.TagName = "span";
      output.Attributes.SetAttribute("class", "alert alert-info");
      output.Content.SetHtmlContent($"{Value.ToShortDateString()} - {Value.ToShortTimeString()}");

      // bu method içerisinde html çıktısı output ile üretiriz.
      base.Process(context, output);
    }
  }
}
