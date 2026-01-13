Public Class DTOWtbolSerp
    Inherits DTOBaseGuid

    Property Session As DTOSession
    Property Ip As String
    Property UserAgent As String
    Property CountryCode As String
    Property Product As DTOProduct
    Property Fch As DateTime
    Property Items As List(Of Item)

    Public Sub New()
        MyBase.New()
        _Items = New List(Of Item)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Items = New List(Of Item)
    End Sub

    Public Class Item
        Property Pos As Integer
        Property Site As DTOWtbolSite
    End Class
End Class
