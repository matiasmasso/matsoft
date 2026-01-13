$(document).ready(function () {

    if ($('.Address:visible').length == 1) {
        $('.Address').removeAttr("href");
        $(document).trigger('CustomerSelected', { guid: $('.Address').data('guid') });
    }

    $('.Address').click(function () {
        /*event.preventDefault();*/

        if ($('.Address').length > 1) {
            if ($('.Address:visible').length == 1) {
                $('.Title').show();
                $('.Address').show();
                $(document).trigger('CustomerSelected', { guid: '' });
            }
            else {
                $('.Title').hide();
                $(this).siblings("a").hide();
                $(document).trigger('CustomerSelected', { guid: $(this).data('guid') });
            }
        }

    })

})