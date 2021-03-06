﻿using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace NetRadioPlayer.Mobile.Model
{
  public class NetRadio : TableEntity
  {
    public string RadioName { get;  set; }
    public string RadioUrl { get;  set; }
    public string Folder { get;  set; }

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
      ETag = "*";
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
  }
}
