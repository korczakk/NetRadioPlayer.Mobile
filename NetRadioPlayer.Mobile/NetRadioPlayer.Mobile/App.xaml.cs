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

      MainPage = new MainPage();
    }

    protected async override void OnStart()
    {
      await SqliteDatabaseHandler.LoadSqliteDatabase();

      // Handle when your app starts
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
