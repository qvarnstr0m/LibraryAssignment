using AutoMapper;
using LibraryAssignment.BlazorWASM.DTOs;
using LibraryAssignment.Data.Models;

namespace LibraryAssignment.BlazorWASM.Mapping;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<CreateBookDto, Book>().ReverseMap();
        CreateMap<UpdateBookDto, Book>().ReverseMap();
    }
}