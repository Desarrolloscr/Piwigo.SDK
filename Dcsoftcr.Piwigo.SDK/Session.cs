using Dcsoftcr.Piwigo.SDK.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dcsoftcr.Piwigo.SDK
{
    public class Session : IPWGSession
    {
        private HttpClient _client;

        public Session(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> LogIn(string username, string password)
        {
            var postData = new Dictionary<String, String>();
            postData.Add("method", "pwg.session.login");
            postData.Add("username", username);
            postData.Add("password", password);
            postData.Add("Content-Type", "form-data");
            return await Helper.ProcessRequest(postData, _client);
        }

        public async Task<string> GetStatus()
        {
            var postData = new Dictionary<String, String>();
            postData.Add("method", "pwg.session.getStatus");
            postData.Add("Content-Type", "form-data");
            return await Helper.ProcessRequest(postData, _client);
        }

    }
}
