using System.ComponentModel.DataAnnotations;

namespace ComicShelf.Models.Base
{
	public class EntityDto : IEntityDto
	{
		[Required]
		public int Id { get; set; }
	}
}
