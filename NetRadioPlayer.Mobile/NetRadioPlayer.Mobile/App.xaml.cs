using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using NetRadioPlayer.Mobile.Configuration;
using NetRadioPlayer.Mobile.Persistance;
using NetRadioPlayer.Mobile.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetRadioPlayer.Mobile
{
  public partial class App : Application
  {
    private EventProcessorHost eventProcessorHost;

    public App()
    {
      InitializeComponent();

      SqliteDatabaseHandler.LoadSqliteDatabase(DependencyService.Get<IDbPath>());
      MainPage = new MainPage();
    }

    protected async override void OnStart()
    {
      string consumerGroupName = DependencyService.Resolve<IConsumerGroupName>().CreateConsumerGroupName();

      var eventHubName = Appsettings.Settings["EventHubName"];
      var eventHubCN = Appsettings.Settings["EventHubConnectionString"];
      var eventStorageCN = Appsettings.Settings["EventStorageConnectionString"];
      var eventContainerName = Appsettings.Settings["EventStorageContainerName"];
      eventProcessorHost = new EventProcessorHost(
        eventHubName,
        consumerGroupName,
        eventHubCN,
        eventStorageCN,
        eventContainerName);
      await eventProcessorHost.RegisterEventProcessorAsync<DeviceEventProcessor>();
    }

    protected async override void OnSleep()
    {
      await eventProcessorHost.UnregisterEventProcessorAsync();
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }
  }
}
