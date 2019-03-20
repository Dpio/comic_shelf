using AutoMapper;
using ComicShelf.DataAccess.Entities;
using ComicShelf.Logic.Helpers;
using ComicShelf.Logic.Impl;
using ComicShelf.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ComicShelf.Api.Controllers
{
	
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : Controller
	{

		private IUserService _userService;
		private IMapper _mapper;

		public UserController(
			IUserService userService,
			IMapper mapper)
		{
			_userService = userService;
			_mapper = mapper;
		}

		[AllowAnonymous]
		[HttpPost("register")]
		[Produces("application/json", Type = typeof(UserDto))]
		public IActionResult Register([FromBody]CreateUserDto userdto)
		{
			// map dto to entity
			var user = _mapper.Map<User>(userdto);

			try
			{
				// save 
				_userService.Create(user);
				return Ok(user);
			}
			catch (AppException ex)
			{
				// return error message if there was an exception
				return BadRequest(ex.Message);
			}
		}

		[Authorize]
		[HttpGet("getAll")]
		[Produces("application/json", Type = typeof(IEnumerable<UserDto>))]
		public IActionResult GetAll()
		{
			var users = _userService.GetAll();
			var userDtos = _mapper.Map<IList<UserDto>>(users);
			return Ok(userDtos);
		}

		[AllowAnonymous]
		[HttpGet("{id}")]
		[Produces("application/json", Type = typeof(UserDto))]
		public IActionResult GetById(int id)
		{
			var user = _userService.GetById(id);
			var userDto = _mapper.Map<UserDto>(user);
			return Ok(userDto);
		}

		[Authorize]
		[HttpPut()]
		[Produces("application/json", Type = typeof(UserDto))]
		public IActionResult Update([FromBody]UserDto userdto)
		{
			// map dto to entity and set id
			var user = _mapper.Map<User>(userdto);

			try
			{
				// save 
				_userService.Update(user);
				return Ok(user);
			}
			catch (AppException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize]
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_userService.Delete(id);
			return Ok();
		}
	}
}
