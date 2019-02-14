using AutoMapper;
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
		public IActionResult AddToCollection([FromBody]CreateUserCollectionDto createUserCollectionDto)
		{
			try
			{
				// save 
				var userCollectionDto = _userCollectionService.AddToUserCollection(createUserCollectionDto);
				return Ok(userCollectionDto);
			}
			catch (AppException ex)
			{
				// return error message if there was an exception
				return BadRequest(ex.Message);
			}
		}

		[Authorize]
		[HttpDelete("deleteCollectionFromUserCollection/{id}")]
		public IActionResult DeleteCollectionFromUserCollection(int id)
		{
			_userCollectionService.DeleteCollectionFromUserCollection(id);
			return Ok();
		}

		[Authorize]
		[HttpGet("getUserCollection/{id}")]
		[Produces("application/json", Type = typeof(IEnumerable<UserCollectionDto>))]
		public IActionResult GetUsersCollection(int id)
		{
			var userCollections = _userCollectionService.GetUserCollection(id);
			return Ok(userCollections);
		}
	}
}
