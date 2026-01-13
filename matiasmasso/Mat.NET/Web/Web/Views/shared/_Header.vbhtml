@ModelType DTOUser
@Code
    Dim CookiesBannerClass As String = IIf(Mvc.ContextHelper.GetCookiesAccepted(), "CookiesBanner", "CookiesBannerHidden")
    Dim oUser = Mvc.ContextHelper.FindUserSync
End Code

<section class="loading" hidden="hidden">
    <span>
        @Mvc.ContextHelper.Tradueix("cargando datos...", "carregant dades...", "loading data...")
    </span>
</section>


<section class="CookiesBannerClass" hidden="hidden">
    <div>
        <span>
            @Html.Raw(Mvc.ContextHelper.Tradueix("Al utilizar este sitio web, aceptas ", "Al utilitzar aquest web, acceptes ", "If you continue browsing this site, we consider you accept its "))
            <a href="/politicadecookies">
                @Html.Raw(Mvc.ContextHelper.Tradueix("el uso que hacemos de las cookies", "l'us que fem de les cookies", "we consider you accept its use of cookies"))
            </a>
        </span>
        <a href="#" class="CookiesBannerButton">aceptar</a>
    </div>
</section>



<section id="nickname">
    @Html.Raw(DTOUser.NicknameOrElse(Model))
</section>

<section id="topnavbar">
    @Html.Raw(FEB2.Navbar.Html(Mvc.GlobalVariables.Emp, oUser, Mvc.ContextHelper.GetLang, Mvc.ContextHelper.GetLastProductBrowsed()))
    <!--Html.Partial("_TopNavBar") -->
</section>



<section class="LogoBand">
    <div class="Row GBSearchRow">
        <div class="CellShortTxt GBSearchMenu">
            <a href="/" title='@Mvc.ContextHelper.Tradueix("(volver a página de inicio)", "(tornar a la plana de inici)", "(back to home page)")'>
            </a>
        </div>

        <!--
    <div class="CellTxt GBSearchText">
        <input type="text" value=@@ViewBag.SearchKey
              class="speech-input" data-ready="Habla ahora"
               />
    </div>
    <div class="CellNum GBSearchButton">
        <a href="#">
            @@Html.Raw(Mvc.ContextHelper.Tradueix("buscar", "buscar", "search", "pesquisa"))
        </a>
    </div>
        -->
    </div>
</section>

<div class="MobNavBand">
    <div class="Row GBSearchRow">
        <div class="CellNum GBSearchMenu">
            <a href="/menu" title='menu'>
                <img src="~/Media/Img/Misc/menuMob32.jpg" alt="menu" />
            </a>
        </div>
        <div class="CellTxt GBSearchText">
            <input type="text" value=@ViewBag.SearchKey
                   class="speech-input" data-ready="Habla ahora" />
        </div>
        <div class="CellNum GBSearchButton">
            <a href="#">
                @Html.Raw(Mvc.ContextHelper.Tradueix("buscar", "buscar", "search", "pesquisa"))
            </a>
        </div>
    </div>
</div>

<section id="breadcrumbs">
    @If ViewBag.BreadCrumb IsNot Nothing Then
        @Html.Partial("_BreadCrumbs")
    End If
</section>











