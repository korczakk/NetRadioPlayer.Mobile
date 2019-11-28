using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetRadioPlayer.Mobile.Model
{
  public class NetRadio : TableEntity
  {
    public string RadioName { get; set; }
    public string RadioUrl { get; set; }
    public string Folder { get; set; }
  }
}
