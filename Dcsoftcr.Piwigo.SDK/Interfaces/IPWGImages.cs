using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Dcsoftcr.Piwigo.SDK.Interfaces
{
    public interface IPWGImages
    {
        Task<string> AddSimple(IFormFile file, string id);
    }
}