$(document).on('contextmenu', '.Grid', function (e) {
    var grid = $(this);
    var activeTag = $(grid).find('.Active');
    var contextMenu = ContextMenu(grid);
    LoadContextMenu(contextMenu, e.pageX, e.pageY);

    var data = {
        activeTag: activeTag,
        contextmenu: contextMenu
    };

    $(document).trigger('ContextMenuAppear', data)

    // disable default context menu
    return false;
});



$(document).on('click', '.ContextMenu > a', function (e) {
    event.preventDefault();
    var menuItem = $(this);
    if (!menuItem.hasClass('Disabled')) {
        var url = menuItem.data('url')
        var menu = menuItem.closest('[data-grid]');
        var grid = Grid(menu);
        var activeTag = grid.children('.Active').first();
        var guid = activeTag.data('guid');
        if (url) {
            url = url.replace("{guid}", guid);
            var navTarget = menuItem.attr('target');
            if (navTarget === undefined) {
                location.href = url;
            } else {
                if (navTarget === '_blank')
                    window.open(url, '_blank')
                else
                    location.href = url;
            }

            $(".ContextMenu").hide();
        } else {
            var data = {
                contextmenu: menu.data('grid'),
                guid: guid,
                action: menuItem.data('action'),
                activeTag: activeTag
            };
            menu.hide();
            $(document).trigger('ContextMenuClick', data)
        }
    }
});

$(document).on('click', '.Grid > .Row', function (e) {
    var row = $(this);
    var rows = row.siblings('.Row');
    rows.removeClass('Active');
    row.addClass('Active');
});



function ContextMenu(grid) {
    var menuName = $(grid).data('contextmenu');
    var retval = $('.ContextMenu[data-grid=' + menuName + ']');
    return retval;
}

function Grid(menu) {
    var retval = null;
    var menuName = menu.data('grid');
    retval = $('.MainContent').find('.Grid[data-contextmenu=' + menuName + ']');
    return retval;
}


function LoadContextMenu(menu, pageX, pageY) {
    var top = pageY + 5;
    var left = pageX;

    $(menu).toggle(100).css({
        top: top + "px",
        left: left + "px"
    });

}

//================================= for IOS ===================================

var onlongtouch;
var timer;
var touchduration = 800; //length of time we want the user to touch before we do something

function touchstart(e) {
    e.preventDefault();
    if (!timer) {
        timer = setTimeout(onlongtouch, touchduration, e);
    }
}

function touchend() {
    //stops short touches from firing the event
    if (timer) {
        clearTimeout(timer);
        timer = null;
    }
}

onlongtouch = function (e) {
    timer = null;

    var grid = e.target;
    if (grid.hasClass('Grid')) {
        var activeTag = $(grid).find('.Active');
        var contextMenu = ContextMenu(grid);
        LoadContextMenu(contextMenu, e.pageX, e.pageY);

        var data = {
            activeTag: activeTag,
            contextmenu: contextMenu
        };

        $(document).trigger('ContextMenuAppear', data)

        // disable default context menu
        return false;
    }

};

document.addEventListener("DOMContentLoaded", function (event) {
    window.addEventListener("touchstart", touchstart, false);
    window.addEventListener("touchend", touchend, false);
});