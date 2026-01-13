Public Class DTOZona
    Inherits DTOArea

    Shadows Property country As DTOCountry
    Shadows Property provincia As DTOAreaProvincia

    Property lang As DTOLang
    Property exportCod As DTOInvoice.ExportCods

    Property splitByComarcas As Boolean
    Property locations As List(Of DTOLocation)
    Property contacts As List(Of DTOContact)

    Property portsCondicio As DTOPortsCondicio

    Public Enum wellknowns
        bizkaia
        barcelona
        girona
        lleida
        tarragona
        CanariasTenerife
        CanariasLaPalma
        CanariasGranCanaria
        CanariasHierro
        CanariasLaGomera
        CanariasFuerteventura
        CanariasLanzarote
        Ceuta
        Melilla
        Madeira
        Azores
        Andorra
    End Enum

    Public Sub New()
        MyBase.New()
        MyBase.Cod = Cods.Zona
        _Locations = New List(Of DTOLocation)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        MyBase.Cod = Cods.Zona
        _Locations = New List(Of DTOLocation)
    End Sub

    Shared Function wellknown(id As DTOZona.wellknowns) As DTOZona
        Dim retval As DTOZona = Nothing
        Dim sGuid As String = ""
        Select Case id
            Case DTOZona.wellknowns.bizkaia
                sGuid = "8EC77DB0-BE4C-448C-8A50-986F925188DF"
            Case DTOZona.wellknowns.barcelona
                sGuid = "5D799DC5-B56B-4F8D-AA86-BB318EBFB89F"
            Case DTOZona.wellknowns.girona
                sGuid = "383EBF0E-2557-493B-B1A2-4F99EA458165"
            Case DTOZona.wellknowns.lleida
                sGuid = "C4E352AC-88D0-4DDC-9B56-39BB679239DE"
            Case DTOZona.wellknowns.tarragona
                sGuid = "D5289044-9986-41B0-A16D-75261EE67EFD"
            Case DTOZona.wellknowns.CanariasTenerife
                sGuid = "F38971B4-001D-4128-8FF9-1F4C7B9CE3A2"
            Case DTOZona.wellknowns.CanariasLaPalma
                sGuid = "BE926DE7-ED8D-4F54-B965-2DFD182B5E90"
            Case DTOZona.wellknowns.CanariasGranCanaria
                sGuid = "20391DF1-9687-4F46-A645-3EBE8E8C2730"
            Case DTOZona.wellknowns.CanariasHierro
                sGuid = "3BC6964A-29C0-4B50-A3DD-C518B26A7D23"
            Case DTOZona.wellknowns.CanariasLaGomera
                sGuid = "69BDF9DF-15F8-4B89-9CBE-E78BCCAC9D9A"
            Case DTOZona.wellknowns.CanariasFuerteventura
                sGuid = "7D2513A9-B459-4661-A2F3-F69AE0A9102E"
            Case DTOZona.wellknowns.CanariasLanzarote
                sGuid = "0DB00A60-4DE8-4B5C-8214-F8CCD61F909E"
            Case DTOZona.wellknowns.Ceuta
                sGuid = "F059B58D-6E0C-49E5-AC2E-9EBCE7707B11"
            Case DTOZona.wellknowns.Melilla
                sGuid = "49CF61A5-F56A-4C48-B824-E08134D277B9"
            Case DTOZona.wellknowns.Madeira
                sGuid = "B4236E17-8D42-48E9-80A6-E96321D1D324"
            Case DTOZona.wellknowns.Azores
                sGuid = "55C49704-1019-4805-8D44-A7894D1EB9CF"
            Case DTOZona.wellknowns.Andorra
                sGuid = "D0F8B86A-6BCD-4F1B-A581-DAED87CB809E"
        End Select

        If sGuid > "" Then
            Dim oGuid As New Guid(sGuid)
            retval = New DTOZona(oGuid)
        End If
        Return retval
    End Function

    Overloads Shared Function Factory(oCountry As DTOCountry) As DTOZona
        Dim retval As New DTOZona
        With retval
            .country = oCountry
            .lang = oCountry.lang
            .exportCod = oCountry.exportCod
        End With
        Return retval
    End Function

    Public Shadows Function FullNom(oLang As DTOLang) As String
        Dim retval As String = MyBase.Nom
        If _Country IsNot Nothing Then
            If _Country.ISO <> "ES" Then
                Dim sNom As String = _country.langNom.tradueix(oLang)
                If sNom > "" Then
                    retval = retval & " (" & sNom & ")"
                End If
            End If
        End If
        Return retval
    End Function


    Public Shadows Function FullNomSegmented(oLang As DTOLang) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(MyBase.Nom)
        sb.Append("/")
        sb.Append(_country.langNom.tradueix(oLang))
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Public Shadows Function FullNomSegmentedReversed(oLang As DTOLang) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(_country.langNom.tradueix(oLang))
        sb.Append("/")
        sb.Append(MyBase.Nom)
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Public Function SuggestedLang() As DTOLang
        Dim retval = If(_Lang, _Country.SuggestedLang)
        Return retval
    End Function

    Public Function CountryNom(oLang As DTOLang) As String
        Dim retval As String = ""
        If _Country IsNot Nothing Then
            retval = _country.langNom.tradueix(oLang)
        End If
        Return retval
    End Function

    Shared Function IsCanarias(oZona As DTOZona) As Boolean
        Dim retval As Boolean = oZona.Nom.Contains("Canarias")
        Return retval
    End Function

    Shared Function ISOCountryPrefixed(oZona As DTOZona) As String
        Dim retval As String = ""
        If oZona.Country IsNot Nothing Then
            retval = retval & oZona.Country.ISO & "."
            retval = retval & oZona.Nom
        End If
        Return retval
    End Function
End Class
