using System.Security.Cryptography;
using System.Diagnostics;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using Entities.Exceptions;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.RequestFeatures;

namespace Services;

public class BookManager : IBookService
{
    private readonly IRepositoryManager _manager;
    private readonly ILoggerService _logger;
    private readonly IMapper _mapper;

    public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
    {
        _manager = manager;
        _logger = logger;
        _mapper = mapper;
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

    public async Task<(IEnumerable<BookDto> books, MetaData metaData)> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
    {
        if (!bookParameters.validPriceRange)
            throw new PriceOutoFRangeBadRequestException();
            
        var booksWithMetaData = await _manager.Book.GetAllBooksAsync(bookParameters, trackChanges);
        var bookDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);
        return (bookDto, booksWithMetaData.MetaData);
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
