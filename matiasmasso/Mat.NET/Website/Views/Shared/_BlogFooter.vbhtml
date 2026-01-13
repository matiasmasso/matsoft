@Code
    
End Code

<div id="LegalContainer">
    <a href="/avisolegal">
        @Html.Raw(ContextHelper.Tradueix("Aviso legal", "Avís Legal", "Loyal notice", "Políticas legais"))
    </a>
    |
    <a href="/privacidad">
        @Html.Raw(ContextHelper.Tradueix("Privacidad", "Privacitat", "Privacity", "Política de Privacidade"))
    </a>
    |
    <a href="/about">
        @Html.Raw(ContextHelper.Tradueix("¿Quien somos?", "Qui som?", "About us", "Sobre nós"))
    </a>
</div>

