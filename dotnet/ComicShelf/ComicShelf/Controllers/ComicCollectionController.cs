using AutoMapper;
using ComicShelf.Logic.Helpers;
using ComicShelf.Logic.Impl;
using ComicShelf.Models.ComicCollection;
using ComicShelf.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ComicShelf.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ComicCollectionController : Controller
	{
		private IComicCollectionService _comicCollectionService;
		private IMapper _mapper;

		public ComicCollectionController(
			IComicCollectionService comicCollectionService,
			IMapper mapper)
		{
			_comicCollectionService = comicCollectionService;
			_mapper = mapper;
		}

		[Authorize]
		[HttpPost("addToCollection")]
		[Produces("application/json", Type = typeof(ComicCollectionDto))]
		public IActionResult AddToCollection([FromBody]CreateComicCollectionDto createComicCollectionDto)
		{
			try
			{
				// save 
				var comicCollectionDto = _comicCollectionService.AddToCollection(createComicCollectionDto);
				return Ok(comicCollectionDto);
			}
			catch (AppException ex)
			{
				// return error message if there was an exception
				return BadRequest(ex.Message);
			}
		}

		[Authorize]
		[HttpDelete("deleteFromCollection/{id}")]
		public IActionResult DeleteFromCollection(int id)
		{
			_comicCollectionService.DeleteComicFromCollection(id);
			return Ok();
		}

		[Authorize]
		[HttpGet("getComicsCollection/{id}")]
		[Produces("application/json", Type = typeof(IEnumerable<ComicCollectionDto>))]
		public IActionResult GetComicsCollection(int id)
		{
			var comics = _comicCollectionService.GetComicsCollection(id);
			return Ok(comics);
		}

		[Authorize]
		[HttpGet("getComicCollection/{userId}/{comicId}")]
		[Produces("application/json", Type = typeof(IEnumerable<ComicCollectionDto>))]
		public IActionResult GetComicCollection(int userId, int comicId)
		{
			var comicCollection = _comicCollectionService.GetComicCollection(userId, comicId);
			return Ok(comicCollection);
		}

		[Authorize]
		[HttpGet("getUserWithComic/{comicId}")]
		[Produces("application/json", Type = typeof(IEnumerable<UserDto>))]
		public IActionResult getUserWithComic(int comicId)
		{
			var users = _comicCollectionService.GetUsersWithComic(comicId);
			return Ok(users);
		}
	}
}
