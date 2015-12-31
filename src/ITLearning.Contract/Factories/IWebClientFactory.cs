using System.Net;

namespace ITLearning.Contract.Factories
{
    public interface IWebClientFactory
    {
        WebClient CreateWebClient();
    }
}