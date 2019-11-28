using Microsoft.WindowsAzure.Storage.Table;

namespace NetRadioPlayer.Mobile.Helpers
{
  public interface ITableStorageHelper
  {
    CloudTable GetTableReference(string tableName);
  }
}