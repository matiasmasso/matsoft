$(document).ready(function () {

    $("input").change(function () {
        var $row = $(this).parents('.ContactRow');
        var $select = $('select', $row);
        if ($(this).is(':checked'))
            $select.show();
        else
            $select.hide();
    });

    $('select').change(function () {
        $(this).removeClass();
        $(this).addClass('Category' + $(this).val());
    });

    $('.RowSubmit input').click(function () {
        event.preventDefault();
        var result = 'OK';

        var json = [];
        $(".ContactRow").each(function (index) {
            if ($(this).find('input:checkbox').is(':checked')) {
                var category = $(this).find('select').val();
                if (category == '0')
                    result = 'ERR';
                else {
                    //create Json Object
                    var myObject = new Object();
                    myObject.shop = $(this).data('guid');
                    myObject.category = category;
                    json.push(myObject);
                }
            }
        });



        if (result == 'ERR')
            alert('por favor seleccione la superficie de los establecimientos en las casillas amarillas');
        else {
            $("div.loading").show();
            var data = JSON.stringify(json);
            $.getJSON(
                '/Quiz/nuevaredoficialinglesina_upload',
                { data: data },
                function (success) {
                    $("div.loading").hide();
                    $(".pagewrapper").toggle();
                    $(".Thanks").toggle();
                    if (success.result == 'KO')
                        $(".Thanks").html('error:<br/>' + success.message);
                });
        }

    });

});

