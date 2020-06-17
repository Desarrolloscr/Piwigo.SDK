using System;
using System.Threading.Tasks;
using Dcsoftcr.Piwigo.SDK.Configuration;
using Dcsoftcr.Piwigo.SDK.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Dcsoftcr.Piwigo.SDK.Helpers
{
    public static class ImageUploaderHelper
    {
        public static async Task<string> UploadImage(IFormFile file, string id, IPWGImages images, IPWGSession session, PiwigoConfiguration piwigoConfiguration)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            if (images is null)
            {
                throw new ArgumentNullException(nameof(images));
            }

            if (session is null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (piwigoConfiguration is null)
            {
                throw new ArgumentNullException(nameof(piwigoConfiguration));
            }

            var status = await session.GetStatus().ConfigureAwait(false);
            string result;
            if (!status.Contains(piwigoConfiguration.UserName, StringComparison.InvariantCulture))
            {
                result = await session.LogIn(piwigoConfiguration.UserName, piwigoConfiguration.Password).ConfigureAwait(false);
            }
            else
            {
                result = status;
            }

            if (result.Contains("ok", StringComparison.InvariantCulture))
            {
                return await images.AddSimple(file, id).ConfigureAwait(false);
            }

            return null;
        }
    }
}
