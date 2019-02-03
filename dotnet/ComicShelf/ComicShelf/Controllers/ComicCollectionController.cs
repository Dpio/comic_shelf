using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Logic.Helpers;
using ComicShelf.Logic.Impl;
using ComicShelf.Models.ComicCollection;
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

		[HttpPost("addToCollection")]
		[Produces("application/json", Type = typeof(ComicCollectionDto))]
		public IActionResult AddToCollection([FromBody]ComicCollectionDto comicCollectionDto)
		{
			try
			{
				// save 
				_comicCollectionService.AddToCollection(comicCollectionDto);
				return Ok();
			}
			catch (AppException ex)
			{
				// return error message if there was an exception
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("deleteFromCollection")]
		public IActionResult DeleteFromCollection(int id)
		{
			_comicCollectionService.DeleteComicFromCollection(id);
			return Ok();
		}

		[HttpGet("getComicsCollection")]
		[Produces("application/json", Type = typeof(IEnumerable<ComicCollection>))]
		public IActionResult GetComicsCollection(int userId)
		{
			var comics = _comicCollectionService.GetComicsCollection(userId);
			var comicsDtos = _mapper.Map<IList<ComicCollectionDto>>(comics);
			return Ok(comicsDtos);
		}
	}
}
