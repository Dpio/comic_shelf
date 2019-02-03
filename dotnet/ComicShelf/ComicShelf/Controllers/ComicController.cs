using ComicShelf.Logic.Impl;
using ComicShelf.Models.Comic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ComicShelf.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class ComicController : CrudBaseController<ComicDto, CreateComicDto, ComicDto>
	{
		private readonly IComicService _service;

		public ComicController(IComicService service) : base(service)
		{
			_service = service;
		}

		[Authorize]
		[HttpGet("{id}")]
		[Produces("application/json", Type = typeof(ComicDto))]
		public override IActionResult Get(int id)
		{
			return base.Get(id);
		}

		[Authorize]
		[HttpDelete("{id}")]
		public override IActionResult Delete(int id)
		{
			return base.Delete(id);
		}

		[Authorize]
		[HttpPost()]
		[Produces("application/json", Type = typeof(ComicDto))]
		public override IActionResult Post([FromBody] CreateComicDto value)
		{
			return base.Post(value);
		}

		[Authorize]
		[HttpPut()]
		[Produces("application/json", Type = typeof(ComicDto))]
		public override IActionResult Put([FromBody] ComicDto input)
		{
			return base.Put(input);
		}

		[Authorize]
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
