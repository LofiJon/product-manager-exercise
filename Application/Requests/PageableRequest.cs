namespace Application.Requests;

public class PageableRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string Search { get; set; }
    public PageableRequest()
    {
        this.PageNumber = 1;
        this.PageSize = 10;
        this.Search = "";
    }
    public PageableRequest(int pageNumber, int pageSize, string search)
    {
        this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
        this.PageSize = pageSize > 10 ? 10 : pageSize;
        this.Search = search;
    }
}