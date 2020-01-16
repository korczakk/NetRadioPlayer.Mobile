using System.Collections.Generic;

namespace NetRadioPlayer.Mobile.Model
{
  public class NetRadioGroup : List<NetRadio>
  {
    public string FolderName { get; set; }
    public List<NetRadio> RadioStations => this;

    public NetRadioGroup(string folderName, List<NetRadio> radioStations)
      :base(radioStations)
    {
      FolderName = folderName;
    }
  }
}
