using ComicShelf.Logic.Impl;
using ComicShelf.Models.Rent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ComicShelf.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class RentController : CrudBaseController<RentDto, CreateRentDto, RentDto>
	{
		private readonly IRentService _service;

		public RentController(IRentService service) : base(service)
		{
			_service = service;
		}

		[Authorize]
		[HttpGet("{id}")]
		[Produces("application/json", Type = typeof(RentDto))]
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
		[Produces("application/json", Type = typeof(RentDto))]
		public override IActionResult Post([FromBody] CreateRentDto value)
		{
			return base.Post(value);
		}

		[Authorize]
		[HttpPut()]
		[Produces("application/json", Type = typeof(RentDto))]
		public override IActionResult Put([FromBody] RentDto input)
		{
			return base.Put(input);
		}

		[Authorize]
		[HttpGet("GetRentsForUser/{id}")]
		[Produces("application/json", Type = typeof(IEnumerable<RentDto>))]
		public IActionResult GetRentsForUser(int id)
		{
			var dto = _service.GetRentsForUser(id);
			if (dto == null)
			{
				return NotFound();
			}
			return Ok(dto);
		}
		[Authorize]
		[HttpGet("GetRentRequestsCount/{id}")]
		[Produces("application/json", Type = typeof(int))]
		public IActionResult GetRentRequestsCount(int id)
		{
			return Ok(1);
		}
		
	}
}
