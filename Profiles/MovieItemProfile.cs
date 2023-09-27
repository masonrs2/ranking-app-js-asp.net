using AutoMapper;
using RankingApp.Models;

namespace RankingApp.Profiles 
{
    public class MovieItemProfile : Profile {

        public MovieItemProfile() {
            CreateMap<MovieItemModel, MovieItemModelDTO>();
        }
    }
 }