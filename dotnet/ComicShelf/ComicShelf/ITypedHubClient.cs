using System.Threading.Tasks;

namespace ComicShelf.Api
{
	public interface ITypedHubClient
	{
		Task BroadcastMessage(string msg);
		Task BroadcastMessageForUser(int userId, string msg);
	}
}

