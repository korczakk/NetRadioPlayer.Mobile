using NetRadioPlayer.Mobile.Model;
using NetRadioPlayer.Mobile.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace NetRadioPlayer.Mobile.ViewModels
{
  public class AddRadioStationViewModel : INotifyPropertyChanged
  {
    private readonly NetRadioStationsService netradioService;
    private string radioName;
    private string radioUrl;
    private string folderName;

    public event PropertyChangedEventHandler PropertyChanged;

    public string RadioName
    { 
      get
      {
        return radioName;
      }
      set
      {
        radioName = value;
        OnPropertyChanged(nameof(CanSave));
      }
    }
    public string RadioUrl
    {
      get
      {
        return radioUrl;
      }
      set
      {
        radioUrl = value;
        OnPropertyChanged(nameof(CanSave));
      }
    }
    public string FolderName 
    { 
      get
      {
        return folderName;
      }
      set
      {
        folderName = value;
        OnPropertyChanged(nameof(CanSave));
      }
    }

    public bool CanSave
    { 
      get
      {
        return !String.IsNullOrEmpty(RadioName)
          && !String.IsNullOrEmpty(RadioUrl)
          && !String.IsNullOrEmpty(FolderName);
      }
    }

    public AddRadioStationViewModel(NetRadioStationsService netradioService)
    {
      this.netradioService = netradioService;
    }

    public async Task Save()
    {
      var radio = new NetRadio(RadioName, RadioUrl, FolderName);

      await netradioService.AddRadioStationToAzure(radio);
    }

    private void OnPropertyChanged(string name)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
  }
}
