window.triggerClick = (element) => {
    element.click();
};

window.getSelectedFiles = async (element) => {
    const files = element.files;
    const results = [];
    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        const reader = new FileReader();
        results.push(await new Promise(resolve => {
            reader.onload = () => resolve(reader.result);
            reader.readAsDataURL(file);
        }));
    }
    return results;
};

window.getWindowWidth = () => window.innerWidth;


/*Get image dimensions for VideoThumbnails*/
window.getImageDims = async function (url) {
    return new Promise(
        resolve => {
            const img = new Image();
            img.onload = () => resolve({ width: img.naturalWidth, height: img.naturalHeight });
            img.src = url;
        });
};



