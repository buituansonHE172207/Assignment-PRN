﻿using Assignment_PRN.Entities;
using Assignment_PRN.Models;
using AutoMapper;

namespace Assignment_PRN.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, SelectOption>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));

            CreateMap<Genre, SelectOption>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));

            CreateMap<Episode, EpisodeItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => src.ThumbnailUrl))
                .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => src.VideoUrl));

            CreateMap<Film, FilmItemList>()
                .ForMember(dest => dest.Genres,
                    opt => opt.MapFrom(src => src.Genres.Select(g => new SelectOption { Value = g.Id, Label = g.Name })));

            CreateMap<Film, FilmItemDetail>()
                .ForMember(dest => dest.Country,
                    opt => opt.MapFrom(src =>
                        src.Country != null ? new SelectOption { Value = src.Country.Id, Label = src.Country.Name } : null))
                .ForMember(dest => dest.Genres,
                    opt => opt.MapFrom(src => src.Genres.Select(g => new SelectOption { Value = g.Id, Label = g.Name })))
                .ForMember(dest => dest.Episodes, opt => opt.MapFrom(src => src.Episodes.OrderBy(e => e.Number)));
        }
    }
}
