using System.Dynamic;
using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contracts;
public interface IBookService
{
    Task<(LinkResponse linkResponse, MetaData metaData)> GetAllBooksAsync(LinkParameters linkParameters, bool trackChanges);
    Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges);
    Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto);
    Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges);
    Task DeleteOneBookAsync(int id, bool trackChanges);
    Task<List<Book>> GetAllBooksAsync(bool trackChanges);
}
