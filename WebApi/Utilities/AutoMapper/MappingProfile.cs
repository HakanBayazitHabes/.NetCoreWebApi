﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Utilities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BookDtoForUpdate, Book>();
        CreateMap<Book, BookDto>();
        CreateMap<BookDtoForInsertion, Book>();
        CreateMap<UserForRegistrationDto, User>();
    }
}
