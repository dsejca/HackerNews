using AutoMapper;
using Core.Models;


namespace Core.Profiles
{
    public class HackNewsProfile : Profile
    {
        public HackNewsProfile()
        {
            CreateMap<ItemResponse, HackNewsResponse>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Title))
            .ForMember(dest => dest.Uri, act => act.MapFrom(src => src.Url))
            .ForMember(dest => dest.PostedBy, act => act.MapFrom(src => src.By))
            .ForMember(dest => dest.Time, act => act.MapFrom(src => UnixTimeToDateTime(src.Time)))
            .ForMember(dest => dest.Score, act => act.MapFrom(src => src.Score))
            .ForMember(dest => dest.CommentCount, act => act.MapFrom(src => src.Descendants));
        }

        // Helper function to convert Unix timestamp to DateTime (UTC)
        private DateTime UnixTimeToDateTime(long unixTime)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);  // Important:  DateTimeKind.Utc
            return dateTimeVal.AddSeconds(unixTime);  // Adds seconds to the Epoch
        }
    }
}
