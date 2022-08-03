using AutoMapper;

namespace BoardGamesCenter.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Entities.Author, ExternalModels.AuthorDTO>();
            CreateMap<ExternalModels.AuthorDTO, Entities.Author>();

            CreateMap<Entities.Publisher, ExternalModels.PublisherDTO>();
            CreateMap<ExternalModels.PublisherDTO, Entities.Publisher>();

            CreateMap<Entities.Game, ExternalModels.GameDTO>();
            CreateMap<ExternalModels.GameDTO, Entities.Game>();
        }
    }
}
