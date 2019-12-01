using NetRadioPlayer.Mobile.Persistance;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetRadioPlayer.Mobile
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();

      SqliteDatabaseHandler.LoadSqliteDatabase(DependencyService.Get<IDbPath>());
      MainPage = new MainPage();
    }

    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }
  }
}
