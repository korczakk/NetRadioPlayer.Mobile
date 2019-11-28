using NetRadioPlayer.Mobile.Persistance;
using NetRadioPlayer.Mobile.UWP.Persistance;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(DbPath))]
namespace NetRadioPlayer.Mobile.UWP.Persistance
{
  public class DbPath : IDbPath
  {
    public string GetSqliteDbPath()
    {
      string path = ApplicationData.Current.LocalFolder.Path + "\\NetRadioStations.db3";

      return path;
    }


  }
}
