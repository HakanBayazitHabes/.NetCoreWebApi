﻿using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IServiceManager _manager;

    public BooksController(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public IActionResult GetAllBooks()
    {
        try
        {
            var books = _manager.BookService.GetAllBooks(false);
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
            var book = _manager.BookService.GetOneBookById(id, false);
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
                return BadRequest();

            _manager.BookService.CreateOneBook(book);
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
                return BadRequest(); //400

            _manager.BookService.UpdateOneBook(id, book, true);

            return NoContent(); //204
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
            _manager.BookService.DeleteOneBook(id, false);
            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
