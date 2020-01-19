using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Linq;
using NetRadioPlayer.Mobile.Model;
using NetRadioPlayer.Mobile.Services;
using NetRadioPlayer.Mobile.Helpers;
using NetRadioPlayer.Mobile.UIStrategies;

namespace NetRadioPlayer.Mobile.ViewModels
{
  public class MainPageViewModel : INotifyPropertyChanged
  {
    private NetRadioStationsService netRadioStationsService;
    private ObservableCollection<NetRadioGroup> radioStations = new ObservableCollection<NetRadioGroup>();
    private IIoTDeviceService device;
    private readonly UIVisibilityStrategyFactory visibilityStrategyFactory;
    private bool isPlayVisible = false;
    private bool isPauseVisible = false;
    private bool isTurnOffVisible = false;
    private NetRadio currentlyPlayingRadioStation;
    private IList<NetRadio> netRadiosFromDb;

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
        OnPropertyChanged(nameof(RadioStations));
      }
    }
    public NetRadio SelectedRadioStation { get; private set; }
    public NetRadio CurrentlyPlayingRadioStation
    {
      get => currentlyPlayingRadioStation;
      private set
      {
        currentlyPlayingRadioStation = value;
        OnPropertyChanged(nameof(CurrentlyPlayingRadioStation));
      }
    }

    private IUIVisibilityStrategy uiStrategy;

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

    public MainPageViewModel(NetRadioStationsService netRadioService, IIoTDeviceService iotDeviceService, UIVisibilityStrategyFactory visibilityStrategyFactory)
    {
      netRadioStationsService = netRadioService;
      netRadioStationsService.DataSynchronized += OnDataSynchronized;

      device = iotDeviceService;
      device.OpenConnection();

      DeviceEventProcessor.MessageFromDevice += OnMessageFromDevice;

      this.visibilityStrategyFactory = visibilityStrategyFactory;
    }

    public async Task LoadNetRadiosFromDb()
    {
      netRadiosFromDb = await netRadioStationsService.GetRadioStationsFromSqliteAsync();

      RadioStations = new ObservableCollection<NetRadioGroup>(netRadiosFromDb.ToNetRadioGroup());
    }

    public async Task SyncDataWithAzure()
    {
      await netRadioStationsService.SyncWithCloud(netRadiosFromDb);
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

    public async Task CallDeviceForStatus()
    {
      try
      {
        await device.ExecuteCommand("askforstate", "{}");
      }
      catch (Exception)
      {
        //show notification that device is unreachable
      }
    }

    public void SelectRadiostation(NetRadio selectedRadio)
    {
      SelectedRadioStation = selectedRadio;

      IsPlayVisible = uiStrategy.PlayButtonVisibility(CurrentlyPlayingRadioStation?.RadioUrl, SelectedRadioStation?.RadioUrl);
    }

    private void OnMessageFromDevice(Device2CloudMessage content)
    {
      uiStrategy = visibilityStrategyFactory.CreateStrategy(content);

      IsPlayVisible = uiStrategy.PlayButtonVisibility(content.JsonPayload, SelectedRadioStation?.RadioUrl);
      IsPauseVisible = uiStrategy.PauseButtonVisibility();
      IsTurnOffVisible = uiStrategy.ShutdownButtonVisibility();
      CurrentlyPlayingRadioStation = uiStrategy.ShouldClearCurrentlyPlaying()
        ? null
        : RadioStations
            .SelectMany(x => x.RadioStations)
            .Where(x => x.RadioUrl == content.JsonPayload)
            .FirstOrDefault();        
    }

    private void OnDataSynchronized(object sender, IList<NetRadio> radios)
    {
      RadioStations = new ObservableCollection<NetRadioGroup>(radios.ToNetRadioGroup());
    }
  }
}
