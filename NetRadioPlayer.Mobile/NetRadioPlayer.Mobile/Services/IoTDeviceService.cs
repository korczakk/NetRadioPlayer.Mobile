using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using NetRadioPlayer.Mobile.Configuration;
using NetRadioPlayer.Mobile.Services;

[assembly: Xamarin.Forms.Dependency(typeof(IoTDeviceService))]
namespace NetRadioPlayer.Mobile.Services
{
  public class IoTDeviceService : IIoTDeviceService
  {
    private ServiceClient device;

    public int MethodStatus { get; set; }

    public void OpenConnection()
    {
      var cn = Appsettings.Settings["IotHubServiceConnectionString"];
      device = ServiceClient.CreateFromConnectionString(cn, TransportType.Amqp);
    }

    public async Task ExecuteCommand(string methodName, string jsonPayload)
    {
      string deviceId = Appsettings.Settings["DeviceID"];

      var method = new CloudToDeviceMethod(methodName, TimeSpan.FromSeconds(10));
      method.SetPayloadJson(jsonPayload);

      var result = await device.InvokeDeviceMethodAsync(deviceId, method);

      MethodStatus = result.Status;
    }
  }
}
