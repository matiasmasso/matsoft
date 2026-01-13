Public Class AreaLoader
    Shared Function NewArea(oGuid As Guid, oCod As DTOArea.Cods, Optional sNom As String = "") As DTOArea
        Dim retval As New DTOArea(oGuid) ' = Nothing
        retval.nom = sNom
        retval.Cod = oCod
        Return retval
        Select Case oCod
            Case DTOArea.Cods.Country
                retval.Nom = sNom
            Case DTOArea.Cods.Zona
                retval = New DTOZona(oGuid)
                DirectCast(retval, DTOZona).Nom = sNom
            Case DTOArea.Cods.Location
                retval = New DTOLocation(oGuid)
                DirectCast(retval, DTOLocation).Nom = sNom
            Case DTOArea.Cods.Zip
                retval = New DTOZip(oGuid)
                DirectCast(retval, DTOZip).ZipCod = sNom
                retval.Nom = sNom
        End Select
        Return retval
    End Function

    Shared Function NewArea(oCod As DTOArea.Cods, oCountryGuid As Guid, sCountryNomEsp As String, sCountryNomCat As String, sCountryNomEng As String, sCountryISO As String, _
                            Optional oZonaGuid As Object = Nothing, Optional sZonaNom As Object = "", _
                            Optional oLocationGuid As Object = Nothing, Optional sLocationNom As Object = "", _
                            Optional oZipGuid As Object = Nothing, Optional sZipCod As Object = "") As DTOArea

        Dim retval As DTOArea = Nothing
        Select Case oCod
            Case DTOArea.Cods.Country
                retval = New DTOCountry(oCountryGuid)
                With DirectCast(retval, DTOCountry)
                    .LangNom.Esp = sCountryNomEsp
                    .LangNom.Cat = sCountryNomCat
                    .LangNom.Esp = sCountryNomEng
                    .ISO = sCountryISO
                    .Nom = sCountryNomEsp
                End With
            Case DTOArea.Cods.Zona
                Dim oCountry As New DTOCountry(oCountryGuid)
                With oCountry
                    .LangNom.Esp = sCountryNomEsp
                    .LangNom.Cat = sCountryNomCat
                    .LangNom.Eng = sCountryNomEng
                    .ISO = sCountryISO
                End With
                retval = New DTOZona(oZonaGuid)
                With DirectCast(retval, DTOZona)
                    .Country = oCountry
                    .Nom = sZonaNom
                End With
            Case DTOArea.Cods.Location
                Dim oCountry As New DTOCountry(oCountryGuid)
                With oCountry
                    .LangNom.Esp = sCountryNomEsp
                    .LangNom.Cat = sCountryNomCat
                    .LangNom.Eng = sCountryNomEng
                    .ISO = sCountryISO
                End With
                Dim oZona As New DTOZona(oZonaGuid)
                With oZona
                    .Country = oCountry
                    .Nom = sZonaNom
                End With
                retval = New DTOLocation(oLocationGuid)
                With DirectCast(retval, DTOLocation)
                    .Zona = oZona
                    .Nom = sLocationNom
                End With
            Case DTOArea.Cods.Zip
                Dim oCountry As New DTOCountry(oCountryGuid)
                With oCountry
                    .LangNom.Esp = sCountryNomEsp
                    .LangNom.Cat = sCountryNomCat
                    .LangNom.Eng = sCountryNomEng
                    .ISO = sCountryISO
                End With
                Dim oZona As New DTOZona(oZonaGuid)
                With oZona
                    .Country = oCountry
                    .Nom = sZonaNom
                End With
                Dim oLocation As New DTOLocation(oLocationGuid)
                With oLocation
                    .Zona = oZona
                    .Nom = sLocationNom
                End With
                retval = New DTOZip(oZipGuid)
                With DirectCast(retval, DTOZip)
                    .Location = oLocation
                    .ZipCod = sZipCod
                End With
        End Select


        Return retval
    End Function

    Shared Function Find(oGuid As Guid) As DTOArea
        Dim retval As DTOArea = Nothing
        Dim oArea As New DTOArea(oGuid)
        If Load(oArea) Then
            retval = oArea
        End If
        Return retval
    End Function


    Shared Function Load(ByRef oArea As DTOArea) As Boolean
        Dim retval As Boolean

        If Not oArea.IsLoaded Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM VwAreaNom ")
            sb.AppendLine("WHERE Guid=@Guid")
            Dim SQL As String = sb.ToString

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oArea.Guid.ToString())
            If oDrd.Read Then
                Dim oCod As DTOArea.Cods = oDrd("Cod")
                Select Case oCod
                    Case DTOArea.Cods.Country
                        oArea = New DTOCountry(oDrd("CountryGuid"))
                        With DirectCast(oArea, DTOCountry)
                            .LangNom.Esp = oDrd("CountryNomEsp")
                            .LangNom.Cat = oDrd("CountryNomCat")
                            .LangNom.Eng = oDrd("CountryNomEng")
                        End With
                    Case DTOArea.Cods.Zona
                        Dim oCountry As New DTOCountry(oDrd("CountryGuid"))
                        With DirectCast(oCountry, DTOCountry)
                            .LangNom.Esp = oDrd("CountryNomEsp")
                            .LangNom.Cat = oDrd("CountryNomCat")
                            .LangNom.Eng = oDrd("CountryNomEng")
                        End With
                        oArea = New DTOZona(oDrd("ZonaGuid"))
                        With DirectCast(oArea, DTOZona)
                            .Nom = oDrd("ZonaNom")
                            .Country = oCountry
                        End With
                    Case DTOArea.Cods.Location
                        Dim oCountry As New DTOCountry(oDrd("CountryGuid"))
                        With DirectCast(oCountry, DTOCountry)
                            .LangNom.Esp = oDrd("CountryNomEsp")
                            .LangNom.Cat = oDrd("CountryNomCat")
                            .LangNom.Eng = oDrd("CountryNomEng")
                        End With
                        Dim oZona As New DTOZona(oDrd("ZonaGuid"))
                        With DirectCast(oZona, DTOZona)
                            .Nom = oDrd("ZonaNom")
                            .Country = oCountry
                        End With
                        oArea = New DTOLocation(oDrd("LocationGuid"))
                        With DirectCast(oArea, DTOLocation)
                            .Nom = oDrd("LocationNom")
                            .Zona = oZona
                        End With
                    Case DTOArea.Cods.Zip
                        Dim oCountry As New DTOCountry(oDrd("CountryGuid"))
                        With DirectCast(oCountry, DTOCountry)
                            .LangNom.Esp = oDrd("CountryNomEsp")
                            .LangNom.Cat = oDrd("CountryNomCat")
                            .LangNom.Eng = oDrd("CountryNomEng")
                        End With
                        Dim oZona As New DTOZona(oDrd("ZonaGuid"))
                        With DirectCast(oZona, DTOZona)
                            .Nom = oDrd("ZonaNom")
                            .Country = oCountry
                        End With
                        Dim oLocation As New DTOLocation(oDrd("LocationGuid"))
                        With DirectCast(oLocation, DTOLocation)
                            .Nom = oDrd("LocationNom")
                            .Zona = oZona
                        End With
                        oArea = New DTOZip(oDrd("ZipGuid"))
                        With DirectCast(oArea, DTOZip)
                            .ZipCod = oDrd("ZipCod")
                            .Location = oLocation
                        End With
                End Select
                oArea.IsLoaded = True
            End If
            oDrd.Close()
        End If

        retval = oArea.IsLoaded
        Return retval
    End Function
End Class
