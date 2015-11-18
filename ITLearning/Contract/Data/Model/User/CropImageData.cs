namespace ITLearning.Frontend.Web.Contract.Data.Model.User
{
    public class CropImageData
    {
        public string ImageUrl { get; set; }
        public int ImageOriginalWidth { get; set; }
        public int ImageOriginalHeight { get; set; }
        public int ImageScaledWidth { get; set; }
        public int ImageScaledHeight { get; set; }
        public int ImageCropStartPointY { get; set; }
        public int ImageCropStartPointX { get; set; }
        public int ImageCropHeight { get; set; }
        public int ImageCropWidth { get; set; }
    }
}