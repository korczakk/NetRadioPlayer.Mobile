using NetRadioPlayer.Mobile.Persistance;
using Xamarin.Forms;
using Xamarin.Essentials;
using NetRadioPlayer.Mobile.Helpers;

namespace NetRadioPlayer.Mobile
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();
      SqliteDatabaseHandler.LoadSqliteDatabase(DependencyService.Get<IDbPath>());

      Connectivity.ConnectivityChanged += ConnectivityChanged;

      if (Connectivity.NetworkAccess == NetworkAccess.Internet)
        MainPage = new MainPage();
      else
        MainPage = new NoConnectionPage();      
    }

    private async void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
      if (Connectivity.NetworkAccess == NetworkAccess.Internet)
      {
        MainPage = new MainPage();
        await EventHubRegistrator.Register();
      }
      else
      {
        MainPage = new NoConnectionPage();
      }
    }

    protected async override void OnStart()
    {
      if (Connectivity.NetworkAccess == NetworkAccess.Internet)
        await EventHubRegistrator.Register();
    }

    protected async override void OnSleep()
    {
      await EventHubRegistrator.Unregister();
    }

    protected override async void OnResume()
    {
      if (Connectivity.NetworkAccess == NetworkAccess.Internet)
        await EventHubRegistrator.Register();
    }
  }
}
