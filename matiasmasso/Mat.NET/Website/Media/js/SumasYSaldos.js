$(document).ready(function () {

    $('section.year select').on('change', function () {
        $('.loading').show();
        var year = $(this).val();
        var contact = $(this).data('contact');
        $('section.detail').load('/SumasYSaldos/FromYearContact', { contact: contact, year: year }, function () { $('.loading').hide(); })
    });

});


