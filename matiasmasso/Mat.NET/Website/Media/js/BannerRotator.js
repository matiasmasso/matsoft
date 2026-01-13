var _banners;
var index = 1;

function LoadBannerRotator(banners) {
    _banners = banners;
    setTimeout(StartTimer(), 5000);
}

function StartTimer() {
    setInterval(function () {
        NextBanner();
    }, 5000);
}

$(document).on('click', '.BannerRotator .dot', function () {
    index = $(this).data('tag');
    return ShowBanner();
});

$(document).on('click', '.BannerRotator .prev', function () {
    index -= 1;
    if (index < 0) index = _banners.length - 1;
    return ShowBanner();
});

$(document).on('click', '.BannerRotator .next', function () {
    index += 1;
    if (index >= _banners.length) index = 0;
    return ShowBanner();
});

function NextBanner() {
    ShowBanner();
    if (index == _banners.length - 1)
        index = 0;
    else
        index += 1;
}

function ShowPreviousBanner() {
    index -= 1;
    if (index < 0) index = 0;
    return ShowBanner();
}

function ShowNextBanner() {
    index += 1;
    if (index >= _banners.length) index = 0;
    return ShowBanner();
}

function ShowDotBanner(e) {
    index = e.target.data('tag');
    return ShowBanner();
}

function ShowBanner() {
    $(".BannerRotator .dot").removeClass('active');
    $(".BannerRotator .dot[data-tag=" + index + "]").addClass('active');
    $('.BannerRotator .img-anchor').attr('href', _banners[index]['NavigateTo']);
    $('.BannerRotator .img-anchor img').attr('src', _banners[index]['ImageUrl']);
    $('.BannerRotator .img-anchor img').attr('alt', _banners[index]['Title']);
    return false;
}



