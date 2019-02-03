namespace ComicShelf.Models.Authenticate
{
	public class AuthenticateResponse
	{
		public string GoogleId { get; set; }
		public string Name { get; set; }
		public string GivenName { get; set; }
		public string Email { get; set; }
		public string Picture { get; set; }
		public string Token { get; set; }
	}
}
