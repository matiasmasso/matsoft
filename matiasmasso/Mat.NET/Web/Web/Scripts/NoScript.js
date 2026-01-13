$(document).ready(function () {

    if (navigator.cookieEnabled) {
        // Cookies are enabled
    }
    else {
        // Cookies are disabled
        $('.JsEnabled').hide();
        $('.JsDisabled').show();
    }
});