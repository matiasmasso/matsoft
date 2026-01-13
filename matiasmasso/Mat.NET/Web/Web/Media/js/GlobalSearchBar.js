$(document).ready(function () {

    $('.GBSearchButton a').click(function () {
        var $parent = $(this).closest('.GBSearchRow');
        var searchKey =$parent.find('input[type=text]').val();
        if (searchKey) {
            $('.loading').show();
            $(".Main").load('/search/searchrequest', { searchKey: searchKey }, function () { $('.loading').hide(); });
        }
    });

});