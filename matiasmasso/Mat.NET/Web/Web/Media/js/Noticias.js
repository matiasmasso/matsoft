$(document).ready(function () {
    $('.FirstItems').last().append('<div class="MoreNews"><a href="#" onclick="MoreNews()">Más noticias...</a></div>');
});

function MoreNews() {
    $('.More').show();
    $('.MoreNews').hide();
}