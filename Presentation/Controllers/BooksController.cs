﻿using System.Text.Json;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers;

//[ApiVersion("1.0")]
[ServiceFilter(typeof(LogFilterAttribute))]
[ApiController]
[Route("api/books")]
[ApiExplorerSettings(GroupName = "v1")]
//[ResponseCache(CacheProfileName = "5mins")]
//[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 80)]
public class BooksController : ControllerBase
{
    private readonly IServiceManager _manager;

    public BooksController(IServiceManager manager)
    {
        _manager = manager;
    }

    [Authorize(Roles = "User, Editor, Admin")]
    [HttpHead]
    [HttpGet(Name = "GetAllBooksAsync")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    //[ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookParameters bookParameters)
    {
        var linkParameters = new LinkParameters()
        {
            BookParameters = bookParameters,
            HttpContext = HttpContext
        };

        var result = await _manager.BookService.GetAllBooksAsync(linkParameters, false);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

        return result.linkResponse.HasLinks ?
        Ok(result.linkResponse.LinkedEntities) :
        Ok(result.linkResponse.ShapedEntities);
    }

    [Authorize(Roles = "User, Editor, Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
    {
        var book = await _manager.BookService.GetOneBookByIdAsync(id, false);

        return Ok(book);
    }

    [Authorize(Roles = "Editor, Admin")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost(Name = "CreateOneBookAsync")]
    public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion bookDto)
    {
        var book = await _manager.BookService.CreateOneBookAsync(bookDto);
        return StatusCode(201, book);
    }

    [Authorize(Roles = "Editor, Admin")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
    {
        await _manager.BookService.UpdateOneBookAsync(id, bookDto, true);

        return NoContent(); //204

    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
    {
        await _manager.BookService.DeleteOneBookAsync(id, false);
        return NoContent();
    }

    [Authorize]
    [HttpOptions]
    public IActionResult GetBooksOptions()
    {
        Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS");
        return Ok();
    }
}

