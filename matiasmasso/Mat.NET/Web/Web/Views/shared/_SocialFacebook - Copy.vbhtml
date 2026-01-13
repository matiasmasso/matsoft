@Code
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim oProduct As DTO.DTOProduct = BLL.BLLSession.GetLastProductBrowsed(HttpContext.Current)
    Dim oFacebookWidget As DTO.DTOrrssWidget = FEBL.Product.FacebookWidget(oProduct)
End Code

<style>
    .fb-comments, .fb-comments iframe[style], .fb-like-box, .fb-like-box iframe[style] {
        width: 100% !important;
        display: block;
        z-index: 2000;
        position: relative;
    }

        .fb-comments span, .fb-comments iframe span[style], .fb-like-box span, .fb-like-box iframe span[style] {
            width: 100% !important;
            display: block;
            z-index: 2000;
            position: relative;
        }
</style>




<div id="fb-root"></div>


<script>
    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/@Html.Raw(DTO.DTOLang.Locale(oWebsession.Lang))/all.js#xfbml=1&appId=@oFacebookWidget.widgetId";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));

</script>



<div class="fb-like-box"
     data-href="@oFacebookWidget.Url"
     data-width="100%"
     data-colorscheme="light"
     data-show-faces="true"
     data-header="false"
     data-stream="false"
     data-show-border="false">
</div>


