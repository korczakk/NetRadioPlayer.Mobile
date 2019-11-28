using System.IO;
using NetRadioPlayer.Mobile.Droid.Persistance;
using NetRadioPlayer.Mobile.Persistance;

[assembly: Xamarin.Forms.Dependency(typeof(DbConnectionString))]
namespace NetRadioPlayer.Mobile.Droid.Persistance
{
  public class DbConnectionString : IDbPath
  {
    public string GetSqliteDbPath()
    {
      string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "NetRadioStations.db3");

      return path;
    }
  }
}