﻿using ComicShelf.DataAccess.Entities;
using System.Collections.Generic;

namespace ComicShelf.DataAccess.Repositories
{
	public interface IRentRepository : IGenericRepository<Rent>
	{
		IEnumerable<Rent> GetRentsForUser(int userId);
		int GetNewRequestsCount(int userId);
		int GetPendingRequestsCountForComicByUser(int userId, int comicId);
		Rent GetRentRequestForUserByComic(int userId, int comicId);
		Rent GetRentRequestForuserInProgress(int userId, int comicId);
	}
}
