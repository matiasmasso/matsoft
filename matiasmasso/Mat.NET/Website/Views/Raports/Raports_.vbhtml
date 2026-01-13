@ModelType IEnumerable(Of DTOMem)

@Code
    
End Code

@If Model.Count = 0 Then
    ContextHelper.Tradueix("(sin informes registrados)", "(sense raports)", "(no raports available)")
Else

    For Each oMem As DTOMem In Model
        @<p>
            <b>
                @Format(oMem.Fch, "dd/MM/yy")
                <span class="UserNickname"> @DTOUsrLog2.UsrCreatedNom(oMem.UsrLog)</span>
                @oMem.Contact.Nom
            </b>
            <span>@oMem.Text</span>
        </p>
    Next

End If





