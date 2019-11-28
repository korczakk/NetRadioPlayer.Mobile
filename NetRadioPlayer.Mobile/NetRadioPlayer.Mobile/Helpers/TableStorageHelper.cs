using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using NetRadioPlayer.Mobile.Configuration;

namespace NetRadioPlayer.Mobile.Helpers
{
  public class TableStorageHelper : ITableStorageHelper
  {
    public CloudTable GetTableReference(string tableName)
    {
      string cn = Appsettings.Settings["TableStorageConnectionString"];

      var account = CloudStorageAccount.Parse(cn);
      var tableClient = account.CreateCloudTableClient();
      return tableClient.GetTableReference(tableName);
    }
  }
}
