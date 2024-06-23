using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using backoffice.Databases;
using backoffice.Models;
using backoffice.Services;

namespace backoffice.Pages_Sessions
{
    public class EditModel : PageModel
    {
        private readonly SessionService _sessionService;
        private readonly ToolService _toolService;
        private readonly MovieService _movieService;

        public EditModel(SessionService sessionService, ToolService toolService, MovieService movieService)
        {
            _sessionService = sessionService;
            _toolService = toolService;
            _movieService = movieService;
        }

        [BindProperty] public Session Session { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "id not found!";
                return RedirectToPage("./Index");
            }

            var session = await _sessionService.FindByIdAsync(id);
            if (session == null)
            {
                TempData["error"] = $"Session {id} not found!";
                return RedirectToPage("./Index");
            }

            Session = session;
            ViewData["MovieId"] = new SelectList(_movieService.FindSet(), "Id", "Title");
            ViewData["Rooms"] = new SelectList(_toolService.GetNumbers());
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _sessionService.UpdateSession(Session);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_sessionService.SessionExists(Session.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["success"] = "Session updated successfully!";
            return RedirectToPage("./Index");
        }
    }
}