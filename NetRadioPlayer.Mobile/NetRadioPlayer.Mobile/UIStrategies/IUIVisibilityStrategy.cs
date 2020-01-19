using NetRadioPlayer.Mobile.Model;

namespace NetRadioPlayer.Mobile.UIStrategies
{
  public interface IUIVisibilityStrategy
  {
    bool PlayButtonVisibility(string currentlyPlayingUrl, string currentlySelectedStationUrl);
    bool PauseButtonVisibility();
    bool ShutdownButtonVisibility();
    bool ShouldClearCurrentlyPlaying();
  }
}
