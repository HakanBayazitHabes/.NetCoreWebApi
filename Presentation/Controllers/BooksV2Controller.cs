using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers;

[ApiVersion("2.0")]
[ApiController]
[Route("api/{v:apiversion}/books")]
public class BooksV2Controller : ControllerBase
{
    private readonly IServiceManager _manager;

    public BooksV2Controller(IServiceManager manager)
    {
        _manager = manager;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllBooksAsync()
    {
        var books = await _manager.BookService.GetAllBooksAsync(false);
        var bookV2 = books.Select(b => new
        {
            Title = b.Title,
            Id = b.Id
        });
        return Ok(bookV2);
    }
}
