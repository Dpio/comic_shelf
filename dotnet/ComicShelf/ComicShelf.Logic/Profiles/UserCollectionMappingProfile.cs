﻿using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.UserCollection;

namespace ComicShelf.Logic.Profiles
{
	public class UserCollectionMappingProfile : Profile
	{
		public UserCollectionMappingProfile()
		{
			CreateMap<UserCollection, UserCollectionDto>();
			CreateMap<UserCollectionDto, UserCollection>();
		}
	}
}
