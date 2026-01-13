Public Class AreaProvinciaLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOAreaProvincia
        Dim retval As DTOAreaProvincia = Nothing
        Dim oAreaProvincia As New DTOAreaProvincia
        oAreaProvincia.Guid = oGuid
        If Load(oAreaProvincia) Then
            retval = oAreaProvincia
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oAreaProvincia As DTOAreaProvincia) As Boolean
        If Not oAreaProvincia.IsLoaded And Not oAreaProvincia.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Provincia.Nom, Provincia.Regio, Provincia.Mod347, Provincia.Intrastat ")
            sb.AppendLine(", Country.* ")
            sb.AppendLine("FROM Provincia ")
            sb.AppendLine("INNER JOIN Regio ON Provincia.Regio = Regio.Guid ")
            sb.AppendLine("INNER JOIN Country ON Regio.Country = Country.Guid ")
            sb.AppendLine("WHERE Provincia.Guid='" & oAreaProvincia.Guid.ToString & "'")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oRegio As DTOAreaRegio = Nothing
                If Not IsDBNull(oDrd("Regio")) Then
                    oRegio = New DTOAreaRegio(oDrd("Regio"))
                    With oRegio
                        .Nom = oDrd("Region")
                        .Country = SQLHelper.GetCountryFromDataReader(oDrd)
                    End With
                End If

                With oAreaProvincia
                    .Nom = oDrd("Nom")
                    .Mod347 = SQLHelper.GetStringFromDataReader(oDrd("Mod347"))
                    .Intrastat = SQLHelper.GetStringFromDataReader(oDrd("Intrastat"))
                    .Regio = oRegio
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oAreaProvincia.IsLoaded
        Return retval
    End Function

    Shared Function FromSpanishZipCod(sZipCod As String) As DTOAreaProvincia
        Dim retval As DTOAreaProvincia = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Provincia.Guid, Provincia.Nom ")
        sb.AppendLine("FROM Provincia ")
        sb.AppendLine("INNER JOIN Regio ON Provincia.Regio = Regio.Guid ")
        sb.AppendLine("WHERE Regio.Country = '" & DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain).Guid.ToString() & "' ")
        sb.AppendLine("AND Provincia.Zip ='" & sZipCod.Substring(0, 2) & "'")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOAreaProvincia(New Guid(oDrd("Guid").ToString()))
            retval.Nom = oDrd("Nom")
        End If

        oDrd.Close()
        Return retval
    End Function


    Shared Function Update(oAreaProvincia As DTOAreaProvincia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oAreaProvincia, oTrans)
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


    Shared Sub Update(oAreaProvincia As DTOAreaProvincia, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Provincia ")
        sb.AppendLine("WHERE Guid='" & oAreaProvincia.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oAreaProvincia.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oAreaProvincia
            oRow("Nom") = .Nom
            oRow("Mod347") = SQLHelper.NullableString(.Mod347)
            oRow("Intrastat") = SQLHelper.NullableString(.Intrastat)
            If .Regio Is Nothing Then
                oRow("Regio") = System.DBNull.Value
            Else
                oRow("Regio") = .Regio.Guid
            End If
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oAreaProvincia As DTOAreaProvincia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oAreaProvincia, oTrans)
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


    Shared Sub Delete(oAreaProvincia As DTOAreaProvincia, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Provincia WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oAreaProvincia.Guid.ToString())
    End Sub

#End Region

    Shared Function Zonas(oProvincia As DTOAreaProvincia) As List(Of DTOZona)
        Dim retval As New List(Of DTOZona)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwZona.* ")
        sb.AppendLine("FROM VwZona ")
        sb.AppendLine("WHERE VwZona.ProvinciaGuid='" & oProvincia.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY VwZona.ZonaNom")
        Dim SQL As String = sb.ToString
        Dim oRegio As New DTOAreaRegio

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim firstTime As Boolean = True
        Do While oDrd.Read
            Dim oZona = SQLHelper.GetZonaFromDataReader(oDrd)
            retval.Add(oZona)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class AreaProvinciasLoader

    Shared Function All(oCountry As DTOCountry) As List(Of DTOAreaProvincia)
        Dim retval As New List(Of DTOAreaProvincia)
        Dim oRegions As New List(Of DTOAreaRegio)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Provincia.Guid, Provincia.Nom, Provincia.Mod347, Provincia.Intrastat, Provincia.Regio ")
        sb.AppendLine(", Regio.Nom AS RegioNom, Regio.Country AS CountryGuid ")
        sb.AppendLine(", Country.ISO AS CountryISO, Country.PrefixeTelefonic, Country.CEE, Country.Nom_Esp AS CountryEsp, Country.Nom_Cat AS CountryCat, Country.Nom_Eng AS CountryEng, Country.Nom_Por AS CountryPor ")
        sb.AppendLine("FROM Provincia ")
        sb.AppendLine("LEFT OUTER JOIN Regio ON Provincia.Regio = Regio.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Country ON Regio.Country = Country.Guid ")
        sb.AppendLine("WHERE Regio.Country='" & oCountry.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Regio.Nom, Provincia.Nom")
        Dim SQL As String = sb.ToString
        Dim oRegio As New DTOAreaRegio

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim firstTime As Boolean = True
        Do While oDrd.Read
            If firstTime Then
                oCountry = SQLHelper.GetCountryFromDataReader(oDrd)
                firstTime = True
            End If

            If Not oRegio.Guid.Equals(oDrd("Regio")) Then
                oRegio = New DTOAreaRegio(oDrd("Regio"))
                With oRegio
                    .Nom = oDrd("RegioNom")
                    .Country = oCountry
                End With
            End If

            Dim item As New DTOAreaProvincia(oDrd("Guid"))
            With item
                .Regio = oRegio
                .Nom = oDrd("Nom")
                .Intrastat = SQLHelper.GetStringFromDataReader(oDrd("Intrastat"))
                .Mod347 = SQLHelper.GetStringFromDataReader(oDrd("Mod347"))
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oRegio As DTOAreaRegio) As List(Of DTOAreaProvincia)
        Dim retval As New List(Of DTOAreaProvincia)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Provincia.Guid, Provincia.Nom, Provincia.Mod347, Provincia.Intrastat ")
        sb.AppendLine("FROM Provincia ")
        sb.AppendLine("WHERE Regio='" & oRegio.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOAreaProvincia()
            With item
                .Guid = oDrd("Guid")
                .Nom = oDrd("Nom")
                .Mod347 = SQLHelper.GetStringFromDataReader(oDrd("Mod347"))
                .Intrastat = SQLHelper.GetIntegerFromDataReader(oDrd("Intrastat"))
                .Regio = oRegio
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

