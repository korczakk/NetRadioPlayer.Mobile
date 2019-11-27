using System.IO;
using Android.Content.Res;
using NetRadioPlayer.Mobile.Helpers;

namespace NetRadioPlayer.Mobile.Droid.Helpers
{
  public class ReadAppsettingsFile : IAppsettingsReader
  {
    public string ReadFile()
    {
      string read = default;

      AssetManager assets = Android.App.Application.Context.Assets;
      var appsettingsFile = assets.Open("appsettings.json");

      using (var stream = new StreamReader(appsettingsFile))
      {
        read = stream.ReadToEnd();
      }

      return read;
    }
  }
}