using ITLearning.Contract.Providers;
using ITLearning.Shared;
using ITLearning.Shared.Configs;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.PlatformAbstractions;

namespace ITLearning.Shared.Providers
{
    public class AppConfigurationProvider : IAppConfigurationProvider
    {
        private readonly IApplicationEnvironment _hostingEnvironment;
        private readonly IOptions<PathsConfiguration> _pathsConfiguration;

        public AppConfigurationProvider(IApplicationEnvironment hostingEnvironment, IOptions<PathsConfiguration> pathsConfiguration)
        {
            _hostingEnvironment = hostingEnvironment;
            _pathsConfiguration = pathsConfiguration;
        }

        public string GetHostingEnvironmentWWWRootPath()
        {
            return _hostingEnvironment.ApplicationBasePath + "/wwwroot/";
        }

        public string GetProfileOriginalImagesFolderPath()
        {
            return _hostingEnvironment.ApplicationBasePath + _pathsConfiguration.Value.ProfileImagesPath + "original/";
        }

        public string GetProfileCroppedImagesFolderPath()
        {
            return _hostingEnvironment.ApplicationBasePath + _pathsConfiguration.Value.ProfileImagesPath + "cropped/";
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
    }
}