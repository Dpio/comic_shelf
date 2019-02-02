﻿using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.User;

namespace ComicShelf.Logic.Profiles
{
	public class UserMappingProfile : Profile
	{
		public UserMappingProfile()
		{
			CreateMap<User, UserDto>();
			CreateMap<UserDto, User>();
		}
	}
}
