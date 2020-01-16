using NetRadioPlayer.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRadioPlayer.Mobile.Helpers
{
  public static class NetRadioExtension
  {
    public static IEnumerable<NetRadioGroup> ToNetRadioGroup(this IList<NetRadio> netRadios)
    {
      return netRadios.GroupBy(key => key.Folder)
        .Select(x => new NetRadioGroup(x.Key, x.ToList()));
    }
  }
}
