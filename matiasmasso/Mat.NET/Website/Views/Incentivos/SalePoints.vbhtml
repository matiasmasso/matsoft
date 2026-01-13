@ModelType DTOIncentiu
@Code
    Dim exs As New List(Of Exception)
    Layout = "~/Views/shared/_Layout.vbhtml"
    
    Dim oLang As DTOLang = ContextHelper.lang()
    Dim oParticipants = FEB.Incentiu.ParticipantsSync(exs, Model)
    Dim oProvincia As New DTOAreaProvincia
    ViewBag.Title = ContextHelper.Tradueix("puntos de venta participantes", "punts de venda participants", "sale points", "lojas")
End Code


<div class="pagewrapper">
    <div class="PageTitle">
        @Model.Title.Tradueix(oLang)
        <br/>
        @ViewBag.Title
    </div>

    <div>
        
        @For Each item As DTOContact In oParticipants
            If DTOAddress.Provincia(item.Address) IsNot Nothing Then
                If oProvincia.UnEquals(DTOAddress.Provincia(item.Address)) Then
                    oProvincia = DTOAddress.Provincia(item.Address)
                    @<div class="Provincia">
                        @oProvincia.Nom
                    </div>
                End If
                @<div class="SalePoint">
                    @DTOContact.NomComercialOrRaoSocialAndAddress(item)
                </div>
            End If
        Next
    </div>




</div>

@Section Styles

    <style>
        
        .pagewrapper .Thumbnail {
            display:inline-block;
            width: 178px;
        }

        .Provincia {
            margin:20px 0 10px 0;
        }

        .SalePoint {
            margin:0 0 0 20px;
        }

    </style>
End Section


