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
    private ObservableCollection<NetRadioGroup> radioStations = new ObservableCollection<NetRadioGroup>();
    private IIoTDeviceService device;
    private bool isPlayVisible = false;
    private bool isPauseVisible = false;
    private bool isTurnOffVisible = false;
    private NetRadioGroup currentlyPlayingRadioStation;

    public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<NetRadioGroup> RadioStations
    {
      get
      {
        return radioStations;
      }
      set
      {
        radioStations = value;
        OnPropertyChanged(nameof(radioStations));
      }
    }
    public NetRadio SelectedRadioStation { get; private set; }
    public NetRadioGroup CurrentlyPlayingRadioStation
    {
      get => currentlyPlayingRadioStation;
      private set
      {
        currentlyPlayingRadioStation = value;
        OnPropertyChanged(nameof(CurrentlyPlayingRadioStation));
      }
    }

    public bool IsPlayVisible
    {
      get
      {
        return isPlayVisible;
      }

      private set
      {
        this.isPlayVisible = value;
        OnPropertyChanged(nameof(this.IsPlayVisible));
      }
    }
    public bool IsTurnOffVisible
    {
      get
      {
        return isTurnOffVisible;
      }

      private set
      {
        isTurnOffVisible = value;
        OnPropertyChanged(nameof(IsTurnOffVisible));
      }
    }
    public bool IsPauseVisible
    {
      get
      {
        return isPauseVisible;
      }

      private set
      {
        isPauseVisible = value;
        OnPropertyChanged(nameof(IsPauseVisible));
      }
    }

    public MainPageViewModel(NetRadioStationsService netRadioService, IIoTDeviceService iotDeviceService)
    {
      netRadioStationsService = netRadioService;
      netRadioStationsService.DataSynchronized += OnDataSynchronized;

      device = iotDeviceService;
      device.OpenConnection();

      DeviceEventProcessor.MessageFromDevice += OnMessageFromDevice;
      
      CallDeviceForStatus();
    }

    public void OnDisappearing()
    {
      netRadioStationsService.DataSynchronized -= OnDataSynchronized;
      DeviceEventProcessor.MessageFromDevice -= OnMessageFromDevice;
    }

    public async Task LoadNetRadios()
    {
      var result = await netRadioStationsService.GetRadioStationsFromSqliteAsync();

      var grouped = CreateGroupedRadioStations(result);
      RadioStations = new ObservableCollection<NetRadioGroup>(grouped);

      //to wynieść do eventu który tu jest invoke
      await netRadioStationsService.SyncWithCloud(result);
    }

    public void OnPropertyChanged(string name)
    {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public async Task Play()
    {
      await device.ExecuteCommand("play", "{\"Uri\": \"" + SelectedRadioStation.RadioUrl + "\"}");
    }

    public async Task Pause()
    {
      await device.ExecuteCommand("pause", "{}");
    }

    public async Task Shutdown()
    {
      await device.ExecuteCommand("shutdown", "{}");
    }

    public void CallDeviceForStatus()
    {
      device.ExecuteCommand("askforstate", "{}");
    }

    public void SelectRadiostation(NetRadio selectedRadio)
    {
      SelectedRadioStation = selectedRadio;
    }

    private void OnMessageFromDevice(Device2CloudMessage content)
    {
      switch (content.DeviceState)
      {
        case DeviceState.DeviceReady:
          IsPlayVisible = true;
          IsTurnOffVisible = true;
          IsPauseVisible = false;
          break;
        case DeviceState.Paused:
          IsPlayVisible = true;
          IsPauseVisible = false;
          IsTurnOffVisible = true;
          CurrentlyPlayingRadioStation = null;
          break;
        case DeviceState.Playing:
          IsPlayVisible = false;
          IsPauseVisible = true;
          IsTurnOffVisible = true;          
          CurrentlyPlayingRadioStation = RadioStations.FirstOrDefault(x => x.Any(z => z.RadioUrl == content.JsonPayload));
          break;
        case DeviceState.NotSet:
        case DeviceState.TurnedOff:
          AllButtonsDisabled();
          break;
        default:
          throw new NotImplementedException($"Property not implemented: {content.DeviceState.ToString()}");
      }
    }

    private void AllButtonsDisabled()
    {
      IsPlayVisible = false;
      IsPauseVisible = false;
      IsTurnOffVisible = false;
    }

    private void OnDataSynchronized(object sender, IList<NetRadio> radios)
    {
      IEnumerable<NetRadioGroup> grouped = CreateGroupedRadioStations(radios);
      RadioStations = new ObservableCollection<NetRadioGroup>(grouped);
    }

    private IEnumerable<NetRadioGroup> CreateGroupedRadioStations(IList<NetRadio> radiosInput) =>
      radiosInput.GroupBy(key => key.Folder)
      .Select(x => new NetRadioGroup(x.Key, x.ToList()));
    
  }
}
