using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using NetRadioPlayer.Mobile.Configuration;

namespace NetRadioPlayer.Mobile.Services
{
  public class IoTDeviceService
  {
    private ServiceClient device;
    
    public string MethodResult { get; set; }
    public int MethodStatus { get; set; }

    public void OpenConnection()
    {
      var cn = Appsettings.Settings["DeviceConnectionString"];
      device = ServiceClient.CreateFromConnectionString(cn);
    }

    public async Task ExecuteCommand(string methodName, string jsonPayload)
    {
      var method = new CloudToDeviceMethod(methodName, TimeSpan.FromSeconds(10));
      method.SetPayloadJson(jsonPayload);

      var result = await device.InvokeDeviceMethodAsync("deviceid", method);

      MethodResult = result.GetPayloadAsJson();
      MethodStatus = result.Status;
    }
  }
}
