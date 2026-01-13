Public Class DTOGuidNomNode
    Inherits DTOGuidNom
    Property Parent As DTOGuidNomNode
    Property Children As New List(Of DTOGuidNomNode)

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Sub AddChild(value As DTOGuidNomNode)
        value.Parent = Me
        _Children.Add(value)
    End Sub
End Class
