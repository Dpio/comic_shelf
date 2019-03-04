﻿using ComicShelf.Logic.Helpers;
using ComicShelf.Logic.Impl;
using ComicShelf.Models.Collection;
using ComicShelf.Models.Comic;
using ComicShelf.Models.ComicCollection;
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

		[Authorize]
		[HttpGet("getCollectionsForUser/{userId}")]
		[Produces("application/json", Type = typeof(IEnumerable<CollectionDto>))]
		public IActionResult GetCollectionsForUser(int userId)
		{
			var dto = _service.GetCollectionsForUser(userId);
			if (dto == null)
				return NotFound();
			return Ok(dto);
		}

		[Authorize]
		[HttpPost("addComicToCollection")]
		[Produces("application/json", Type = typeof(IEnumerable<ComicCollectionDto>))]
		public IActionResult AddComicToCollection(CreateComicCollectionDto input)
		{

			try
			{
				var dto = _service.AddComicToCollection(input);
				return Ok(dto);
			}
			catch (AppException ex)
			{
				// return error message if there was an exception
				return BadRequest(new { message = ex.Message });
			}
		}

		[Authorize]
		[HttpGet("getComicsInCollection/{collectionId}")]
		[Produces("application/json", Type = typeof(IEnumerable<ComicDto>))]
		public IActionResult GetComicsInCollection(int collectionId)
		{
			var dto = _service.GetComicsInCollection(collectionId);
			if (dto == null)
				return NotFound();
			return Ok(dto);
		}

		[Authorize]
		[HttpDelete("deleteComicFromCollection/{id}")]
		public IActionResult DeleteComicFromCollection(int id)
		{
			_service.DeleteComicFromCollection(id);
			return Ok();
		}

		[Authorize]
		[HttpGet("getComicCollection/{comicId}/{collectionId}")]
		[Produces("application/json", Type = typeof(IEnumerable<ComicCollectionDto>))]
		public IActionResult GetComicCollection(int comicId, int collectionId)
		{
			var dto = _service.GetComicCollection(comicId, collectionId);
			if (dto == null)
				return NotFound();
			return Ok(dto);
		}

		[Authorize]
		[HttpGet("getCollectionByName/{name}/{userId}")]
		[Produces("application/json", Type = typeof(CollectionDto))]
		public IActionResult GetCollectionByName(string name, int userId)
		{
			var dto = _service.GetCollectionByName(name, userId);
			if (dto == null)
				return NotFound();
			return Ok(dto);
		}
	}
}