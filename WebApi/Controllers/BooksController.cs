using Azure;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Repositories.EfCore;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IRepositoryManager _manager;

    public BooksController(IRepositoryManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public IActionResult GetAllBooks()
    {
        try
        {
            var books = _manager.Book.GetAllBooks(false);
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
            var book = _manager.Book.GetOneBookById(id, false);
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
            _manager.Book.CreateOneBook(book);
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
            var entity = _manager.Book.GetOneBookById(id, true);
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
            _manager.Save();
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
            var entity = _manager.Book.GetOneBookById(id, false);
            if (entity is null)
            {
                return NotFound(new
                {
                    statusCode = 400,
                    message = $"Book with id:{id} could not found"
                });
            }
            _manager.Book.DeleteOneBook(entity);
            _manager.Save();
            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
