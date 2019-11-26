using Android.Content.Res;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NetRadioPlayer.Mobile.Helpers
{
  public class Appsettings
  {
    private static Appsettings instance;
    private static object lockObject = new object();
    private Dictionary<string, string> settings;

    public static Appsettings Settings
    {
      get
      {
        lock (lockObject)
        {
          if (instance == null)
          {
            instance = new Appsettings();
          }
          return instance;
        }

      }
    }

    private Appsettings()
    {
      // read file
      ReadFile();
    }

    public string this[string name]
    {
      get
      {
        return this.settings[name];
      }
    }
    private async Task ReadFile()
    {
      try
      {
        AssetManager assets = Android.App.Application.Context.Assets;
        var test = assets.Open("appsettings.json");

        using (var stream = new StreamReader(test))
        {
          var read = stream.ReadToEnd();
        }
      }
      catch (System.Exception ex)
      {

        throw;
      }

    }

    //private async Task ReadFile()
    //{
    //  try
    //  {
        

    //    using (var stream = new StreamReader("appsettings.json"))
    //    {
    //      var read = stream.ReadToEnd();
    //    }
    //  }
    //  catch (System.Exception ex)
    //  {

    //    throw;
    //  }

    //}

  }
}
