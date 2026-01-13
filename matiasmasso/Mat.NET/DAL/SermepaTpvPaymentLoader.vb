Public Class SermepaTpvPaymentLoader

    Shared Function Find(sOrder As String) As DTOSermepaTpvPayment
        Dim retval As DTOSermepaTpvPayment = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SermepaTpvPayment ")
        sb.AppendLine("WHERE Ds_Order='" & sOrder & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOSermepaTpvPayment
            With retval
                .Ds_Merchant_Order = oDrd("Ds_Order")
                .Ds_Date = oDrd("Ds_Date")
                .Ds_Hour = oDrd("Ds_Hour")
                .Ds_Merchant_Amount = oDrd("Ds_Amount")
                .Ds_Merchant_Currency = oDrd("Ds_Currency")
                .Ds_Merchant_MerchantCode = oDrd("Ds_MerchantCode")
                .Ds_Merchant_Terminal = oDrd("Ds_Terminal")
                .Ds_Merchant_MerchantSignature = oDrd("Ds_Signature")
                .Ds_Response = oDrd("Ds_Response")
                .Ds_Merchant_MerchantData = oDrd("Ds_MerchantData")
                .Ds_Merchant_ProductDescription = oDrd("Ds_ProductDescription")
                .Ds_SecurePayment = oDrd("Ds_SecurePayment")
                .Ds_Merchant_TransactionType = oDrd("Ds_TransactionType")
                .Ds_Card_Country = oDrd("Ds_Card_Country")
                .Ds_AuthorisationCode = oDrd("Ds_AuthorisationCode")
                .Ds_Merchant_ConsumerLanguage = oDrd("Ds_ConsumerLanguage")
                .Ds_Card_Type = oDrd("Ds_Card_Type")
                .Ref = oDrd("CcaGuid")
            End With
            oDrd.Close()
        End If
        Return retval
    End Function

    Shared Function Update(oSermepaTpvPayment As DTOSermepaTpvPayment, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSermepaTpvPayment, oTrans)
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


    Shared Sub Update(oSermepaTpvPayment As DTOSermepaTpvPayment, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM SermepaTpvPayment ")
        sb.AppendLine("WHERE Ds_Order='" & oSermepaTpvPayment.Ds_Merchant_Order & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Ds_Order") = oSermepaTpvPayment.Ds_Merchant_Order
        Else
            oRow = oTb.Rows(0)
        End If

        With oSermepaTpvPayment
            'oRow("Nom") = .Nom
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSermepaTpvPayment As DTOSermepaTpvPayment, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSermepaTpvPayment, oTrans)
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


    Shared Sub Delete(oSermepaTpvPayment As DTOSermepaTpvPayment, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE TpvLog ")
        sb.AppendLine("WHERE Ds_Order='" & oSermepaTpvPayment.Ds_Merchant_Order & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class
