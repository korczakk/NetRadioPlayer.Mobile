using NetRadioPlayer.Mobile.Model;
using NetRadioPlayer.Mobile.Persistance;
using NetRadioPlayer.Mobile.ViewModels;
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
    private MainVm viewModel;

    public MainPage()
    {
      InitializeComponent();
      viewModel = new MainVm();
      
    }

    protected async override void OnAppearing()
    {
      var db = await SqliteDatabaseHandler.GetDatabase();

      await db.InsertAsync(new NetRadio { RadioName = "Test" });
      var test = await db.QueryAsync<NetRadio>("select * from NetRadio");

      base.OnAppearing();
    }
  }
}
