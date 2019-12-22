using NetRadioPlayer.Mobile.Model;
using SQLite;

namespace NetRadioPlayer.Mobile.Persistance
{
  public static class SqliteDatabaseHandler
  {
    private static SQLiteAsyncConnection sqliteConnection;

    public static SQLiteAsyncConnection DbContext
    {
      get
      {
        return sqliteConnection;
      }      
    }

    public static void LoadSqliteDatabase(IDbPath dbPath)
    {
      string path = dbPath.GetSqliteDbPath();
      var dbConnection = new SQLiteAsyncConnection(path);
      sqliteConnection = dbConnection;
      
      dbConnection.CreateTableAsync<NetRadio>(CreateFlags.None);
    }
  }
}
