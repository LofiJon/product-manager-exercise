using Application.Dtos;
using Application.Requests;
using Application.Services;

namespace Infrastructure.Helpers;

public class PageableHelper
{
    public static PageableDto<T> CreatePagedReponse<T>(List<T> pagedData, PageableRequest validFilter, int totalRecords, IUriService uriService, string? route)
    {
        var respose = new PageableDto<T>(pagedData, validFilter.PageNumber, validFilter.PageSize);
        var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
        int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
        respose.NextPage =
            validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PageableRequest(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.Search), route)
                : null;
        respose.PreviousPage =
            validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PageableRequest(validFilter.PageNumber - 1, validFilter.PageSize, validFilter.Search), route)
                : null;
        respose.FirstPage = uriService.GetPageUri(new PageableRequest(1, validFilter.PageSize, validFilter.Search), route);
        respose.LastPage = uriService.GetPageUri(new PageableRequest(roundedTotalPages, validFilter.PageSize, validFilter.Search), route);
        respose.TotalPages = roundedTotalPages;
        respose.TotalRecords = totalRecords;
        return respose;
    }
}