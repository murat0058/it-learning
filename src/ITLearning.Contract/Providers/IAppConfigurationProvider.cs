namespace ITLearning.Contract.Providers
{
    public interface IAppConfigurationProvider
    {
        string GetHostingEnvironmentWWWRootPath();
        string GetProfileOriginalImagesFolderPath();
        string GetProfileCroppedImagesFolderPath();
        string GetProfileOriginalImagesFolderInternalPath();
        string GetProfileCroppedImagesFolderInternalPath();
        string GetProfileDefaultImagePath();
    }
}