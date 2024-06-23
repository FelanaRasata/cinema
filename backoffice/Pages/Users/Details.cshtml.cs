using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using backoffice.Databases;
using backoffice.Models;
using backoffice.Services;

namespace backoffice.Pages_Users
{
    public class DetailsModel : PageModel
    {
        private readonly UserService _userService;

        public DetailsModel(UserService userService)
        {
            _userService = userService;
        }

        public User User { get; set; } = default!;

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
            else
            {
                User = user;
            }
            return Page();
        }
    }
}
