$(document).on('click', '.sidenav .menuItems a', function (e) {
    var tag = $(this).data('tag');
    switch (tag) {
        case 1: //@CInt(DTOProduct.Tabs.coleccion):
            loadOrNavigate($('#Collection'), '/product/PartialCollection');
            break;
        case 2: //@CInt(DTOProduct.Tabs.distribuidores):
            loadOrNavigate($('#StoreLocator'), '/product/StoreLocator');
            break;
        case 5: //@CInt(DTOProduct.Tabs.accesorios):
            loadOrNavigate($('#Accessories'), '/product/PartialAccessories');
            break;
        case 4: //@CInt(DTOProduct.Tabs.descargas):
            loadOrNavigate($('#Downloads'), '/product/PartialDownloads');
            break;
        case 3: //@CInt(DTOProduct.Tabs.galeria):
            loadOrNavigate($('#ImgGallery'), '/product/PartialImgGallery');
            break;
        case 6: //@CInt(DTOProduct.Tabs.videos):
            loadOrNavigate($('#Videos'), '/product/PartialVideos');
            break;
        case 7: //@CInt(DTOProduct.Tabs.bloggerposts):
            loadOrNavigate($('#BloggerPosts'), '/product/PartialBloggerPosts');
            break;
    }
});

function loadOrNavigate(target, url) {
    if (!target.hasClass('isLoaded')) {
        target.show();
        var productGuid = $("#productGuid").val();
        var data = { 'guid': productGuid };
        $.ajax({
            type: "POST",
            url: url,
            data: data,
            success: function (response) {
                target.children('.Spinner64').hide();
                target.append(response);
                target.addClass('isLoaded');
            }
        });
    }
    $('html,body').animate({ scrollTop: target.offset().top });
}



// ------------------------------------------------------------ ImgGallery
$(document).on('click', 'div.ShowMore a', function (e) {
    event.preventDefault();
    var cod = $(this).data("cod");
    $(".ShowMore[data-cod='" + cod + "']").hide();
    $(".ShowLess[data-cod='" + cod + "']").show();
    $("div.CategoryBody[data-cod='" + cod + "']").css('height', 'auto');
});

$(document).on('click', 'div.ShowLess a', function (e) {
    event.preventDefault();
    var cod = $(this).data("cod");
    $(".ShowMore[data-cod='" + cod + "']").show();
    $(".ShowLess[data-cod='" + cod + "']").hide();
    $("div.CategoryBody[data-cod='" + cod + "']").css('height', '185px');
});