using ComicShelf.DataAccess.Entities;

namespace ComicShelf.DataAccess.Repositories
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
		private readonly ApplicationDbContext _context;

		public UserRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
