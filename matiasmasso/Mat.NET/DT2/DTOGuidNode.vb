Public Class DTOGuidNode
    Inherits DTOGuidNom

    Property Items As List(Of DTOGuidNode)
    Public Sub New(oGuid As Guid, Optional sNom As String = "")
        MyBase.New(oGuid, sNom)
        _Items = New List(Of DTOGuidNode)
    End Sub
End Class
