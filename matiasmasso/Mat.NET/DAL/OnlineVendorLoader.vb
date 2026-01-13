Public Class OnlineVendorLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid, IncludeLogo As Boolean) As DTOOnlineVendor
        Dim retval As DTOOnlineVendor = Nothing
        Dim oOnlineVendor As New DTOOnlineVendor(oGuid)
        If Load(oOnlineVendor, IncludeLogo) Then
            retval = oOnlineVendor
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oOnlineVendor As DTOOnlineVendor, Optional IncludeLogo As Boolean = False) As Boolean
        If Not oOnlineVendor.IsLoaded And Not oOnlineVendor.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT OnlineVendor.Nom, OnlineVendor.Url, OnlineVendor.LandingPage, OnlineVendor.Obs, OnlineVendor.Customer, OnlineVendor.IsActive  ")
            If IncludeLogo Then sb.AppendLine(", OnlineVendor.Logo")
            sb.AppendLine(", Clx.Clx ")
            sb.AppendLine("FROM OnlineVendor ")
            sb.AppendLine("LEFT OUTER JOIN Clx ON OnlineVendor.Customer = Clx.Guid ")
            sb.AppendLine("WHERE OnlineVendor.Guid='" & oOnlineVendor.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oOnlineVendor
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                    .Url = SQLHelper.GetStringFromDataReader(oDrd("Url"))
                    .LandingPage = SQLHelper.GetStringFromDataReader(oDrd("LandingPage"))
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .IsActive = SQLHelper.GetBooleanFromDatareader(oDrd("IsActive"))

                    If Not IsDBNull(oDrd("Customer")) Then
                        .Customer = New DTOCustomer(oDrd("Customer"))
                        .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("Clx"))
                    End If

                    If IncludeLogo Then
                        .Logo = SQLHelper.GetImageFromDatareader(oDrd("Logo"))
                        .IsLoaded = True
                    End If
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oOnlineVendor.IsLoaded
        Return retval
    End Function


    Shared Function Update(oOnlineVendor As DTOOnlineVendor, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oOnlineVendor, oTrans)
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


    Shared Sub Update(oOnlineVendor As DTOOnlineVendor, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM OnlineVendor ")
        sb.AppendLine("WHERE Guid='" & oOnlineVendor.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oOnlineVendor.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oOnlineVendor
            oRow("Nom") = SQLHelper.NullableString(.Nom)
            oRow("Url") = SQLHelper.NullableString(.Url)
            oRow("LandingPage") = SQLHelper.NullableString(.LandingPage)
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            oRow("Customer") = SQLHelper.NullableBaseGuid(.Customer)
            oRow("IsActive") = .IsActive
            oRow("Logo") = SQLHelper.NullableImage(.Logo)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oOnlineVendor As DTOOnlineVendor, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oOnlineVendor, oTrans)
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


    Shared Sub Delete(oOnlineVendor As DTOOnlineVendor, ByRef oTrans As SqlTransaction)
        DeleteHeader(oOnlineVendor, oTrans)
    End Sub

    Shared Sub DeleteHeader(oOnlineVendor As DTOOnlineVendor, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE OnlineVendor WHERE Guid='" & oOnlineVendor.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class OnlineVendorsLoader

    Shared Function All(Optional oDomain As DTOWebPageAlias.domains = Nothing) As List(Of DTOOnlineVendor)
        Dim retval As New List(Of DTOOnlineVendor)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT OnlineVendor.Nom, OnlineVendor.Url, OnlineVendor.LandingPage, OnlineVendor.Obs, OnlineVendor.IsActive ")
        sb.AppendLine(", OnlineVendor.Customer, Clx.Clx ")
        sb.AppendLine("FROM OnlineVendor ")
        sb.AppendLine("LEFT OUTER JOIN Clx ON OnlineVendor.Customer = Clx.Guid ")
        sb.AppendLine("ORDER BY OnlineVendor.Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOOnlineVendor(oDrd("Guid"))
            With item
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
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
