$(document).ready(function () {

    $('#Year').on('change', function () {
        $('.loading').show();
        var year = $(this).val();
        $('section.detail').load('/SumasYSaldos/FromYear', {year: year }, function () { $('.loading').hide(); })
    });

});