using NetRadioPlayer.Mobile.Helpers;
using NetRadioPlayer.Mobile.Services;
using NetRadioPlayer.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetRadioPlayer.Mobile
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class AddNewStationPage : ContentPage
  {
    private AddRadioStationViewModel vm;

    public AddNewStationPage()
    {
      InitializeComponent();

      var netRadioService = new NetRadioStationsService(DependencyService.Resolve<ITableStorageHelper>());
      vm = new AddRadioStationViewModel(netRadioService);

      BindingContext = vm;
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
      try
      {
        await vm.Save();
        
      }
      catch (Exception)
      {
        await DisplayAlert("Error", "Data not saved!", "OK");
      }

      await Navigation.PopModalAsync();
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
      await Navigation.PopModalAsync(true);
    }
  }
}