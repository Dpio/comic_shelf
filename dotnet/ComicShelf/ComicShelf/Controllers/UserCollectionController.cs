using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Logic.Helpers;
using ComicShelf.Logic.Impl;
using ComicShelf.Models.UserCollection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ComicShelf.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserCollectionController : Controller
	{
		private IUserCollectionService _userCollectionService;
		private IMapper _mapper;

		public UserCollectionController(
			IUserCollectionService UserCollectionService,
			IMapper mapper)
		{
			_userCollectionService = UserCollectionService;
			_mapper = mapper;
		}

		[Authorize]
		[HttpPost("addToUserCollection")]
		[Produces("application/json", Type = typeof(UserCollectionDto))]
		public IActionResult AddToCollection([FromBody]UserCollectionDto UserCollectionDto)
		{
			try
			{
				// save 
				_userCollectionService.AddToUserCollection(UserCollectionDto);
				return Ok();
			}
			catch (AppException ex)
			{
				// return error message if there was an exception
				return BadRequest(ex.Message);
			}
		}

		[Authorize]
		[HttpDelete("deleteCollectionFromUserCollection")]
		public IActionResult DeleteCollectionFromUserCollection(int id)
		{
			_userCollectionService.DeleteCollectionFromUserCollection(id);
			return Ok();
		}

		[Authorize]
		[HttpGet("getUserCollection")]
		[Produces("application/json", Type = typeof(IEnumerable<UserCollection>))]
		public IActionResult GetUsersCollection(int collectionId)
		{
			var userCollections = _userCollectionService.GetUserCollection(collectionId);
			var usercollectionsDtos = _mapper.Map<IList<UserCollectionDto>>(userCollections);
			return Ok(usercollectionsDtos);
		}
	}
}
