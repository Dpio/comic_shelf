using ComicShelf.Models.Base;
using System;

namespace ComicShelf.Models.Comic
{
	public class CreateComicDto : ICreateEntityDto
	{
		public string Title { get; set; }
		public int Issue { get; set; }
		public int Volume { get; set; }
		public string Publisher { get; set; }
		public string Series { get; set; }
		public string ScriptWriter { get; set; }
		public string Draftsman { get; set; }
		public string Translator { get; set; }
		public string Cover { get; set; }
		public DateTime PremierDate { get; set; }
		public string Note { get; set; }
		public byte[] Image { get; set; }
	}
}
