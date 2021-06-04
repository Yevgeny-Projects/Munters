using System.Net.Http;
using System.Threading.Tasks;

namespace Munters.Assignment.Core
{
    public interface IHttpGetRequestSender
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}