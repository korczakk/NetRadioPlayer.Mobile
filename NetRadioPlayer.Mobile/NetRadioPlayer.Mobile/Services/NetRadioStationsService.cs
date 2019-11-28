using Microsoft.WindowsAzure.Storage.Table;
using NetRadioPlayer.Mobile.Model;
using System.Collections.Generic;
using NetRadioPlayer.Mobile.Helpers;
using System.Threading.Tasks;

namespace NetRadioPlayer.Mobile.Services
{
  class NetRadioStationsService
  {
    private readonly ITableStorageHelper tableHelper;

    public NetRadioStationsService(ITableStorageHelper tableHelper)
    {
      this.tableHelper = tableHelper;
    }

    public async Task<IEnumerable<NetRadio>> GetRadioStations()
    {
      CloudTable tableRef = tableHelper.GetTableReference("NetRadioStations");

      TableContinuationToken continuationToken = null;
      var netRadios = new List<NetRadio>();

      do
      {
        var result = await tableRef.ExecuteQuerySegmentedAsync<NetRadio>(new TableQuery<NetRadio>(), continuationToken);
        netRadios.AddRange(result.Results);

      } while (continuationToken != null);

      return netRadios;
    }
  }
}
