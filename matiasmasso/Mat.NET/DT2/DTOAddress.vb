Public Class DTOAddress
    Property viaNom As String
    Property num As String
    Property pis As String

    Property text As String
    Property zip As DTOZip
    Property coordenadas As GeoHelper.Coordenadas

    Property src As DTOBaseGuid
    Property codi As Codis

    Property isNew As Boolean

    Public Enum Codis
        NotSet
        Fiscal
        Correspondencia
        Entregas
    End Enum

    Shared Function Factory(oSrc As DTOBaseGuid, oCodi As DTOAddress.Codis) As DTOAddress
        Dim retval As New DTOAddress
        With retval
            .Src = oSrc
            .Codi = oCodi
        End With
        Return retval
    End Function

    Public Shadows Function Equals(oCandidate As Object) As Boolean
        Dim retval As Boolean
        If oCandidate IsNot Nothing Then
            If TypeOf oCandidate Is DTOAddress Then
                Dim oAddress As DTOAddress = oCandidate
                If _Text = oAddress.Text Then
                    Dim oZip As DTOZip = oAddress.Zip
                    retval = _Zip.Equals(oZip)
                End If
            End If
        End If
        Return retval
    End Function


    Public Function ExportCod() As DTOInvoice.ExportCods
        Dim retval As DTOInvoice.ExportCods = DTOInvoice.ExportCods.NotSet

        If _Zip IsNot Nothing Then
            Dim oLocation As DTOLocation = _Zip.Location
            If oLocation IsNot Nothing Then
                Dim oZona As DTOZona = oLocation.Zona
                If oZona IsNot Nothing Then
                    retval = oZona.ExportCod
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function ExportCod(oAddress As DTOAddress) As DTOInvoice.ExportCods
        Dim retval As DTOInvoice.ExportCods = DTOInvoice.ExportCods.NotSet
        If oAddress IsNot Nothing Then
            retval = oAddress.ExportCod()
        End If
        Return retval
    End Function

    Shared Function FullText(oAddress As DTOAddress, oLang As DTOLang) As String
        Dim retval As String = oAddress.toMultilineString(oLang).Replace(vbCrLf, " - ")
        Return retval
    End Function

    Public Function toMultilineString(oLang As DTOLang) As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine(_Text)
        sb.AppendLine(_Zip.FullNom(oLang))
        Dim retval = sb.ToString
        Return retval
    End Function

    Shared Function TextOrDefault(oAddress As DTOAddress) As String
        Dim retval As String = ""
        If oAddress IsNot Nothing Then
            retval = oAddress.Text
        End If
        Return retval
    End Function

    Shared Function ZipyCit(oAddress As DTOAddress) As String
        Dim retval As String = ""
        If oAddress IsNot Nothing AndAlso oAddress.Zip IsNot Nothing Then
            retval = oAddress.Zip.ZipyCit
        End If
        Return retval
    End Function

    Public Function SingleLineText() As String
        'Dim lines As String() = _text.Split(vbCrLf)
        Dim retval = _text.Replace(vbCrLf, " ").Replace(vbLf, " ")
        Return retval
    End Function

    Public Function Location() As DTOLocation
        Dim retval As DTOLocation = Nothing
        If Zip IsNot Nothing Then
            retval = _Zip.Location
        End If
        Return retval
    End Function

    Shared Function Location(oAddress As DTOAddress) As DTOLocation
        Dim retval As DTOLocation = Nothing
        If oAddress IsNot Nothing Then
            retval = oAddress.Location
        End If
        Return retval
    End Function

    Public Function Zona() As DTOZona
        Dim retval As DTOZona = Nothing
        Dim oLocation = Me.Location
        If oLocation IsNot Nothing Then
            retval = oLocation.Zona
        End If
        Return retval
    End Function

    Shared Function Zona(oAddress As DTOAddress) As DTOZona
        Dim retval As DTOZona = Nothing
        If oAddress IsNot Nothing Then
            retval = oAddress.Zona
        End If
        Return retval
    End Function

    Public Function Provincia() As DTOAreaProvincia
        Dim retval As DTOAreaProvincia = Nothing
        Dim oZona = Me.Zona
        If oZona IsNot Nothing Then
            retval = oZona.Provincia
        End If
        Return retval
    End Function

    Shared Function Provincia(oAddress As DTOAddress) As DTOAreaProvincia
        Dim retval As DTOAreaProvincia = Nothing
        If oAddress IsNot Nothing Then
            retval = oAddress.Provincia
        End If
        Return retval
    End Function

    Public Function Country() As DTOCountry
        Dim retval As DTOCountry = Nothing
        Dim oZona = Me.Zona
        If oZona IsNot Nothing Then
            retval = oZona.Country
        End If
        Return retval
    End Function

    Shared Function Country(oAddress As DTOAddress) As DTOCountry
        Dim retval As DTOCountry = Nothing
        If oAddress IsNot Nothing Then retval = oAddress.Country
        Return retval
    End Function

    Public Function countryNom(oLang As DTOLang) As String
        Dim retval As String = ""
        If _zip IsNot Nothing Then
            retval = _zip.CountryNom(oLang)
        End If
        Return retval
    End Function

    Public Function suggestedLang() As DTOLang
        Dim retval As DTOLang = Nothing
        Dim oZona As DTOZona = Me.Zona
        If oZona Is Nothing Then
            retval = DTOLang.ESP
        Else
            retval = oZona.suggestedLang
        End If
        Return retval
    End Function

    Shared Function LocationFullNom(oAddress As DTOAddress, oLang As DTOLang) As String
        Dim retval As String = ""
        If oAddress IsNot Nothing AndAlso oAddress.Zip IsNot Nothing AndAlso oAddress.Zip.Location IsNot Nothing Then
            retval = DTOLocation.FullNom(oAddress.Zip.Location, oLang)
        End If
        Return retval
    End Function


    Shared Function ClxLocation(oAddress As DTOAddress) As String
        'per generar Contact.Fullnom
        Dim sb As New Text.StringBuilder
        If oAddress IsNot Nothing Then
            If oAddress.Zip IsNot Nothing Then
                Dim oLocation = oAddress.Zip.Location
                If oLocation IsNot Nothing Then
                    Dim sCit As String = oLocation.Nom
                    sb.Append(sCit)
                    Dim oZona = oLocation.Zona
                    If oZona IsNot Nothing Then
                        Dim oCountry = oZona.Country
                        If oCountry Is Nothing Then
                        ElseIf oCountry.Equals(DTOCountry.wellknown(DTOCountry.wellknowns.Spain)) Then
                            If oZona.Provincia IsNot Nothing Then
                                Dim sProvincia As String = oZona.Provincia.Nom
                                If sProvincia <> "" And sProvincia <> oLocation.Nom Then
                                    sb.Append(String.Format(", {0}", sProvincia))
                                End If
                            End If
                        Else
                            Dim sCountry As String = oCountry.langNom.Esp
                            If sCountry > "" Then
                                sb.Append(String.Format(", {0}", sCountry))
                            End If
                        End If
                    End If
                End If
            End If
        End If

        Dim retval = sb.ToString
        Return retval
    End Function

    Public Function IsEmpty() As Boolean
        Dim retval As Boolean = _Text = "" And _ViaNom = "" And _Num = "" And _Zip Is Nothing
        Return retval
    End Function

    Shared Function Lines(oContact As DTOContact) As List(Of String)
        Dim retval As New List(Of String)
        With retval
            .Add(IIf(oContact.Nom = "", oContact.NomComercial, oContact.Nom))
            .Add(oContact.Address.Text)
            .Add(oContact.Address.Zip.ZipyCit())
        End With
        Return retval
    End Function

    Shared Function ProvinciaOPais(oZip As DTOZip) As String
        Dim retval As String = ""
        If oZip IsNot Nothing Then
            If DTOArea.IsEsp(oZip) Then
                Dim exs As New List(Of Exception)
                Dim oProvincia = DTOZip.Provincia(oZip)
                If oProvincia IsNot Nothing Then
                    retval = "(" & oProvincia.Nom & ")"
                End If
            Else
                retval = "(" & DTOArea.Country(oZip).langNom.Esp & ")"
            End If
        End If
        Return retval
    End Function

    Public Function IsEsp() As Boolean
        Dim retval As Boolean = DTOArea.IsEsp(_Zip)
        Return retval
    End Function

    Shared Function GoogleText(oAddress As DTOAddress) As String
        Dim sb As New System.Text.StringBuilder
        If oAddress.ViaNom > "" Then
            sb.Append(oAddress.ViaNom)
            sb.Append(",")
        End If
        If oAddress.Num > "" Then
            sb.Append(oAddress.Num)
            sb.Append(",")
        End If
        If oAddress.Zip IsNot Nothing Then
            sb.Append(oAddress.Zip.ZipCod)
            sb.Append(",")
            sb.Append(oAddress.Location.Nom)
            sb.Append(",")
            sb.Append(oAddress.CountryNom(DTOLang.ESP))
        End If
        Dim retval = sb.ToString
        Return retval
    End Function

    Shared Function GoogleNormalized(oSrc As DTOAddress) As String
        Dim sb As New System.Text.StringBuilder
        If oSrc IsNot Nothing AndAlso Not String.IsNullOrEmpty(oSrc.Text) Then
            Dim sAdrSegments() As String = oSrc.Text.Split(" ")
            For Each segment As String In sAdrSegments
                'elimina el segon numero del carrer en cas de varios numeros
                If IsNumeric(segment) Then
                    If segment.Contains("-") Then
                        segment = segment.Substring(0, segment.IndexOf("-"))
                    End If
                End If
            Next
            Dim sAdr As String = String.Join("+", sAdrSegments)
            Dim sCit As String = oSrc.Zip.Location.Nom.Replace(" ", "+")
            Dim sZona As String = oSrc.Zip.Location.Zona.Nom.Replace(" ", "+")
            Dim sCountry As String = oSrc.zip.location.zona.country.langNom.Esp.Replace(" ", "+")
            sb.Append(sAdr)
            If oSrc.Zip.ZipCod > "" Then sb.Append("," & oSrc.Zip.ZipCod)
            sb.Append("," & sCit)
            sb.Append("," & sZona)
            sb.Append("," & sCountry)
        End If
        Dim retval As String = sb.ToString
        Return retval

    End Function

    Shared Function MultiLine(oAddress As DTOAddress) As String
        Dim sb As New System.Text.StringBuilder
        If oAddress IsNot Nothing Then
            If oAddress.Text > "" Then
                sb.AppendLine(oAddress.Text.Trim)
            End If
            sb.Append(DTOZip.FullNom(oAddress.Zip))
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Public Function ToHtml() As String
        Dim sLines = MultiLine(Me)
        Dim retval As String = TextHelper.ToHtml(sLines)
        Return retval
    End Function


    Shared Function ReverseSingleLine(oAddress As DTOAddress, oLang As DTOLang) As String
        Dim sb As New Text.StringBuilder
        Dim sLocation As String = DTOAddress.LocationFullNom(oAddress, oLang)
        If sLocation > "" Then
            sb.Append(sLocation)
            If oAddress.Text > "" Then
                sb.Append(", ")
            End If
        End If
        sb.Append(oAddress.Text)
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function FullText(oAddress As DTOAddress) As String
        Dim retval As String = DTOAddress.MultiLine(oAddress).Replace(vbCrLf, " - ")
        Return retval
    End Function

    Shared Function FullHtml(oAddress As DTOAddress) As String
        Dim retval As String = DTOAddress.MultiLine(oAddress).Replace(vbCrLf, "<br/>")
        Return retval
    End Function


    Public Function Trimmed() As DTOAddress
        Dim oType As Type = Me.GetType()
        Dim retval = Activator.CreateInstance(Me.GetType())
        Try
            Dim oProperties = oType.GetProperties()
            For Each oProperty In oProperties
                If GetType(DTOBaseGuid).IsAssignableFrom(oProperty.PropertyType) Then
                    Try
                        Dim oBaseGuid As DTOBaseGuid = oProperty.GetValue(Me)
                        If oBaseGuid IsNot Nothing Then
                            Dim oTrimmedPropertyValue = Activator.CreateInstance(oProperty.PropertyType, oBaseGuid.Guid)
                            oProperty.SetValue(retval, oTrimmedPropertyValue)
                        End If

                    Catch ex As Exception
                        oProperty.SetValue(retval, oProperty.GetValue(Me))
                    End Try
                Else
                    oProperty.SetValue(retval, oProperty.GetValue(Me))
                End If
            Next

        Catch ex As Exception
            Stop
        End Try
        Return retval
    End Function
End Class
