using System;
using System.Net.Http;
using System.Threading.Tasks;
using Dcsoftcr.Piwigo.SDK;
using Dcsoftcr.Piwigo.SDK.Configuration;
using Dcsoftcr.Piwigo.SDK.Helpers;
using Dcsoftcr.Piwigo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dcsoftcr.Piwigo.Web.Pages.Main
{
    public class UploadFileModel : PageModel
    {

        [BindProperty]
        public UploadFile FileUpload { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (FileUpload.File != null)
            {
                var config = new PiwigoConfiguration
                {
                    BaseURL = ""
                }; 
                HttpClient client = new HttpClient { BaseAddress = new Uri(config.WSURL) };
                var session = new Session(client);
                var images = new Images(client);
                _ = await ImageUploaderHelper.UploadImage(FileUpload.File, FileUpload.Id, images, session, config).ConfigureAwait(false);
            }

            return Page();
        }
    }
}