using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using NetRadioPlayer.Mobile.Model;

namespace NetRadioPlayer.Mobile.ViewModels
{
  public class MainVm
  {
    public List<NetRadio> RadioStations { get; private set; }
    public ObservableCollection<bool> IsPlayerReady { get; private set; }

    public async Task GetNetRadios()
    {

    }

    public async Task<bool> CheckIfPlayerIsReady()
    {
      return await new Task<bool>(() => true);
    }
  }
}
