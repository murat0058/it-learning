$(document).ready(function () {
    var cropperOptions = {
        uploadUrl: "UploadImage",
        cropUrl: "CropImage",
        //loadPicture: '../Cropped/Przechwytywanie.PNG',
        onReset: function () {
            //todo call to controller and reset img

            cropper.destroy();

            var newOptions = {
                uploadUrl: cropperOptions.uploadUrl,
                cropUrl: cropperOptions.cropUrl,
            }

            cropper = new Croppic('croppic', newOptions);
        }
    }

    var cropper = new Croppic('croppic', cropperOptions);
});