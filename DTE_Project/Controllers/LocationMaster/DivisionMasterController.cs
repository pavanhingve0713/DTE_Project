using DTE_Project.Models;
using DTE_Project.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class DivisionMasterController : Controller
{
    private readonly DBDTEPortalContext _context;

    public DivisionMasterController(DBDTEPortalContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var divisionList = await (from d in _context.MstDivisions
                                      join s in _context.MstStates on d.StateId equals s.StateId
                                      select new MstDivisionVM
                                      {
                                          DivisionId = d.DivisionId,
                                          StateId = d.StateId,
                                          StateNameEng = s.StateNameEng,
                                          DivisionNameEng = d.DivisionNameEng,
                                          DivisionNameHin = d.DivisionNameHin,
                                          DivisionCode = d.DivisionCode,
                                          IsActive = d.IsActive
                                      }).ToListAsync();

            return View(divisionList);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error loading divisions: {ex.Message}");
            return View(new List<MstDivisionVM>());
        }
    }

    public async Task<IActionResult> Create()
    {
        try
        {
            await PopulateStateList();
            return View();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error initializing creation form: {ex.Message}");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MstDivision division)
    {
        if (!ModelState.IsValid)
        {
            await PopulateStateList(division.StateId);
            return View(division);
        }

        try
        {
            _context.MstDivisions.Add(division);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error creating division: {ex.Message}");
            await PopulateStateList(division.StateId);
            return View(division);
        }
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (!id.HasValue)
        {
            ModelState.AddModelError(string.Empty, "Division ID is required.");
            return RedirectToAction(nameof(Index));
        }

        try
        {
            var division = await _context.MstDivisions.FindAsync(id);
            if (division == null)
            {
                return NotFound();
            }

            await PopulateStateList(division.StateId);
            return View(division);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error loading division for edit: {ex.Message}");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, MstDivision division)
    {
        if (id != division.DivisionId)
        {
            ModelState.AddModelError(string.Empty, "Division ID mismatch.");
            return RedirectToAction(nameof(Index));
        }

        if (!ModelState.IsValid)
        {
            await PopulateStateList(division.StateId);
            return View(division);
        }

        try
        {
            _context.Update(division);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error updating division: {ex.Message}");
            await PopulateStateList(division.StateId);
            return View(division);
        }
    }

    private async Task PopulateStateList(int? selectedStateId = null)
    {
        try
        {
            var states = await _context.MstStates.ToListAsync();
            ViewBag.StateList = new SelectList(states, "StateId", "StateNameEng", selectedStateId);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error loading state list: {ex.Message}");
            ViewBag.StateList = new SelectList(new List<MstState>());
        }
    }

    private bool MstDivisionExists(int id)
    {
        try
        {
            return _context.MstDivisions.Any(e => e.DivisionId == id);
        }
        catch
        {
            return false;
        }
    }
}