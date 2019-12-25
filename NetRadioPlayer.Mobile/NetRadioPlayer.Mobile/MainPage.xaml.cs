using NetRadioPlayer.Mobile.Model;
using NetRadioPlayer.Mobile.Persistance;
using NetRadioPlayer.Mobile.Services;
using NetRadioPlayer.Mobile.ViewModels;
using NetRadioPlayer.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NetRadioPlayer.Mobile
{
  // Learn more about making custom code visible in the Xamarin.Forms previewer
  // by visiting https://aka.ms/xamarinforms-previewer
  [DesignTimeVisible(false)]
  public partial class MainPage : ContentPage
  {
    private MainPageViewModel viewModel;

    public MainPage()
    {
      InitializeComponent();

      var netRadioService = new NetRadioStationsService(DependencyService.Resolve<ITableStorageHelper>());
      var iotDeviceService = DependencyService.Resolve<IIoTDeviceService>();
      viewModel = new MainPageViewModel(netRadioService, iotDeviceService);

      BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {    
      base.OnAppearing();   
    }

    protected override async void OnBindingContextChanged()
    {
      await viewModel.LoadNetRadios();
      base.OnBindingContextChanged();
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
  }
}
