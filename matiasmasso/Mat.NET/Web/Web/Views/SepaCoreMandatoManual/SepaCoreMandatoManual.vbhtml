@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code


        <h1>@ViewBag.Title</h1>

    <section class="SepaInfo">
        <p>
            @Html.Raw(lang.Tradueix("Cada vez que seleccionemos uno de los siguientes enlaces descargaremos un formulario con un número de referencia distinto.",
                                                                            "Cada cop que seleccionem un dels següents enllaços descarregarem un mandat amb un nou número de referència."))
    </p>
        <p>
    @Html.Raw(lang.Tradueix("Es muy importante que imprimamos <strong>dos copias</strong> de cada descarga para que el número de referencia de la copia que se queda el cliente coincida exactamente con el del documento que ha firmado.",
                                                                                      "Es molt important que imprimim <strong>dos copies</strong> de cada descarrega perque el número de referència de la copia que es quedi el client coincideixi amb el del document que ha signat."))</p>
    <p>
        @Html.Raw(lang.Tradueix("Recuerda que en caso de persona jurídica la firma debe ir acompañada del sello de la empresa y del nombre y número de DNI del firmante.",
                                                                                                 "Recorda que en cas de persona jurídica la signatura ha de anar acompanyada del segell de la empresa i del nom i DNI de la persona que signa."))</p>
</section>

<section class="LangSelection">
    <div>
        <a href="@FEB2.SepaMandato.UrlForNewMandatoManual(DTOLang.ESP)" target="_blank">
            @lang.Tradueix("Español", "Espanyol", "Spanish", "Espanhol")
        </a>
    </div>
    <div>
        <a href="@FEB2.SepaMandato.UrlForNewMandatoManual(DTOLang.CAT)" target="_blank">
            @lang.Tradueix("Catalán", "Català", "Catalan", "Catalão")
        </a>
    </div>
    <div>
        <a href="@FEB2.SepaMandato.UrlForNewMandatoManual(DTOLang.ENG)" target="_blank">
            @lang.Tradueix("Inglés", "Anglés", "English", "Inglês")
        </a>
    </div>
    <div>
        <a href="@FEB2.SepaMandato.UrlForNewMandatoManual(DTOLang.POR)" target="_blank">
            @lang.Tradueix("Portugués", "Portuguès", "Portuguese", "Português")
        </a>
    </div>
</section>


@Section styles
    <Style scoped>
        .ContentColumn {
            max-width: 600px;
            margin: 0 auto;
        }

        .LangSelection {
            margin: 30px 0 0 50px;
        }

            .LangSelection div {
                margin: 10px 0 0 0;
            }

                .LangSelection div a:hover {
                    color: red;
                }
    </Style>
End Section
