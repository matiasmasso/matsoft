Public Class TpvRedSys

    Shared Async Function Config(exs As List(Of Exception), oEmp As DTOEmp, oEnvironment As DTOPaymentGateway.Environments) As Task(Of DTOPaymentGateway)
        Dim retval As DTOPaymentGateway = Nothing
        Select Case oEnvironment
            Case DTOPaymentGateway.Environments.Production
                retval = Await FEB2.PaymentGateway.ProductionEnvironment(exs, oEmp)
            Case DTOPaymentGateway.Environments.Testing
                retval = Await FEB2.PaymentGateway.TestingEnvironment(exs, oEmp)
        End Select
        Return retval
    End Function

    Shared Async Function LogResponse(oEmp As DTOEmp, oTpvLog As DTOTpvLog, exs As List(Of Exception)) As Task(Of Boolean)
        If DTOTpvLog.isValidResponse(oTpvLog, exs) Then
            If oTpvLog.User Is Nothing Then oTpvLog.User = DTOUser.Wellknown(DTOUser.Wellknowns.info)

            oTpvLog = Await FEB2.TpvRedSys.Procesa(oEmp, oTpvLog, exs)
            If exs.Count = 0 Then
                oTpvLog.ProcessedSuccessfully = True
                oTpvLog.Result = oTpvLog.Result
            End If
        End If

        'registra l'event
        Dim sb As New System.Text.StringBuilder
        For Each ex As Exception In exs
            sb.AppendLine(ex.Message)
        Next

        oTpvLog.Exceptions = sb.ToString
        Await FEB2.TpvLog.Update(oTpvLog, exs)

        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function



    Shared Async Function Procesa(oEmp As DTOEmp, oLog As DTOTpvLog, exs As List(Of Exception)) As Task(Of DTOTpvLog)
        Dim retval As DTOTpvLog = oLog
        oLog.Result = Await FEB2.TpvLog.CcaFactory(oEmp, oLog, exs)
        Select Case oLog.Mode
            Case DTOTpvRequest.Modes.Alb
                retval = Await FEB2.Delivery.CobraPerVisa(exs, oEmp, oLog)
            Case DTOTpvRequest.Modes.Pdc
                retval = Await FEB2.PurchaseOrder.CobraPerVisa(exs, oLog)
            Case DTOTpvRequest.Modes.Impagat
                retval = FEB2.Impagat.CobraPerVisa(oEmp, oLog, exs)
            Case Else
                oLog.Result.Id = Await FEB2.Cca.Update(exs, oLog.Result)
                If exs.Count = 0 Then
                    retval = oLog
                End If
        End Select
        Return retval
    End Function

    Shared Async Function LoadLog(oEmp As DTOEmp, oTpvLog As DTOTpvLog, MerchantParameters As String, signatureReceived As String, oEnvironment As DTOPaymentGateway.Environments, exs As List(Of Exception)) As Task(Of DTOTpvLog)
        Dim retval = Await FEB2.TpvRedSys.DecodeMerchantParameters(oEmp, MerchantParameters, signatureReceived, oEnvironment, exs)
        Return retval
    End Function

    Shared Async Function SignedRequest(exs As List(Of Exception), oConfig As DTOPaymentGateway, oUser As DTOUser, oLang As DTOLang, oRequestMode As DTOTpvRequest.Modes, oSourceGuid As Guid, oAmt As DTOAmt, sConcepte As String) As Task(Of DTOTpvRedsysPasarela)
        Dim retval As New DTOTpvRedsysPasarela
        Dim oTpvLog As New DTOTpvLog

        oTpvLog = Await FEB2.TpvRedSys.BookRequest(oTpvLog, oConfig, oUser, oLang, oRequestMode, oSourceGuid, oAmt, sConcepte, exs)
        If exs.Count = 0 Then
            Dim sMerchantParameters As String = DTOTpvRequest.CreateMerchantParameters(oConfig, oTpvLog)

            retval = New DTOTpvRedsysPasarela
            With retval
                .Ds_Url_Tpv = oConfig.SermepaUrl()
                .Ds_MerchantParameters = sMerchantParameters
                .Ds_Signature = DTOTpvRequest.CreateMerchantSignature(oConfig, sMerchantParameters, oTpvLog.Ds_Order)
                .Ds_SignatureVersion = "HMAC_SHA256_V1"
            End With
        End If

        Return retval
    End Function

    Shared Async Function BookRequest(oTpv As DTOTpvLog, oConfig As DTOPaymentGateway, oUser As DTOUser, oLang As DTOLang, oRequestMode As DTOTpvRequest.Modes, oSourceGuid As Guid, oAmt As DTOAmt, sConcepte As String, exs As List(Of Exception)) As Task(Of DTOTpvLog)
        ' oTpv = New DTOTpvLog
        With oTpv
            .User = oUser
            .Mode = oRequestMode
            .Ds_Amount = CInt(oAmt.Eur * 100)
            .Ds_ConsumerLanguage = FEB2.TpvRedSys.GetConsumerLanguage(oLang)
            .Ds_Currency = "978" ' ISO-4217
            .Ds_MerchantCode = oConfig.MerchantCode
            .Ds_Terminal = "1"
            .Ds_TransactionType = "0" 'autorización
            .Ds_ConsumerLanguage = FEB2.TpvRedSys.GetConsumerLanguage(oLang)
            .Ds_ProductDescription = sConcepte

            Select Case oRequestMode
                Case DTOTpvRequest.Modes.Alb
                    Dim oDelivery = Await FEB2.Delivery.Find(oSourceGuid, exs)
                    .Request = oDelivery
                    .Titular = FEB2.Customer.CcxOrMe(exs, oDelivery.Customer)
                Case DTOTpvRequest.Modes.Pdc
                    Dim oPurchaseOrder = Await FEB2.PurchaseOrder.Find(oSourceGuid, exs)
                    .Request = oPurchaseOrder
                    .Titular = FEB2.Customer.CcxOrMe(exs, oPurchaseOrder.Contact)
                Case DTOTpvRequest.Modes.Impagat
                    Dim oImpagat = Await FEB2.Impagat.Find(oSourceGuid, exs)
                    .Request = oImpagat
                    .Titular = oImpagat.Csb.Contact
                Case Else
                    If .Titular Is Nothing Then
                        Dim oContacts As List(Of DTOContact) = Await FEB2.User.Contacts(exs, oUser)
                        If oContacts.Count > 0 Then
                            Dim oContact = oContacts.FirstOrDefault(Function(x) x.Nifs.Count > 0) 'prioritza raons socials amb nif
                            If oContact Is Nothing Then oContact = oContacts.First()
                            Dim oCustomer As New DTOCustomer(oContact.Guid)
                            .Titular = FEB2.Customer.CcxOrMe(exs, oCustomer)
                        End If
                    End If
            End Select
        End With

        Dim pTpv = Await FEB2.TpvLog.BookRequest(exs, oTpv)
        If exs.Count = 0 Then
            oTpv = pTpv
        End If

        Return oTpv
    End Function

    Shared Async Function ValidateSignature(exs As List(Of Exception), oEmp As DTOEmp, data As String, oLog As DTOTpvLog, oEnvironment As DTOPaymentGateway.Environments) As Task(Of Boolean)
        Dim oConfig As DTOPaymentGateway = Await FEB2.TpvRedSys.Config(exs, oEmp, oEnvironment)

        'Shared Function ValidateSignature(publicKey As String, privateKey As String, data As String, orderNum As String) As Boolean
        Dim k As Byte() = System.Convert.FromBase64String(oConfig.SignatureKey)

        'Decode base64 url to string  (Get the parameters)
        'Dim deco As String = Base64Decode_url(data)

        ' Convert JSON string to Dictionary
        Dim derivatekey = CryptoHelper.Encrypt3DES(oLog.Ds_Order, k)

        ' Calculate HMAC SHA256 with Encoded base64 JSON string using derivated key calculated previously
        Dim Res = CryptoHelper.GetHMACSHA256(data, derivatekey)

        ' Encode Byte() res to Base64 String
        Dim res2 As String = CryptoHelper.ToBase64(Res)

        ' Convert the result to be compatible with url
        Dim result As String = res2.Replace("+", "-").Replace("/", "_")
        Dim retval As Boolean = (result = oLog.Ds_Signature)
        Return retval
    End Function

    Shared Async Function DecodeMerchantParameters(oEmp As DTOEmp, merchantParameters As String, signatureReceived As String, oEnvironment As DTOPaymentGateway.Environments, exs As List(Of Exception)) As Task(Of DTOTpvLog)
        Dim retval As DTOTpvLog = Nothing
        Try

            'Decode base64 url to string  (Get the parameters)ilreis@sonaesr.com
            'Dim deco As String = Base64Decode_url(merchantParameters)
            ' Convert JSON string to Dictionary
            'Dim oJson As Dictionary(Of String, String) = JsonHelper.DictionaryFromJsonString(deco)

            Dim oJson As Dictionary(Of String, String) = CryptoHelper.FromUrlFriendlyBase64Json(merchantParameters)

            Dim Ds_Order As String = oJson("Ds_Order")
            retval = Await FEB2.TpvLog.FromOrder(Ds_Order, exs)
            If FEB2.TpvLog.Load(retval, exs) Then
                With retval
                    .Ds_Date = oJson("Ds_Date")
                    .Ds_Hour = oJson("Ds_Hour")
                    .Ds_Response = oJson("Ds_Response")
                    .Ds_MerchantData = oJson("Ds_MerchantData")
                    .Ds_SecurePayment = oJson("Ds_SecurePayment")
                    .Ds_TransactionType = oJson("Ds_TransactionType")
                    .Ds_Card_Country = oJson("Ds_Card_Country")
                    .Ds_AuthorisationCode = oJson("Ds_AuthorisationCode")
                    .Ds_Signature = signatureReceived
                    .SignatureValidated = Await FEB2.TpvRedSys.ValidateSignature(exs, oEmp, merchantParameters, retval, oEnvironment)
                    '.Ds_ConsumerLanguage = oJson("Ds_ConsumerLanguage")
                    '.Ds_Amount = oJson("Ds_Amount")
                    '.Ds_Currency = oJson("Ds_Currency")
                    '.Ds_Order = oJson("Ds_Order")
                    '.Ds_MerchantCode = oJson("Ds_MerchantCode")
                    '.Ds_Terminal = oJson("Ds_Terminal")
                    .Ds_MerchantParameters = merchantParameters
                    .Ds_SignatureReceived = signatureReceived
                End With
            End If
        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function

    Shared Function GetConsumerLanguage(oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case oLang.Id
            Case DTOLang.Ids.CAT
                retval = "003"
            Case DTOLang.Ids.ENG
                retval = "002"
            Case Else
                retval = "001"
        End Select
        Return retval
    End Function

    Shared Async Function GetLogFromEncriptedData(oEmp As DTOEmp, MerchantParameters As String, SignatureReceived As String) As Task(Of DTOTpvLog)
        Dim retval As DTOTpvLog = Nothing

        Dim exs As New List(Of Exception)

        retval = Await FEB2.TpvRedSys.LoadLog(oEmp, retval, MerchantParameters, SignatureReceived, DTOPaymentGateway.Environments.Production, exs)
        If exs.Count = 0 Then
        Else
            Dim ex1 As New Exception("Ds_MerchantParameters: " & MerchantParameters)
            Dim ex2 As New Exception("Ds_Signature: " & SignatureReceived)
            exs.Add(ex1)
            exs.Add(ex2)
            FEB2.MailMessage.MailAdminSync("Error a TpvController KO", ExceptionsHelper.ToFlatString(exs), exs)
        End If
        Return retval
    End Function
End Class
