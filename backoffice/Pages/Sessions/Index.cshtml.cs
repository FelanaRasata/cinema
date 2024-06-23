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
using X.PagedList;

namespace backoffice.Pages_Sessions
{
    public class IndexModel : PageModel
    {
        private readonly SessionService _sessionService;

        public IndexModel(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public IPagedList<Session> Sessions { get; set; }
        public async Task OnGetAsync(int pageNumber = 1, int pageSize = 10)
        {
            Sessions = await _sessionService.GetSessionsPaginate(pageNumber, pageSize);
        }
    }
}