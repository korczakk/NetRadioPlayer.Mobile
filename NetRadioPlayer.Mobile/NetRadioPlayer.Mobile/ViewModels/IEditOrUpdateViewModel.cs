using System.Threading.Tasks;

namespace NetRadioPlayer.Mobile.ViewModels
{
  public interface IEditOrUpdateViewModel
  {
    string PageTitle { get; set; }
    Task Save();
  }
}
