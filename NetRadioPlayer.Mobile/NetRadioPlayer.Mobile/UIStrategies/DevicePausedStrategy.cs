using NetRadioPlayer.Mobile.Model;

namespace NetRadioPlayer.Mobile.UIStrategies
{
  public class DevicePausedStrategy : IUIVisibilityStrategy
  {
    public bool PauseButtonVisibility() => false;

    public bool PlayButtonVisibility(string currentlyPlayingUrl, string currentlySelectedStationUrl)
      => true;

    public bool ShouldClearCurrentlyPlaying() => true;

    public bool ShowVolumeButton() => true;

    public bool ShutdownButtonVisibility() => true;
  }
}
