@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim exs As New List(Of Exception)
    
    Dim oBoxItems = FEB2.BoxItems.FromEBooksSync(Mvc.GlobalVariables.Emp, exs)
    Dim sTitle As String = Mvc.ContextHelper.Tradueix("Descarga de Catálogos en Libro Electrónico", "Descarrega de catalegs en llibre electronic", "Download e-book catalogues")
    ViewBag.Title = sTitle

End Code


<div class="pagewrapper">
    <div class="PageTitle">@sTitle</div>


    <p>
        @Html.Raw(Mvc.ContextHelper.Tradueix("Los siguientes ficheros le permitirán trabajar con toda la documentación en un iPad, desde donde podrá mostrar al consumidor las distintas imágenes de producto, características y precios.<br />Puede ir actualizando sus catálogos desde esta misma página.",
                                                "Els seguents fitxers li permetran treballar amb tota la documentació al seu iPad, des de on podrá mostrar al consumidor les diferents imatges de producte, caracteristiques i preus.<br />Recomanem actualitzi els catalegs des d'aquesta mateixa pagina.",
                                                "You may load next files to your iPad in order to show your customers product images, prices and features.<br /> We recomend you update your catalogues from te to time from this page."))
    </p>

    @If Not FEB2.Session.is_IOS_Browser(System.Web.HttpContext.Current) Then
        @<p class="warning">
            @Html.Raw(Mvc.ContextHelper.Tradueix("Está abriendo esta página con un dispositivo distinto de iPhone o iPad.<br />Puede descargar los ficheros, pero necesitará un software adecuado para leerlos.<br />Si desea descargarlos ahora para leerlos más tarde en un iPhone o un iPad, puede transferirlos mediante la aplicación iTunes.",
                                                                                                        "Está obrin aquesta pagina amb un dispositiu que no es ni un iPhone ni un iPad.<br />Pot descarregar els fitxers, pero necessitará el software adequat per llegir-los<br />També pot descarregar-los ara i llegir-los mes tard en un iPhone/iPad transferint-los via iTunes.",
                                                                                                        "You are browsing this page with a non iPad/iPhone device.<br />You may download the files, but you may need the proper software installed to read them.<br />You may also download them now and transfer them afterwards to an iPhone/iPad via iTunes."))
        </p>
            @<p class="warning">
                @Html.Raw(Mvc.ContextHelper.Tradueix("Para utilizar catálogos en formato electrónico deberá tener la aplicación iBooks instalada.<br />Si no la tiene, puede descargarsela gratuitamente de la ",
                                                                                           "Per llegir catalegs en format electronic ha de tenir la aplicació iBooks instalada<br />Si no la te, pot baixar-la gratuitament de l'",
                                                                                           "In order to read eBooks you need the application iBooks installed.<br />If you are missing it you may download it free of charge from "))

                <a href='http://itunes.apple.com/us/app/ibooks/id364709193?mt=8' target='_blank'>AppStore</a>
            </p>
    End If


    <p>
        @Html.Raw(Mvc.ContextHelper.Tradueix("Seleccione una de las marcas comerciales siguientes para descargar su catálogo.<br />Cuando le pregunte cómo quiere abrir el fichero seleccione la aplicación iBooks.",
                                                                      "Trii una de les marques comercials enumerades a continuació per descarregar el cataleg.<br />Quan li pregunti com vol obrir-lo seleccioni la aplicació iBooks.",
                                                                      "Pick up one of following brand links to download your catalogue.<br />Answer iBooks when asked to choose an app to read it."))

    </p>

    @For Each oBoxItem As DTOBoxItem In oBoxItems
        @<div class="VideoBrand-Container">
            <a href="@oBoxItem.NavigateUrl">
                <div class="VideoBrand-ImgContainer">
                    <img src="@oBoxItem.ImageUrl" alt="@oBoxItem.Title" />
                </div>
            </a>
        </div>
    Next
</div>

@Section Styles
<style>
            h1 {
                font-size: 1.5em;
                margin-top: 0;
            }

            .warning {
                color: red;
                font-weight: 600;
            }

            .VideoBrand-Container {
                position: relative;
                display: inline-block;
                width: 150px;
                height: 75px;
                background: #FFFFFF;
                border: 1px solid gray;
                margin-bottom: 5px;
            }

                .VideoBrand-Container a {
                    color: #FFFFFF;
                }

            .VideoBrand-ImgContainer {
                position: relative;
                height: 75px;
                width: 100%;
                /*
            display: table-cell;
            text-align: center;
            vertical-align: middle;
    */
            }

                .VideoBrand-ImgContainer img {
                    position: absolute;
                    top: 0;
                    left: 0;
                    bottom: 0;
                    right: 0;
                    margin: auto;
                    display: block;
                    width: 80%;
                }
</style>
End Section