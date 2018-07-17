using AutoMapper;
using Library.API.DTO;
using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Helper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<User, UserForLogin>();
            CreateMap<User, UserForBookPage>();
            CreateMap<Book, BookForEditAdmin>();
            CreateMap<BookForEditAdmin, Book>();
            CreateMap<User, UserForAdmin>();
            CreateMap<UserForAdmin, User>();
        }
    }
}
