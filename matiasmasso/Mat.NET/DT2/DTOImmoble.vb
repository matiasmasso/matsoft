Public Class DTOImmoble
    Inherits DTOGuidNom

    Property Emp As DTOEmp
    Property Address As DTOAddress
    Property Cadastre As String
    Property FchFrom As Date
    Property FchTo As Date

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
