Public Class PaymentGatewayLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOPaymentGateway
        Dim retval As DTOPaymentGateway = Nothing
        Dim oPaymentGateway As New DTOPaymentGateway(oGuid)
        If Load(oPaymentGateway) Then
            retval = oPaymentGateway
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPaymentGateway As DTOPaymentGateway) As Boolean
        If Not oPaymentGateway.IsLoaded And Not oPaymentGateway.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM PaymentGateway ")
            sb.AppendLine("WHERE Guid='" & oPaymentGateway.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oPaymentGateway
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                    .MerchantCode = SQLHelper.GetStringFromDataReader(oDrd("MerchantCode"))
                    .SignatureKey = SQLHelper.GetStringFromDataReader(oDrd("SignatureKey"))
                    .SermepaUrl = SQLHelper.GetStringFromDataReader(oDrd("SermepaUrl"))
                    .MerchantURL = SQLHelper.GetStringFromDataReader(oDrd("MerchantURL"))
                    .UrlOK = SQLHelper.GetStringFromDataReader(oDrd("UrlOK"))
                    .UrlKO = SQLHelper.GetStringFromDataReader(oDrd("UrlKO"))
                    .UserAdmin = SQLHelper.GetStringFromDataReader(oDrd("UserAdmin"))
                    .Pwd = SQLHelper.GetStringFromDataReader(oDrd("Pwd"))
                    .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                    .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oPaymentGateway.IsLoaded
        Return retval
    End Function

    Shared Function Update(oPaymentGateway As DTOPaymentGateway, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oPaymentGateway, oTrans)
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


    Shared Sub Update(oPaymentGateway As DTOPaymentGateway, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PaymentGateway ")
        sb.AppendLine("WHERE Guid='" & oPaymentGateway.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPaymentGateway.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPaymentGateway
            oRow("Nom") = .Nom
            oRow("MerchantCode") = .MerchantCode
            oRow("SignatureKey") = .SignatureKey
            oRow("SermepaUrl") = .SermepaUrl
            oRow("MerchantURL") = .MerchantURL
            oRow("UrlOK") = .UrlOK
            oRow("UrlKO") = .UrlKO
            oRow("UserAdmin") = .UserAdmin
            oRow("Pwd") = .Pwd
            oRow("FchFrom") = .FchFrom
            oRow("FchTo") = SQLHelper.GetFchFromDataReader(.FchTo)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oPaymentGateway As DTOPaymentGateway, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oPaymentGateway, oTrans)
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


    Shared Sub Delete(oPaymentGateway As DTOPaymentGateway, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE PaymentGateway WHERE Guid='" & oPaymentGateway.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class PaymentGatewaysLoader

    Shared Function All() As List(Of DTOPaymentGateway)
        Dim retval As New List(Of DTOPaymentGateway)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PaymentGateway ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOPaymentGateway(oDrd("Guid"))
            With item
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .MerchantCode = SQLHelper.GetStringFromDataReader(oDrd("MerchantCode"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
