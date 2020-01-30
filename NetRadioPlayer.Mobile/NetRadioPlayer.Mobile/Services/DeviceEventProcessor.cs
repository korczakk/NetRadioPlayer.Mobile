using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using NetRadioPlayer.Mobile.Model;
using Newtonsoft.Json;

namespace NetRadioPlayer.Mobile.Services
{
  public class DeviceEventProcessor : IEventProcessor
  {
    public Task CloseAsync(PartitionContext context, CloseReason reason)
    {
      return Task.CompletedTask;
    }

    public static event DeviceEventHandler MessageFromDevice;

    public delegate void DeviceEventHandler(Device2CloudMessage content);

    public Task OpenAsync(PartitionContext context)
    {
      return Task.CompletedTask;
    }

    public Task ProcessErrorAsync(PartitionContext context, Exception error)
    {
      return Task.CompletedTask;
    }

    public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
    {
      var messagesFromDevice = new List<Device2CloudMessage>();

      foreach (var eventData in messages)
      {
        var data = Encoding.ASCII.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);        
        Device2CloudMessage devMessage = JsonConvert.DeserializeObject<Device2CloudMessage>(data);
        messagesFromDevice.Add(devMessage);
      }

      var lastMessage = messagesFromDevice.LastOrDefault(x => x.CheckIsMessageValid(TimeSpan.FromMinutes(3)));
      MessageFromDevice.Invoke(lastMessage ?? new Device2CloudMessage("", DeviceState.NotSet, new MediaPlayerState(String.Empty, 0)));

      return context.CheckpointAsync();
    }
  }
}
