$(document).on('click', '#Feedback a', function (event) {
    event.preventDefault();
    highlight($(this));
    $('#Feedback .Spinner64').show();
    var url = '/feedback';
    var data = {
        'guid': $('#Feedback').data('guid'),
        'cod': $('#Feedback').data('cod'),
        'source': $('#Feedback').data('source'),
        'nom': $('#Feedback').data('nom'),
        'score': $(this).data('score'),
        'comment': $('#Feedback .Comment textarea').val()
    };

    var jqxhr = $.post(url, data, function (result) {
        $('#Feedback .Spinner64').hide();
        $('#Feedback').data('guid', result.guid);
        $('#Feedback .Comment').css('display', 'flex');
    });
});


function highlight(element) {
    //clear all smiles
    element.siblings().each(function (index) {
        deactivate($(this).children('img'));
    });

    //set active smile
    activate(element.children('img'))
}

function activate(img) {
    var originalSrc = img.attr("src");
    if (!originalSrc.includes("_Active.png")) {
        var amendedSrc = originalSrc.replace(".png", "_Active.png");
        img.attr("src", amendedSrc);
    }
}
function deactivate(img) {
    var originalSrc = img.attr("src");
    if (originalSrc.includes("_Active.png")) {
        var amendedSrc = originalSrc.replace("_Active.png", ".png");
        img.attr("src", amendedSrc);
    }
}

$(document).on('click', '#Feedback .Comment input[type=button]', function () {
    //$(this).hide();
    //var spinner = $('<div />', {
    //    'class': 'Spinner64'
    //});
    $('#Feedback .SubmitDiv').append(spinner);
    var data = {
        'guid': $('#Feedback').data('guid'),
        'comment': $('#Feedback .Comment textarea').val()
    };
    var url = '/feedback/saveComment';

    var jqxhr = $.post(url, data, function (result) {
        spinner.remove();
        $('#Feedback .Comment').hide();
    });

})
