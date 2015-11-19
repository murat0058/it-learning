var cropper = null;
var cropperOptions = null;

function startCropper(data) {
    cropperOptions = {
        uploadUrl: "UploadImage",
        cropUrl: "CropImage",
        onReset: function () {
            onResetCropper();
        }
    };

    if (data.ProfileImagePath && data.ProfileImagePath.indexOf("default") == -1) {
        cropperOptions.loadPicture = data.ProfileImagePath;
    }

    initializeCropper(cropperOptions);
}

function initializeCropper(cropperOptions) {
    cropper = new Croppic('croppic', cropperOptions);
}

function onResetCropper() {
    $.ajax(
        {
            method: "POST",
            url: "/User/DeleteImage/"
        }).success(function () {
            cropper.destroy();

            var newOptions = {
                uploadUrl: cropperOptions.uploadUrl,
                cropUrl: cropperOptions.cropUrl,
            }

            initializeCropper(newOptions);
        });
}