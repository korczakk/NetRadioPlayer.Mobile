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

    public string JsonPayload { get; set; }

    public DateTime EventTime { get; set; }
    public Device2CloudMessage(string message, DeviceState state, string jsonPayload)
    {
      MessageContent = message;
      DeviceState = state;
      JsonPayload = jsonPayload;
    }

    public bool CheckIsMessageValid(TimeSpan span)
    {
      var diff = DateTime.UtcNow.Subtract(this.EventTime);

      return diff < span;
    }
  }
}
