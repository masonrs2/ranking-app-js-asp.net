using AutoMapper;
using RankingApp.Models;

namespace RankingApp.Profiles 
{
    public class AlbumItemProfile : Profile {

        public AlbumItemProfile() {
            CreateMap<AlbumItemModel, AlbumItemModelDTO>();
        }
    }
 }