using System.Threading.Tasks;
using Dcsoftcr.Piwigo.SDK.Configuration;
using Dcsoftcr.Piwigo.SDK.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Dcsoftcr.Piwigo.SDK.Helpers
{
    public class ImageUploaderHelper
    {
        public static async Task<string> UploadImage(IFormFile file, string id, IPWGImages images, IPWGSession session, PiwigoConfiguration piwigoConfiguration)
        {
            var status = await session.GetStatus();
            string resul;
            if (!status.Contains(piwigoConfiguration.UserName))
            {
                resul = await session.LogIn(piwigoConfiguration.UserName, piwigoConfiguration.Password);
            }
            else
            {
                resul = status;
            }

            if (resul.Contains("ok"))
            {
                return await images.AddSimple(file, id);
            }

            return null;
        }
    }
}
