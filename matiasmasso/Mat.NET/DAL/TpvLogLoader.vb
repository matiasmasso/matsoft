Public Class TpvLogLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOTpvLog
        Dim retval As DTOTpvLog = Nothing
        Dim oTpvLog As New DTOTpvLog(oGuid)
        If Load(oTpvLog) Then
            retval = oTpvLog
        End If
        Return retval
    End Function

    Shared Function FromOrder(sDs_Order As String) As DTOTpvLog
        Dim retval As DTOTpvLog = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM TpvLog ")
        sb.AppendLine("WHERE Ds_Order='" & sDs_Order & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOTpvLog(oDrd("Guid"))
            retval.Ds_Order = sDs_Order
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oTpvLog As DTOTpvLog) As Boolean
        If Not oTpvLog.IsLoaded And Not oTpvLog.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT TpvLog.*, RedsysErr.ErrDsc ")
            sb.AppendLine("FROM TpvLog ")
            sb.AppendLine("LEFT OUTER JOIN RedsysErr ON TpvLog.Ds_Response = RedsysErr.Id ")
            sb.AppendLine("WHERE TpvLog.Guid='" & oTpvLog.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oTpvLog
                    .Ds_Order = SQLHelper.GetStringFromDataReader(oDrd("Ds_Order"))
                    .Ds_Date = SQLHelper.GetStringFromDataReader(oDrd("Ds_Date"))
                    .Ds_Hour = SQLHelper.GetStringFromDataReader(oDrd("Ds_Hour"))
                    .Ds_Amount = SQLHelper.GetStringFromDataReader(oDrd("Ds_Amount"))
                    .Ds_Currency = SQLHelper.GetStringFromDataReader(oDrd("Ds_Currency"))
                    .Ds_MerchantCode = SQLHelper.GetStringFromDataReader(oDrd("Ds_MerchantCode"))
                    .Ds_Terminal = SQLHelper.GetStringFromDataReader(oDrd("Ds_Terminal"))
                    .Ds_Signature = SQLHelper.GetStringFromDataReader(oDrd("Ds_Signature"))
                    .Ds_Response = SQLHelper.GetStringFromDataReader(oDrd("Ds_Response"))
                    .Ds_MerchantData = SQLHelper.GetStringFromDataReader(oDrd("Ds_MerchantData"))
                    .Ds_ProductDescription = SQLHelper.GetStringFromDataReader(oDrd("Ds_ProductDescription"))
                    .Ds_SecurePayment = SQLHelper.GetStringFromDataReader(oDrd("Ds_SecurePayment"))
                    .Ds_TransactionType = SQLHelper.GetStringFromDataReader(oDrd("Ds_TransactionType"))
                    .Ds_Card_Country = SQLHelper.GetStringFromDataReader(oDrd("Ds_Card_Country"))
                    .Ds_AuthorisationCode = SQLHelper.GetStringFromDataReader(oDrd("Ds_AuthorisationCode"))
                    .Ds_ConsumerLanguage = SQLHelper.GetStringFromDataReader(oDrd("Ds_ConsumerLanguage"))
                    .Ds_Card_Type = SQLHelper.GetStringFromDataReader(oDrd("Ds_Card_Type"))

                    'raw response:
                    .Ds_MerchantParameters = SQLHelper.GetStringFromDataReader(oDrd("Ds_MerchantParameters"))
                    .Ds_SignatureReceived = SQLHelper.GetStringFromDataReader(oDrd("Ds_SignatureReceived"))
                    .ErrDsc = SQLHelper.GetStringFromDataReader(oDrd("ErrDsc"))

                    If Not IsDBNull(oDrd("CcaGuid")) Then
                        .Result = New DTOCca(oDrd("CcaGuid"))
                    End If
                    If Not IsDBNull(oDrd("User")) Then
                        .User = New DTOUser(DirectCast(oDrd("User"), Guid))
                    End If
                    If Not IsDBNull(oDrd("Titular")) Then
                        .Titular = New DTOContact(DirectCast(oDrd("Titular"), Guid))
                    End If
                    .Mode = SQLHelper.GetIntegerFromDataReader(oDrd("Mode"))
                    If Not IsDBNull(oDrd("Request")) Then
                        Select Case .Mode
                            Case DTOTpvRequest.Modes.Alb
                                .Request = New DTODelivery(oDrd("Request"))
                            Case DTOTpvRequest.Modes.Pdc
                                .Request = New DTOPurchaseOrder(oDrd("Request"))
                            Case DTOTpvRequest.Modes.Impagat
                                .Request = New DTOImpagat(oDrd("Request"))
                        End Select
                    End If
                    .FchCreated = oDrd("FchCreated")
                    .SignatureValidated = oDrd("SignatureValidated")
                    .ProcessedSuccessfully = oDrd("ProcessedSuccessfully")
                    .Exceptions = SQLHelper.GetStringFromDataReader(oDrd("Exceptions"))

                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oTpvLog.IsLoaded
        Return retval
    End Function

    Shared Function Update(oTpvLog As DTOTpvLog, ByRef exs As List(Of Exception)) As Boolean
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


    Shared Sub Update(oTpvLog As DTOTpvLog, ByRef oTrans As SqlTransaction)
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
            oRow("Guid") = .Guid
            oRow("Ds_Date") = .Ds_Date
            oRow("Ds_Hour") = .Ds_Hour
            oRow("Ds_Amount") = .Ds_Amount
            oRow("Ds_Currency") = .Ds_Currency
            oRow("Ds_MerchantCode") = .Ds_MerchantCode
            oRow("Ds_Terminal") = .Ds_Terminal
            oRow("Ds_Signature") = .Ds_Signature
            oRow("Ds_Response") = .Ds_Response
            oRow("Ds_MerchantData") = .Ds_MerchantData
            oRow("Ds_ProductDescription") = Left(.Ds_ProductDescription, 125)
            oRow("Ds_SecurePayment") = .Ds_SecurePayment
            oRow("Ds_TransactionType") = .Ds_TransactionType
            oRow("Ds_Card_Country") = .Ds_Card_Country
            oRow("Ds_AuthorisationCode") = .Ds_AuthorisationCode
            oRow("Ds_ConsumerLanguage") = .Ds_ConsumerLanguage
            oRow("Ds_Card_Type") = .Ds_Card_Type
            oRow("Ds_MerchantParameters") = SQLHelper.NullableString(.Ds_MerchantParameters)
            oRow("Ds_SignatureReceived") = SQLHelper.NullableString(.Ds_SignatureReceived)

            oRow("CcaGuid") = SQLHelper.NullableBaseGuid(.Result)
            oRow("Request") = SQLHelper.NullableBaseGuid(.Request)

            If .User IsNot Nothing Then
                If Not .User.Guid.Equals(Guid.Empty) Then
                    oRow("User") = .User.Guid
                End If
            End If

            oRow("Titular") = SQLHelper.NullableBaseGuid(.Titular)
            oRow("Mode") = .Mode

            oRow("FchCreated") = SQLHelper.NullableFch(.FchCreated)
            oRow("SignatureValidated") = .SignatureValidated
            oRow("ProcessedSuccessfully") = .ProcessedSuccessfully
            oRow("Exceptions") = SQLHelper.NullableString(.Exceptions)

        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oTpvLog As DTOTpvLog, ByRef exs As List(Of Exception)) As Boolean
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


    Shared Sub Delete(oTpvLog As DTOTpvLog, ByRef oTrans As SqlTransaction)
        Dim oCca As DTOCca = oTpvLog.Result
        Select Case oTpvLog.Mode
            Case DTOTpvRequest.Modes.Alb
                Dim oDelivery As DTODelivery = oTpvLog.Request
                DeliveryLoader.Load(oDelivery)
                oDelivery.RetencioCod = DTODelivery.RetencioCods.VISA
                DeliveryLoader.Update(oDelivery, oTrans)
            Case DTOTpvRequest.Modes.Pdc
                Dim oPurchaseOrder As DTOPurchaseOrder = oTpvLog.Request
                PurchaseOrderLoader.Load(oPurchaseOrder)
                PurchaseOrderLoader.Update(oPurchaseOrder, oTrans)
            Case DTOTpvRequest.Modes.Impagat
                Dim oImpagat As DTOImpagat = oTpvLog.Request
                ImpagatLoader.Load(oImpagat)
                Dim oCobrat As DTOAmt = DTOAmt.Factory(CDec(oTpvLog.Ds_Amount) / 100)
                oImpagat.PagatACompte.Substract(oCobrat)
                oImpagat.status = DTOImpagat.StatusCodes.enNegociacio
                ImpagatLoader.Update(oImpagat, oTrans)
        End Select

        CcaLoader.Delete(oCca, oTrans)

        Dim SQL As String = "DELETE TpvLog WHERE Ds_Order='" & oTpvLog.Ds_Order & "' "
        'SQLHelper.ExecuteNonQuery(SQL, oTrans)
        '(no podem eliminar perque les comandes son consecutives i Redsys no deixará repetir el numero)
    End Sub

#End Region

    Shared Function BookRequest(oTpvLog As DTOTpvLog, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            oTpvLog.Ds_Order = NextRequest(oTrans)
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

    Protected Shared Function NextRequest(oTrans As SqlTransaction) As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Ds_Order AS LastId ")
        sb.AppendLine("From TpvLog ")
        sb.AppendLine("where dS_oRDER like '9999A%' ")
        sb.AppendLine("ORDER BY Ds_Order DESC ")

        Dim SQL As String = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim sLastRequest As String = ""
        If oTb.Rows.Count > 0 Then
            Dim oRow As DataRow = oTb.Rows(0)
            If Not IsDBNull(oRow("LastId")) Then
                sLastRequest = oRow("LastId")
            End If
        End If

        '12 caracters alfanumerics, els 4 primers han de ser numerics
        'Des de Gener 2016 comencem tots per '9999A' + 7 digits numerics consecutius

        Dim lastId As Integer
        If sLastRequest.StartsWith("9999A") Then
            lastId = sLastRequest.Substring(5)
        End If

        Dim retval As String = String.Format("9999A{0:0000000}", lastId + 1)
        Return retval
    End Function

End Class

Public Class TpvLogsLoader

    Shared Function All() As List(Of DTOTpvLog)
        Dim retval As New List(Of DTOTpvLog)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TpvLog.*, Email.Adr, Email.Nickname, CliGral.FullNom, RedsysErr.ErrDsc ")
        sb.AppendLine("FROM TpvLog ")
        sb.AppendLine("LEFT OUTER JOIN RedsysErr ON TpvLog.Ds_Response = RedsysErr.Id ")
        sb.AppendLine("LEFT OUTER JOIN Email ON TpvLog.[User]=Email.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON TpvLog.Titular=CliGral.Guid ")
        sb.AppendLine("ORDER BY FchCreated DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOTpvLog(DirectCast(oDrd("Guid"), Guid))
            With item
                .Ds_Order = SQLHelper.GetStringFromDataReader(oDrd("Ds_Order"))
                .Ds_Date = SQLHelper.GetStringFromDataReader(oDrd("Ds_Date"))
                .Ds_Hour = SQLHelper.GetStringFromDataReader(oDrd("Ds_Hour"))
                .Ds_Amount = SQLHelper.GetStringFromDataReader(oDrd("Ds_Amount"))
                .Ds_Currency = SQLHelper.GetStringFromDataReader(oDrd("Ds_Currency"))
                .Ds_MerchantCode = SQLHelper.GetStringFromDataReader(oDrd("Ds_MerchantCode"))
                .Ds_Terminal = SQLHelper.GetStringFromDataReader(oDrd("Ds_Terminal"))
                .Ds_Signature = SQLHelper.GetStringFromDataReader(oDrd("Ds_Signature"))
                .Ds_Response = SQLHelper.GetStringFromDataReader(oDrd("Ds_Response"))
                .Ds_MerchantData = SQLHelper.GetStringFromDataReader(oDrd("Ds_MerchantData"))
                .Ds_ProductDescription = SQLHelper.GetStringFromDataReader(oDrd("Ds_ProductDescription"))
                .Ds_SecurePayment = SQLHelper.GetStringFromDataReader(oDrd("Ds_SecurePayment"))
                .Ds_TransactionType = SQLHelper.GetStringFromDataReader(oDrd("Ds_TransactionType"))
                .Ds_Card_Country = SQLHelper.GetStringFromDataReader(oDrd("Ds_Card_Country"))
                .Ds_AuthorisationCode = SQLHelper.GetStringFromDataReader(oDrd("Ds_AuthorisationCode"))
                .Ds_ConsumerLanguage = SQLHelper.GetStringFromDataReader(oDrd("Ds_ConsumerLanguage"))
                .Ds_Card_Type = SQLHelper.GetStringFromDataReader(oDrd("Ds_Card_Type"))

                'raw response:
                .Ds_MerchantParameters = SQLHelper.GetStringFromDataReader(oDrd("Ds_MerchantParameters"))
                .Ds_SignatureReceived = SQLHelper.GetStringFromDataReader(oDrd("Ds_SignatureReceived"))
                .ErrDsc = SQLHelper.GetStringFromDataReader(oDrd("ErrDsc"))

                If Not IsDBNull(oDrd("Mode")) Then
                    .Mode = oDrd("Mode")
                    Select Case .Mode
                        Case DTOTpvRequest.Modes.Alb
                            .Request = New DTODelivery(oDrd("Request"))
                        Case DTOTpvRequest.Modes.Pdc
                            .Request = New DTOPurchaseOrder(oDrd("Request"))
                        Case DTOTpvRequest.Modes.Impagat
                            .Request = New DTOImpagat(oDrd("Request"))
                    End Select
                End If

                If Not IsDBNull(oDrd("Titular")) Then
                    .Titular = New DTOContact(DirectCast(oDrd("Titular"), Guid))
                    With .Titular
                        .FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    End With
                End If

                If Not IsDBNull(oDrd("User")) Then
                    .User = New DTOUser(DirectCast(oDrd("User"), Guid))
                    With .User
                        .EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                        .NickName = SQLHelper.GetStringFromDataReader(oDrd("NickName"))
                    End With
                End If

                If Not IsDBNull(oDrd("CcaGuid")) Then
                    .Result = New DTOCca(oDrd("CcaGuid"))
                End If

                .FchCreated = oDrd("FchCreated")
                .SignatureValidated = SQLHelper.GetBooleanFromDatareader(oDrd("SignatureValidated"))
                .ProcessedSuccessfully = SQLHelper.GetBooleanFromDatareader(oDrd("ProcessedSuccessfully"))
                .Exceptions = SQLHelper.GetStringFromDataReader(oDrd("Exceptions"))

                .IsLoaded = True
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
