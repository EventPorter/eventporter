function handleFileSelect(evt) {
    var file = evt.target.files[0];

    if (!file.type.match('image.*')) {
        return;
    }

    var reader = new FileReader();

    reader.onload = (function (theFile) {
        return function (e) {
            document.getElementById('thumb_preview').src = e.target.result;
        };
    })(file);

    reader.readAsDataURL(file);
}

document.getElementById('thumb_input').addEventListener('change', handleFileSelect, false);