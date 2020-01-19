using NetRadioPlayer.Mobile.Model;
using NetRadioPlayer.Mobile.UIStrategies;
using System;

namespace NetRadioPlayer.Mobile.UIStrategies
{
  public class DevicePlayingStrategy : IUIVisibilityStrategy
  {    
    public bool PauseButtonVisibility() => true;

    public bool PlayButtonVisibility(string currentlyPlayingUrl, string currentlySelectedStationUrl)
      => !String.IsNullOrEmpty(currentlySelectedStationUrl) && currentlyPlayingUrl != currentlySelectedStationUrl;

    public bool ShouldClearCurrentlyPlaying() => false;

    public bool ShutdownButtonVisibility() => true;
  }
}
