using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DotnetIdentity.Views.Shared
{
    public class _Identity : PageModel
    {
        private readonly ILogger<_Identity> _logger;

        public _Identity(ILogger<_Identity> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
