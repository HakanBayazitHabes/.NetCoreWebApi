using System.Security.Cryptography;
using System.Diagnostics;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using Entities.Exceptions;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using System.Dynamic;
using Entities.LinkModels;

namespace Services;

public class BookManager : IBookService
{
    private readonly IRepositoryManager _manager;
    private readonly ILoggerService _logger;
    private readonly IMapper _mapper;
    private readonly IBookLinks _bookLinks;

    public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IBookLinks bookLinks)
    {
        _manager = manager;
        _logger = logger;
        _mapper = mapper;
        _bookLinks = bookLinks;
    }

    public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
    {
        var entity = _mapper.Map<Book>(bookDto);
        _manager.Book.CreateOneBook(entity);
        await _manager.SaveAsync();
        return _mapper.Map<BookDto>(entity);
    }

    public async Task DeleteOneBookAsync(int id, bool trackChanges)
    {
        var entity = await GetOneBookByIdAndCheckExits(id, trackChanges);
        _manager.Book.DeleteOneBook(entity);
        await _manager.SaveAsync();
    }

    public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllBooksAsync(LinkParameters linkParameters, bool trackChanges)
    {
        if (!linkParameters.BookParameters.validPriceRange)
            throw new PriceOutoFRangeBadRequestException();

        var booksWithMetaData = await _manager.Book.GetAllBooksAsync(linkParameters.BookParameters, trackChanges);

        var bookDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);

        var links = _bookLinks.TryGenerateLinks(bookDto, linkParameters.BookParameters.Fields, linkParameters.HttpContext);

        return (linkResponse: links, metaData: booksWithMetaData.MetaData);
    }

    public async Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
    {
        var book = await GetOneBookByIdAndCheckExits(id, trackChanges);
        return _mapper.Map<BookDto>(book);
    }

    public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges)
    {
        var entity = await GetOneBookByIdAndCheckExits(id, trackChanges);
        //mapping
        entity = _mapper.Map<Book>(bookDto);

        _manager.Book.Update(entity);
        await _manager.SaveAsync();

    }

    private async Task<Book> GetOneBookByIdAndCheckExits(int id, bool trackChanges)
    {
        var entity = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
        if (entity is null)
            throw new BookNotFoundException(id);

        return entity;
    }

}
