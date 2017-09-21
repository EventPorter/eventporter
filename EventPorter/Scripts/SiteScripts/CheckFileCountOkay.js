document.getElementById('file_input').addEventListener('change', handleFileInputChange, false);
function handleFileInputChange(e) {
    var files = e.target.files;
    if (files.length > 5) {
        alert("Only 5 of these files will be processed and stored.");
    }
}