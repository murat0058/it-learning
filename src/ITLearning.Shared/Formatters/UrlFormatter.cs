using ITLearning.Shared.Configs;
using Microsoft.Extensions.OptionsModel;
using System;

namespace ITLearning.Shared.Formatters
{
    public static class UrlFormatter
    {
        private static string sourceControlRestApiUrl = string.Empty;

        public static string FormatSourceControlUrl(string repositoryName)
        {
            return sourceControlRestApiUrl + repositoryName + ".git";
        }

        public static void SetSourceControlRestApiUrl(string sourceControlRestApiUrlParam)
        {
            if (sourceControlRestApiUrl == string.Empty)
            {
                sourceControlRestApiUrl = sourceControlRestApiUrlParam;
            }
            else
            {
                throw new InvalidOperationException("UrlFormatter can be initialized only once!");
            }
        }
    }

    public interface IStaticProvidersConfigurator
    {
        void Init();
    }

    public class StaticProvidersConfigurator : IStaticProvidersConfigurator
    {
        private readonly IOptions<SourceControlRestApiConfiguration> _sourceControlRestApiConfiguration;

        public StaticProvidersConfigurator(IOptions<SourceControlRestApiConfiguration> sourceControlRestApiConfiguration)
        {
            _sourceControlRestApiConfiguration = sourceControlRestApiConfiguration;
        }

        public void Init()
        {
            UrlFormatter.SetSourceControlRestApiUrl(_sourceControlRestApiConfiguration.Value.Url);
        }
    }
}