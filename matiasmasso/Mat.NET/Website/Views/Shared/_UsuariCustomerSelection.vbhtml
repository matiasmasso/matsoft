@ModelType List(Of DTOCustomer)
@Code
    Dim exs As New List(Of Exception)

    Dim oActiveCustomers As List(Of DTOCustomer) = Nothing
    If Model Is Nothing Then
        Dim oUser = ContextHelper.FindUserSync()
        Dim oCustomers = FEB.User.GetCustomersSync(oUser, exs)
        oActiveCustomers = oCustomers.FindAll(Function(x) x.Obsoleto = False).ToList
    Else
        oActiveCustomers = Model
    End If

End Code


<style>
    .Address {
        display: block;
        margin: 1em 0 1.2em;
        text-align:left;
    }

        .Address:hover {
            color: red;
        }

    #customerSelection {
        text-align:left;
    }

</style>


<div id="customerSelection">

    <div class="Title" @IIf(oActiveCustomers.Count > 1, "", "hidden = 'hidden'")>
        @ContextHelper.Tradueix("Seleccione el establecimiento", "Sel·lecióni l'establiment", "Select your sale point selection")
    </div>

    @For Each oContact As DTOContact In oActiveCustomers
        @<a class="Address" @Html.Raw(IIf(oActiveCustomers.Count=1,"","href='#'")) data-guid="@oContact.Guid.ToString">
            <span>@oContact.NomComercialOrDefault()</span>
            <br />
            @Html.Raw(DTOAddress.FullHtml(oContact.Address))
        </a>
    Next

</div>






