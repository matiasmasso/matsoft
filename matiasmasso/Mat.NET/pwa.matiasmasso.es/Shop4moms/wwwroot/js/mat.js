function triggerClick(elt) {
    elt.click();
}

function showAlert(message) {
    alert(message);
}

function hasFocus(element) {
    return element === document.activeElement;
}

function focusedElement() {
    return document.activeElement;
}

window.clipboardCopy = {
    copyText: function (text) {
        navigator.clipboard.writeText(text).then(function () {
            /*alert("Copied to clipboard!");*/
        })
            .catch(function (error) {
                alert(error);
            });
    }
};

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

//Used by AnchorNavigation component 
//to scroll to inner anchors(i.e.: www.mysite.com#anchorId)
function BlazorScrollToId(id) {
    const element = document.getElementById(id);
    if (element instanceof HTMLElement) {
        element.scrollIntoView({
            behavior: "smooth",
            block: "start",
            inline: "nearest"
        });
    }
}



