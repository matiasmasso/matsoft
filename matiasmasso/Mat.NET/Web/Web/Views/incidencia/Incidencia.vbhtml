@ModelType DTOIncidencia
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)

    ViewBag.Title = Mvc.ContextHelper.Tradueix("Incidencia", "Incidencia", "Support incidence")
    Dim exs As New List(Of Exception)
    FEB2.Contact.Load(Model.Customer, exs)
End Code




    <div class="DivRow">
        <div class="DivLabel">
            @Mvc.ContextHelper.Tradueix("Incidencia num.", "Incidencia num.", "Incident #")
        </div>
        <div class="DivData">
            @Model.Num
        </div>
    </div>
    <div class="DivRow">
        <div class="DivLabel">
            @Mvc.ContextHelper.Tradueix("Fecha", "Data", "Date")
        </div>
        <div class="DivData">
            @Model.Fch
        </div>
    </div>
    <div class="DivRow">
        <div class="DivLabel">
            @Mvc.ContextHelper.Tradueix("Cliente", "Client", "Customer")
        </div>
        <div class="DivData">
            @Html.Raw(FEB2.Contact.HtmlNameAndAddress(Model.Customer))
        </div>
    </div>
    <div class="DivRow">
        <div class="DivLabel">
            @Mvc.ContextHelper.Tradueix("Persona de contacto", "Persona de contacte", "Contact person", "Pessoa de contacto")
        </div>
        <div class="DivData">
            @Model.ContactPerson
        </div>
    </div>
    <div class="DivRow">
        <div class="DivLabel">
            @Mvc.ContextHelper.Tradueix("Teléfono", "Telefon", "Phone")
        </div>
        <div class="DivData">
            @Model.Tel
        </div>
    </div>

    <div class="DivRow">
        <div class="DivLabel">
            @Mvc.ContextHelper.Tradueix("Su referencia", "La seva referencia", "Your reference", “A sua referencia”)
        </div>
        <div class="DivData">
            @Model.CustomerRef
        </div>
    </div>
    <div class="DivRow">
        <div class="DivLabel">
            @Mvc.ContextHelper.Tradueix("producto", "producte", "Product")
        </div>
        <div class="DivData">
            @Model.product.FullNom()
        </div>
    </div>
    <div class="DivRow">
        <div class="DivLabel">
            @Mvc.ContextHelper.Tradueix("Numero de serie", "Numero de serie", "Serial number")
        </div>
        <div class="DivData">
            @Model.serialNumber
        </div>
    </div>
    <div class="DivRow">
        <div class="DivLabel">
            @Mvc.ContextHelper.Tradueix("Fecha fabricación", "Data fabricació", "Manufacturing date")
        </div>
        <div class="DivData">
            @Model.ManufactureDate
        </div>
    </div>
    <div class="DivRow">
        <div class="DivLabel">
            @Mvc.ContextHelper.Tradueix("Descripción", "Descripció", "Description")
        </div>
        <div class="DivData">
            @Model.description
        </div>
    </div>
    <div class="DivRow">
        <div class="DivLabel">
            @Mvc.ContextHelper.Tradueix("Vídeos")
        </div>
        <div class="DivData">
            @If (Model.videos.Count > 0) Then
                For Each oDocFile As DTODocFile In Model.Videos
                    @<video height="150" width="300" controls>
                        <source src="@oDocFile.DownloadUrl()"/>
                    </video>
                Next
            Else
                @("no")
            End If
        </div>
    </div>
    <div class="DivRow">
        <div class="DivLabel">
            @Mvc.ContextHelper.Tradueix("Imágenes", "Imatges", "Images")
        </div>
        <div class="DivData">
            @If (Model.DocFileImages.Count > 0) Then
                For Each oDocFile As DTODocFile In Model.DocFileImages
                    @<div class="DocFile">
                        <a href="@FEB2.DocFile.DownloadUrl(oDocFile, False)" target="_blank">
                            <img src="@FEB2.DocFile.ThumbnailUrl(oDocFile)" width='150px' height='auto' />
                        </a>
                    </div>
                Next
            Else
                @("no")
            End If
        </div>
    </div>
    <div class="DivRow">
        <div class="DivLabel">
            @Mvc.ContextHelper.Tradueix("Ticket de compra", "Ticket de compra", "Purchase ticket")
        </div>
        <div class="DivData">
            @If (Model.purchaseTickets.Count > 0) Then
                For Each oDocFile As DTODocFile In Model.purchaseTickets
                    @<div class="DocFile">
                        <a href="@FEB2.DocFile.DownloadUrl(oDocFile, False)" target="_blank">
                            <img src="@FEB2.DocFile.ThumbnailUrl(oDocFile)" width='150px' height='auto' />
                        </a>
                    </div>
                Next
            Else
                @("no")
            End If
        </div>
    </div>
    <div class="DivRow">
        <div class="DivLabel">
            @Mvc.ContextHelper.Tradueix("Fecha de cierre", "Data de tancament", "Closure date")
        </div>
        <div class="DivData">
            @If Model.FchClose <> Nothing Then
                Html.Raw(Model.FchClose.ToString("dd/MM/yy"))
            End If
        </div>
    </div>
    <div class="DivRow">
        <div class="DivLabel">
            @Mvc.ContextHelper.Tradueix("Solución", "Solució", "Solution")
        </div>
        <div class="DivData">
            @DTOIncidencia.CodiNom(Model.Tancament, Mvc.ContextHelper.lang())
        </div>
    </div>


@Section Styles
<style>
    .ContentColumn {
        max-width:600px;
        margin:auto;
    }

    .DivRow {
        border: solid 1px lightgray;
        padding:7px 7px 5px 7px;
    }

    .DivLabel {
        display: inline-block;
        width: 150px;
        vertical-align: top;
        color: gray;
        font-weight: 500;
    }

    .DivData {
        display: inline-block;
        min-width: 320px;
        vertical-align: top;
        padding:0 0 0 20px;
    }

    .DocFile {
        display: inline-block;
        width: 100px;
    }

        .DocFile img {
            width: 100%;
        }
</style>
End Section
