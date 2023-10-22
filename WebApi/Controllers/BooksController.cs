using System.IO.Compression;
using Entities.Models;
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
        try
        {
            var books = _context.Books.ToList();
            return Ok(books);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
    {
        try
        {
            var book = _context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();
            if (book is null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateOneBook([FromBody] Book book)
    {
        try
        {
            if (book is null)
            {
                return BadRequest();
            }
            _context.Books.Add(book);
            _context.SaveChanges();
            return StatusCode(201, book);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
    {
        try
        {
            if (book is null)
            {
                return BadRequest();
            }
            var entity = _context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();
            if (entity is null)
            {
                return NotFound();
            }
            if (id != book.Id)
            {
                return BadRequest();
            }

            entity.Title = book.Title;
            entity.Price = book.Price;
            _context.SaveChanges();
            return Ok(book);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
    {
        try
        {
            var entity = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();
            if (entity is null)
            {
                return NotFound(new
                {
                    statusCode = 400,
                    message = $"Book with id:{id} could not found"
                });
            }
            _context.Books.Remove(entity);
            _context.SaveChanges();
            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
