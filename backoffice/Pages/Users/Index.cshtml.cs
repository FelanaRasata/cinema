using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backoffice.Pages_Users
{
    public class IndexModel : PageModel
    {
        private readonly UserService _userService;

        public IndexModel(UserService userService)
        {
            _userService = userService;
        }

        public IList<User> User { get; set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _userService.FindAllAsync();
        }
    }
}