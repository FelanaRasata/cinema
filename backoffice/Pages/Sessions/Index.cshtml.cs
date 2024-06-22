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

namespace backoffice.Pages_Sessions
{
    public class IndexModel : PageModel
    {
        private readonly SessionService _sessionService;

        public IndexModel(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public IList<Session> Session { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Session = await _sessionService.FindAllAsync();
        }
    }
}