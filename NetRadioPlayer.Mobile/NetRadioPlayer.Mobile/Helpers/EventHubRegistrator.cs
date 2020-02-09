using Microsoft.Azure.EventHubs.Processor;
using NetRadioPlayer.Mobile.Configuration;
using NetRadioPlayer.Mobile.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NetRadioPlayer.Mobile.Helpers
{
  public class EventHubRegistrator
  {
    private static EventProcessorHost eventProcessorHost;

    public static async Task Register()
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

    public static async Task Unregister()
    {
      if (eventProcessorHost != null)
      {
        await eventProcessorHost.UnregisterEventProcessorAsync();
      }
    }
  }
}
