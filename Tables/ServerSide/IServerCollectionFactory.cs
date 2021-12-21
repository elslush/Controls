using Controls.Paging;
using Controls.QueryOptions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.ServerSide;

public abstract class IServerCollectionFactory<T>
{
    private readonly QueryProperties queryProperties = new();

    public async ValueTask<ItemsProviderResult<T>> LoadItems(ItemsProviderRequest request)
    {
        if (!IsValid)
            return new ItemsProviderResult<T>(Array.Empty<T>(), 0);

        var numRows = Math.Min(request.Count, queryProperties.Pagination.Take);
        queryProperties.Pagination = new()
        {
            Skip = queryProperties.Pagination.Skip + request.StartIndex,
            Take = numRows,
        };

        var collection = await GetCollection(queryProperties, request.CancellationToken);
        ItemCount = collection.Count;

        //rowSelectState.AllItems = rowCollection.Rows;

        var total = Math.Min(collection.Count - queryProperties.Pagination.Skip, queryProperties.Pagination.Take);
        return new ItemsProviderResult<T>(collection.Items, total);
    }

    public event Action? OnRefreshData;

    private int ItemCount { get; set; } = 0;

    private void OnPage(Pagination pagination)
    {
        queryProperties.Pagination = pagination;
        OnRefreshData?.Invoke();
    }

    private async Task OnSortAdd(IEnumerable<Sort> sort)
    {
        queryProperties.Sorts.UnionWith(sort);
        OnRefreshData?.Invoke();
    }

    private async Task OnSortRemove(IEnumerable<Sort> sort)
    {
        queryProperties.Sorts.ExceptWith(sort);
        OnRefreshData?.Invoke();
    }

    private async Task OnGlobalSort(string filterText)
    {
        queryProperties.GlobalFilter = filterText;
        OnRefreshData?.Invoke();
    }

    protected abstract Task<IServerCollection<T>> GetCollection(QueryProperties queryProperties, CancellationToken cancellationToken);

    protected bool IsValid { get; }
}