$(document).ready(function () {

    $('section.Year select').on('change', function () {
        $('.loading').show();
        var year = $(this).val();
        var contact = $(this).data('contact');
        var cta = $(this).data('cta');
        var url = '/extracte/YearChanged';
        $('section.Detail').load(url, { year: year, cta: cta, contact: contact }, function () { $('.loading').hide(); });
    });

});