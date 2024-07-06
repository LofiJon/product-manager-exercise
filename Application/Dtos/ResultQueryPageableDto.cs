namespace Application.Dtos;

public class ResultQueryPageableDto<TContent>
{
    public List<TContent> Data { get; set; }
    public int TotalRecords { get; set; }

    public ResultQueryPageableDto(List<TContent> data, int TotalRecords)
    {
        this.Data = data;
        this.TotalRecords = TotalRecords;
    }
}
