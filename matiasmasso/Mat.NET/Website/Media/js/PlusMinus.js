$(document).on('click', '.PlusMinus', function (event) {
    event.preventDefault();
    var $myDiv = $(this).closest(".Collapsed,.Expanded");
    if ($myDiv.hasClass("Collapsed")) {
        $myDiv.removeClass("Collapsed");
        $myDiv.addClass("Expanded");
    }
    else {
        $myDiv.removeClass("Expanded");
        $myDiv.addClass("Collapsed");
    }
})