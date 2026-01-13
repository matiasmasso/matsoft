@Code
    Dim lang = If(ViewBag.Lang, Mvc.ContextHelper.Lang)
End Code

<div id="cookieConsent">
    <div id="closeCookieConsent">x</div>
    @lang.tradueix("Esta página usa cookies.", "Aquesta web fa servir cookies.", "This website is using cookies.", "Este site está usando cookies.")
    <a href="/cookies" target="_blank">
        @lang.tradueix("Leer más...", "Llegir més...", "More info...", "consulte mais informação...")
    </a> 
    <a class="cookieConsentOK">
        @lang.tradueix("De acuerdo", "D'acord", "That's Fine", "Isso é bom")
    </a>
</div>

