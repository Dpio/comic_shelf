﻿using ComicShelf.DataAccess.Entities;
using System.Collections.Generic;

namespace ComicShelf.DataAccess.Repositories
{
	public interface IComicCollectionRepository : IGenericRepository<ComicCollection>
	{
		IEnumerable<ComicCollection> GetComicsCollection(int userId);
	}
}
