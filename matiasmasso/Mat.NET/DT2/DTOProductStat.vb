Public Class DTOProductStat
    Inherits DTOProduct
    Property items As List(Of DTOYearMonth)

    Public Sub New()
        MyBase.New
        _items = New List(Of DTOYearMonth)
    End Sub

    Shared Shadows Function Factory(oProduct As DTOProduct, exs As List(Of Exception)) As DTOProductStat
        Dim retval As New DTOProductStat
        DTOBaseGuid.CopyPropertyValues(Of DTOProduct)(oProduct, retval, exs)
        Return retval
    End Function
End Class
