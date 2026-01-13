@ModelType DTOProductBrand
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code

        <div class="Properties">
            <div>
                @lang.Tradueix("Nombre", "Nom", "Name")
            </div>
            <div>@Model.nom</div>
            <div>
                @lang.Tradueix("CNAP")
            </div>
            <div>@DTOCnap.FullNom(Model.cnap, lang)</div>
            <div>
                @lang.Tradueix("Cod.Arancelario (TARIC)", "Cod.Arancelari (TARIC)", "Customs Tax code (TARIC)")
            </div>
            <div>@DTOCodiMercancia.FullNom(Model.codiMercancia)</div>
            <div>
                @lang.Tradueix("Made in")
            </div>
            <div>@DTOCountry.FullNom(Model.madeIn, lang)</div>
            <div>
                @lang.Tradueix("Habilitado para consumidores", "Habilitat per consumidors", "Consumers enabled")
            </div>
            <div>@IIf(Model.enabledxConsumer, lang.Tradueix("Sí", "Sí", "Yes"), lang.Tradueix("No", "No", "Not"))</div>
            <div>
                @lang.Tradueix("Habilitado para profesionales", "Habilitat per professionals", "Professionals enabled")
            </div>
            <div>@IIf(Model.enabledxPro, lang.Tradueix("Sí", "Sí", "Yes"), lang.Tradueix("No", "No", "Not"))</div>
            <div>
                @lang.Tradueix("Marca obsoleta", "Marca obsoleta", "outdated brand")
            </div>
            <div>@IIf(Model.obsoleto, lang.Tradueix("Sí", "Sí", "Yes"), lang.Tradueix("No", "No", "Not"))</div>
        </div>

@Section Styles
    <style>
        .Properties {
            display: grid;
            grid-template-columns: auto 1fr;
            grid-gap: 10px;
        }
    </style>
End Section