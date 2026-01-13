@ModelType DTO.DTOProductPageQuery
@Code
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    BLL.BLLProduct.Load(Model.Product)

    Dim oDistributor As DTO.DTOContact = Mvc.ContextHelper.GetDistribuidorFromCookie(Me.Context)
    Dim oLang As DTO.DTOLang = oWebsession.Lang

    Dim oCountries As List(Of DTO.DTOGuidNom) = BLL.BLLWebAtlas.Countries(BLL.BLLProduct.CategoryOrBrand(Model.Product), oLang)
    Dim oDefaultCountry As DTO.DTOCountry = Nothing
    Dim oZonas As New List(Of DTO.DTOGuidNom)
    Dim oBestZona As DTO.DTOZona = Nothing
    Dim oLocations As New List(Of DTO.DTOGuidNom)
    Dim oBestLocation As DTO.DTOLocation = Nothing
    Dim oDistributors As List(Of DTO.DTOProductDistributor)

    Dim onlineSellers = BLL.BLLWtbolLandingPages.All(Model.Product).Count > 0
    BLL.BLLWebAtlas.SetSalePoints(Model, oWebsession, oCountries, oDefaultCountry, oZonas, oBestZona, oLocations, oBestLocation, oDistributors)


End Code

<style>
    .ProductPillTitle {
        clear:both;
    } 
    
    .AreaDropdown {
        width: 32%;
        display: inline-block;
    }
    .AreaDropdown select {
        width:95%;
    }
    .recommendedDistributor {
        width:320px;
        margin:20px auto;
    }
</style>

@If oDistributor Is Nothing Then

    @<div>
        <div class="ProductPillTitle">
            <h3>
                @Html.Raw(oLang.Tradueix("¡Encuentra tu punto de venta más cercano!", "Troba el teu punt de venda mes proper!", "Find your closest sale point!", "Encontra o ponto de venda mais próximo de ti!"))
                @If onlineSellers Then
                    @<div class="RightAlign">
                        @Html.Raw(oLang.Tradueix("o cómpralo ahora online", "o compra'l ara online", "or purchase it online now", "o compralo ahora online"))
                        <a href="@BLL.BLLProduct.LandingPagesUrl(Model.Product)">@Html.Raw(oLang.Tradueix("aquí", "aquí", "here", "aquí"))</a>
                    </div>
                End If
            </h3>
        </div>

        <div class="AreaDropdown">
            <select id="dropdownCountry" onchange="onDropdownCountryChange()">
                @For Each oCountry As DTO.DTOGuidNom In oCountries
                    If oCountry.Equals(oDefaultCountry) Then
                    @<option value="@oCountry.Guid.ToString" selected>@oCountry.Nom</option>
                    Else
                    @<option value="@oCountry.Guid.ToString">@oCountry.Nom</option>
                    End If
                Next
            </select>
        </div>

        <div class="AreaDropdown">
            <select id="dropdownZona" onchange="onDropdownZonaChange()">
                @For Each oZona As DTO.DTOGuidNom In oZonas
                    ' If oZona.Nom = "Madrid" Then Stop
                    If oBestLocation IsNot Nothing AndAlso (oZona.Equals(oBestLocation.Zona) Or oZona.Equals(oBestLocation.Zona.Provincia)) Then
                        @<option value="@oZona.Guid.ToString" selected>@oZona.Nom</option>
                    Else
                        @<option value="@oZona.Guid.ToString">@oZona.Nom</option>
                    End If
                Next
            </select>
        </div>

        <div class="AreaDropdown">
            <select id="dropdownLocation" onchange="onDropdownLocationChange()">
                @For Each oLocation As DTO.DTOGuidNom In oLocations
                    If oLocation.Equals(oBestLocation) Then
                    @<option value="@oLocation.Guid.ToString" selected>@oLocation.Nom</option>
                    Else
                    @<option value="@oLocation.Guid.ToString">@oLocation.Nom</option>
                    End If
                Next
            </select>
        </div>

        <div id="divDistributors">
           @Html.Partial("_SalePointsDistributors", oDistributors)
        </div>
    </div>

Else

    @<div class="recommendedDistributor">
        @oWebSession.Tradueix("Distribuidor recomendado:","Distribuidor recomenat:","Recommended distributor:")
        <p>
            @For Each line As String In BLL.BLLAddress.Lines(oDistributor)
                @line
                @<br />
            Next
        </p>
    </div>
    
End If

