$(document).on('click', '.ChevronLeft', function (e) {
    e.preventDefault();
    var plugin = $(this).closest('.Plugin');
    var div = $(plugin).find('div');
    var pos = $(div).scrollLeft()+300;
    $(div).scrollLeft(pos);
});

$(document).on('click', '.ChevronRight', function (e) {
    e.preventDefault();
    var plugin = $(this).closest('.Plugin');
    var div = $(plugin).find('div');
    var pos = $(div).scrollLeft() -300;
    if (pos < 150)
        pos = 0;
    $(div).scrollLeft(pos);
});