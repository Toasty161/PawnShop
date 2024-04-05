using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawnShop.Core.Models;
using PawnShop.Core.Services;
using PawnShop.Data;
using System.Security.Claims;

namespace PawnShop.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly ChatMessageService _service;
        private readonly ApplicationDbContext _context;

        public ChatController(ChatMessageService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.ChatMessages.Select(cm => new ChatMessageViewModel()
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
        public async Task<IActionResult> Add(ChatMessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            model.SenderId = GetUserId();
            await _service.AddAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await _context.ChatMessages.FindAsync(id) == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (_service.GetByIdAsync(id).Result.SenderId != GetUserId())
            {
                if (!User.IsInRole("Administrator"))
                {
					return RedirectToAction(nameof(Index));
				}
            }

            TempData["EditChatMessageId"] = id;

            var model = await _service.GetByIdAsync(id);

            TempData["EditChatMessageUserId"] = model.SenderId;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ChatMessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            int id = (int)TempData["EditChatMessageId"];

            model.Id = id;
            model.SenderId = TempData["EditChatMessageUserId"].ToString();
            await _service.EditAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.ChatMessages.FindAsync(id);

			if (entity == null || entity.SenderId != GetUserId())
            {
                if (!User.IsInRole("Administrator"))
                {
					return RedirectToAction(nameof(Index));
				}
            }

            TempData["DeleteChatMessageId"] = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            int id = (int)TempData["DeleteChatMessageId"];

            var entity = await _context.ChatMessages.FindAsync(id);

			if (entity == null || entity.SenderId != GetUserId())
            {
				if (!User.IsInRole("Administrator"))
				{
					return RedirectToAction(nameof(Index));
				}
			}

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
