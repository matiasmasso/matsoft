Public Class OnlineVendorSerpLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOOnlineVendorSerp
        Dim retval As DTOOnlineVendorSerp = Nothing
        Dim oOnlineVendorSerp As New DTOOnlineVendorSerp(oGuid)
        If Load(oOnlineVendorSerp) Then
            retval = oOnlineVendorSerp
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oOnlineVendorSerp As DTOOnlineVendorSerp) As Boolean
        If Not oOnlineVendorSerp.IsLoaded And Not oOnlineVendorSerp.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT OnlineVendorSerp.Fch ")
            sb.AppendLine(", VwSkuNom.* ")
            sb.AppendLine(", OnlineVendorTrack.Guid AS TrackGuid, OnlineVendorTrack.Vendor, OnlineVendorTrack.Pos, OnlineVendorTrack.Clicked, OnlineVendorTrack.Eur ")
            sb.AppendLine(", OnlineVendor.Nom AS VendorNom ")
            sb.AppendLine("FROM OnlineVendorSerp ")
            sb.AppendLine("INNER JOIN VwSkuNom ON OnlineVendorSerp.Sku=VwSkuNom.SkuGuid ")
            sb.AppendLine("INNER JOIN OnlineVendorTrack ON OnlineVendorSerp.Guid = OnlineVendorTrack.Serp ")
            sb.AppendLine("INNER JOIN OnlineVendor ON OnlineVendorTrack.Vendor = OnlineVendor.Guid ")
            sb.AppendLine("WHERE OnlineVendorSerp.Guid='" & oOnlineVendorSerp.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oOnlineVendorSerp.IsLoaded Then
                    With oOnlineVendorSerp
                        .Fch = oDrd("Fch")
                        .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                        .Tracks = New List(Of DTOOnlineVendorTrack)
                        .IsLoaded = True
                    End With
                End If

                Dim oTrack As New DTOOnlineVendorTrack(oDrd("TrackGuid"))
                With oTrack
                    .Serp = oOnlineVendorSerp
                    .Vendor = New DTOOnlineVendor
                    .Vendor.Nom = oDrd("VendorNom")
                    .Pos = oDrd("Pos")
                    .Clicked = oDrd("Clicked")
                    .Eur = SQLHelper.GetDecimalFromDataReader(oDrd("Eur"))
                End With
                oOnlineVendorSerp.Tracks.Add(oTrack)
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oOnlineVendorSerp.IsLoaded
        Return retval
    End Function

    Shared Function Update(oOnlineVendorSerp As DTOOnlineVendorSerp, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oOnlineVendorSerp, oTrans)
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


    Shared Sub Update(oOnlineVendorSerp As DTOOnlineVendorSerp, ByRef oTrans As SqlTransaction)
        UpdateHeader(oOnlineVendorSerp, oTrans)
        UpdateTracks(oOnlineVendorSerp, oTrans)
    End Sub

    Shared Sub UpdateHeader(oOnlineVendorSerp As DTOOnlineVendorSerp, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM OnlineVendorSerp ")
        sb.AppendLine("WHERE Guid='" & oOnlineVendorSerp.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oOnlineVendorSerp.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oOnlineVendorSerp
            oRow("Sku") = .Sku.Guid
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateTracks(oOnlineVendorSerp As DTOOnlineVendorSerp, ByRef oTrans As SqlTransaction)
        DeleteTracks(oOnlineVendorSerp, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM OnlineVendorTrack ")
        sb.AppendLine("WHERE Serp='" & oOnlineVendorSerp.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oTrack In oOnlineVendorSerp.Tracks
            Dim oRow As DataRow = oTb.NewRow
            oRow("Serp") = oOnlineVendorSerp.Guid
            With oTrack
                oRow("Guid") = .Guid
                oRow("Vendor") = .Vendor.Guid
                oRow("Pos") = .Pos
                oRow("Clicked") = .Clicked
                oRow("Eur") = .Eur
            End With
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oOnlineVendorSerp As DTOOnlineVendorSerp, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oOnlineVendorSerp, oTrans)
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

    Shared Sub Delete(oOnlineVendorSerp As DTOOnlineVendorSerp, ByRef oTrans As SqlTransaction)
        DeleteTracks(oOnlineVendorSerp, oTrans)
        DeleteHeader(oOnlineVendorSerp, oTrans)
    End Sub

    Shared Sub DeleteTracks(oOnlineVendorSerp As DTOOnlineVendorSerp, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE OnlineVendorTrack WHERE Serp='" & oOnlineVendorSerp.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
    Shared Sub DeleteHeader(oOnlineVendorSerp As DTOOnlineVendorSerp, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE OnlineVendorSerp WHERE Guid='" & oOnlineVendorSerp.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

    Shared Function Factory(oSku As DTOProductSku) As DTOOnlineVendorSerp
        Dim retval As New DTOOnlineVendorSerp
        With retval
            .Fch = Now
            .Sku = oSku
            .Tracks = New List(Of DTOOnlineVendorTrack)
        End With

        If oSku.Ean13 IsNot Nothing Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT OnlineVendorStock.Vendor, OnlineVendor.Nom, OnlineVendor.Url, OnlineVendor.LandingPage, OnlineVendor.Obs, OnlineVendor.IsActive ")
            sb.AppendLine(", OnlineVendor.Customer, Clx.Clx ")
            sb.AppendLine("FROM OnlineVendor ")
            sb.AppendLine("INNER JOIN OnlineVendorStock ON OnlineVendor.Guid = OnlineVendorStock.Vendor ")
            sb.AppendLine("LEFT OUTER JOIN Clx ON OnlineVendor.Customer = Clx.Guid ")
            sb.AppendLine("WHERE OnlineVendorStock.Ean = '" & oSku.Ean13.Value & "' ")
            sb.AppendLine("ORDER BY OnlineVendor.Nom")
            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Dim idx As Integer = 1
            Do While oDrd.Read

                Dim oTrack As New DTOOnlineVendorTrack
                With oTrack
                    .Vendor = New DTOOnlineVendor(oDrd("Vendor"))
                    With .Vendor
                        .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                        .Url = SQLHelper.GetStringFromDataReader(oDrd("Url"))
                        .LandingPage = SQLHelper.GetStringFromDataReader(oDrd("LandingPage"))
                        .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                        .IsActive = SQLHelper.GetBooleanFromDatareader(oDrd("IsActive"))

                        If Not IsDBNull(oDrd("Customer")) Then
                            .Customer = New DTOCustomer(oDrd("Customer"))
                            .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("Clx"))
                        End If
                    End With
                    .Pos = idx
                    .Serp = retval
                    If oSku.RRPP IsNot Nothing Then
                        .Eur = oSku.RRPP.Eur
                    End If
                End With
                idx += 1
                retval.Tracks.Add(oTrack)
            Loop
            oDrd.Close()
        End If
        Return retval
    End Function

End Class

Public Class OnlineVendorSerpsLoader

    Shared Function All() As List(Of DTOOnlineVendorSerp)
        Dim retval As New List(Of DTOOnlineVendorSerp)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM OnlineVendorSerp ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOOnlineVendorSerp(oDrd("Guid"))
            With item
                '.Nom = oDrd("Nom")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class