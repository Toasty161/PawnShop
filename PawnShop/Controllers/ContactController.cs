using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawnShop.Core.Models;
using PawnShop.Core.Services;
using PawnShop.Data;
using PawnShop.Infrastructure.Data.Models;
using System.Security.Claims;

namespace PawnShop.Controllers
{
	[Authorize]
	public class ContactController : Controller
	{
		private readonly ContactMessageService _service;
		private readonly ApplicationDbContext _context;

        public ContactController(ContactMessageService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
		public async Task<IActionResult> All()
		{
			var model = await _context.ContactMessages.Select(cm => new ContactMessageViewModel()
			{
				Id = cm.Id,
				Message = cm.Message,
				Sender = cm.Sender,
				SenderId = cm.SenderId,
				TimeSent = cm.TimeSent
			}).ToListAsync();

			return View(model);
		}

		[HttpGet]
        public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(ContactMessageViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction("All", "Product");
			}

			model.SenderId = GetUserId();
			await _service.AddAsync(model);

			return RedirectToAction("All", "Product");
		}

		private string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
