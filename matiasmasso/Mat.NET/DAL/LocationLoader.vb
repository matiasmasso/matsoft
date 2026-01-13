Public Class LocationLoader

    Shared Function NewLocation(Optional oGuid As Object = Nothing, Optional sLocationNom As Object = Nothing, Optional oZonaGuid As Object = Nothing, Optional sZonaNom As Object = Nothing, Optional sCountryISO As Object = Nothing, Optional oCountryGuid As Object = Nothing, Optional sCountryEsp As Object = Nothing, Optional sCountryCat As Object = Nothing, Optional sCountryEng As Object = Nothing, Optional oProvinciaGuid As Object = Nothing, Optional sProvinciaNom As Object = Nothing) As DTOLocation
        Dim oZona As DTOZona = ZonaLoader.NewZona(oZonaGuid, sZonaNom, sCountryISO, oCountryGuid, sCountryEsp, sCountryCat, sCountryEng)
        If GuidHelper.IsGuid(oProvinciaGuid) AndAlso oProvinciaGuid <> Nothing Then
            oZona.Provincia = New DTOAreaProvincia(oProvinciaGuid)
            oZona.Provincia.Nom = sProvinciaNom
        End If
        Dim retval As New DTOLocation(oGuid)
        With retval
            .Nom = sLocationNom
            .Zona = oZona
        End With
        Return retval
    End Function

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOLocation
        Dim retval As DTOLocation = Nothing
        Dim oLocation As New DTOLocation(oGuid)
        If Load(oLocation) Then
            retval = oLocation
        End If
        Return retval
    End Function

    Shared Function FromNom(sNom As String, Optional sCountryISO As String = "") As DTOLocation
        Dim retval As DTOLocation = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM VwLocation ")
        sb.AppendLine("WHERE LocationNom COLLATE Latin1_General_CI_AI = '" & sNom.Replace("_", " ") & "' COLLATE Latin1_General_CI_AI ") 'CI=Case Insensitive, AI=Accent insensitive
        If sCountryISO > "" Then
            sb.AppendLine("AND CountryISO ='" & sCountryISO & "' ")
        End If

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = SQLHelper.GetLocationFromDataReader(oDrd)
        End If

        oDrd.Close()

        Return retval
    End Function

    Shared Function FromZip(oCountry As DTOCountry, sZipCod As String) As DTOLocation
        Dim retval As DTOLocation = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwAreaNom.AreaCod, VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom, VwAreaNom.ZipGuid, VwAreaNom.ZipCod ")
        sb.AppendLine("FROM VwAreaNom ")
        sb.AppendLine("WHERE ZipCod ='" & sZipCod & "' ")
        If oCountry IsNot Nothing Then
            sb.AppendLine("AND CountryGuid ='" & oCountry.Guid.ToString & "' ")
        End If

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
            Dim oZona As New DTOZona(oDrd("ZonaGuid"))
            With oZona
                .Nom = oDrd("ZonaNom")
                .Country = oCountry
            End With
            retval = New DTOLocation(oDrd("LocationGuid"))
            With retval
                .Nom = oDrd("LocationNom")
                .Zona = oZona
                .IsLoaded = True
            End With
        End If

        oDrd.Close()

        Return retval
    End Function

    Shared Function Load(ByRef oLocation As DTOLocation) As Boolean
        If Not oLocation.IsLoaded And Not oLocation.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT VwLocation.* ")
            sb.AppendLine("FROM VwLocation ")
            sb.AppendLine("WHERE VwLocation.LocationGuid='" & oLocation.Guid.ToString & "'")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim tmpLocation = SQLHelper.GetLocationFromDataReader(oDrd)
                Dim exs As New List(Of Exception)
                DTOBaseGuid.CopyPropertyValues(Of DTOLocation)(tmpLocation, oLocation, exs)
                oLocation.IsLoaded = True
            End If
            oDrd.Close()
        End If

        Dim retval As Boolean = oLocation.IsLoaded
        Return retval
    End Function

    Shared Function Update(oLocation As DTOLocation, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oLocation, oTrans)
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


    Shared Sub Update(oLocation As DTOLocation, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Location ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oLocation.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oLocation.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oLocation
            oRow("Zona") = .Zona.Guid
            oRow("Comarca") = SQLHelper.NullableBaseGuid(.Comarca)
            oRow("Nom") = .Nom
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function checkDelete(exs As List(Of Exception), oLocation As DTOLocation) As Boolean
        Dim sb As New Text.StringBuilder

        sb.AppendLine("SELECT 1 as cod, count(Guid) as qty FROM Zip WHERE Location = '" & oLocation.Guid.ToString & "' ")
        sb.AppendLine("UNION ")
        sb.AppendLine("SELECT 2 as cod, count(Guid) as qty FROM Bn2 WHERE Location = '" & oLocation.Guid.ToString & "' ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If oDrd("qty") > 0 Then
                Select Case oDrd("Cod")
                    Case 1
                        exs.Add(New Exception(String.Format("S'han trobat {0} codis postals assignats a aquesta població", oDrd("qty"))))
                    Case 2
                        exs.Add(New Exception(String.Format("S'han trobat {0} oficines bancàries assignades a aquesta població", oDrd("qty"))))
                End Select
            End If
        Loop
        oDrd.Close()
        Return exs.Count = 0
    End Function

    Shared Function delete(exs As List(Of Exception), oLocation As DTOLocation) As Boolean
        If checkDelete(exs, oLocation) Then
            Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
            Dim oTrans As SqlTransaction = oConn.BeginTransaction
            Try
                Dim SQL As String = "DELETE Location WHERE Guid='" & oLocation.Guid.ToString & "'"
                SQLHelper.ExecuteNonQuery(SQL, oTrans)
                oTrans.Commit()
            Catch ex As Exception
                oTrans.Rollback()
                exs.Add(ex)
            Finally
                oConn.Close()
            End Try
        End If
        Return exs.Count = 0
    End Function


#End Region

End Class

Public Class LocationsLoader

    Shared Function FromZona(oZona As DTOZona) As List(Of DTOLocation)
        Dim retval As New List(Of DTOLocation)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Location ")
        sb.AppendLine("WHERE Zona = '" & oZona.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOLocation(oDrd("Guid"))
            With item
                .Nom = oDrd("Nom")
                .Zona = oZona
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromZip(oCountry As DTOCountry, sZipCod As String) As List(Of DTOLocation)
        Dim oZips As New List(Of DTOZip)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwZip.* ")
        sb.AppendLine("FROM VwZip ")
        sb.AppendLine("WHERE CountryGuid='" & oCountry.Guid.ToString & "' ")
        sb.AppendLine("AND ZipCod ='" & sZipCod & "' ")
        sb.AppendLine("FROM ORDER BY LocationNom, ZipCod ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oZip = SQLHelper.GetZipFromDataReader(oDrd)
            oZips.Add(oZip)
        Loop
        oDrd.Close()

        Dim retval = oZips.GroupBy(Function(x) x.Location.Guid).Select(Function(y) y.First).Select(Function(z) z.Location).ToList
        Return retval
    End Function

    Shared Function reLocate(exs As List(Of Exception), oZonaTo As DTOZona, oLocations As List(Of DTOLocation)) As Integer
        Dim retval As Integer

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim sb As New Text.StringBuilder
            sb.AppendLine("UPDATE Location SET Location.Zona ='" & oZonaTo.Guid.ToString & "' ")
            sb.AppendLine("WHERE (")
            For Each oLocation In oLocations
                If Not oLocation.Equals(oLocations.First) Then sb.AppendLine("OR ")
                sb.AppendLine("Location.Guid='" & oLocation.Guid.ToString & "' ")
            Next
            sb.AppendLine(")")
            Dim SQL As String = sb.ToString
            retval = SQLHelper.ExecuteNonQuery(SQL, oTrans)

            oTrans.Commit()
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

End Class
