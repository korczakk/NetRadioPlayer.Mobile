using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using NetRadioPlayer.Mobile.Model;
using NetRadioPlayer.Mobile.Services;
using Xamarin.Forms;

namespace NetRadioPlayer.Mobile.ViewModels
{
  public class MainPageViewModel : INotifyPropertyChanged
  {
    private NetRadioStationsService netRadioStationsService;
    private ObservableCollection<NetRadio> radioStations = new ObservableCollection<NetRadio>();

    public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<NetRadio> RadioStations
    {
      get
      {
        return radioStations;
      }
      set
      {
        radioStations = value;
        OnPropertyChanged("RadioStations");
      }
    }
    public IObservable<bool> IsPlayerReady { get; private set; }

    public MainPageViewModel(NetRadioStationsService netRadioService)
    {
      netRadioStationsService = netRadioService;
      netRadioStationsService.DataSynchronized += OnDataSynchronized;
    }

    public async Task LoadNetRadios()
    {
      var result = await netRadioStationsService.GetRadioStationsFromSqliteAsync();
      
      await Task.Delay(TimeSpan.FromSeconds(5));

      Device.BeginInvokeOnMainThread(() => RadioStations = new ObservableCollection<NetRadio>(result));

      await netRadioStationsService.SyncWithCloud(result);

    }
    public void OnPropertyChanged(string name)
    {
      if (this.PropertyChanged != null)
        this.PropertyChanged(this, new PropertyChangedEventArgs(name));
    }

    private void OnDataSynchronized(object sender, IList<NetRadio> radios)
    {
      Device.BeginInvokeOnMainThread(() => RadioStations = new ObservableCollection<NetRadio>(radios));
    }

  }
}
