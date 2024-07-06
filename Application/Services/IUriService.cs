using Application.Requests;

namespace Application.Services;

public interface IUriService
{
    public Uri GetPageUri(PageableRequest pageableRequest, string? route);
}
