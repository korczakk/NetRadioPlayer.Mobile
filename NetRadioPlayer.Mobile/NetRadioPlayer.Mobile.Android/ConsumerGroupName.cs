using NetRadioPlayer.Mobile.Configuration;
using NetRadioPlayer.Mobile.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(ConsumerGroupName))]
namespace NetRadioPlayer.Mobile.Droid
{
  public class ConsumerGroupName : IConsumerGroupName
  {
    public string CreateConsumerGroupName()
    {
      return "androidapp";
    }
  }
}