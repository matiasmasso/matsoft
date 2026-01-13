Public Class DTOZip
    Inherits DTOArea

    Property zipCod As String
    Shadows Property location As DTOLocation
    Property contacts As List(Of DTOContact)

    Public Sub New()
        MyBase.New()
        MyBase.Cod = Cods.Country
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        MyBase.Cod = Cods.Country
    End Sub

    Shared Shadows Function Factory(oLocation As DTOLocation, sZipCod As String) As DTOZip
        Dim retval As New DTOZip
        With retval
            .Location = oLocation
            .ZipCod = sZipCod
        End With
        Return retval
    End Function

    Public Function ZipyCit() As String
        Dim sb As New System.Text.StringBuilder
        If _ZipCod > "" Then
            sb.Append(_ZipCod & " ")
        End If
        If _Location IsNot Nothing Then
            sb.Append(_Location.Nom)
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function ZipyCit(oZip As DTOZip) As String
        Dim retval As String = ""
        If oZip IsNot Nothing Then
            retval = oZip.ZipyCit
        End If
        Return retval
    End Function

    Public Shadows Function FullNom(oLang As DTOLang) As String
        Dim sb As New System.Text.StringBuilder
        If _ZipCod > "" Then
            sb.Append(_ZipCod)
        End If
        If _Location IsNot Nothing Then
            sb.Append(" ")
            sb.Append(_Location.FullNom(oLang))
        End If

        Dim retval As String = sb.ToString
        Return retval
    End Function

  shadows  Shared Function FullNom(oZip As DTOZip, Optional oLang As DTOLang = Nothing) As String
        Dim retval As String = ""
        If oZip IsNot Nothing Then
            retval = oZip.FullNom(oLang)
        End If
        Return retval
    End Function

    Shared Shadows Function FullNomSegmented(oZip As DTOZip, oLang As DTOLang) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(oZip.ZipyCit())
        sb.Append("/")
        sb.Append(oZip.Location.Zona.FullNomSegmented(oLang))
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Shadows Function FullNomSegmentedReversed(oZip As DTOZip, oLang As DTOLang) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(DTOLocation.FullNomSegmented(oZip.Location, oLang))
        sb.Append("/")
        sb.Append(oZip.ZipCod)
        Dim retval As String = sb.ToString
        Return retval
    End Function


    Shared Function Validate(oCountry As DTOCountry, sZipCod As String) As Boolean
        Dim retval As Boolean = True
        If oCountry.Equals(DTOCountry.wellknown(DTOCountry.wellknowns.Spain)) Then
            Dim sPattern = "^[0-9]{5}$"
            retval = TextHelper.RegexMatch(sZipCod, sPattern)
        ElseIf oCountry.Equals(DTOCountry.wellknown(DTOCountry.wellknowns.Portugal)) Then
            Dim sPattern = "^[0-9]{4}-[0-9]{3}$"
            retval = TextHelper.RegexMatch(sZipCod, sPattern)
        End If
        Return retval
    End Function

    Public Function CountryNom(oLang As DTOLang) As String
        Dim retval As String = ""
        If _Location IsNot Nothing Then
            retval = _Location.CountryNom(oLang)
        End If
        Return retval
    End Function

    Shared Function ExportCod(oZip As DTOZip) As DTOInvoice.ExportCods
        Dim retval As DTOInvoice.ExportCods = DTOInvoice.ExportCods.NotSet
        If oZip IsNot Nothing Then
            If oZip.Location IsNot Nothing Then
                retval = DTOLocation.ExportCod(oZip.Location)
            End If
        End If
        Return retval
    End Function

    Shared Shadows Function Provincia(oZip As DTOZip) As DTOAreaProvincia
        Dim retval As DTOAreaProvincia = Nothing
        If oZip IsNot Nothing Then
            If oZip.Location IsNot Nothing Then
                retval = DTOLocation.Provincia(oZip.Location)
            End If
        End If
        Return retval
    End Function



    Shared Function Countries(oZips As IEnumerable(Of DTOZip)) As List(Of DTOCountry)
        Dim retval = oZips.GroupBy(Function(x) x.Location.Zona.Country.Guid).Select(Function(y) y.First).Select(Function(z) z.Location.Zona.Country).ToList
        Return retval
    End Function

    Shared Function Zonas(oZips As IEnumerable(Of DTOZip), Optional FromCountry As DTOCountry = Nothing) As List(Of DTOZona)
        Dim retval = oZips.GroupBy(Function(x) x.Location.Zona.Guid).Select(Function(y) y.First).Select(Function(z) z.Location.Zona).ToList
        If FromCountry IsNot Nothing Then
            retval = retval.Where(Function(x) x.Country.Equals(FromCountry)).ToList
        End If
        Return retval
    End Function

    Shared Function Locations(oZips As IEnumerable(Of DTOZip), Optional FromZona As DTOZona = Nothing) As List(Of DTOLocation)
        Dim retval = oZips.GroupBy(Function(x) x.Location.Guid).Select(Function(y) y.First).Select(Function(z) z.Location).ToList
        If FromZona IsNot Nothing Then
            retval = retval.Where(Function(x) x.Zona.Equals(FromZona)).ToList
        End If
        Return retval
    End Function

    Shared Function Zips(oZips As IEnumerable(Of DTOZip), FromLocation As DTOLocation) As List(Of DTOZip)
        Dim retval = oZips.Where(Function(x) x.Location.Equals(FromLocation)).ToList
        Return retval
    End Function
End Class