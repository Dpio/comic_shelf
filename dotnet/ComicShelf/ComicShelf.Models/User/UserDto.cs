using ComicShelf.Models.Base;

namespace ComicShelf.Models.User
{
	public class UserDto : EntityDto
	{
		public string GoogleId { get; set; }
		public string Name { get; set; }
		public string GivenName { get; set; }
		public string Email { get; set; }
		public string Picture { get; set; }
	}
}
