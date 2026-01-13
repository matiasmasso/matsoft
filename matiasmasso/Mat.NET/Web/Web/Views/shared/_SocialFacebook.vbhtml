@ModelType DTOUser
@Code
    Dim oProduct As DTOProduct = Mvc.ContextHelper.GetLastProductBrowsed()
    Dim exs As New List(Of Exception)
    Dim oFacebookWidget = FEB2.SocialMediaWidget.WidgetSync(exs, Model, DTOSocialMediaWidget.Platforms.Facebook, oProduct)
End Code

<style scoped>
    /* 
Make the Facebook Like box responsive (fluid width)
https://developers.facebook.com/docs/reference/plugins/like-box/ 
*/

    /* 
This element holds injected scripts inside iframes that in 
some cases may stretch layouts. So, we're just hiding it. 
*/

    #fb-root {
        display: none;
    }

    /* To fill the container and nothing else */

    .fb_iframe_widget, .fb_iframe_widget span, .fb_iframe_widget span iframe[style] {
        width: 100% !important;
    }
</style>




<div id="fb-root"></div>


<script>
    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/@Html.Raw(DTOLang.Locale(Mvc.ContextHelper.lang()))/sdk.js#xfbml=1&appId=@oFacebookWidget.widgetId&version=v3.0";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));

</script>

<div class="fb-page" 
     data-href="@DTOSocialMediaWidget.Url(oFacebookWidget)" 
     data-tabs=""
     data-width="180" 
     data-height="500" 
     data-small-header="false" 
     data-adapt-container-width="true" 
     data-hide-cover="true" 
     data-show-facepile="true">
        <div class="fb-xfbml-parse-ignore">
            <blockquote cite="@DTOSocialMediaWidget.Url(oFacebookWidget)">
                <a href="@DTOSocialMediaWidget.Url(oFacebookWidget)">@DTOSocialMediaWidget.BrandNom(oFacebookWidget)</a>
            </blockquote>
    </div>
</div>

<!--
<div class="fb-like-box"
     data-height="314px"
     data-href="@@oFacebookWidget.Url"
     data-width="100%"
     data-hide-cover="true"
     data-colorscheme="light"
     data-show-faces="true"
     data-header="true"
     data-stream="false"
     data-show-border="false">
</div>


<div class="fb-page"
     data-href="@@oFacebookWidget.Url"
     data-small-header="false"
     data-hide-cover="true"
     data-show-facepile="true"
     data-show-posts="false">
</div>    
    -->