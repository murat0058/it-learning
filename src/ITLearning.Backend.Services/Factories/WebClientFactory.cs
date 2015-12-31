using ITLearning.Contract.Factories;
using ITLearning.Contract.Services;
using ITLearning.Shared;
using ITLearning.Shared.Configs;
using Microsoft.Extensions.OptionsModel;
using System;
using System.Net;
using System.Text;

namespace ITLearning.Backend.Business.Factories
{
    public class WebClientFactory : IWebClientFactory
    {
        private readonly IOptions<SourceControlRestApiConfiguration> _sourceControlRestApiConfiguration;

        private readonly IUserService _userService;

        private readonly string _authKey;

        public WebClientFactory(IOptions<SourceControlRestApiConfiguration> sourceControlRestApiConfiguration, IUserService userService)
        {
            _sourceControlRestApiConfiguration = sourceControlRestApiConfiguration;

            _userService = userService;

            _authKey = "D756F8A9-06E4-43A6-A19E-CFAA0E69ABDB"; //_sourceControlRestApiConfiguration.Value.AuthKey;
        }

        public WebClient CreateWebClient()
        {
            WebClient webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            webClient.Headers[HttpRequestHeader.Authorization] = GetAuthorizationHeader();
            webClient.Headers["AuthKey"] = _authKey;
            return webClient;
        }

        private string GetAuthorizationHeader()
        {
            var userPasswordHash = _userService.GetUserPassword(StaticManager.UserName).Item;
            var encodedDataAsBytes = Convert.FromBase64String(userPasswordHash);
            var userPassword = Encoding.ASCII.GetString(encodedDataAsBytes);
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(StaticManager.UserName + ":" + userPassword));
            var credentialsHeader = string.Format("Basic {0}", credentials);
            return credentialsHeader;
        }
    }
}