using ComicShelf.DataAccess.Entities;
using System.Threading.Tasks;

namespace ComicShelf.Logic.Impl
{
	public interface IAuthenticationService
	{
		User Authenticate(string Name, string GoogleId);
		Task<User> GetAccessToken(string code);
	}
}
