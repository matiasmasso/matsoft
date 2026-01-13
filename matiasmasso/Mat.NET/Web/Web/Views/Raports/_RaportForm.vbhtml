@Code

    Dim exs As New List(Of Exception)

    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim oRep = FEB2.User.GetRepSync(oUser, exs)
    Dim oProductDistributors As List(Of DTOProductDistributor) = FEB2.ProductDistributors.FromRepSync(exs, oRep)
    Dim oCountries As New List(Of DTOArea)
    Dim oZonas As New List(Of DTOArea)
    Dim oLocations As New List(Of DTOArea)
    Dim oCountry As New DTOArea
    Dim oZona As DTOArea
    Dim oLocation As DTOArea
    Dim oCustomers As List(Of DTOProductDistributor)
    Dim oCustomer As DTOContact = Nothing
    oCountries = DTOProductDistributor.Countries(oProductDistributors)
    If Model.contact IsNot Nothing Then

        oCustomer = Model.contact

        oCountry = New DTOArea(DTOAddress.Country(oCustomer.address).Guid, DTOAddress.Country(oCustomer.address).LangNom.Tradueix(Mvc.ContextHelper.lang()))
        oZona = New DTOArea(DTOAddress.Zona(oCustomer.Address).Guid, DTOAddress.Zona(oCustomer.Address).Nom)
        oLocation = New DTOArea(DTOAddress.Location(oCustomer.Address).Guid, DTOAddress.Location(oCustomer.Address).Nom)

        oZonas = DTOProductDistributor.Zonas(oProductDistributors, oCountry)
        oLocations = DTOProductDistributor.Locations(oProductDistributors, oZona)
        oCustomers = DTOProductDistributor.All(oProductDistributors, oLocation)
    End If
End Code

<div id="CustomerSelection">
    <div>
        <select>
            <option value="">@Mvc.ContextHelper.Tradueix("(seleccionar pais)", "(sel.leccionar pais)", "(country selection)")</option>
            @For Each item In oCountries
                If item.Equals(oCountry) Then
                    @<option value="@item.Guid.ToString" selected>@item.Nom</option>
                Else
                    @<option value="@item.Guid.ToString" >@item.Nom</option>
                End If
            Next
        </select>
    </div>
    <div>
        <select>
            <option value="">@Mvc.ContextHelper.Tradueix("(seleccionar zona)", "(sel.leccionar zona)", "(area selection)")</option>
            @If oCustomer IsNot Nothing Then
                For Each item In oZonas
                    If item.Equals(oZona) Then
                        @<option value="@item.Guid.ToString" selected>@item.Nom</option>
                    Else
                        @<option value="@item.Guid.ToString">@item.Nom</option>
                    End If
                Next
            End If
        </select>
    </div>
    <div>
        <select>
            <option value="">@Mvc.ContextHelper.Tradueix("(seleccionar población)", "(sel.leccionar població)", "(location selection)")</option>
            @If oCustomer IsNot Nothing Then
                For Each item In oLocations
                    If item.Equals(oLocation) Then
                        @<option value="@item.Guid.ToString" selected>@item.Nom</option>
                    Else
                        @<option value="@item.Guid.ToString">@item.Nom</option>
                    End If
                Next
            End If
        </select>
    </div>
    <div>
        <select>
            <option value="">@Mvc.ContextHelper.Tradueix("(seleccionar cliente)", "(sel.leccionar client)", "(customer selection)")</option>
            @If oCustomer IsNot Nothing Then
                For Each item In oCustomers
                    If item.Equals(oCustomer) Then
                        @<option value="@item.Guid.ToString" selected>@item.Nom</option>
                    Else
                        @<option value="@item.Guid.ToString">@item.Nom</option>
                    End If
                Next
            End If
        </select>
    </div>
</div>

<div Class="Fch">
    @Mvc.ContextHelper.Tradueix("fecha de visita", "data de visita", "visit date")
    <input type = "date" value="@DateTime.Today.ToString("yyyy-MM-dd")" @IIf(Model.IsNew, "", "disabled = 'disabled'") />

<div>
    <textarea @IIf(Model.IsNew, "", "disabled = 'disabled'") ></textarea>
</div>

<div Class="Submit" >
    <input type = "button" id="SubmitButton" @IIf(Model.IsNew, "", "hidden = 'hidden'") value='@Mvc.ContextHelper.Tradueix("Enviar", "Enviar", "Submit")' />
</div>
