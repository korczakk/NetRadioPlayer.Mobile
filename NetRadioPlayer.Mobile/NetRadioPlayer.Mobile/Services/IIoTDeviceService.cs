using System.Threading.Tasks;

namespace NetRadioPlayer.Mobile.Services
{
  public interface IIoTDeviceService
  {
    int MethodStatus { get; set; }

    Task ExecuteCommand(string methodName, string jsonPayload);
    void OpenConnection();
  }
}