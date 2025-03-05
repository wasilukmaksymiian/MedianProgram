using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedianProgram.Data;
using MedianProgram.Models;
using MedianProgram.Services;

namespace MedianProgram.Controllers
{
    public class MedianModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly MedianModelsServices _medianService;

        public MedianModelsController(ApplicationDbContext context)
        {
            _context = context;
            _medianService = new MedianModelsServices();
        }

        // GET: MedianModels
        public async Task<IActionResult> Index()
        {
            var history = await _context.MedianModels
                .OrderByDescending(x => x.Id)
                .Take(10)
                .ToListAsync();

            return View(history);

        }

        //Post: MedianModels/Calculate
        [HttpPost]
        public async Task<IActionResult> Calculate(int inputNumber)
        {
            if (inputNumber < 2)
            {
                TempData["ErrorMessage"] = "Podana liczba musi być więkza niż 1";
                return RedirectToAction(nameof(Index));
            }

            var primes = _medianService.GetPrime(inputNumber);
            var medianPrimes = _medianService.GetMedianPrime(primes);
            var calculatedModel = new MedianModel
            {
                InputNumber = inputNumber,
                MedianPrimes = string.Join(", ", medianPrimes)
            };

            _context.MedianModels.Add(calculatedModel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




        // POST: MedianModels/ClearHistory
        [HttpPost, ActionName("ClearHistory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearHistory()
        {
            _context.MedianModels.RemoveRange(_context.MedianModels);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedianModelExists(int id)
        {
            return _context.MedianModels.Any(e => e.Id == id);
        }
    }
}
