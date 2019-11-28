using NetRadioPlayer.Mobile.Model;
using SQLite;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NetRadioPlayer.Mobile.Persistance
{
  public static class SqliteDatabaseHandler
  {
    private static SQLiteAsyncConnection sqliteConnection;

    public static SQLiteAsyncConnection SqliteDatabase
    {
      get
      {
        if (sqliteConnection is null)
        {
          throw new System.Exception("Database has not been loaded.");
        }
        return sqliteConnection;
      }
    }

    public async static Task LoadSqliteDatabase()
    {     
      string path = DependencyService.Get<IDbPath>().GetSqliteDbPath();
      var dbConnection = new SQLiteAsyncConnection(path);
      sqliteConnection = dbConnection;

      await dbConnection.CreateTableAsync<NetRadio>(CreateFlags.None);
    }
  }
}
