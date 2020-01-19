using NetRadioPlayer.Mobile.Model;
using System;

namespace NetRadioPlayer.Mobile.UIStrategies
{
  public class UIVisibilityStrategyFactory
  {
    public IUIVisibilityStrategy CreateStrategy(Device2CloudMessage message)
    {
      switch (message.DeviceState)
      {
        case DeviceState.DeviceReady:
          return new DeviceReadyStrategy();
        case DeviceState.Paused:
          return new DevicePausedStrategy();
        case DeviceState.Playing:
          return new DevicePlayingStrategy();
        case DeviceState.NotSet:          
        case DeviceState.TurnedOff:
          return new DeviceTurnedoffStrategy();
        default:
          throw new NotImplementedException($"Strategy not implemented: {message.DeviceState.ToString()}");
      }
    }
  }
}
