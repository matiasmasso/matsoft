Public Class DTOCodiMercancia
    Shared EmptyValue As New String("0", 8)

    Property Id As String
    Property Dsc As String

    Property IsLoaded As Boolean

    Public Sub New(Id As String)
        MyBase.New
        _Id = Id
    End Sub

    Shared Function FullNom(oCodiMercancia As DTOCodiMercancia) As String
        Dim retval As String = ""
        If oCodiMercancia IsNot Nothing Then
            retval = String.Format("{0} {1}", oCodiMercancia.Id, oCodiMercancia.Dsc)
        End If
        Return retval
    End Function
End Class
