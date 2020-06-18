using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Dcsoftcr.Piwigo.Web.Models
{
    public class UploadFile
    {
        [Required]
        [StringLength(128)]
        public string Id { get; set; }

        [Display(Name = "Foto")]
        public IFormFile File { get; set; }
    }
}
