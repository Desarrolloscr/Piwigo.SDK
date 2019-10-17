using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dcsoftcr.Piwigo.SDK
{
    internal static class Helper
    {
        public static async Task<string> ProcessRequest(Dictionary<String, String> postData, HttpClient _client)
        {
            var formEncoded = new FormUrlEncodedContent(postData);
            try
            {
                using (var response = await _client.PostAsync("?format=json", formEncoded))
                {
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
