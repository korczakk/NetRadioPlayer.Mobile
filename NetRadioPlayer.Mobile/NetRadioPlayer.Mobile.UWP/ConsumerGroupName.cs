using NetRadioPlayer.Mobile.Configuration;
using NetRadioPlayer.Mobile.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(ConsumerGroupName))]
namespace NetRadioPlayer.Mobile.UWP
{
  public class ConsumerGroupName : IConsumerGroupName
  {
    public string CreateConsumerGroupName()
    {
      return "uwpapp";
    }
  }
}
