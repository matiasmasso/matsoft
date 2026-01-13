Public Class DTOProjecte
    Inherits DTOBaseGuid

    Property Nom As String
    Property Dsc As String
    Property FchFrom As Date
    Property FchTo As Date

    Property Amt As DTOAmt

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
