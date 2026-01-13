@ModelType List(Of DTOContact)
@Code
    
End Code

<style>
    .Contact {
        margin:20px 0 20px 30px;
    }
    .Contact:first-line {
        font-weight:600;
    }
    .Contact a:hover {
        color:red;
    }
    .SelectedContact:first-line {
        font-weight:600;
    }

</style>

@If Model.Count = 0 Then
    @<div>
        <p>
            @ContextHelper.Tradueix("No nos consta ninguna cuenta vinculada a este usuario.",
                                           "No ens consta cap compte vinculat a aquest usuari.",
                                           "No account seems to be currently linked to this user.")
            <br />
            @ContextHelper.Tradueix("Por favor contacte con nuestras oficinas para tramitarlo.",
                                           "Si us plau contacti amb les nostres oficines per tramitar-ho.",
                                           "Please contact our offices to register.")
        </p>
    </div>

ElseIf Model.Count = 1 Then

    @<div class="SelectedContact" data-guid="@Model(0).Guid.ToString">
        @Html.Raw(FEB.Contact.HtmlNameAndAddress(Model(0)))
    </div>

Else

    @<div class="SelectContact">
        <p>
            @ContextHelper.Tradueix("Por favor seleccione una cuenta:",
                                                   "Si us plau seleccioni un compte:",
                                                   "Please select your account:")
        </p>

        @For Each oContact As DTOContact In Model
            @<div class="Contact">
                <a href='#' data-guid="@oContact.Guid.ToString">
                    @Html.Raw(FEB.Contact.HtmlNameAndAddress(oContact))
                </a>
            </div>
        Next
    </div>

    @<div class="SelectedContact" data-guid="" hidden="hidden">
         <div class="Contact"></div>
    </div>

End If


