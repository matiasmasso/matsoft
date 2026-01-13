Public Class DTOAreaProvincia
    Inherits DTOArea

    Shadows Property Cod As String
    Property Regio As DTOAreaRegio
    Property Zonas As List(Of DTOZona)

    Public Enum wellknowns
        barcelona
    End Enum

    Public Sub New()
        MyBase.New()
        _Zonas = New List(Of DTOZona)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Zonas = New List(Of DTOZona)
    End Sub

    Shared Shadows Function Factory(oCountry As DTOCountry) As DTOAreaProvincia
        Dim retval As New DTOAreaProvincia
        With retval
            .Regio = DTOAreaRegio.Factory(oCountry)
            .Regio.nom = "(sel·lecionar regió)"
            .nom = "(nova provincia)"
        End With
        Return retval
    End Function

    Shared Function wellknown(id As DTOAreaProvincia.wellknowns) As DTOAreaProvincia
        Dim retval As DTOAreaProvincia = Nothing
        Dim sGuid As String = ""
        Select Case id
            Case DTOAreaProvincia.wellknowns.barcelona
                sGuid = "C4E41C16-93C9-41CA-9340-28ADA23B299D"
        End Select

        If sGuid > "" Then
            Dim oGuid As New Guid(sGuid)
            retval = New DTOAreaProvincia(oGuid)
        End If
        Return retval
    End Function
End Class
