using Application.Requests;
using Application.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace Infrastructure.Pageable;

public class UriAdapter : IUriService
{
    private readonly string _baseUri;

    public UriAdapter(string baseUri)
    {
        _baseUri = baseUri;
    }
    public Uri GetPageUri(PageableRequest filter, string? route)
    {
        var _enpointUri = new Uri(string.Concat(_baseUri, route));
        var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
        modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
        return new Uri(modifiedUri);
    }
}
