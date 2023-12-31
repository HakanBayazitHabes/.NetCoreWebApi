using Entities.RequestFeatures;

namespace Entities.DataTransferObjects;

public record LinkParameters
{
    public BookParameters BookParameters { get; init; }
    public HttpContent HttpContext { get; init; }
}
