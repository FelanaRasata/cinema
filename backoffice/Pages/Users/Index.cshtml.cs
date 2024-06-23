using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace backoffice.Pages_Users
{
    public class IndexModel : PageModel
    {
        private readonly UserService _userService;

        public IndexModel(UserService userService)
        {
            _userService = userService;
        }

        public IPagedList<User> Users { get; set; }
        public string Keyword { get; set; }

        public async Task OnGetAsync(int pageNumber = 1, int pageSize = 10, string keyword="")
        {
            Keyword = keyword;
            Users = await _userService.GetUsersPaginate(pageNumber,pageSize,keyword);
        }
    }
}