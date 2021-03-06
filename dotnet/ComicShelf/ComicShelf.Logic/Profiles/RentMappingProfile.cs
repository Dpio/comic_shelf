﻿using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.Rent;

namespace ComicShelf.Logic.Profiles
{
	public class RentMappingProfile : Profile
	{
		public RentMappingProfile()
		{
			CreateMap<Rent, RentDto>();
			CreateMap<RentDto, Rent>()
				.ForMember(e => e.Comic, e => e.Ignore())
				.ForMember(e => e.Giver, e => e.Ignore())
				.ForMember(e => e.Receiver, e => e.Ignore());
			CreateMap<CreateRentDto, Rent>()
				.ForMember(e => e.Comic, e => e.Ignore())
				.ForMember(e => e.Giver, e => e.Ignore())
				.ForMember(e => e.Receiver, e => e.Ignore())
				.ForMember(e => e.Id, e => e.Ignore());
			CreateMap<UpdateRentDto, Rent>()
				.ForMember(e => e.Comic, e => e.Ignore())
				.ForMember(e => e.Giver, e => e.Ignore())
				.ForMember(e => e.Receiver, e => e.Ignore());
		}
	}
}
