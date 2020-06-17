using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dcsoftcr.Piwigo.SDK
{
    internal static class Helper
    {
        public static async Task<string> ProcessRequest(Dictionary<string, string> postData, HttpClient _client)
        {
            if (postData is null)
            {
                throw new ArgumentNullException(nameof(postData));
            }

            if (_client is null)
            {
                throw new ArgumentNullException(nameof(_client));
            }

            try
            {
                using var formEncoded = new FormUrlEncodedContent(postData);
                using var response = await _client.PostAsync("?format=json", formEncoded).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
