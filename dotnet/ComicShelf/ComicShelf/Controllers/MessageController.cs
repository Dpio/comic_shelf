using ComicShelf.Logic.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;

namespace ComicShelf.Api.Controllers
{
	[Route("api/[controller]")]
	public class MessageController : Controller
	{
		private IHubContext<NotifyHub, ITypedHubClient> _hubContext;

		public MessageController(IHubContext<NotifyHub, ITypedHubClient> hubContext)
		{
			_hubContext = hubContext;
		}

		[HttpPost]
		public string Post([FromBody]Message msg)
		{
			string retMessage = string.Empty;
			try
			{
				_hubContext.Clients.All.BroadcastMessage(msg.Text);
				retMessage = "Success";
			}
			catch (Exception e)
			{
				retMessage = e.ToString();
			}
			return retMessage;
		}

		[HttpPost("PostForUser")]
		public void PostForUser([FromBody]MessageForUser msg)
		{
			string retMessage = string.Empty;
			try
			{
				_hubContext.Clients.All.BroadcastMessageForUser(msg.userId, msg.Text);
				retMessage = "Success";
			}
			catch (Exception e)
			{
				retMessage = e.ToString();
			}
		}
	}
}
