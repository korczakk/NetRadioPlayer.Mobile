using NetRadioPlayer.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetRadioPlayer.Mobile.UIStrategies
{
  public class DeviceReadyStrategy : IUIVisibilityStrategy
  {
    public bool PauseButtonVisibility() => false;

    public bool PlayButtonVisibility(string currentlyPlayingUrl, string currentlySelectedStationUrl)
      => !String.IsNullOrEmpty(currentlySelectedStationUrl);

    public bool ShouldClearCurrentlyPlaying() => false;

    public bool ShutdownButtonVisibility() => true;

    public bool ShowVolumeButton() => true;
  }
}
