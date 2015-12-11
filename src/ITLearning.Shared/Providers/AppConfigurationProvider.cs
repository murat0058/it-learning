using ITLearning.Contract.Providers;
using ITLearning.Shared.Configs;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.PlatformAbstractions;

namespace ITLearning.Shared.Providers
{
    public class AppConfigurationProvider : IAppConfigurationProvider
    {
        private readonly IApplicationEnvironment _hostingEnvironment;
        private readonly IOptions<PathsConfiguration> _pathsConfiguration;
        private readonly IOptions<DisqusConfiguration> _disqusConfiguration;

        public AppConfigurationProvider(IApplicationEnvironment hostingEnvironment, 
            IOptions<PathsConfiguration> pathsConfiguration,
            IOptions<DisqusConfiguration> disqusConfiguration)
        {
            _hostingEnvironment = hostingEnvironment;
            _pathsConfiguration = pathsConfiguration;
            _disqusConfiguration = disqusConfiguration;
        }

        public string GetHostingEnvironmentWWWRootPath()
        {
            return _pathsConfiguration.Value.RootFolderPath;
        }

        public string GetProfileOriginalImagesFolderPath()
        {
            return _pathsConfiguration.Value.ProfileImagesPath + "original/";
        }

        public string GetProfileCroppedImagesFolderPath()
        {
            return _pathsConfiguration.Value.ProfileImagesPath + "cropped/";
        }

        public string GetProfileOriginalImagesFolderInternalPath()
        {
            return _pathsConfiguration.Value.ProfileImagesInternalPath + "original/";
        }

        public string GetProfileCroppedImagesFolderInternalPath()
        {
            return _pathsConfiguration.Value.ProfileImagesInternalPath + "cropped/";
        }

        public string GetProfileDefaultImagePath()
        {
            return _pathsConfiguration.Value.ProfileImagesInternalPath + "default.jpg";
        }

        public string GetDisqusPageUrl()
        {
            return _disqusConfiguration.Value.PageUrl;
        }
    }
}