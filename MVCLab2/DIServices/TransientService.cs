namespace MVCLab2.DIServices
{
  public class TransientService : MicrosoftDI
  {
    public string Id { get; set; }

    public TransientService()
    {
      Id = Guid.NewGuid().ToString();
    }
  }
}
