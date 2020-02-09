using NetRadioPlayer.Mobile.Model;
using NetRadioPlayer.Mobile.Services;
using NetRadioPlayer.Mobile.ViewModels;
using NetRadioPlayer.Mobile.Helpers;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using NetRadioPlayer.Mobile.UIStrategies;

namespace NetRadioPlayer.Mobile
{
  // Learn more about making custom code visible in the Xamarin.Forms previewer
  // by visiting https://aka.ms/xamarinforms-previewer
  [DesignTimeVisible(false)]
  public partial class MainPage : ContentPage
  {
    private MainPageViewModel viewModel;
    private readonly NetRadioStationsService netRadioService;

    public MainPage()
    {
      InitializeComponent();

      netRadioService = new NetRadioStationsService(DependencyService.Resolve<ITableStorageHelper>());
      var iotDeviceService = DependencyService.Resolve<IIoTDeviceService>();
      viewModel = new MainPageViewModel(netRadioService, iotDeviceService, new UIVisibilityStrategyFactory());

      BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
      base.OnAppearing();

      await InitialLoad();
    }

    private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
      viewModel.SelectRadiostation(e.SelectedItem as NetRadio);
    }

    private void Play_Clicked(object sender, EventArgs e)
    {
      Task.Run(() => viewModel.Play());
    }

    private void Pause_clicked(object sender, EventArgs e)
    {
      Task.Run(() => viewModel.Pause());
    }

    private void Shutdown_clicked(object sender, EventArgs e)
    {
      Task.Run(() => viewModel.Shutdown());
    }

    private async void Refresh_Clicked(object sender, EventArgs e)
    {
      await InitialLoad();
    }

    private async void AddNew_Clicked(object sender, EventArgs e)
    {
      var vm = new AddRadioStationViewModel(netRadioService);
      vm.PageTitle = "Add new radio station";

      var modal = new AddOrEditStationPage(vm);

      await Navigation.PushModalAsync(modal);
    }

    private async void Edit_Clicked(object sender, EventArgs e)
    {
      var vm = new EditRadioStationViewModel(netRadioService, (sender as MenuItem).CommandParameter as NetRadio);
      vm.PageTitle = "Update existing radio";

      var modal = new AddOrEditStationPage(vm);

      await Navigation.PushModalAsync(modal);
    }

    private async Task InitialLoad()
    {
      try
      {
        await viewModel.CallDeviceForStatus();
      }
      catch
      {
        await DisplayAlert("Alert", "Device is not responding", "OK");
      }

      await viewModel.LoadNetRadiosFromDb();

      await viewModel.SyncDataWithAzure();
    }

    private async void VolumeChanged(object sender, ValueChangedEventArgs e)
    {
      await viewModel.SetVolume();
    }

    private void VolumeButton_Clicked(object sender, EventArgs e)
    {
      viewModel.ShowVolume = !viewModel.ShowVolume;
    }
  }
}
