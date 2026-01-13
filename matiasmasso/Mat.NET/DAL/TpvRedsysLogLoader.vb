Public Class TpvRedsysLogLoader

#Region "CRUD"

    Shared Function Find(Ds_Order As String) As DTOTpvRedsysResponse
        Dim retval As DTOTpvRedsysResponse = Nothing
        Dim oTpvLog As New DTOTpvRedsysResponse(Ds_Order)
        If Load(oTpvLog) Then
            retval = oTpvLog
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oTpvLog As DTOTpvRedsysResponse) As Boolean
        If Not oTpvLog.IsLoaded And Not oTpvLog.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM TpvLog ")
            sb.AppendLine("WHERE Ds_Order='" & oTpvLog.Ds_Order & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oTpvLog
                    .Ds_Date = SQLHelper.GetStringFromDataReader(oDrd("Ds_Date"))
                    .Ds_Hour = SQLHelper.GetStringFromDataReader(oDrd("Ds_Hour"))
                    .Ds_Amount = SQLHelper.GetStringFromDataReader(oDrd("Ds_Amount"))
                    .Ds_Currency = SQLHelper.GetStringFromDataReader(oDrd("Ds_Currency"))
                    .Ds_MerchantCode = SQLHelper.GetStringFromDataReader(oDrd("Ds_MerchantCode"))
                    .Ds_Terminal = SQLHelper.GetStringFromDataReader(oDrd("Ds_Terminal"))
                    .Ds_Response = SQLHelper.GetStringFromDataReader(oDrd("Ds_Response"))
                    .Ds_MerchantData = SQLHelper.GetStringFromDataReader(oDrd("Ds_MerchantData"))
                    .Ds_SecurePayment = SQLHelper.GetStringFromDataReader(oDrd("Ds_SecurePayment"))
                    .Ds_TransactionType = SQLHelper.GetStringFromDataReader(oDrd("Ds_TransactionType"))
                    .Ds_Card_Country = SQLHelper.GetStringFromDataReader(oDrd("Ds_Card_Country"))
                    .Ds_AuthorisationCode = SQLHelper.GetStringFromDataReader(oDrd("Ds_AuthorisationCode"))
                    .Ds_ConsumerLanguage = SQLHelper.GetStringFromDataReader(oDrd("Ds_ConsumerLanguage"))
                    .Mode = SQLHelper.GetIntegerFromDataReader(oDrd("Mode"))

                    If Not IsDBNull(oDrd("Request")) Then
                        Select Case .Mode
                            Case DTOTpvRequest.Modes.Free
                                .Request = New DTOUser(DirectCast(oDrd("Request"), Guid))
                            Case DTOTpvRequest.Modes.Pdc
                                .Request = New DTOPurchaseOrder(oDrd("Request"))
                            Case DTOTpvRequest.Modes.Alb
                                .Request = New DTODelivery(oDrd("Request"))
                            Case DTOTpvRequest.Modes.Impagat
                                .Request = New DTOImpagat(oDrd("Request"))
                        End Select
                    End If

                    If Not IsDBNull(oDrd("CcaGuid")) Then
                        .Result = New DTOCca(oDrd("CcaGuid"))
                    End If

                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oTpvLog.IsLoaded
        Return retval
    End Function

    Shared Function Update(oTpvLog As DTOTpvRedsysResponse, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oTpvLog, oTrans)
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


    Shared Sub Update(oTpvLog As DTOTpvRedsysResponse, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM TpvLog ")
        sb.AppendLine("WHERE Ds_Order='" & oTpvLog.Ds_Order & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Ds_Order") = oTpvLog.Ds_Order
        Else
            oRow = oTb.Rows(0)
        End If

        With oTpvLog
            oRow("Ds_Date") = .Ds_Date
            oRow("Ds_Hour") = .Ds_Hour
            oRow("Ds_Amount") = .Ds_Amount
            oRow("Ds_Currency") = .Ds_Currency
            oRow("Ds_MerchantCode") = .Ds_MerchantCode
            oRow("Ds_Terminal") = .Ds_Terminal
            oRow("Ds_Response") = .Ds_Response
            oRow("Ds_MerchantData") = .Ds_MerchantData
            oRow("Ds_SecurePayment") = .Ds_SecurePayment
            oRow("Ds_TransactionType") = .Ds_TransactionType
            oRow("Ds_Card_Country") = .Ds_Card_Country
            oRow("Ds_AuthorisationCode") = .Ds_AuthorisationCode
            oRow("Ds_ConsumerLanguage") = .Ds_ConsumerLanguage

            oRow("Mode") = SQLHelper.NullableInt(.Mode)
            oRow("Request") = SQLHelper.NullableBaseGuid(.Request)
            oRow("CcaGuid") = SQLHelper.NullableBaseGuid(.Result)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oTpvLog As DTOTpvRedsysResponse, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oTpvLog, oTrans)
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


    Shared Sub Delete(oTpvLog As DTOTpvRedsysResponse, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE TpvLog WHERE Ds_Order='" & oTpvLog.Ds_Order & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class TpvRedsysLogsLoader

    Shared Function All() As List(Of DTOTpvRedsysResponse)
        Dim retval As New List(Of DTOTpvRedsysResponse)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM TpvLog ")
        sb.AppendLine("ORDER BY SUBSTRING(Ds_Date,7,4) DESC,SUBSTRING(Ds_Date,4,2) DESC,SUBSTRING(Ds_Date,1,2) DESC,Ds_Hour DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOTpvRedsysResponse(oDrd("Guid"))
            With item
                .Ds_Date = SQLHelper.GetStringFromDataReader(oDrd("Ds_Date"))
                .Ds_Hour = SQLHelper.GetStringFromDataReader(oDrd("Ds_Hour"))
                .Ds_Amount = SQLHelper.GetStringFromDataReader(oDrd("Ds_Amount"))
                .Ds_Currency = SQLHelper.GetStringFromDataReader(oDrd("Ds_Currency"))
                .Ds_MerchantCode = SQLHelper.GetStringFromDataReader(oDrd("Ds_MerchantCode"))
                .Ds_Terminal = SQLHelper.GetStringFromDataReader(oDrd("Ds_Terminal"))
                .Ds_Response = SQLHelper.GetStringFromDataReader(oDrd("Ds_Response"))
                .Ds_MerchantData = SQLHelper.GetStringFromDataReader(oDrd("Ds_MerchantData"))
                .Ds_SecurePayment = SQLHelper.GetStringFromDataReader(oDrd("Ds_SecurePayment"))
                .Ds_TransactionType = SQLHelper.GetStringFromDataReader(oDrd("Ds_TransactionType"))
                .Ds_Card_Country = SQLHelper.GetStringFromDataReader(oDrd("Ds_Card_Country"))
                .Ds_AuthorisationCode = SQLHelper.GetStringFromDataReader(oDrd("Ds_AuthorisationCode"))
                .Ds_ConsumerLanguage = SQLHelper.GetStringFromDataReader(oDrd("Ds_ConsumerLanguage"))
                .Mode = SQLHelper.GetIntegerFromDataReader(oDrd("Mode"))

                If Not IsDBNull(oDrd("Request")) Then
                    Select Case .Mode
                        Case DTOTpvRequest.Modes.Free
                            .Request = New DTOUser(DirectCast(oDrd("Request"), Guid))
                        Case DTOTpvRequest.Modes.Pdc
                            .Request = New DTOPurchaseOrder(oDrd("Request"))
                        Case DTOTpvRequest.Modes.Alb
                            .Request = New DTODelivery(oDrd("Request"))
                        Case DTOTpvRequest.Modes.Impagat
                            .Request = New DTOImpagat(oDrd("Request"))
                    End Select
                End If

                If Not IsDBNull(oDrd("CcaGuid")) Then
                    .Result = New DTOCca(oDrd("CcaGuid"))
                End If

                .IsLoaded = True
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
