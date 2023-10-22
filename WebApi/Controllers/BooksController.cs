using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly RepositoryContext _context;

    public BooksController(RepositoryContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllBooks()
    {
        var books = _context.Books.ToList();
        return Ok(books);
    }
}
