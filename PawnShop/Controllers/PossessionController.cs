﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawnShop.Core.Models;
using PawnShop.Core.Services;
using PawnShop.Data;
using System.Security.Claims;

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

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(PossessionViewModel model)
		{
			if (!ModelState.IsValid)
			{
                return RedirectToAction(nameof(All));
            }

			model.OwnerId = GetUserId();

			await _possessionService.AddAsync(model);

			return RedirectToAction(nameof(All));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			if (await _context.Possessions.FindAsync(id) == null)
			{
				return RedirectToAction(nameof(All));
			}

			var model = await _possessionService.GetByIdAsync(id);

			if (GetUserId() != model.OwnerId)
			{
				if (!User.IsInRole("Administrator"))
				{
					return RedirectToAction(nameof(All));
				}
			}

			TempData["EditPossessionId"] = id;
			TempData["EditPossessionOwnerId"] = model.OwnerId;

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(PossessionViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction(nameof(All));
			}

			model.Id = (int)TempData["EditPossessionId"];
			model.OwnerId = TempData["EditPossessionOwnerId"].ToString();

			await _possessionService.EditAsync(model);

			return RedirectToAction(nameof(All));
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			if (await _context.Possessions.FindAsync(id) == null)
			{
				return RedirectToAction(nameof(All));
			}

			var entity = await _context.Possessions.FindAsync(id);

			if (GetUserId() != entity.OwnerId)
			{
				if (!User.IsInRole("Administrator"))
				{
					return RedirectToAction(nameof(All));
				}
			}

			TempData["DeletePossessionId"] = id;

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Delete()
		{
			int id = (int)TempData["DeletePossessionId"];

			if (GetUserId() != _context.Possessions.FindAsync(id).Result.OwnerId)
			{
				if (!User.IsInRole("Administrator"))
				{
					return RedirectToAction(nameof(All));
				}
			}

			await _possessionService.DeleteAsync(id);

			return RedirectToAction(nameof(All));
		}

		private string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
	}
}