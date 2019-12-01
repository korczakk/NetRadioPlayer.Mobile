using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;

namespace NetRadioPlayer.Mobile.Model
{
  public class NetRadio : TableEntity
  {
    public string RadioName { get; private set; }
    public string RadioUrl { get; private set; }
    public string Folder { get; private set; }

    public NetRadio()
    {
    }

    public NetRadio(string radioName, string url, string folder)
    {
      RadioName = radioName;
      RadioUrl = url;
      Folder = folder;
      RowKey = Guid.NewGuid().ToString();
      PartitionKey = folder;
    }

    public override bool Equals(Object obj)
    {
      var input = obj as NetRadio;
      return this.RowKey == input.RowKey;
    }

    public override int GetHashCode()
    {
      return this.RowKey.GetHashCode();
    }

    public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
    {
      RadioName = properties["RadioName"].StringValue;
      Folder = properties["Folder"].StringValue;
      RadioUrl = properties["RadioUrl"].StringValue;
    }
  }
}
