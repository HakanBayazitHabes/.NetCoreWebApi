﻿using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
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
        var books = _manager.BookService.GetAllBooks(false);
        return Ok(books);
    }

    [HttpGet("{id}")]
    public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
    {
        var book = _manager.BookService.GetOneBookById(id, false);

        return Ok(book);
    }

    [HttpPost]
    public IActionResult CreateOneBook([FromBody] BookDtoForInsertion bookDto)
    {
        if (bookDto is null)
            return BadRequest();

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var book = _manager.BookService.CreateOneBook(bookDto);
        return StatusCode(201, book);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
    {
        if (bookDto is null)
            return BadRequest(); //400

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState); //422

        _manager.BookService.UpdateOneBook(id, bookDto, true);

        return NoContent(); //204

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
    {
        _manager.BookService.DeleteOneBook(id, false);
        return NoContent();
    }
}

