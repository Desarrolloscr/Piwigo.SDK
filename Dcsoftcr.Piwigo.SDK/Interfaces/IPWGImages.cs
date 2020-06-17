using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Dcsoftcr.Piwigo.SDK.Interfaces
{
    public interface IPWGImages
    {
        Task<string> AddSimple(IFormFile file, string id);
    }
}