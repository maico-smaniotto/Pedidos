namespace PedidosAPI.DTOs;

public record PageResponse<T>
(
    IEnumerable<T> Content,
    long TotalElements,
    int TotalPages,
    int PageSize,
    int Page
)
{
    public static PageResponse<T> Create(IEnumerable<T> content, int page, int pageSize, int totalElements = 0)
    {
        var list = content.ToList();

        if (totalElements == 0)
            totalElements = list.Count;

        return new PageResponse<T>(
            Content: list,
            TotalElements: totalElements,
            TotalPages: (int)Math.Ceiling((double)totalElements / pageSize),
            Page: page,
            PageSize: pageSize
        );
    }
}