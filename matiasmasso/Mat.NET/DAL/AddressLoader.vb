Imports Microsoft.SqlServer.Types

Public Class AddressLoader



    Private Shared Function Geography(oCoordenadas As GeoHelper.Coordenadas) As Microsoft.SqlServer.Types.SqlGeography
        Dim gb As New SqlGeographyBuilder
        gb.SetSrid(4326) 'Set the Spatial Reference ID (SRID). This must be the first method you call.

        gb.BeginGeography(OpenGisGeographyType.Point)
        gb.BeginFigure(oCoordenadas.Latitud, oCoordenadas.Longitud)
        gb.EndFigure()
        gb.EndGeography()

        Dim retval As New SqlGeography
        retval = gb.ConstructedGeography

        Return retval
    End Function

    Shared Function Update(oAddress As DTOAddress, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oAddress, oTrans)
            UpdateGeo(oAddress.src, oAddress.codi, oTrans)
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


    Shared Sub Update(oAddress As DTOAddress, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SrcGuid, Cod, Adr, AdrViaNom, AdrNum, AdrPis, Zip, Latitud, Longitud ")
        sb.AppendLine("FROM CliAdr ")
        sb.AppendLine("WHERE SrcGuid='" & oAddress.Src.Guid.ToString & "' ")
        sb.AppendLine("AND Cod=" & oAddress.Codi & " ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("SrcGuid") = oAddress.Src.Guid
            oRow("Cod") = oAddress.Codi
        Else
            oRow = oTb.Rows(0)
        End If

        With oAddress
            oRow("Adr") = .Text
            oRow("AdrViaNom") = .ViaNom
            oRow("AdrNum") = .Num
            oRow("AdrPis") = .Pis
            oRow("Zip") = SQLHelper.NullableBaseGuid(.Zip)
            If .Coordenadas IsNot Nothing Then
                oRow("Latitud") = .Coordenadas.Latitud
                oRow("Longitud") = .Coordenadas.Longitud
                ' Dim oGeography As SqlGeography = SqlGeography.Point(.Coordenadas.Latitud, .Coordenadas.Longitud, 4326) ' Geography(.Coordenadas)
                'oRow("Geo") = oGeography
            End If
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function update(exs As List(Of Exception), oUser As DTOUser, oContact As DTOContact, codi As Integer, coordenadas As GeoHelper.Coordenadas) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            update(oUser, oContact, codi, coordenadas, oTrans)
            UpdateGeo(oContact, codi, oTrans)
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

    Shared Sub Update(oUser As DTOUser, oContact As DTOContact, oCodi As DTOAddress.Codis, coordenadas As GeoHelper.Coordenadas, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SrcGuid, Cod, Latitud, Longitud, GeoUserLastEdited, GeoFchLastEdited ")
        sb.AppendLine("FROM CliAdr ")
        sb.AppendLine("WHERE SrcGuid='" & oContact.Guid.ToString & "' ")
        sb.AppendLine("AND Cod=" & oCodi & " ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow = oTb.Rows(0)
        oRow("Latitud") = coordenadas.latitud
        oRow("Longitud") = coordenadas.longitud
        oRow("GeoUserLastEdited") = oUser.Guid
        oRow("GeoFchLastEdited") = DTO.GlobalVariables.Now()
        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateGeo(oSrc As DTOBaseGuid, oCodi As DTOAddress.Codis, ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE CliAdr ")
        sb.AppendLine("SET Geo=geography::STPointFromText('POINT(' + CAST([Longitud] AS VARCHAR(MAX)) + ' ' + 
CAST([Latitud] AS VARCHAR(MAX)) + ')', 4326) ")
        sb.AppendLine("WHERE SrcGuid='" & oSrc.Guid.ToString & "' ")
        sb.AppendLine("AND Cod=" & oCodi & " ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function UpdateCoordenadas(exs As List(Of Exception), oSrc As DTOBaseGuid, oCodi As DTOAddress.Codis, coords As GeoHelper.Coordenadas) As Boolean
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE CliAdr ")
        sb.AppendLine("SET Longitud = " & coords.longitud.ToString.Replace(",", ".") & " ")
        sb.AppendLine(", Latitud = " & coords.latitud.ToString.Replace(",", ".") & " ")
        sb.AppendLine("WHERE SrcGuid='" & oSrc.Guid.ToString & "' ")
        sb.AppendLine("AND Cod=" & oCodi & " ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Return exs.Count = 0
    End Function




    Shared Function Delete(oAddress As DTOAddress, ByRef exs As List(Of Exception)) As Boolean
        Return Delete(oAddress.Src, oAddress.Codi, exs)
    End Function

    Shared Function Delete(oSrc As DTOBaseGuid, oCod As DTOAddress.Codis, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSrc, oCod, oTrans)
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

    Shared Sub Delete(oSrc As DTOBaseGuid, oCod As DTOAddress.Codis, oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CliAdr WHERE SrcGuid='" & oSrc.Guid.ToString & "' AND Cod=" & oCod & " "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class

Public Class AddressesLoader
    Shared Function All(oContact As DTOContact, Optional oCod As DTOAddress.Codis = DTOAddress.Codis.NotSet) As List(Of DTOAddress)
        Dim retval As New List(Of DTOAddress)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwAddressBase.* ")
        sb.AppendLine("FROM VwAddressBase ")
        sb.AppendLine("WHERE VwAddressBase.SrcGuid='" & oContact.Guid.ToString & "' ")
        If oCod <> DTOAddress.Codis.NotSet Then
            sb.AppendLine("AND VwAddressBase.Cod = " & oCod & " ")
        End If

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oAdr As DTOAddress = SQLHelper.GetAddressFromDataReader(oDrd)
            oAdr.Src = oContact
            retval.Add(oAdr)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Delete(oSrc As DTOBaseGuid, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSrc, oTrans)
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

    Shared Sub Delete(oSrc As DTOBaseGuid, oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CliAdr WHERE SrcGuid='" & oSrc.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class
