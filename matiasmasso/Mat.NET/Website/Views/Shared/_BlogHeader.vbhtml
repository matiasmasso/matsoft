@Code
    
    Dim CookiesBannerClass As String = IIf(FEB.Session.GetCookiesAccepted(System.Web.HttpContext.Current), "CookiesBanner", "CookiesBannerHidden")
End Code

<section class="loading" hidden="hidden">
    <span>
        @ContextHelper.Tradueix("cargando datos...", "carregant dades...", "loading data...")
    </span>
</section>


<section class="CookiesBannerClass" hidden="hidden">
    <div>
        <span>
            @Html.Raw(ContextHelper.Tradueix("Al utilizar este sitio web, aceptas ", "Al utilitzar aquest web, acceptes ", "If you continue browsing this site, we consider you accept its "))
            <a href="/politicadecookies">
                @Html.Raw(ContextHelper.Tradueix("el uso que hacemos de las cookies", "l'us que fem de les cookies", "we consider you accept its use of cookies"))
            </a>
        </span>
        <a href="#" class="CookiesBannerButton">aceptar</a>
    </div>
</section>


<section>
        <img class="BlogBanner" src="~/Media/Img/Blog/Blog_Header_avatar_1106x394.gif" />
</section>

<section id="BlogNavBar">
    @Html.Raw("Blog NavBar")
</section>













