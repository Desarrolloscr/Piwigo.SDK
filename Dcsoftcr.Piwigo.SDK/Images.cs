using Dcsoftcr.Piwigo.SDK.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Dcsoftcr.Piwigo.SDK
{
    public class Images : IPWGImages
    {
        private HttpClient _client;

        public Images(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> AddSimple(IFormFile file, string id)
        {
            if (file.Length <= 0)
            {
                return null;
            }

            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StringContent("pwg.images.addSimple"), "method");
                form.Add(new StringContent(id), "name");

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
                    using (var response = await _client.PostAsync("?format=json", form))
                    {
                        response.EnsureSuccessStatusCode();
                        var pwg = response.Content.ReadAsStringAsync().Result;
                        var one = pwg.IndexOf("/upload");
                        var two = pwg.LastIndexOf("\"") - one;
                        var result = pwg.Substring(one, two).Replace("\\", "");
                        result = result.Insert(result.LastIndexOf("."), "-sq");
                        return result;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
