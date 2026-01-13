Imports DTO

Public Class DTOTpvRequest
    Property Mode As Modes
    Property Ref As Guid
    Property Concept As String
    Property Eur As Decimal
    Property Amt As DTOAmt.Compact

    Public Enum Modes
        NotSet
        Free
        Alb
        Pdc
        Impagat
    End Enum

    Public Sub New()
        MyBase.New()
        _Amt = DTOAmt.Compact.Factory()
    End Sub

    Public Sub New(oMode As DTOTpvRequest.Modes)
        MyBase.New()
        _Mode = oMode
        _Amt = DTOAmt.Compact.Factory()
    End Sub

    Shared Function FromFreeConcept(oLang As DTOLang, sConcept As String, DcEur As Decimal, oUserGuid As Guid) As DTOTpvRequest
        Dim retval As New DTOTpvRequest(DTOTpvRequest.Modes.Free)
        With retval
            '.Lang = oLang
            .Ref = oUserGuid
            .Concept = sConcept
            .Amt = DTOAmt.Compact.Factory(DcEur)
        End With
        Return retval
    End Function

    Shared Function FromPdc(oPurchaseOrder As DTOPurchaseOrder, oLang As DTOLang) As DTOTpvRequest
        Dim retval As New DTOTpvRequest(DTOTpvRequest.Modes.Pdc)
        With retval
            '.Lang = oLang
            .Ref = oPurchaseOrder.Guid
            .Concept = oPurchaseOrder.caption(oLang)
            '.OurConcepte = "comanda " & oPurchaseOrder.Num.ToString & " de " & oPurchaseOrder.Contact.FullNom
            .Amt = DTOPurchaseOrder.totalIvaInclos(oPurchaseOrder).ToCompact()
        End With
        Return retval
    End Function

    Shared Function FromAlb(oDelivery As DTODelivery, oLang As DTOLang) As DTOTpvRequest
        Dim retval As New DTOTpvRequest(DTOTpvRequest.Modes.Alb)
        With retval
            '.Lang = oLang
            .Ref = oDelivery.Guid
            .Concept = DTODelivery.caption(oDelivery, oLang)
            '.OurConcepte = "alb." & oDelivery.Id.ToString & " de " & oDelivery.Customer.FullNom
            .Amt = oDelivery.Import.ToCompact()
        End With
        Return retval
    End Function

    Shared Function FromImpagat(oImpagat As DTOImpagat, oLang As DTOLang) As DTOTpvRequest
        Dim retval As DTOTpvRequest = Nothing
        If oImpagat IsNot Nothing Then
            Dim oContact As DTOContact = oImpagat.csb.Contact

            retval = New DTOTpvRequest(DTOTpvRequest.Modes.Impagat)
            With retval
                '.Lang = oLang
                .Ref = oImpagat.Guid
                .Concept = String.Format("{0} {1}", oLang.Tradueix("reposición impagado", "reposició impagat", "unpayment settlement"), oImpagat.csb.Txt)
                .Amt = DTOImpagat.PendentAmbGastos(oImpagat).ToCompact()
            End With
        End If

        Return retval
    End Function

    Shared Function EncodeMerchantData(oUser As DTOUser, oRequestMode As DTOTpvRequest.Modes, oGuid As Guid) As String
        Dim oUserGuid As Guid = Guid.Empty
        If oUser IsNot Nothing Then oUserGuid = oUser.Guid

        Dim oJson As New MatJSonObject
        oJson.AddValuePair("User", oUserGuid.ToString())
        oJson.AddValuePair("Mode", CInt(oRequestMode))
        oJson.AddValuePair("Guid", oGuid.ToString())
        Dim retval As String = oJson.ToBase64
        Return retval
    End Function

    Public Shared Function CreateMerchantSignature(oConfig As DTOSermepaConfig, sMerchantParameters As String, sMerchantOrder As String) As String
        'Decode key to byte[]
        Dim SignatureKeyBytes As Byte() = System.Convert.FromBase64String(oConfig.SignatureKey)

        'Calculate derivated key by encrypting with 3DES the "DS_MERCHANT_ORDER" with decoded key 
        Dim DerivatedKeyBytes As Byte() = CryptoHelper.Encrypt3DES(sMerchantOrder, SignatureKeyBytes)

        'Calculate HMAC SHA256 with Encoded base64 JSON string using derivated key calculated previously
        Dim resultBytes As Byte() = CryptoHelper.GetHMACSHA256(sMerchantParameters, DerivatedKeyBytes)

        Dim retval As String = System.Convert.ToBase64String(resultBytes)
        Return retval
    End Function

    Shared Function CreateMerchantParameters(oConfig As DTOSermepaConfig, oTpvLog As DTOTpvLog) As String
        Dim oJson As New MatJSonObject
        With oJson
            .AddValuePair("Ds_Merchant_Amount", oTpvLog.Ds_Amount)
            .AddValuePair("Ds_Merchant_Order", oTpvLog.Ds_Order)
            .AddValuePair("Ds_Merchant_MerchantCode", oTpvLog.Ds_MerchantCode)
            .AddValuePair("Ds_Merchant_Currency", oTpvLog.Ds_Currency)
            .AddValuePair("Ds_Merchant_Terminal", oTpvLog.Ds_Terminal)
            .AddValuePair("Ds_Merchant_TransactionType", oTpvLog.Ds_TransactionType)
            .AddValuePair("Ds_Merchant_MerchantUrl", oConfig.MerchantURL) 'url de notificacio operacions. Hauria de ser https://www.matiasmasso.es/tpv/log
            .AddValuePair("Ds_Merchant_UrlOK", oConfig.UrlOK)
            .AddValuePair("Ds_Merchant_UrlKO", oConfig.UrlKO)
            .AddValuePair("Ds_Merchant_ProductDescription", oTpvLog.Ds_ProductDescription)
            .AddValuePair("Ds_Merchant_ConsumerLanguage", oTpvLog.Ds_ConsumerLanguage)
            .AddValuePair("Ds_Merchant_MerchantData", DTOTpvRequest.EncodeMerchantData(oTpvLog.User, oTpvLog.Mode, oTpvLog.Guid)) 'Callback a retornar via log
        End With

        Dim sJsonRequest As String = oJson.ToString
        Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(sJsonRequest)
        Dim retval As String = Convert.ToBase64String(bytes)

        'tmp for testing ----------------------------------------------
        'Dim sCodedMerchantData As String = retval
        'Dim oBytes As Byte() = Convert.FromBase64String(sCodedMerchantData)
        'Dim sDecodedMerchantData As String = System.Text.Encoding.UTF8.GetString(oBytes)

        Return retval
    End Function

End Class
