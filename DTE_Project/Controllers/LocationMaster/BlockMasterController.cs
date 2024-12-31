using DTE_Project.Models;
using DTE_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DTE_Project.Controllers.LocationMaster
{
    public class BlockMasterController : Controller
    {
        private readonly DBDTEPortalContext _context;

        public BlockMasterController(DBDTEPortalContext context) => _context = context;

        // Index method to display the list of blocks
        public async Task<IActionResult> Index()
        {
            try
            {
                var blockList = await (from b in _context.MstBlocks
                                       join d in _context.MstDistricts on b.DistrictId equals d.DistrictId
                                       join div in _context.MstDivisions on b.DivisionId equals div.DivisionId
                                       join s in _context.MstStates on b.StateId equals s.StateId
                                       select new MstBlockVM
                                       {
                                           BlockId = b.BlockId,
                                           DivisionId = b.DivisionId,
                                           DistrictId = b.DistrictId,
                                           StateId = b.StateId,
                                           BlockNameEng = b.BlockNameEng,
                                           BlockNameHin = b.BlockNameHin,
                                           BlockCode = b.BlockCode,
                                           IsActive = b.IsActive,
                                           DivisionNameEng = div.DivisionNameEng,
                                           DistrictNameEng = d.DistrictNameEng,
                                           StateNameEng = s.StateNameEng
                                       }).ToListAsync();

                return View(blockList);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading blocks: " + ex.Message);
                return View(new List<MstBlockVM>());
            }
        }

        // Create method to display the form and save the block
        public async Task<IActionResult> Create()
        {
            await PopulateStateAndDivisionLists();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MstBlock block)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await PopulateStateAndDivisionLists(block.StateId, block.DivisionId, block.DistrictId);
                    return View(block);
                }

                _context.MstBlocks.Add(block);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the block: " + ex.Message);
                await PopulateStateAndDivisionLists(block.StateId, block.DivisionId, block.DistrictId);
                return View(block);
            }
        }

        // Edit method to display the form for editing the block
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue) return NotFound();

            try
            {
                var block = await _context.MstBlocks.FindAsync(id);
                if (block == null) return NotFound();

                await PopulateStateAndDivisionLists(block.StateId, block.DivisionId, block.DistrictId);
                return View(block);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading the block: " + ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MstBlock block)
        {
            if (id != block.BlockId)
            {
                return NotFound();
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    await PopulateStateAndDivisionLists(block.StateId, block.DivisionId, block.DistrictId);
                    return View(block);
                }

                _context.Update(block);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the block: " + ex.Message);
                await PopulateStateAndDivisionLists(block.StateId, block.DivisionId, block.DistrictId);
                return View(block);
            }
        }

        // Populate state, division, and district dropdown lists
        private async Task PopulateStateAndDivisionLists(short? stateId = null, int? divisionId = null, int? districtId = null)
        {
            try
            {
                var states = await _context.MstStates.ToListAsync();
                ViewBag.StateList = new SelectList(states, "StateId", "StateNameEng", stateId);

                var divisions = stateId.HasValue
                    ? await _context.MstDivisions.Where(d => d.StateId == stateId).ToListAsync()
                    : new List<MstDivision>();

                ViewBag.DivisionList = new SelectList(divisions, "DivisionId", "DivisionNameEng", divisionId);

                var districts = divisionId.HasValue
                    ? await _context.MstDistricts.Where(d => d.DivisionId == divisionId).ToListAsync()
                    : new List<MstDistrict>();

                ViewBag.DistrictList = new SelectList(districts, "DistrictId", "DistrictNameEng", districtId);
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

        // Get districts by division for AJAX request
        public async Task<IActionResult> GetDistrictsByDivision(int divisionId)
        {
            try
            {
                var districts = await _context.MstDistricts
                    .Where(d => d.DivisionId == divisionId)
                    .Select(d => new { value = d.DistrictId, text = d.DistrictNameEng })
                    .ToListAsync();

                return Json(districts);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while fetching districts: " + ex.Message);
                return Json(new List<object>());
            }
        }

        private bool MstBlockExists(int id) => _context.MstBlocks.Any(e => e.BlockId == id);

    }
}
