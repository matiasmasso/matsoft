Public Class DTOTpvLog
    Inherits DTOBaseGuid

    Property Ds_Date As String
    Property Ds_Hour As String
    Property Ds_Amount As String
    Property Ds_Currency As String
    Property Ds_Order As String
    Property Ds_MerchantCode As String
    Property Ds_Terminal As String
    Property Ds_Signature As String
    Property Ds_Response As String
    Property Ds_MerchantData As String
    Property Ds_ProductDescription As String
    Property Ds_SecurePayment As String
    Property Ds_TransactionType As String
    Property Ds_Card_Country As String
    Property Ds_AuthorisationCode As String
    Property Ds_ConsumerLanguage As String
    Property Ds_Card_Type As String

    'response:
    Property Ds_MerchantParameters As String
    Property Ds_SignatureReceived As String
    Property ErrDsc As String


    Property Mode As DTOTpvRequest.Modes
    Property Request As DTOBaseGuid
    Property Result As DTOCca
    Property User As DTOUser
    Property Titular As DTOContact
    Property FchCreated As DateTime
    Property SignatureValidated As Boolean
    Property ProcessedSuccessfully As Boolean

    Property Exceptions As String


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Lang(oTpvLog As DTOTpvLog) As DTOLang
        Dim retval = DTOLang.Factory(DTOLang.Ids.ESP)
        If TextHelper.VbIsNumeric(oTpvLog.Ds_ConsumerLanguage) Then
            Select Case CInt(oTpvLog.Ds_ConsumerLanguage)
                Case 2
                    retval = DTOLang.Factory(DTOLang.Ids.ENG)
                Case 3
                    retval = DTOLang.Factory(DTOLang.Ids.CAT)
            End Select
        End If
        Return retval
    End Function

    Shared Function Amt(oTpvLog As DTOTpvLog) As DTOAmt
        Dim DcAmt As Decimal
        Dim sAmt As String = oTpvLog.Ds_Amount
        If TextHelper.VbIsNumeric(sAmt) Then
            DcAmt = CDec(sAmt) / 100
        End If
        Dim retVal As DTOAmt = DTOAmt.Factory(DcAmt)
        Return retVal
    End Function

    Shared Function CreditCtaCod(oTitular As DTOContact) As DTOPgcPlan.Ctas
        Dim retval = DTOPgcPlan.Ctas.TransferenciesDesconegudes
        If oTitular IsNot Nothing Then
            retval = DTOPgcPlan.Ctas.Clients_Anticips
        End If
        Return retval
    End Function

    Shared Function DebitCtaCod() As DTOPgcPlan.Ctas
        Return DTOPgcPlan.Ctas.VisasCobradas
    End Function

    Shared Function isValidResponse(oLog As DTOTpvLog, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If oLog.Ds_Response = "0000" Then
            retval = True
        Else
            exs.Add(New Exception(String.Format("Codi de error Redsys {0}: {1}", oLog.Ds_Response, oLog.ErrDsc)))
        End If

        If Not oLog.SignatureValidated Then
            exs.Add(New Exception("Les signatures no coincideixen"))
            retval = False
        End If

        If Not TextHelper.VbIsNumeric(oLog.Ds_AuthorisationCode) Then
            exs.Add(New Exception("Operació no autoritzada"))
            retval = False
        End If
        Return retval
    End Function

    Shared Function MailMessage(oTpvLog As DTOTpvLog) As DTOMailMessage
        Dim sRecipient = DTOMailMessage.wellknownAddress(DTOMailMessage.wellknownRecipients.Info)
        Dim sSubject = String.Format("Avís de Tpv: {0}", oTpvLog.Result.concept)
        Dim retval = DTOMailMessage.Factory(sRecipient, sSubject)
        Return retval
    End Function
End Class
