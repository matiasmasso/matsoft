$(document).ready(function () {
    var paginationUrl;
    var guid;
    var pagesize;
    var lastpageindex;
    var pageindex = 0;


    /*Defineix un contenidor #Items per fer reload i un de buit #Pagination
    <div id="Items">
        @Html.Partial("Raffles_", Model.Take(pagesize))
    </div>
    <div id='Pagination' data-paginationurl='@Url.Action("pageindexchanged")' data-pagesize='@pagesize' data-itemscount='@Model.Count'></div>
    */

    paginationConfig($('#Pagination'));


    function paginationConfig($div) {
        paginationUrl = $div.data('paginationurl');
        guid = $div.data('guid');
        pagesize = $div.data('pagesize');
        itemscount = $div.data('itemscount');
        lastpageindex = Math.floor((itemscount - 1) / pagesize);

        if (lastpageindex == 0)
            $div.hide();
        else {
                var initialpageindex = $div.data('initialpageindex');
                if (initialpageindex)
                    pageindex = initialpageindex;

                var style = '<style scoped>';
                style += '#Pagination { margin-top: 20px; text-align: right;}';
                style += '#Pagination .Active {color: red;}';
                style += '#Pagination a {padding: 8px 14px;border: 1px solid #ddd;text-decoration: none;-webkit-border-radius: 3px;-moz-border-radius: 3px; border-radius: 3px;}';
                style += '#Pagination a:hover, #Pagination .active a {background-color: #f5f5f5;}';
                style += '#Pagination .active a {color: #999999;cursor: default;}';
                style += '#Pagination .disabled span,#Pagination .disabled a,#Pagination .disabled a:hover {color: #999999;background-color: transparent;cursor: default;}';
                style += '</style>';

                $div.empty();
                $div.append('<a href="#" data-pageindex="0" id="PageFirst">&lt;&lt;</a>')
                $div.append('<a href="#" data-pageindex="' + ((pageindex <= 1) ? 0 : pageindex - 1) + '" id="PagePrevious">&lt;</a>');
                $div.append('<a href="#" data-pageindex="' + (pageindex + 0) + '" id="PageA" class="Active">' + (pageindex + 1) + '</a>');
                $div.append('<a href="#" data-pageindex="' + (pageindex + 1) + '" id="PageB" ' + ((pageindex + 1 > lastpageindex) ? 'hidden = "hidden"' : '') + '>' + (pageindex + 2) + '</a>');
                $div.append('<a href="#" data-pageindex="' + (pageindex + 2) + '" id="PageC" ' + ((pageindex + 2 > lastpageindex) ? 'hidden = "hidden"' : '') + '>' + (pageindex + 3) + '</a>');
                $div.append('<a href="#" data-pageindex="' + ((pageindex > lastpageindex - 1) ? lastpageindex : pageindex + 1) + '" id="PageNext">&gt;</a>');
                $div.append('<a href="#" data-pageindex="' + lastpageindex + '" id="PageLast">&gt;&gt;</a>');

                if ($('#PageC').data('pageindex') > lastpageindex ) 
                    $('#PageC').hide()
                else
                    $('#PageC').show()

                if ($('#PageB').data('pageindex') > lastpageindex) 
                    $('#PageB').hide()
                else
                    $('#PageB').show()

                $div.before(style);
        }

    };


    $(document).on('click','#Pagination a', function (event) {
        event.preventDefault();
        $(this).siblings('.Active').removeClass('Active');
        var pageindex = $(this).data('pageindex');
        var lastpageindex = $('#PageLast').data('pageindex');

        if (pageindex > 0)
            $('#PagePrevious').data('pageindex', pageindex - 1)

        if (pageindex <= lastpageindex - 1)
            $('#PageNext').data('pageindex', pageindex + 1)

        var pageAnumber = $('#PageA').data('pageindex');
        var pageBnumber = $('#PageB').data('pageindex');
        var pageCnumber = $('#PageC').data('pageindex');

        switch ($(this).attr('id')) {
            case 'PageFirst':
                pageindex = 0
                pageAnumber = pageindex
                pageBnumber = pageindex + 1
                pageCnumber = pageindex + 2

                $('#PageA').text(pageAnumber + 1)
                $('#PageA').data('pageindex', pageAnumber)
                $('#PageB').text(pageBnumber + 1)
                $('#PageB').data('pageindex', pageBnumber)
                $('#PageC').text(pageCnumber + 1)
                $('#PageC').data('pageindex', pageCnumber)

            case 'PagePrevious':
                if (pageAnumber > pageindex) {
                    pageAnumber = pageindex
                    pageBnumber = pageindex + 1
                    pageCnumber = pageindex + 2

                    $('#PageA').text(pageAnumber + 1)
                    $('#PageA').data('pageindex', pageAnumber)
                    $('#PageB').text(pageBnumber + 1)
                    $('#PageB').data('pageindex', pageBnumber)
                    $('#PageC').text(pageCnumber + 1)
                    $('#PageC').data('pageindex', pageCnumber)
                };

            case 'PageNext':
                if (pageCnumber < pageindex) {
                    pageCnumber = pageindex
                    pageBnumber = pageindex - 1
                    pageAnumber = pageindex - 2

                    $('#PageA').text(pageAnumber + 1)
                    $('#PageA').data('pageindex', pageAnumber)
                    $('#PageB').text(pageBnumber + 1)
                    $('#PageB').data('pageindex', pageBnumber)
                    $('#PageC').text(pageCnumber + 1)
                    $('#PageC').data('pageindex', pageCnumber)
                };

            case 'PageLast':
                if (pageAnumber < pageindex - 2) {
                    pageindex = lastpageindex
                    pageCnumber = pageindex
                    pageBnumber = pageindex - 1
                    pageAnumber = pageindex - 2

                    $('#PagePrevious').data('pageindex', pageindex - 1)
                    $('#PageNext').data('pageindex', pageindex)

                    $('#PageA').text(pageAnumber + 1)
                    $('#PageA').data('pageindex', pageAnumber)
                    $('#PageB').text(pageBnumber + 1)
                    $('#PageB').data('pageindex', pageBnumber)
                    $('#PageC').text(pageCnumber + 1)
                    $('#PageC').data('pageindex', pageCnumber)
                }

        }

        if (pageAnumber == pageindex)
            $('#PageA').addClass('Active');
        else if (pageBnumber == pageindex)
            $('#PageB').addClass('Active')
        else if (pageCnumber == pageindex)
            $('#PageC').addClass('Active')


        if ($('#PageC').data('pageindex') > lastpageindex) 
            $('#PageC').hide()
        else
            $('#PageC').show()

        if ($('#PageB').data('pageindex') > lastpageindex )
            $('#PageB').hide()
        else
            $('#PageB').show()

        reload();
    });

    function reload() {
        var pageindex = $('#Pagination .Active').data('pageindex');
        var data = { pageindex: pageindex, pagesize: pagesize, guid: guid };
        $('.loading').show();

        $('#Items').load(paginationUrl, data, function (e) {
            $('.loading').hide();
        });
    }
});