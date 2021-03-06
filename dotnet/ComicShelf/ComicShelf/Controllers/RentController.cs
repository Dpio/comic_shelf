﻿using ComicShelf.Logic.Helpers;
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
	public class RentController : CrudBaseController<RentDto, CreateRentDto, UpdateRentDto>
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
			try
			{
				var dto = _service.Create(value);
				return Ok(dto);
			}
			catch (AppException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize]
		[HttpPut()]
		[Produces("application/json", Type = typeof(UpdateRentDto))]
		public override IActionResult Put([FromBody] UpdateRentDto input)
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
		[HttpGet("GetNewRentRequestsCount/{id}")]
		[Produces("application/json", Type = typeof(int))]
		public IActionResult GetRentRequestsCount(int id)
		{
			var requestCount = _service.GetNewRequestsCount(id);
			return Ok(requestCount);
		}

		
		[Authorize]
		[HttpGet("GetPendingRequestsCountForComicByUser/{userId}/{comicId}")]
		[Produces("application/json", Type = typeof(int))]
		public IActionResult GetPendingRequestsCountForComicByUser(int userId, int comicId)
		{
			var requests = _service.GetPendingRequestsForComicByUser(userId, comicId);
			return Ok(requests);
		}
	}
}
