Public Class DTOLocation
    Inherits DTOArea

    Shadows Property zona As DTOZona
    Property export As Boolean
    Property comarca As DTOComarca
    Property zips As List(Of DTOZip)
    Property contacts As List(Of DTOContact)

    Property portsCondition As DTOPortsCondicio

    Public Enum wellknowns
        madrid
    End Enum

    Public Sub New()
        MyBase.New()
        MyBase.Cod = Cods.Location
        _Zips = New List(Of DTOZip)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        MyBase.Cod = Cods.Location
        _Zips = New List(Of DTOZip)
    End Sub

    Shared Function wellknown(id As DTOLocation.wellknowns) As DTOLocation
        Dim retval As DTOLocation = Nothing
        Select Case id
            Case DTOLocation.wellknowns.madrid
                retval = New DTOLocation(New Guid("3EC9E266-C7D7-42B0-94DD-27334A5EE118"))
        End Select
        Return retval
    End Function

    Overloads Shared Function Factory(oZona As DTOZona) As DTOLocation
        Dim retval As New DTOLocation
        retval.zona = oZona
        Return retval
    End Function

    Shared Shadows Function FullNom(oLocation As DTOLocation, Optional oLang As DTOLang = Nothing) As String
        Dim retval As String = ""
        If oLocation IsNot Nothing Then
            retval = oLocation.FullNom(oLang)
        End If
        Return retval
    End Function

    Public Shadows Function FullNom(Optional oLang As DTOLang = Nothing) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(MyBase.Nom)
        Dim oZona As DTOZona = _Zona
        If oZona IsNot Nothing Then
            Dim oCountry As DTOCountry = _Zona.Country
            If DTOArea.IsEsp(oCountry) Then
                Dim oProvincia As DTOAreaProvincia = oZona.Provincia
                If oProvincia Is Nothing Then
                    If MyBase.Nom <> oZona.Nom Then
                        sb.Append(" (" & oZona.Nom & ")")
                    End If
                Else
                    If MyBase.Nom <> oProvincia.Nom Then
                        sb.Append(" (" & oProvincia.Nom & ")")
                    End If
                End If
            Else
                If oCountry IsNot Nothing Then
                    If oLang Is Nothing Then oLang = DTOLang.ESP
                    sb.Append(" (" & oCountry.langNom.tradueix(oLang) & ")")
                End If
            End If
        End If

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Shadows Function FullNomSegmented(oLocation As DTOLocation, oLang As DTOLang) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(oLocation.Nom)
        sb.Append("/")
        sb.Append(oLocation.Zona.FullNomSegmented(oLang))
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Shadows Function FullNomSegmentedReversed(oLocation As DTOLocation, oLang As DTOLang) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(oLocation.Zona.FullNomSegmented(oLang))
        sb.Append("/")
        sb.Append(oLocation.Nom)
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Public Function CountryNom(oLang As DTOLang) As String
        Dim retval As String = ""
        If _Zona IsNot Nothing Then
            retval = _Zona.CountryNom(oLang)
        End If
        Return retval
    End Function

    Shared Function ExportCod(oLocation As DTOLocation) As DTOInvoice.ExportCods
        Dim retval As DTOInvoice.ExportCods = DTOInvoice.ExportCods.NotSet
        If oLocation IsNot Nothing Then
            If oLocation.Zona IsNot Nothing Then
                Select Case oLocation.Zona.ExportCod
                    Case DTOInvoice.ExportCods.Intracomunitari, DTOInvoice.ExportCods.Extracomunitari
                        retval = oLocation.Zona.ExportCod
                    Case Else
                        If oLocation.Zona.Country IsNot Nothing Then
                            retval = oLocation.Zona.Country.ExportCod
                        End If
                End Select
            End If
        End If
        Return retval
    End Function

    Shared Shadows Function Provincia(oLocation As DTOLocation) As DTOAreaProvincia
        Dim retval As DTOAreaProvincia = Nothing
        If oLocation IsNot Nothing Then
            If oLocation.Zona IsNot Nothing Then
                retval = oLocation.Zona.Provincia
            End If
        End If
        Return retval
    End Function

    Shared Function ProvinciaOrZonaNom(oLocation As DTOLocation) As String
        Dim retval As String = ""
        If oLocation IsNot Nothing AndAlso oLocation.Zona IsNot Nothing Then
            If oLocation.Zona.Provincia Is Nothing Then
                retval = oLocation.Zona.Nom
            Else
                retval = oLocation.Zona.Provincia.Nom
            End If
        End If
        Return retval
    End Function
End Class
