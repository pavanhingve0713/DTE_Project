using DTE_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DTE_Project.Controllers.LocationMaster
{
	public class StateMasterController : Controller
	{
		private readonly DBDTEPortalContext _context;

		public StateMasterController(DBDTEPortalContext context) => _context = context;

		public async Task<IActionResult> Index()
		{
			try
			{
				var states = await _context.MstStates.ToListAsync();
				return View(states);
			}
			catch
			{
				return View("Error", new { message = "An error occurred while fetching states. Please try again." });
			}
		}

		[HttpGet]
		public IActionResult Create() => View();

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(MstState mstState)
		{
			if (!ModelState.IsValid) return View(mstState);

			if (await StateCodeExists(mstState.StateCode))
			{
				ModelState.AddModelError("StateCode", "The State Code already exists.");
				return View(mstState);
			}

			try
			{
				_context.Add(mstState);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View("Error", new { message = "An error occurred while saving the state. Please try again." });
			}
		}

		public async Task<IActionResult> Edit(short? id)
		{
			if (!id.HasValue) return NotFound();

			try
			{
				var mstState = await _context.MstStates.FindAsync(id);
				return mstState == null ? NotFound() : View(mstState);
			}
			catch
			{
				return View("Error", new { message = "An error occurred while fetching the state for editing. Please try again." });
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(short id, MstState mstState)
		{
			if (id != mstState.StateId) return NotFound();

			if (!ModelState.IsValid) return View(mstState);

			if (await StateCodeExists(mstState.StateCode, id))
			{
				ModelState.AddModelError("StateCode", "The State Code already exists.");
				return View(mstState);
			}

			try
			{
				_context.Update(mstState);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch (DbUpdateConcurrencyException)
			{
				return View("Error", new { message = "A concurrency error occurred while updating the state. Please try again." });
			}
			catch
			{
				return View("Error", new { message = "An error occurred while updating the state. Please try again." });
			}
		}

		private async Task<bool> StateCodeExists(string stateCode, short? id = null)
		{
			return await _context.MstStates.AnyAsync(e => e.StateCode == stateCode && (!id.HasValue || e.StateId != id));
		}
	}
}