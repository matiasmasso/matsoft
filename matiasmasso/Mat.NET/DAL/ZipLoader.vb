Imports DTO.DTOBancTerm

Public Class ZipLoader

    Shared Function NewZip(oGuid As Guid, sZipCod As String, oLocationGuid As Guid, sLocationNom As String, oZonaGuid As Guid, sZonaNom As String, sCountryISO As String, oCountryGuid As Guid, sCountryEsp As String, Optional sCountryCat As String = "", Optional sCountryEng As String = "") As DTOZip
        Dim oLocation As DTOLocation = LocationLoader.NewLocation(oLocationGuid, sLocationNom, oZonaGuid, sZonaNom, sCountryISO, oCountryGuid, sCountryEsp, sCountryCat, sCountryEng)
        Dim retval As New DTOZip(oGuid)
        With retval
            .ZipCod = sZipCod
            .Location = oLocation
        End With
        Return retval
    End Function


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOZip
        Dim retval As DTOZip = Nothing
        Dim oZip As New DTOZip(oGuid)
        If Load(oZip) Then
            retval = oZip
        End If
        Return retval
    End Function

    Shared Function FromZipCod(sZipCod As String, oCountry As DTOCountry) As DTOZip
        Dim retval As DTOZip = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Zip.Guid AS ZipGuid, Zip.Location AS LocationGuid, Location.Nom AS LocationNom ")
        sb.AppendLine(", Location.Zona AS ZonaGuid, Zona.Nom AS ZonaNom, Zona.Lang AS ZonaLang, Zona.ExportCod ")
        sb.AppendLine(", Zona.Country AS CountryGuid, Country.Nom_Esp AS CountryEsp, Country.Nom_Cat AS CountryCat, Country.Nom_Eng AS CountryEng, Country.Nom_Por AS CountryPor ")
        sb.AppendLine("FROM Zip ")
        sb.AppendLine("INNER JOIN Location ON Zip.Location = Location.Guid ")
        sb.AppendLine("INNER JOIN Zona ON Location.Zona = Zona.Guid ")
        sb.AppendLine("INNER JOIN Country ON Zona.Country = Country.Guid ")
        sb.AppendLine("WHERE Zona.Country='" & oCountry.Guid.ToString & "' ")
        sb.AppendLine("AND Zip.ZipCod='" & sZipCod.Trim() & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = SQLHelper.GetZipFromDataReader(oDrd)
        End If
        oDrd.Close()

        Return retval
    End Function


    Shared Function Load(ByRef oZip As DTOZip) As Boolean
        If Not oZip.IsLoaded And Not oZip.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Zip.Location, Zip.ZipCod, Location.Zona, Location.Nom AS LocationNom ")
            sb.AppendLine(", Zona.Nom AS ZonaNom, Zona.Country, Zona.Lang AS ZonaLang, Zona.ExportCod AS ZonaExportCod ")
            sb.AppendLine(", Country.ISO, Country.Nom_ESP, Country.Nom_CAT, Country.Nom_ENG ")
            sb.AppendLine("FROM Zip ")
            sb.AppendLine("INNER JOIN Location ON Zip.Location = Location.Guid ")
            sb.AppendLine("INNER JOIN Zona ON Location.Zona = Zona.Guid ")
            sb.AppendLine("INNER JOIN Country ON Zona.Country = Country.Guid ")
            sb.AppendLine("WHERE Zip.Guid='" & oZip.Guid.ToString & "'")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oCountry As New DTOCountry(oDrd("Country"))
                With oCountry
                    .ISO = oDrd("ISO")
                    .LangNom.Esp = oDrd("Nom_Esp")
                    .LangNom.Cat = oDrd("Nom_Cat")
                    .LangNom.Eng = oDrd("Nom_Eng")
                End With
                Dim oZona As New DTOZona(oDrd("Zona"))
                With oZona
                    .Nom = oDrd("ZonaNom")
                    .Country = oCountry
                    .ExportCod = oDrd("ZonaExportCod")
                    If Not IsDBNull(oDrd("ZonaLang")) Then
                        .Lang = DTOLang.Factory(oDrd("ZonaLang"))
                    End If
                End With
                Dim oLocation As New DTOLocation(oDrd("Location"))
                With oLocation
                    .Nom = oDrd("LocationNom")
                    .Zona = oZona
                End With
                With oZip
                    .ZipCod = oDrd("ZipCod")
                    .Location = oLocation
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oZip.IsLoaded
        Return retval
    End Function

    Shared Function Update(oZip As DTOZip, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oZip, oTrans)
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


    Shared Sub Update(oZip As DTOZip, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Zip ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oZip.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oZip.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oZip
            oRow("Location") = .Location.Guid
            oRow("ZipCod") = .ZipCod
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oZip As DTOZip, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oZip, oTrans)
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


    Shared Sub Delete(oZip As DTOZip, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Zip WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oZip.Guid.ToString())
    End Sub

    Shared Function MoveChildren(OldValue As DTOZip, NewValue As DTOZip, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            MoveContacts(OldValue, NewValue, oTrans)
            MoveDeliveries(OldValue, NewValue, oTrans)
            Delete(OldValue, oTrans)

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

    Shared Sub MoveContacts(OldValue As DTOZip, NewValue As DTOZip, ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE CliAdr ")
        sb.AppendLine("SET Zip = '" & NewValue.Guid.ToString & "' ")
        sb.AppendLine("WHERE Zip = '" & OldValue.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub MoveDeliveries(OldValue As DTOZip, NewValue As DTOZip, ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE Alb ")
        sb.AppendLine("SET Zip = '" & NewValue.Guid.ToString & "' ")
        sb.AppendLine("WHERE Zip = '" & OldValue.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


#End Region

End Class

Public Class ZipsLoader

    Shared Function FromLocation(oLocation As DTOLocation) As List(Of DTOZip)
        Dim retval As New List(Of DTOZip)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Zip ")
        sb.AppendLine("WHERE Location = '" & oLocation.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY ZipCod")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOZip(oDrd("Guid"))
            With item
                .ZipCod = oDrd("ZipCod")
                .Location = oLocation
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Search(SearchKey As String) As List(Of DTOZip)
        Dim retval As New List(Of DTOZip)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("VwAreaNom.AreaCod, VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom, VwAreaNom.ZipGuid, VwAreaNom.ZipCod ")
        sb.AppendLine("FROM VwAreaNom ")
        sb.AppendLine("WHERE ZipCod+' '+LocationNom LIKE '%" & SearchKey & "%' ")
        sb.AppendLine("AND VwAreaNom.AreaCod =" & DTOArea.Cods.Zip & " ")
        sb.AppendLine("ORDER BY ZipCod, LocationNom ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oZip As DTOZip = AreaLoader.NewArea(DirectCast(oDrd("AreaCod"), DTOArea.Cods), DirectCast(oDrd("CountryGuid"), Guid), oDrd("CountryNomEsp").ToString, oDrd("CountryNomCat").ToString, oDrd("CountryNomEng").ToString, oDrd("CountryISO").ToString, oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZipGuid"), oDrd("ZipCod"))
            retval.Add(oZip)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oLang As DTOLang) As List(Of DTOCountry)
        Dim retval As New List(Of DTOCountry)
        If oLang Is Nothing Then oLang = DTOLang.ESP
        Dim sCountryFieldNom As String = oLang.Tradueix("CountryEsp", "CountryCat", "CountryEng", "CountryPor")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * FROM VwZip ")
        sb.AppendLine("ORDER BY " & sCountryFieldNom & ", CountryGuid, ZonaNom, ZonaGuid, LocationNom, LocationGuid, ZipCod")

        Dim SQL As String = sb.ToString
        Dim oCountry As New DTOCountry
        Dim oZona As New DTOZona
        Dim oLocation As New DTOLocation
        Dim oProvincia As New DTOAreaProvincia
        Dim oRegio As New DTOAreaRegio
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oDrd("CountryGuid").Equals(oCountry.Guid) Then
                oCountry = SQLHelper.GetCountryFromDataReader(oDrd)
                'If oCountry.ISO = "ES" Then Stop '======================================================================================================
                retval.Add(oCountry)
            End If
            If Not IsDBNull(oDrd("RegioGuid")) Then
                If Not oDrd("RegioGuid").Equals(oRegio.Guid) Then
                    oRegio = oCountry.Regions.FirstOrDefault(Function(x) x.Guid.Equals(oDrd("RegioGuid")))
                    If oRegio Is Nothing Then
                        oRegio = New DTOAreaRegio(oDrd("RegioGuid"))
                        oRegio.Nom = oDrd("RegioNom")
                        oCountry.Regions.Add(oRegio)
                    End If
                End If
            End If
            If Not IsDBNull(oDrd("ProvinciaGuid")) Then
                If Not oDrd("ProvinciaGuid").Equals(oProvincia.Guid) Then
                    oProvincia = oRegio.Provincias.FirstOrDefault(Function(x) x.Guid.Equals(oDrd("ProvinciaGuid")))
                    If oProvincia Is Nothing Then
                        oProvincia = New DTOAreaProvincia(oDrd("ProvinciaGuid"))
                        oProvincia.Nom = oDrd("ProvinciaNom")
                        oRegio.Provincias.Add(oProvincia)
                    End If
                End If
            End If
            If Not IsDBNull(oDrd("ZonaGuid")) Then
                If Not oDrd("ZonaGuid").Equals(oZona.Guid) Then
                    oZona = SQLHelper.GetZonaFromDataReader(oDrd).Trimmed
                    If Not IsDBNull(oDrd("ProvinciaGuid")) Then
                        oZona.Provincia = oProvincia
                    End If
                    oCountry.zonas.Add(oZona)
                End If
            End If
            If Not IsDBNull(oDrd("LocationGuid")) Then
                If Not oDrd("LocationGuid").Equals(oLocation.Guid) Then
                    oLocation = SQLHelper.GetLocationFromDataReader(oDrd).Trimmed
                    oZona.Locations.Add(oLocation)
                End If
            End If
            If Not IsDBNull(oDrd("ZipGuid")) Then
                Dim oZip As New DTOZip(oDrd("ZipGuid"))
                oZip.ZipCod = oDrd("ZipCod")
                oZip.Location = oLocation
                oLocation.Zips.Add(oZip)
            End If

        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Tree(Optional oLang As DTOLang = Nothing) As List(Of DTOCountry)
        Dim retval As New List(Of DTOCountry)
        If oLang Is Nothing Then oLang = DTOLang.ESP
        Dim sCountryFieldNom As String = oLang.Tradueix("CountryEsp", "CountryCat", "CountryEng", "CountryPor")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * FROM VwZip ")
        sb.AppendLine("ORDER BY " & sCountryFieldNom & ", ZonaNom, LocationNom, ZipCod")

        Dim SQL As String = sb.ToString

        Dim oCountry As New DTOCountry
        Dim oZona As New DTOZona
        Dim oLocation As New DTOLocation

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Try

                If Not oLocation.Guid.Equals(oDrd("LocationGuid")) Then
                    If Not oZona.Guid.Equals(oDrd("ZonaGuid")) Then
                        If Not oCountry.Guid.Equals(oDrd("CountryGuid")) Then
                            oCountry = SQLHelper.GetCountryFromDataReader(oDrd)
                            retval.Add(oCountry)
                        End If

                        If Not IsDBNull(oDrd("ZonaGuid")) Then
                            oZona = New DTOZona(oDrd("ZonaGuid"))
                            oZona.Nom = oDrd("ZonaNom")
                            oCountry.Zonas.Add(oZona)
                        End If
                    End If
                    If Not IsDBNull(oDrd("LocationGuid")) Then
                        oLocation = New DTOLocation(oDrd("LocationGuid"))
                        oLocation.Nom = oDrd("LocationNom")
                        oZona.Locations.Add(oLocation)
                    End If
                End If
                If Not IsDBNull(oDrd("ZipGuid")) Then
                    Dim oZip As New DTOZip(oDrd("ZipGuid"))
                    oZip.ZipCod = oDrd("ZipCod")
                    oLocation.Zips.Add(oZip)
                End If
            Catch ex As Exception
                'Stop
            End Try
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oCountry As DTOCountry, sZipCod As String) As List(Of DTOZip)
        Dim retval As New List(Of DTOZip)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwZip.* ")
        sb.AppendLine("FROM VwZip ")
        sb.AppendLine("WHERE CountryGuid='" & oCountry.Guid.ToString & "' ")
        sb.AppendLine("AND ZipCod ='" & sZipCod & "' ")
        sb.AppendLine("ORDER BY LocationNom, ZipCod ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oZip = SQLHelper.GetZipFromDataReader(oDrd)
            retval.Add(oZip)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function Delete(oGuids As List(Of Guid), oTrans As SqlTransaction) As Integer
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids.Where(Function(x) Not x.Equals(oGuids.First()))
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("DELETE Zip ")
        sb.AppendLine("FROM Zip ")
        sb.AppendLine("INNER JOIN @Table X ON Zip.Guid = X.Guid ")
        Dim SQL = sb.ToString()
        Dim retval = SQLHelper.ExecuteNonQuery(SQL, oTrans)
        Return retval
    End Function

    Shared Function Merge(exs As List(Of Exception), oGuids As List(Of Guid)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Dim a = MergeAddress(oGuids, oTrans)
            Dim b = MergeAlbs(oGuids, oTrans)
            Dim c = MergeEmails(oGuids, oTrans)
            Dim d = MergeFras(oGuids, oTrans)
            Dim e = MergeImmobles(oGuids, oTrans)
            Dim f = MergeSatRecalls(oGuids, oTrans)
            Dim g = MergeSpvs(oGuids, oTrans)
            Dim z = Delete(oGuids, oTrans)
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

    Shared Function MergeAddress(oGuids As List(Of Guid), oTrans As SqlTransaction) As Integer
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids.Where(Function(x) Not x.Equals(oGuids.First()))
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("UPDATE CliAdr ")
        sb.AppendLine("SET Zip ='" & oGuids.First().ToString() & "' ")
        sb.AppendLine("FROM CliAdr ")
        sb.AppendLine("INNER JOIN @Table X ON CliAdr.Zip = X.Guid ")
        Dim SQL = sb.ToString()
        Dim retval = SQLHelper.ExecuteNonQuery(SQL, oTrans)
        Return retval
    End Function

    Shared Function MergeAlbs(oGuids As List(Of Guid), oTrans As SqlTransaction) As Integer
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids.Where(Function(x) Not x.Equals(oGuids.First()))
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("UPDATE Alb ")
        sb.AppendLine("SET Zip ='" & oGuids.First().ToString() & "' ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("INNER JOIN @Table X ON Alb.Zip = X.Guid ")
        Dim SQL = sb.ToString()
        Dim retval = SQLHelper.ExecuteNonQuery(SQL, oTrans)
        Return retval
    End Function

    Shared Function MergeFras(oGuids As List(Of Guid), oTrans As SqlTransaction) As Integer
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids.Where(Function(x) Not x.Equals(oGuids.First()))
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("UPDATE Fra ")
        sb.AppendLine("SET Zip ='" & oGuids.First().ToString() & "' ")
        sb.AppendLine("FROM Fra ")
        sb.AppendLine("INNER JOIN @Table X ON Fra.Zip = X.Guid ")
        Dim SQL = sb.ToString()
        Dim retval = SQLHelper.ExecuteNonQuery(SQL, oTrans)
        Return retval
    End Function

    Shared Function MergeEmails(oGuids As List(Of Guid), oTrans As SqlTransaction) As Integer
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids.Where(Function(x) Not x.Equals(oGuids.First()))
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("UPDATE Email ")
        sb.AppendLine("SET Residence='" & oGuids.First().ToString() & "' ")
        sb.AppendLine("FROM Email ")
        sb.AppendLine("INNER JOIN @Table X ON Email.Residence = X.Guid ")
        Dim SQL = sb.ToString()
        Dim retval = SQLHelper.ExecuteNonQuery(SQL, oTrans)
        Return retval
    End Function

    Shared Function MergeImmobles(oGuids As List(Of Guid), oTrans As SqlTransaction) As Integer
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids.Where(Function(x) Not x.Equals(oGuids.First()))
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("UPDATE Immoble ")
        sb.AppendLine("SET ZipGuid='" & oGuids.First().ToString() & "' ")
        sb.AppendLine("FROM Immoble ")
        sb.AppendLine("INNER JOIN @Table X ON Immoble.ZipGuid = X.Guid ")
        Dim SQL = sb.ToString()
        Dim retval = SQLHelper.ExecuteNonQuery(SQL, oTrans)
        Return retval
    End Function

    Shared Function MergeSatRecalls(oGuids As List(Of Guid), oTrans As SqlTransaction) As Integer
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids.Where(Function(x) Not x.Equals(oGuids.First()))
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("UPDATE SatRecall ")
        sb.AppendLine("SET Zip='" & oGuids.First().ToString() & "' ")
        sb.AppendLine("FROM SatRecall ")
        sb.AppendLine("INNER JOIN @Table X ON SatRecall.Zip = X.Guid ")
        Dim SQL = sb.ToString()
        Dim retval = SQLHelper.ExecuteNonQuery(SQL, oTrans)
        Return retval
    End Function

    Shared Function MergeSpvs(oGuids As List(Of Guid), oTrans As SqlTransaction) As Integer
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids.Where(Function(x) Not x.Equals(oGuids.First()))
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("UPDATE Spv ")
        sb.AppendLine("SET Zip='" & oGuids.First().ToString() & "' ")
        sb.AppendLine("FROM Spv ")
        sb.AppendLine("INNER JOIN @Table X ON Spv.Zip = X.Guid ")
        Dim SQL = sb.ToString()
        Dim retval = SQLHelper.ExecuteNonQuery(SQL, oTrans)
        Return retval
    End Function


End Class
