using System;
using System.Collections.Generic;
using System.Text;

namespace NetRadioPlayer.Mobile.UIStrategies
{
  public class DeviceTurnedoffStrategy : IUIVisibilityStrategy
  {
    public bool PauseButtonVisibility() => false;

    public bool PlayButtonVisibility(string currentlyPlayingUrl, string currentlySelectedStationUrl)
      => false;

    public bool ShouldClearCurrentlyPlaying() => true;

    public bool ShutdownButtonVisibility() => false;
  }
}
