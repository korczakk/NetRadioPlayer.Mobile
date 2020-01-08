using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NetRadioPlayer.Mobile.Model
{
  public class Device2CloudMessage
  {
    public string MessageContent { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public DeviceState DeviceState { get; set; }

    public string JsonPayload { get; set; }

    public Device2CloudMessage(string message, DeviceState state, string jsonPayload)
    {
      MessageContent = message;
      DeviceState = state;
      JsonPayload = jsonPayload;
    }
  }
}
