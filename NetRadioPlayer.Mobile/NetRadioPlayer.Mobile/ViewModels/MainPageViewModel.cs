using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Linq;
using NetRadioPlayer.Mobile.Model;
using NetRadioPlayer.Mobile.Services;
using Xamarin.Forms;

namespace NetRadioPlayer.Mobile.ViewModels
{
  public class MainPageViewModel : INotifyPropertyChanged
  {
    private NetRadioStationsService netRadioStationsService;
    private ObservableCollection<NetRadio> radioStations = new ObservableCollection<NetRadio>();
    private IoTDeviceService device;

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

    public NetRadio SelectedRadioStation { get; private set; }

    public MainPageViewModel(NetRadioStationsService netRadioService)
    {
      netRadioStationsService = netRadioService;
      netRadioStationsService.DataSynchronized += OnDataSynchronized;

      device = new IoTDeviceService();
      device.OpenConnection();
    }

    public async Task LoadNetRadios()
    {
      var result = await netRadioStationsService.GetRadioStationsFromSqliteAsync();
      
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

    public async Task Play()
    {
      await device.ExecuteCommand("play", "{\"Uri\": \"" + SelectedRadioStation.RadioUrl + "\"}");
    }

    public void SelectRadiostation(NetRadio selectedRadio)
    {
      SelectedRadioStation = selectedRadio;
    }
  }
}
