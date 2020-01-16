using Microsoft.WindowsAzure.Storage.Table;
using NetRadioPlayer.Mobile.Model;
using System.Collections.Generic;
using NetRadioPlayer.Mobile.Helpers;
using System.Threading.Tasks;
using NetRadioPlayer.Mobile.Persistance;
using System.Linq;
using SQLite;
using System;

namespace NetRadioPlayer.Mobile.Services
{
  /// <summary>
  /// Class is responsible for handlind list of radio stations both in the cloud and in local database.
  /// </summary>
  public class NetRadioStationsService
  {
    private readonly ITableStorageHelper tableHelper;
    private readonly SQLiteAsyncConnection dbContext;

    public event DataSyncEventHandler DataSynchronized;
    public delegate void DataSyncEventHandler(object sender, IList<NetRadio> e);

    public NetRadioStationsService(ITableStorageHelper tableHelper)
    {
      this.tableHelper = tableHelper;
      dbContext = SqliteDatabaseHandler.DbContext;
    }

    public async Task<IList<NetRadio>> GetRadioStationsFromSqliteAsync()
    {
      List<NetRadio> result = await dbContext.QueryAsync<NetRadio>("SELECT * FROM NetRadio");

      return result;
    }

    public async Task SyncWithCloud(IList<NetRadio> currentRadioStations)
    {
      var radioStationsInCloud = await GetRadioStationsFromAzure();

      bool compareREsult = radioStationsInCloud.SequenceEqual<NetRadio>(currentRadioStations);
      if (compareREsult)
      {
        return;
      }

      await UpdateDataInDatabase(radioStationsInCloud);

      DataSynchronized?.Invoke(this, radioStationsInCloud);
    }


    private async Task<IList<NetRadio>> GetRadioStationsFromAzure()
    {
      CloudTable tableRef = tableHelper.GetTableReference("NetRadioStations");

      TableContinuationToken continuationToken = null;
      var netRadios = new List<NetRadio>();

      var tableQuery = new Microsoft.WindowsAzure.Storage.Table.TableQuery<NetRadio>();

      do
      {
        var result = await tableRef.ExecuteQuerySegmentedAsync<NetRadio>(tableQuery, continuationToken);
        netRadios.AddRange(result.Results);

      } while (continuationToken != null);

      return netRadios;
    }

    private async Task SaveInSqlite(IList<NetRadio> radios)
    {
      await dbContext.InsertAllAsync(radios);
    }

    private async Task UpdateDataInDatabase(IEnumerable<NetRadio> radios)
    {
      await dbContext.DeleteAllAsync<NetRadio>();
      await dbContext.InsertAllAsync(radios);
    }
  }
}
