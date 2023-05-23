using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using movie.Dto;
using movie.Models;

namespace movie.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Category,CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<ReviewViewDto, Review>();
            CreateMap<Review,ReviewViewDto>();
            CreateMap<Actor, ActorDto>();
            CreateMap<ActorDto, Actor>();  
            CreateMap<UserRegisterDto, User>();
            CreateMap<User,UserRegisterDto>();
            CreateMap<UserLoginDto,User>();
            CreateMap<User, UserLoginDto>();
            CreateMap<User,UserViewDto>();
            CreateMap<UserViewDto, User>();
            CreateMap<Movie, MovieUploadDto>();
            CreateMap<MovieUploadDto, Movie>();

        }
       
    }
}
