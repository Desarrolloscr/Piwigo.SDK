using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Dcsoftcr.Piwigo.SDK.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Dcsoftcr.Piwigo.SDK
{
    public class Images : IPWGImages
    {
        private readonly HttpClient _client;

        public Images(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> AddSimple(IFormFile file, string id)
        {
            if (file == null || file.Length <= 0)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            using var form = new MultipartFormDataContent();
            var method = new StringContent("pwg.images.addSimple");
            form.Add(method, "method");
            var name = new StringContent(id);
            form.Add(name, "name");

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            var fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                Name = "image",
                FileName = file.FileName
            };
            form.Add(fileContent, "image");

            try
            {
                using var response = await _client.PostAsync("?format=json", form).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var pwg = response.Content.ReadAsStringAsync().Result;
                var one = pwg.IndexOf("/upload", StringComparison.InvariantCulture);
                var two = pwg.LastIndexOf("\"", StringComparison.InvariantCulture) - one;
                var result = pwg.Substring(one, two).Replace("\\", "", StringComparison.InvariantCulture);
                result = result.Insert(result.LastIndexOf(".", StringComparison.InvariantCulture), "-sq");
                return result;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                fileContent.Dispose();
                name.Dispose();
                method.Dispose();
            }
        }
    }
}
