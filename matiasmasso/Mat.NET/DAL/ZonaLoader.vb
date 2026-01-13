Public Class ZonaLoader

    Shared Function NewZona(oGuid As Guid, sZonaNom As String, sCountryISO As String, oCountryGuid As Guid, sCountryEsp As String, Optional sCountryCat As String = "", Optional sCountryEng As String = "") As DTOZona
        Dim oCountry As DTOCountry = CountryLoader.NewCountry(sCountryISO, oCountryGuid, sCountryEsp, sCountryCat, sCountryEng)
        Dim retval As New DTOZona(oGuid)
        With retval
            .Nom = sZonaNom
            .Country = oCountry
        End With
        Return retval
    End Function

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOZona
        Dim retval As DTOZona = Nothing
        Dim oZona As New DTOZona(oGuid)
        If Load(oZona) Then
            retval = oZona
        End If
        Return retval
    End Function


    Shared Function FromNom(sNom As String, Optional sCountryISO As String = "") As DTOZona
        Dim retval As DTOZona = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom ")
        sb.AppendLine("FROM VwAreaNom ")
        sb.AppendLine("WHERE ZonaNom ='" & sNom & "' ")
        If sCountryISO > "" Then
            sb.AppendLine("AND CountryISO ='" & sCountryISO & "' ")
        End If

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            Dim oCountry As New DTOCountry(oDrd("CountryGuid"))
            With oCountry
                .ISO = sCountryISO
                .LangNom.Esp = oDrd("CountryNomCat")
                .LangNom.Cat = oDrd("CountryNomCat")
                .LangNom.Eng = oDrd("CountryNomEng")
            End With

            retval = New DTOZona(oDrd("ZonaGuid"))
            With retval
                .Nom = sNom
                .Country = oCountry
            End With
        End If

        oDrd.Close()

        Return retval
    End Function

    Shared Function FromZip(oCountry As DTOCountry, sZipCod As String) As DTOZona
        Dim retval As DTOZona = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwAreaNom.AreaCod, VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom, VwAreaNom.ZipGuid, VwAreaNom.ZipCod ")
        sb.AppendLine("FROM VwAreaNom ")
        sb.AppendLine("WHERE ZipCod ='" & sZipCod & "' ")
        sb.AppendLine("AND CountryGuid ='" & oCountry.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            If oCountry Is Nothing Then
                oCountry = New DTOCountry(oDrd("CountryGuid"))
            End If
            With oCountry
                .ISO = oDrd("CountryISO")
                .LangNom.Esp = oDrd("CountryNomCat")
                .LangNom.Cat = oDrd("CountryNomCat")
                .LangNom.Eng = oDrd("CountryNomEng")
            End With
            retval = New DTOZona(oDrd("ZonaGuid"))
            With retval
                .Nom = oDrd("ZonaNom")
                .Country = oCountry
            End With
        End If

        oDrd.Close()

        Return retval
    End Function

    Shared Function Load(ByRef oZona As DTOZona) As Boolean
        If Not oZona.IsLoaded And Not oZona.IsNew Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT VwZona.*, Zona.* ")
            sb.AppendLine(", PortsCondicions.Nom AS PortsCondicionsNom ")
            sb.AppendLine("FROM VwZona ")
            sb.AppendLine("INNER JOIN Zona ON VwZona.ZonaGuid = Zona.Guid ")
            sb.AppendLine("LEFT OUTER JOIN PortsCondicions ON Zona.PortsCondicions = PortsCondicions.Guid ")
            sb.AppendLine("WHERE VwZona.ZonaGuid='" & oZona.Guid.ToString & "'")
            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim pZona = SQLHelper.GetZonaFromDataReader(oDrd)
                Dim exs As New List(Of Exception)
                DTOBaseGuid.CopyPropertyValues(Of DTOZona)(pZona, oZona, exs)
                With oZona

                    If IsDBNull(oDrd("ZonaLang")) Then
                        .Lang = DTOLang.Factory(oDrd("CountryLang").ToString())
                    Else
                        .Lang = DTOLang.Factory(oDrd("ZonaLang").ToString())
                    End If


                    If Not IsDBNull(oDrd("PortsCondicions")) Then
                        .PortsCondicio = New DTOPortsCondicio(oDrd("PortsCondicions"))
                        .PortsCondicio.Nom = SQLHelper.GetStringFromDataReader(oDrd("PortsCondicionsNom"))
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oZona.IsLoaded
        Return retval
    End Function


    Shared Function Update(oZona As DTOZona, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oZona, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oZona As DTOZona, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Zona WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oZona.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oZona.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oZona
            oRow("Country") = .Country.Guid
            oRow("Nom") = .Nom
            oRow("Provincia") = SQLHelper.NullableBaseGuid(.Provincia)
            oRow("PortsCondicions") = SQLHelper.NullableBaseGuid(.PortsCondicio)
            oRow("ExportCod") = .ExportCod
            If .Lang IsNot Nothing Then
                oRow("Lang") = .Lang.Tag
            End If
            oRow("SplitByComarcas") = .SplitByComarcas
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(exs As List(Of Exception), oZonaFrom As DTOZona, oZonaTo As DTOZona) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim SQL As String = "UPDATE RepProducts SET Area = '" & oZonaTo.Guid.ToString & "' WHERE Area='" & oZonaFrom.Guid.ToString & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            SQL = "UPDATE Comarca SET Zona = '" & oZonaTo.Guid.ToString & "' WHERE Zona='" & oZonaFrom.Guid.ToString & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            SQL = "UPDATE Location SET Zona = '" & oZonaTo.Guid.ToString & "' WHERE Zona='" & oZonaFrom.Guid.ToString & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            SQL = "DELETE Zona WHERE Guid = '" & oZonaFrom.Guid.ToString & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


#End Region

End Class

Public Class ZonasLoader
    Public Function Zonas(oContact As DTOContact, Optional oCountry As DTOCountry = Nothing) As List(Of DTOZona)
        Dim retval As New List(Of DTOZona)
        Dim SQL As String = ""
        Dim oDrd As SqlDataReader = Nothing
        Select Case oContact.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.admin
                SQL = "SELECT Zona.Guid, Zona.Nom, Zona.SplitByComarcas " _
                & "FROM CliAdr INNER JOIN " _
                & "Zip ON CliAdr.Zip=Zip.Guid INNER JOIN " _
                & "Location ON Zip.Location = Location.Guid INNER JOIN " _
                & "Zona ON Location.Zona= Zona.Guid INNER JOIN " _
                & "Country ON Zona.Country=Country.Guid INNER JOIN " _
                & "CliGral ON CliAdr.SrcGuid = CliGral.Guid AND CliAdr.Cod=1 " _
                & "WHERE CliGral.Guid ='" & oContact.Guid.ToString & "' "

                If oCountry IsNot Nothing Then
                    SQL = SQL & " AND Country.Guid='" & oCountry.Guid.ToString & "' "
                End If
                SQL = SQL & "GROUP BY Country.ISO, Zona.Guid, Zona.Nom " _
                & "ORDER BY Country.ISO, Zona.Nom"

                oDrd = SQLHelper.GetDataReader(SQL)
                Do While oDrd.Read
                    Dim oZona As New DTOZona(oDrd("Guid"))
                    oZona.Nom = oDrd("Nom")
                    oZona.SplitByComarcas = oDrd("SplitByComarcas")

                    retval.Add(oZona)
                Loop
                oDrd.Close()
            Case DTORol.Ids.Comercial, DTORol.Ids.Rep
                Dim oRep As DTORep = RepLoader.Find(oContact.Guid)
                retval = All(oRep)

            Case DTORol.Ids.SalesManager
                If oCountry Is Nothing Then oCountry = CountryLoader.Find("ES")
                SQL = "SELECT Guid, Nom FROM Zona WHERE Country='" & oCountry.Guid.ToString & "' ORDER BY Nom"
                oDrd = SQLHelper.GetDataReader(SQL)
                Do While oDrd.Read
                    Dim oZona As New DTOZona(oDrd("Guid"))
                    oZona.Nom = oDrd("Nom")
                    retval.Add(oZona)
                Loop
                oDrd.Close()

            Case Else
                retval.Add(oContact.Address.Zip.Location.Zona)
        End Select

        Return retval
    End Function

    Shared Function All(oRep As DTORep) As List(Of DTOZona)
        Dim retval As New List(Of DTOZona)
        Dim SQL As String = "SELECT Zona.Guid AS ZonaGuid, Zona.Nom, Zona.ExportCod, Zona.Lang AS ZonaLang, Zona.SplitByComarcas, Country.Guid AS CountryGuid, Country.ISO, Country.Nom_ESP, Country.Nom_CAT, Country.Nom_ENG " _
        & "FROM Zona " _
        & "INNER JOIN VwAreaParent ON Zona.Guid=VwAreaParent.ParentGuid " _
        & "INNER JOIN RepProducts ON RepProducts.Area=VwAreaParent.ChildGuid " _
        & "INNER JOIN Country ON Country.Guid=Zona.Country " _
        & "AND RepProducts.Rep = @Rep " _
        & "AND RepProducts.FchFrom <= GETDATE() AND (RepProducts.FchTo IS NULL or RepProducts.FchTo >= GETDATE()) " _
        & "GROUP BY Zona.Guid, Zona.Nom, Zona.ExportCod, Zona.Lang, Country.Guid, Country.ISO, Country.Nom_ESP, Country.Nom_CAT, Country.Nom_ENG " _
        & "ORDER BY Country.ISO, Zona.Nom"

        Dim oCountry As New DTOCountry
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Rep", oRep.Guid.ToString())
        Do While oDrd.Read
            If oCountry.Guid <> oDrd("CountryGuid") Then
                oCountry = New DTOCountry(oDrd("CountryGuid"))
                With oCountry
                    .ISO = oDrd("ISO")
                    .LangNom.Esp = oDrd("Nom_ESP")
                    .LangNom.Cat = oDrd("Nom_CAT")
                    .LangNom.Eng = oDrd("Nom_ENG")
                End With
            End If
            Dim item As New DTOZona(oDrd("ZonaGuid"))
            With item
                .Country = oCountry
                .Nom = oDrd("Nom")
                .ExportCod = oDrd("ExportCod")
                .Lang = DTOLang.Factory(oDrd("ZonaLang").ToString())
                .SplitByComarcas = oDrd("SplitByComarcas")
                .IsLoaded = True
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oCountry As DTOCountry) As List(Of DTOZona)
        Dim retval As New List(Of DTOZona)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwZona.* ")
        sb.AppendLine("FROM VwZona ")
        sb.AppendLine("WHERE VwZona.CountryGuid = '" & oCountry.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY ZonaNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oZona As DTOZona = SQLHelper.GetZonaFromDataReader(oDrd)
            retval.Add(oZona)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oPostalCode As Google.Geonames.postalCodeClass) As List(Of DTOZona)
        Dim retval As New List(Of DTOZona)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwZona.* ")
        sb.AppendLine("FROM VwZona ")
        sb.AppendLine("WHERE VwZona.CountryISO = '" & oPostalCode.countryCode & "' ")
        sb.AppendLine("AND ( ")
        sb.AppendLine("VwZona.Nom = '" & oPostalCode.adminName1 & "' ")
        sb.AppendLine("OR VwZona.Nom = '" & oPostalCode.adminName2 & "' ")
        sb.AppendLine("OR VwZona.Nom = '" & oPostalCode.adminName3 & "' ")
        sb.AppendLine(") ")
        sb.AppendLine("ORDER BY ZonaNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oZona As DTOZona = SQLHelper.GetZonaFromDataReader(oDrd)
            retval.Add(oZona)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oProvincia As DTOAreaProvincia) As List(Of DTOZona)
        Dim retval As New List(Of DTOZona)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwZona.* ")
        sb.AppendLine("FROM VwZona ")
        sb.AppendLine("WHERE VwZona.ProvinciaGuid = '" & oProvincia.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY ZonaNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oZona As DTOZona = SQLHelper.GetZonaFromDataReader(oDrd)
            retval.Add(oZona)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function FindByNom(sZonaNom As String) As List(Of DTOZona)
        Dim retval As New List(Of DTOZona)
        Dim oCountry As New DTOCountry
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Zona.Guid, Zona.Nom, Zona.SplitByComarcas, Zona.Country, Country.Nom_Esp ")
        sb.AppendLine("FROM Zona ")
        sb.AppendLine("INNER JOIN Country ON Zona.Country=Country.Guid ")
        sb.AppendLine("WHERE Nom=@Nom ")
        Dim SQL As String = "SELECT Guid, Nom, Country FROM Zona WHERE Nom=@Nom"
        Dim oDrd As SqlDataReader = DAL.SQLHelper.GetDataReader(SQL, "@Nom", sZonaNom)
        If oDrd.Read Then
            If Not oCountry.Guid.Equals(oDrd("Country")) Then
                oCountry = New DTOCountry(oDrd("Country"))
                oCountry.LangNom.Esp = SQLHelper.GetStringFromDataReader(oDrd("Nom_Esp"))
            End If

            Dim item As New DTOZona(oDrd("Guid"))
            item.Nom = oDrd("Nom")
            item.SplitByComarcas = oDrd("SplitByComarcas")
            item.Country = oCountry
            retval.Add(item)
        End If
        oDrd.Close()
        Return retval
    End Function

End Class