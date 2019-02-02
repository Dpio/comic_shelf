using ComicShelf.Logic.Impl;
using ComicShelf.Models.Comic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ComicShelf.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ComicController : CrudBaseController<ComicDto, CreateComicDto, ComicDto>
	{
		private readonly IComicService _service;

		public ComicController(IComicService service) : base(service)
		{
			_service = service;
		}

		[HttpGet("{id}")]
		[Produces("application/json", Type = typeof(ComicDto))]
		public override IActionResult Get(int id)
		{
			return base.Get(id);
		}

		[HttpDelete("{id}")]
		public override IActionResult Delete(int id)
		{
			return base.Delete(id);
		}

		[HttpPost()]
		[Produces("application/json", Type = typeof(ComicDto))]
		public override IActionResult Post([FromBody] CreateComicDto value)
		{
			return base.Post(value);
		}

		[HttpPut()]
		[Produces("application/json", Type = typeof(ComicDto))]
		public override IActionResult Put([FromBody] ComicDto input)
		{
			return base.Put(input);
		}

		[HttpGet("getAll")]
		[Produces("application/json", Type = typeof(IEnumerable<ComicDto>))]
		public IActionResult GetAll()
		{

			var dto = _service.GetAll();
			if (dto == null)
			{
				return NotFound();
			}
			return Ok(dto);
		}
	}
}
