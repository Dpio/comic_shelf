using ComicShelf.Logic.Impl;
using ComicShelf.Models.Collection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ComicShelf.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class CollectionController : CrudBaseController<CollectionDto, CreateCollectionDto, CollectionDto>
	{
		private readonly ICollectionService _service;

		public CollectionController(ICollectionService service) : base(service)
		{
			_service = service;
		}

		[Authorize]
		[HttpGet("{id}")]
		[Produces("application/json", Type = typeof(CollectionDto))]
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
		[Produces("application/json", Type = typeof(CollectionDto))]
		public override IActionResult Post([FromBody] CreateCollectionDto value)
		{
			return base.Post(value);
		}

		[Authorize]
		[HttpPut()]
		[Produces("application/json", Type = typeof(CollectionDto))]
		public override IActionResult Put([FromBody] CollectionDto input)
		{
			return base.Put(input);
		}

		[Authorize]
		[HttpGet("getAll")]
		[Produces("application/json", Type = typeof(IEnumerable<CollectionDto>))]
		public IActionResult GetAll()
		{
			var dto = _service.GetAll();
			if (dto == null)
			{
				return NotFound();
			}
			return Ok(dto);
		}

		[AllowAnonymous]
		[HttpGet("getUserCollectionNames/{id}")]
		[Produces("application/json", Type = typeof(IEnumerable<string>))]
		public IActionResult getUserCollectionNames(int id)
		{
			var names = _service.GetUserCollection(id);
			return Ok(names);
		}
	}
}
