using System.IO;
using NetRadioPlayer.Mobile.Configuration;

namespace NetRadioPlayer.Mobile.UWP.Helpers
{
  public class ReadAppsettingsFile : IAppsettingsReader
  {
    public string ReadFile()
    {
      string read = default;

      using (var stream = new StreamReader("appsettings.json"))
      {
        read = stream.ReadToEnd();
      }

      return read;
    }
  }
}
