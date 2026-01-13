@ModelType List(Of DTOProductDistributor)
@Code
    
    ViewBag.Title = ContextHelper.Tradueix("Mis clientes", "Els meus clients", "My customers")
    Layout = "~/Views/shared/_Layout.vbhtml"
    Dim oCountry As New DTOGuidNom
    Dim oZona As New DTOGuidNom
    Dim oLocation As New DTOGuidNom

End Code


<div class="pagewrapper">

    <div class="Grid">

        @For Each item As DTOProductDistributor In Model

            If Not item.Country.Equals(oCountry) Then
                oCountry = item.Country
            @<div class="Row">
                <div class="CellIco"></div>
                <div class="CellTxt Country">@oCountry.Nom</div>
            </div>
            End If

            If Not item.Zona.Equals(oZona) Then
                oZona = item.Zona
                @<div class="Row">
                     <div class="CellIco"></div>
                    <div class="CellTxt ">&nbsp;</div>
                </div>
                @<div class="Row">
                     <div class="CellIco"></div>
                    <div class="CellTxt Zona">@oZona.Nom</div>
                </div>
            End If

            If Not item.Location.Equals(oLocation) Then
                oLocation = item.Location
                @<div class="Row">
                    
                     <div class="CellIco"></div>
                    <div class="CellTxt Location">
                        @oLocation.Nom
                    </div>
                </div>
            End If

            @<div class="Row">
                 <div class='@IIf(item.Promo, "CellStar", "CellIco")'></div>
                <div class="CellTxt Customer">
                    <a href="@FEB.Contact.Url(item.Contact)">
                        @item.Nom
                    </a>
                </div>
            </div>

        Next

    </div>
</div>

@Section Styles
    <link href="~/Media/Css/Tables.css" rel="stylesheet" />

    <style>
        .Row .Country {
            font-weight: 900;
        }

        .Row .Zona {
            font-weight: 700;
            padding-left: 20px;
        }

        .Row .Location {
            font-style: italic;
            padding-left: 40px;
        }

        .Row .Customer {
            padding-left: 60px;
        }


    </style>
End Section