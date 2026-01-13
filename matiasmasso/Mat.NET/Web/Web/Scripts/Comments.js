$(document).ready(function () {
    $(document).on('SISU_Authenticated', function (e, eventArgument) {
        $('.AnswerForm[data-answering="' + eventArgument + '"]').show();
    });


$(".AnswerRequest").click(function (event) {
    event.preventDefault();
    var answering = $(this).data('answering');
    var isAuthenticated = $('.SISU').data('isauthenticated');
    if (isAuthenticated === true) {
        $('.AnswerInfo[data-answering="' + answering + '"]').hide();
        $('.AnswerForm[data-answering="' + answering + '"]').show();
        $('.AnswerTextarea[data-answering="' + answering + '"]').val('');
    }
    else {
        //$(document).trigger('SISU_request', answering);

        SISU_SignInOrSignUp($('.SISU[data-guid="' + answering + '"]'), answering);
    }
    return false;
})


$('.AnswerSubmit').click(function (event) {
    var url = '/Comments/Update';
    var answering = $(this).data('answering');
    var data = getAnswerData(answering);
    $.getJSON(url, { data: data }, onAnswered(answering));
    
})


function onAnswered(answering) {
    $('.AnswerForm[data-answering="' + answering + '"]').hide();
    $('.AnswerInfo[data-answering="' + answering + '"]').show();
    $('.loading').hide();
}

function getAnswerData(answering) {
    var myObject = new Object();
    myObject.parent = $('#HiddenParent').val();
    myObject.lang = $('#HiddenParentLang').val();
    myObject.text = $('.AnswerTextarea[data-answering="' + answering + '"]').val();
    myObject.answering = answering;

    var retval = JSON.stringify(myObject);
    return retval;
}

})