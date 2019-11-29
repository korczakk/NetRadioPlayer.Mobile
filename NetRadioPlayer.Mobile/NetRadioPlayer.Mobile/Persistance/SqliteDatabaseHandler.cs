using NetRadioPlayer.Mobile.Model;
using SQLite;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NetRadioPlayer.Mobile.Persistance
{
  public static class SqliteDatabaseHandler
  {
    private static SQLiteAsyncConnection sqliteConnection;
    private static bool isTableCreated;

    public static async Task<SQLiteAsyncConnection> GetDatabase()
    {
      await Task.Run(() => System.Threading.SpinWait.SpinUntil(() => isTableCreated));

      return await Task.FromResult<SQLiteAsyncConnection>(sqliteConnection);
    }

    public async static Task LoadSqliteDatabase(IDbPath dbPath)
    {
      string path = dbPath.GetSqliteDbPath();
      var dbConnection = new SQLiteAsyncConnection(path);
      sqliteConnection = dbConnection;
      
      await dbConnection.CreateTableAsync<NetRadio>(CreateFlags.None);

      isTableCreated = true;
    }
  }
}
