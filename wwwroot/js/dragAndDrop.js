
document.addEventListener('DOMContentLoaded', function () {
    const dropZone = document.getElementById('drop-zone');
    dropZone.addEventListener('click', function (event) {
        if (event.target !== dropZone) {
            return; 
        }
        document.getElementById('fileInput').click();
    });
});

function dragOverHandler(event) {
    event.preventDefault();
    document.getElementById("drop-zone").classList.add("drag-over");
}

function dragLeaveHandler(event) {
    event.preventDefault();
    document.getElementById("drop-zone").classList.remove("drag-over");
}

function dropHandler(event) {
    event.preventDefault();
    document.getElementById("drop-zone").classList.remove("drag-over");

    const files = event.dataTransfer.files;
    handleFiles(files);
}

function fileInputChangeHandler(event) {
    const files = event.target.files;
    handleFiles(files);
}

function handleFiles(files) {
    if (files.length > 0) {
        const file = files[0];
        document.getElementById("fileInput").files = files;

        const fileNameDisplay = document.getElementById("file-name-display");

        fileNameDisplay.textContent = file.name;
    }
}



