﻿using ComicShelf.DataAccess.Entities;
using ComicShelf.Models.ComicCollection;
using System.Collections.Generic;

namespace ComicShelf.Logic.Impl
{
	public interface IComicCollectionService
	{
		IEnumerable<ComicCollectionDto> GetComicsCollection(int userId);
		ComicCollectionDto AddToCollection(CreateComicCollectionDto comicCollection);
		void DeleteComicFromCollection(int id);
	}
}
