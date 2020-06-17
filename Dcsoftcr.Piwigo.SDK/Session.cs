using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dcsoftcr.Piwigo.SDK.Interfaces;

namespace Dcsoftcr.Piwigo.SDK
{
    public class Session : IPWGSession
    {
        private readonly HttpClient _client;

        public Session(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> LogIn(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new System.ArgumentException("message", nameof(username));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new System.ArgumentException("message", nameof(password));
            }

            var postData = new Dictionary<string, string>
            {
                { "method", "pwg.session.login" },
                { "username", username },
                { "password", password },
                { "Content-Type", "form-data" }
            };

            return await Helper.ProcessRequest(postData, _client).ConfigureAwait(false);
        }

        public async Task<string> GetStatus()
        {
            var postData = new Dictionary<string, string>
            {
                { "method", "pwg.session.getStatus" },
                { "Content-Type", "form-data" }
            };

            return await Helper.ProcessRequest(postData, _client).ConfigureAwait(false);
        }

    }
}
