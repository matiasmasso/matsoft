Public Class DTOGuidNomAmt
    Inherits DTOGuidNom
    Property Amt As DTOAmt

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Sub New()
        MyBase.New
    End Sub
End Class
