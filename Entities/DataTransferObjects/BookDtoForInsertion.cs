using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public record BookDtoForInsertion : BookDtoForManipulation
{
    [Required(ErrorMessage = "The CategoryId is required")]
    public int CategoryId { get; init; }
}
