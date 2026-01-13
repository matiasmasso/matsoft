function triggerClick(elt) {
    elt.click();
}

window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}

window.launchUrl = async (url) => {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}

