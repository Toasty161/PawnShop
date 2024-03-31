using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawnShop.Core.Models;
using PawnShop.Core.Services;
using PawnShop.Data;

namespace PawnShop.Controllers
{
	[Authorize]
	public class PossessionController : Controller
	{
		private readonly PossessionService _possessionService;
		private readonly ApplicationDbContext _context;

		public PossessionController(PossessionService possessionService, ApplicationDbContext context)
		{
			_possessionService = possessionService;
			_context = context;
		}

		public async Task<IActionResult> All()
		{
			var model = await _context.Possessions.Select(p => new PossessionViewModel()
			{
				Area = p.Area,
				Id = p.Id,
				Location = p.Location,
				Name = p.Name,
				OwnerId = p.OwnerId
			}).ToListAsync();

			return View(model);
		}
	}
}
