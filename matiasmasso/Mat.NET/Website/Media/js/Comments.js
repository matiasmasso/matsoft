function loadComments(div, target, targetSrc, from) {
    div.append(spinner);
    if (from === undefined)
        from = 0;

    var url = '/Comments/partialtree/';
    var data = {
        'target': target,
        'targetSrc': targetSrc,
        'take': 15,
        'from': from
    }
    div.load(url, data, function () {
        spinner.remove();
    });
}

$(document).on('click', '.ReadAllComments', function (event) {
    event.preventDefault();

    var anchor = event.target;
    var wrapper = $(anchor).closest('.CommentsWrapper');
    var div = $(wrapper).find('.CommentThreads');
    div.append(spinner);

    var take = $(anchor).data('take');
    var takeFrom = $(anchor).data('from');
    var nextCount = parseInt($(anchor).find('.NextCount').text());
    var leftCount = parseInt($(anchor).find('.LeftCount').text());

    var url = '/Comments/partialtree/';
    var data = {
        'target': $(anchor).data('target'),
        'targetSrc': $(anchor).data('targetsrc'),
        'take': $(anchor).data('take'),
        'from': $(anchor).data('from')
    }
    $.post(url, data, function (result) {
        spinner.remove();
        div.append(result);
        $(anchor).data('from', takeFrom + take);
        $(anchor).find('.LeftCount').text(leftCount-take);
    });

});

$(document).on('click', '.CommentThreads .Thread .ShortenedText .ReadMore', function (event) {
    event.preventDefault();
    $(this).parent().siblings('.CompleteText').show();
    $(this).parent().hide();
})

$(document).on('click', '.CommentThreads .Thread .CompleteText .ReadLess', function (event) {
    event.preventDefault();
    $(this).parent().siblings('.ShortenedText').show();
    $(this).parent().hide();
})


//------------------ answer request

$(document).on('click', '.Comment .CallToAnswer', function (event) {
    event.preventDefault();
    var comment = $(this).closest('.Comment')
    var answering = comment.data('guid');
    var div = comment.find('.Reply');
    if (div.is(':empty')) {
        var form = $(document).find('.CommentForm');
        var clone = form.clone();
        clone.find('textarea').val('');
        clone.find('.EmailRequest').hide();
        clone.find('.EmailRequest .Submit').show();
        clone.find('.PasswordRequest').hide();
        clone.find('.DeliveringEmail').hide();
        clone.find('.EmailDelivered').hide();
        clone.find('.NicknameRequest').hide();
        clone.find('.Info.Submitted').hide();
        clone.find('.SubmitRow .Submit').show();
        clone.appendTo(div);
    } else {
        var form = div.find('.CommentForm');
        form.remove();
    }
});


//------------------ submit the comment

$(document).on('click', '.CommentForm .Submit', function (event) {
    event.preventDefault();
    var button = $(this);
    button.hide();
    var form = $(this).closest('.CommentForm');
    if (form.data('EmailRequested')) {
        $(this).parent().append(spinner);
        var url = '/Comments/Update';
        var data = formData(form);
        $.post(url, data, function (result) {

            spinner.remove();
            switch (result) {
                case 'Success':
                    form.find('.EmailDelivered').hide();
                    form.find('.PasswordRequest').hide();
                    form.find('.Submitted').show();
                    break;
                case 'EmptyEmail':
                    form.find('.EmailEmpty').show();
                    break;
                case 'WrongEmail':
                    button.show();
                    form.find('.WrongEmail').show();
                    break;
                case 'EmailNotFound':
                    form.find('.EmailRequest .Info').hide();
                    form.find('.DeliveringEmail').show();
                    button.parent().append(spinner);

                    url = '/account/EmailVerificationCode';
                    var email = form.find('.EmailAddress').val();
                    var data = { email: email };
                    $.post(url, data, function (result) {
                        spinner.remove();
                        form.find('.DeliveringEmail').hide();
                        form.find('.EmailDelivered').show();
                        form.find('.PasswordRequest').show();
                    })
                    break;
                case 'PasswordRequest':
                    form.find('.EmailAddress').setAttribute("readonly", "true");
                    form.find('.PasswordRequest').show();
                    break;
                case 'WrongPassword':
                    form.find('.PasswordRequest').show();
                    form.find('.WrongPassword').show();
                    break;
                case 'NicknameRequest':
                    form.find('.NicknameRequest').show();
                    break;
                case 'SysError':
                    form.find('.SysError').show();
                    break;
            }

        });
    } else {
        form.data('EmailRequested', 'true')
        form.find('.EmailRequest').show();
    }
});

function formData(form) {
    var rootDiv = form.closest('.CommentThreads');
    var target = rootDiv.data('target');
    var targetSrc = rootDiv.data('targetsrc');

    var commentDiv = form.closest('.Comment');
    var answering = commentDiv.data('guid');
    var answerRoot = commentDiv.data('answerroot');

    var retval = {
        Target: target,
        TargetSrc: targetSrc,
        AnswerRoot: answerRoot,
        Answering: answering,
        EmailAddress: form.find('.EmailAddress').val(),
        Password: form.find('.Password').val(),
        Nickname: form.find('.Nickname').val(),
        Text: form.find('textarea').val()
    }

    return retval;
}



