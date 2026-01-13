
$(document).on("mouseenter", ".SelectableRow, .Grid .Row[data-url]", function (e) {
        $(this).css('background', '#ccddff')
});

$(document).on("mouseleave", ".SelectableRow, .Grid .Row[data-url]", function (e) {
    $(this).css('background', '')
});

$(document).on('click', '.SelectableRow, .Grid .Row[data-url]', function (e) {
    var url = $(this).data('url');
    if (url) {
        $('.loading').show();
        window.location.href = url;
    }
})
