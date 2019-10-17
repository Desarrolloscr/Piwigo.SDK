using System;
using System.Threading.Tasks;

namespace Dcsoftcr.Piwigo.SDK.Interfaces
{
    public interface IPWGSession
    {
        Task<string> LogIn(string username, string password);

        Task<string> GetStatus();
    }
}