Public Class DTOBancTransferOld
    Inherits DTOCca
    Property BancEmisorItem As DTOBancTransferItem

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class

Public Class DTOBancTransferItem
    Inherits DTOCcb
    Property Iban As DTOIban
    Property Concept As String

End Class