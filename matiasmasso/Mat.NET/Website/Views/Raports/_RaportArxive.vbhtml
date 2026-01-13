@ModelType List(Of DTOMem)

@Code
    

End Code

@If Model.Count = 0 Then
    ContextHelper.Tradueix("(cliente sin informes previos)", "(client sense raports previs)", "(no previous raports from this customer)")
Else
    @<div  class="truncate">
         @(ContextHelper.Tradueix("ultimos informes de ", "darrers raports de ", "last raports from ") & Model.First.Contact.FullNom)
    </div>

    For Each oMem As DTOMem In Model
        @<p>
             <b>@Format(oMem.Fch, "dd/MM/yy")</b>
             <span>@oMem.Text</span>
        </p>
    Next
End If



