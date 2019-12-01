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

      var netRadioService = new NetRadioStationsService(new TableStorageHelper());
      viewModel = new MainPageViewModel(netRadioService);
      BindingContext = viewModel;
    }

    protected async override void OnAppearing()
    {
      await viewModel.LoadNetRadios();

      base.OnAppearing();
    }
  }
}
