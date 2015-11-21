namespace ITLearning.Contract.Data.Results.FileUpload
{
    public class UploadImageResult
    {
        public string Status { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Url { get; set; }
    }
}