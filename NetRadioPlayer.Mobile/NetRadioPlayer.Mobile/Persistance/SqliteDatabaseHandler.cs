using NetRadioPlayer.Mobile.Model;
using SQLite;
using System.Threading.Tasks;

namespace NetRadioPlayer.Mobile.Persistance
{
  public static class SqliteDatabaseHandler
  {
    private static SQLiteAsyncConnection sqliteConnection;

    public static SQLiteAsyncConnection DbContext => sqliteConnection;

    public static void LoadSqliteDatabase(IDbPath dbPath)
    {
      string path = dbPath.GetSqliteDbPath();
      var dbConnection = new SQLiteAsyncConnection(path);
      sqliteConnection = dbConnection;

      dbConnection.CreateTableAsync<NetRadio>(CreateFlags.None);
    }
  }
}
