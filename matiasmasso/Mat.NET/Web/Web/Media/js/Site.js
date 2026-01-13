var spinner = $('<div />', { 'class': 'Spinner64' });
var spinner16 = $('<div />', { 'class': 'Spinner16' });
var spinner20 = $('<div />', { 'class': 'Spinner20' });



function scrollToAnchor(target) {
    var scrollTop = target.offset().top;
    $('html,body').animate({ scrollTop: scrollTop });
}

function copyToClipboard(src) {
    var $temp = $("<input>");
    $("body").append($temp);
    $temp.val(src).select();
    document.execCommand("copy", false, $temp.val());
    $temp.remove();
}

//----------------------------------- Searchbox -------------------------------------
$(document).on('focusout', 'input[name=SearchKey]', function (e) {
    //called by tab key
    event.preventDefault();
    $(this).focus();
    return SubmitSearchForm(e);
});

$(document).on('keyup', 'input[name=SearchKey]', function (e) {
    // called by return key
    'use strict';
    e = e || event;
    if (e.keyCode == 13) {
        return SubmitSearchForm(e);
    } else {
        return true;
    }
});

function SubmitSearchForm(e) {
    var searchBox = e.target;
    if ($(searchBox).val() == '') {
        return true;
    } else {
        $(searchBox).hide();
        var form = $(searchBox).closest('form');
        form.append(spinner20);
        form.submit();
        return false;
    }
}

//---------------------------------------------------------------------------------------