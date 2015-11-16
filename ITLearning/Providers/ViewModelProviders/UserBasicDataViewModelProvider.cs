using ITLearning.Frontend.Web.Contract.Configs;
using ITLearning.Frontend.Web.Contract.Providers.ViewModelProviders;
using ITLearning.Frontend.Web.ViewModels.User;
using Microsoft.Framework.OptionsModel;

namespace ITLearning.Frontend.Web.Providers.Home
{
    public class UserBasicDataViewModelProvider : IUserBasicDataViewModelProvider
    {
        private IOptions<PathsConfiguration> _pathsConfiguration;

        public UserBasicDataViewModelProvider(IOptions<PathsConfiguration> pathsConfiguration)
        {
            _pathsConfiguration = pathsConfiguration;
        }

        public UserBasicDataViewModel GetUserBasicDataViewModel()
        {
            return new UserBasicDataViewModel
            {
                DisplayName = "nazwa usera",
                ProfileImagePath = _pathsConfiguration.Value.ProfileImagesPath + "default.jpg"
            };
        }
    }
}
