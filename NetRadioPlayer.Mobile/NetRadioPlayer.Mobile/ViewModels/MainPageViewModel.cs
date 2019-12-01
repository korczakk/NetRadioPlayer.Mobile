using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using NetRadioPlayer.Mobile.Model;
using NetRadioPlayer.Mobile.Services;

namespace NetRadioPlayer.Mobile.ViewModels
{
  public class MainPageViewModel
  {
    private NetRadioStationsService netRadioStationsService;

    public ObservableCollection<NetRadio> RadioStations { get; private set; }
    public IObservable<bool> IsPlayerReady { get; private set; }

    public MainPageViewModel(NetRadioStationsService netRadioService)
    {
      netRadioStationsService = netRadioService;
      netRadioStationsService.DataSynchronized += OnDataSynchronized;
    }

    public async Task LoadNetRadios()
    {
      var result = await netRadioStationsService.GetRadioStationsFromSqliteAsync();

      RadioStations = new ObservableCollection<NetRadio>(result);

      await netRadioStationsService.SyncWithCloud(result);

    }

    private void OnDataSynchronized(object sender, IList<NetRadio> radios)
    {
      RadioStations = new ObservableCollection<NetRadio>(radios);
    }
  }
}
