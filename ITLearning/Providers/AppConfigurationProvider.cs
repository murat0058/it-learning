using System;
using ITLearning.Frontend.Web.Contract.Configs;
using ITLearning.Frontend.Web.Contract.Providers;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.OptionsModel;

namespace ITLearning.Frontend.Web.Providers
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
            return _pathsConfiguration.Value.ProfileImagesInternalPath + "cropped//";
        }

        public string GetProfileDefaultImagePath()
        {
            return _pathsConfiguration.Value.ProfileImagesInternalPath + "default.jpg";
        }
    }
}