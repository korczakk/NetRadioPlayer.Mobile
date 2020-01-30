using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace NetRadioPlayer.Mobile.Model
{
  public class Device2CloudMessage
  {
    public string MessageContent { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public DeviceState DeviceState { get; set; }

    public MediaPlayerState PlayerState { get; set; }

    public DateTime EventTime { get; set; }
    public Device2CloudMessage(string message, DeviceState state, MediaPlayerState playerState)
    {
      MessageContent = message;
      DeviceState = state;
      PlayerState = playerState;
    }

    public bool CheckIsMessageValid(TimeSpan span)
    {
      var diff = DateTime.UtcNow.Subtract(this.EventTime);

      return diff < span;
    }
  }
}
