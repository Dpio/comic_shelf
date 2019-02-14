using AutoMapper;
using ComicShelf.Logic.Profiles;
using System;
using Xunit;

namespace ComicShelf.Tests
{
	public class AutoMapperTests : IDisposable
	{
		public AutoMapperTests()
		{
			Mapper.Initialize(c =>
			{
				c.AddProfile(new UserMappingProfile());
				c.AddProfile(new ComicMappingProfile());
				c.AddProfile(new ComicCollectionMappingProfile());
				c.AddProfile(new CollectionMappingProfile());
				c.AddProfile(new UserCollectionMappingProfile());
				c.Advanced.AllowAdditiveTypeMapCreation = true;
			});
		}

		public void Dispose()
		{
			Mapper.Reset();
		}

		[Fact]
		public void CheckMappings()
		{
			Mapper.AssertConfigurationIsValid();
		}
	}
}
