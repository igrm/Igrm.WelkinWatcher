using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Igrm.WelkinWatcher.InfoBoard.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string SignalRHost { get; set; }
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
            SignalRHost = _configuration.GetSection("SiteConfig")
                                        .GetValue<string>("SignalRHost");
        }
        public void OnGet()
        {

        }
    }
}
