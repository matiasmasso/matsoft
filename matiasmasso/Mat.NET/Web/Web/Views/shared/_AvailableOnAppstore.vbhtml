@Code
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code

<div class="AppStore">
    <a href="https://apple.co/3dDXbCl">
        <img src="/Media/Img/appstore.svg" width="120" height="40" />
        <div>
            <span>
                @lang.tradueix("Operativa también disponible", "Operativa també disponible", "Also available", "Também disponível")
            </span>
            <br />
            <span>
                @lang.tradueix("en nuestra App", "a la nostre App", "on our App", "em nosso aplicativo")
            </span>
        </div>
    </a>
</div>