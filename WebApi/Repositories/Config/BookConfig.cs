﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Repositories.Config;

public class BookConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasData(
            new Book { Id = 1, Title = "Karagöz ve Hacivat", Price = 7.99m },
            new Book { Id = 2, Title = "Mesnevi", Price = 8.99m },
            new Book { Id = 3, Title = "Devlet", Price = 9.99m }
        );
    }
}