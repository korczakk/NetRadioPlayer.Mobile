using NetRadioPlayer.Mobile.Model;
using NetRadioPlayer.Mobile.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace NetRadioPlayer.Mobile.ViewModels
{
  public class EditRadioStationViewModel : INotifyPropertyChanged, IEditOrUpdateViewModel
  {
    private readonly NetRadioStationsService netradioService;
    private readonly NetRadio radioToEdit;
    private readonly string rowKey;
    private readonly string partitionKey;
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
        OnPropertyChanged(nameof(RadioName));
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
        OnPropertyChanged(nameof(RadioUrl));
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
        OnPropertyChanged(nameof(FolderName));
      }
    }

    public bool CanSave
    {
      get => true;
    }
    public string PageTitle { get; set; }

    public EditRadioStationViewModel(NetRadioStationsService netradioService, NetRadio radioToEdit)
    {
      this.netradioService = netradioService;
      this.radioToEdit = radioToEdit;
      this.rowKey = radioToEdit.RowKey;
      this.partitionKey = radioToEdit.PartitionKey;

      RadioName = radioToEdit.RadioName;
      RadioUrl = radioToEdit.RadioUrl;
      FolderName = radioToEdit.Folder;
    }

    public async Task Save()
    {
      var radioToDelete = new NetRadio()
      {
        RowKey = rowKey,
        PartitionKey = partitionKey,
        ETag = "*"
      };
      await netradioService.DeleteRadioStation(radioToDelete);

      var newRadioData = new NetRadio(radioName, radioUrl, folderName);
      await netradioService.AddRadioStation(newRadioData);
    }

    private void OnPropertyChanged(string name)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
  }
}
