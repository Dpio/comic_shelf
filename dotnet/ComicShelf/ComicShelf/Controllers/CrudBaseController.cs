using ComicShelf.Logic.Base;
using ComicShelf.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ComicShelf.Api.Controllers
{
	public abstract class CrudBaseController<TEntityDto, TCreateInput, TUpdateInput> : Controller
		where TEntityDto : IEntityDto
		where TUpdateInput : IEntityDto
		where TCreateInput : ICreateEntityDto
	{
		protected readonly ICrudAppService<TEntityDto, TCreateInput, TUpdateInput> Service;

		protected CrudBaseController(ICrudAppService<TEntityDto, TCreateInput, TUpdateInput> service)
		{
			Service = service;
		}

		[HttpGet("{id}")]
		[Produces("application/json", Type = typeof(IEntityDto))]
		public virtual IActionResult Get(int id)
		{
			var dto = Service.Get(id);
			if (dto == null)
			{
				return NotFound();
			}
			return Ok(dto);
		}

		[HttpPost()]
		[Produces("application/json", Type = typeof(ICreateEntityDto))]
		[ProducesResponseType(typeof(IEntityDto), (int)HttpStatusCode.OK)]
		public virtual IActionResult Post([FromBody] TCreateInput type)
		{
			var dto = Service.Create(type);
			return Ok(dto);
		}

		[HttpPut()]
		[Produces("application/json", Type = typeof(IEntityDto))]
		public virtual IActionResult Put([FromBody] TUpdateInput input)
		{
			var dto = Service.Update(input);
			if (dto == null)
			{
				return NotFound();
			}
			return Ok(dto);
		}

		[HttpDelete("{id}")]
		public virtual IActionResult Delete(int id)
		{
			Service.Delete(id);
			return Ok();
		}
	}
}
