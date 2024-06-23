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

namespace backoffice.Pages_Users
{
    public class EditModel : PageModel
    {
        private readonly UserService _userService;

        public EditModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty] public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "id not found!";
                return RedirectToPage("./Index");
            }

            var user = await _userService.FindByIdAsync(id);
            if (user == null)
            {
                TempData["error"] = $"User {id} not found!";
                return RedirectToPage("./Index");
            }

            User = user;
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
                await _userService.UpdateUser(User);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_userService.UserExists(User.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["success"] = "User updated successfully!";
            return RedirectToPage("./Index");
        }
    }
}