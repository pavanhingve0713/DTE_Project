using DTE_Project.Models;
using DTE_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTE_Project.Controllers.LocationMaster
{
    public class DistrictMasterController : Controller
    {
        private readonly DBDTEPortalContext _context;

        public DistrictMasterController(DBDTEPortalContext context) => _context = context;

        // Index method to display list of districts
        public async Task<IActionResult> Index()
        {
            try
            {
                var districtList = await (from d in _context.MstDistricts
                                          join div in _context.MstDivisions on d.DivisionId equals div.DivisionId
                                          join s in _context.MstStates on div.StateId equals s.StateId
                                          select new MstDistrictVM
                                          {
                                              DistrictId = d.DistrictId,
                                              DivisionId = d.DivisionId,
                                              DivisionNameEng = div.DivisionNameEng,
                                              StateId = s.StateId,
                                              StateNameEng = s.StateNameEng,
                                              DistrictNameEng = d.DistrictNameEng,
                                              DistrictNameHin = d.DistrictNameHin,
                                              DistrictCode = d.DistrictCode,
                                              IsActive = d.IsActive
                                          }).ToListAsync();

                return View(districtList);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading districts: " + ex.Message);
                return View(new List<MstDistrictVM>());
            }
        }

        // Create method to display the form and save the district
        public async Task<IActionResult> Create()
        {
            await PopulateStateAndDivisionLists();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MstDistrict district)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await PopulateStateAndDivisionLists(district.StateId, district.DivisionId);
                    return View(district);
                }

                _context.MstDistricts.Add(district);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the district: " + ex.Message);
                await PopulateStateAndDivisionLists(district.StateId, district.DivisionId);
                return View(district);
            }
        }

        // Edit method to display the form for editing the district
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue) return NotFound();

            try
            {
                var district = await _context.MstDistricts.FindAsync(id);
                if (district == null) return NotFound();

                await PopulateStateAndDivisionLists(district.StateId, district.DivisionId);
                return View(district);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading the district: " + ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MstDistrict district)
        {
            if (id != district.DistrictId)
            {
                return NotFound();
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    await PopulateStateAndDivisionLists(district.StateId, district.DivisionId);
                    return View(district);
                }

                _context.Update(district);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the district: " + ex.Message);
                await PopulateStateAndDivisionLists(district.StateId, district.DivisionId);
                return View(district);
            }
        }

        // Populate state and division dropdown lists
        private async Task PopulateStateAndDivisionLists(short? stateId = null, int? divisionId = null)
        {
            try
            {
                var states = await _context.MstStates.ToListAsync();
                ViewBag.StateList = new SelectList(states, "StateId", "StateNameEng", stateId);

                var divisions = stateId.HasValue
                    ? await _context.MstDivisions.Where(d => d.StateId == stateId).ToListAsync()
                    : new List<MstDivision>();

                ViewBag.DivisionList = new SelectList(divisions, "DivisionId", "DivisionNameEng", divisionId);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while populating dropdowns: " + ex.Message);
            }
        }

        // Get divisions by state for AJAX request
        public async Task<IActionResult> GetDivisionsByState(short stateId)
        {
            try
            {
                var divisions = await _context.MstDivisions
                    .Where(d => d.StateId == stateId)
                    .Select(d => new { value = d.DivisionId, text = d.DivisionNameEng })
                    .ToListAsync();

                return Json(divisions);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while fetching divisions: " + ex.Message);
                return Json(new List<object>());
            }
        }

        private bool MstDistrictExists(int id) => _context.MstDistricts.Any(e => e.DistrictId == id);
    }
}
