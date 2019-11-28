using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetRadioPlayer.Mobile.Configuration
{
  public class Appsettings
  {
    private static Appsettings instance;
    private static object lockObject = new object();
    private static Dictionary<string, string> settings;

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

    public string this[string name]
    {
      get
      {
        return Settings[name];
      }
    }

    private Appsettings()
    {
    }

    public static void LoadAppsettings(IAppsettingsReader appsettingsReader)
    {
      string json = appsettingsReader.ReadFile();

      settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
    }


  }
}
