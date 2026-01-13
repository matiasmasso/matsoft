Public Class TpvLog
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOTpvLog)
        Return Await Api.Fetch(Of DTOTpvLog)(exs, "TpvLog", oGuid.ToString())
    End Function

    Shared Async Function FromOrder(sOrderNum As String, exs As List(Of Exception)) As Task(Of DTOTpvLog)
        Return Await Api.Fetch(Of DTOTpvLog)(exs, "TpvLog/fromOrder", sOrderNum)
    End Function

    Shared Function Load(ByRef oTpvLog As DTOTpvLog, exs As List(Of Exception)) As Boolean
        If Not oTpvLog.IsLoaded And Not oTpvLog.IsNew Then
            Dim pTpvLog = Api.FetchSync(Of DTOTpvLog)(exs, "TpvLog", oTpvLog.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOTpvLog)(pTpvLog, oTpvLog, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oTpvLog As DTOTpvLog, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOTpvLog)(oTpvLog, exs, "TpvLog")
        oTpvLog.IsNew = False
    End Function

    Shared Async Function Delete(oTpvLog As DTOTpvLog, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOTpvLog)(oTpvLog, exs, "TpvLog")
    End Function

    Shared Async Function BookRequest(exs As List(Of Exception), oTpvLog As DTOTpvLog) As Task(Of DTOTpvLog)
        Return Await Api.Execute(Of DTOTpvLog, DTOTpvLog)(oTpvLog, exs, "TpvLog/BookRequest")
    End Function

    Shared Function CustomRequestUrl(oLog As DTOTpvLog) As String
        Dim oDictionary As New Dictionary(Of String, String)
        oDictionary.Add("Mode", CInt(oLog.Mode))
        oDictionary.Add("Guid", oLog.Guid.ToString())
        Dim sBase64Json As String = CryptoHelper.UrlFriendlyBase64Json(oDictionary)

        Dim retval = UrlHelper.Factory(True, "tpv", sBase64Json)
        Return retval
    End Function

    Shared Async Function CcaFactory(oEmp As DTOEmp, oLog As DTOTpvLog, exs As List(Of Exception)) As Task(Of DTOCca)
        Dim retval As DTOCca = Nothing
        Try
            Dim oCtas = Await FEB2.PgcCtas.All(exs)
            Dim oTitular As DTOContact = Await FEB2.TpvLog.Titular(oLog, exs)
            Dim oCtaCredit = oCtas.FirstOrDefault(Function(x) x.Codi = DTOTpvLog.CreditCtaCod(oTitular))
            Dim oCtaDebit = oCtas.FirstOrDefault(Function(x) x.Codi = DTOTpvLog.DebitCtaCod())
            Dim oDeutor As DTOBanc = FEB2.TpvLog.Banc(oEmp, exs)
            Dim sConcept As String = FEB2.TpvLog.CcaConcepte(exs, oDeutor, oTitular, oLog)
            Dim oAmt As DTOAmt = DTOTpvLog.Amt(oLog)
            Dim iCdn As Integer = oLog.Ds_Order.Substring(5)

            oLog.User.Emp = oEmp ' per evitar que peti bllccafactory

            retval = DTOCca.Factory(DateTime.Today, oLog.User, DTOCca.CcdEnum.VisaCobros, iCdn)
            With retval
                .Ref = oLog
                .Concept = sConcept
                retval.AddDebit(oAmt, oCtaDebit, oDeutor)
                retval.AddSaldo(oCtaCredit, oTitular)
            End With

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function


    Shared Function MailMessage(oTpvLog As DTOTpvLog) As DTOMailMessage
        'MailHelper.SendMail(GlobalVariables.Emp, wellknownRecipients.Info, "avis de Tpv: " & oTpvLog.Result.Concept & vbCrLf & sb.ToString())
        Dim sRecipient = DTOMailMessage.wellknownAddress(DTOMailMessage.wellknownRecipients.Info)
        Dim sSubject = String.Format("Avís de Tpv: {0}", oTpvLog.Result.Concept)
        Dim retval = DTOMailMessage.Factory(sRecipient, sSubject)
        Return retval
    End Function

    Shared Async Function Titular(oLog As DTOTpvLog, exs As List(Of Exception)) As Task(Of DTOContact)
        Dim retval As DTOContact = oLog.Titular
        If retval Is Nothing Then
            Select Case oLog.Mode
                Case DTOTpvRequest.Modes.Free
                    Dim oDeutors As List(Of DTOCustomer) = Await FEB2.User.GetCustomers(oLog.User, exs)
                    Dim oRaonsSocials = oDeutors.Where(Function(x) x.Ccx Is Nothing).ToList
                    If oRaonsSocials.Count > 0 Then retval = oRaonsSocials.First
                Case DTOTpvRequest.Modes.Pdc
                    Dim oPurchaseOrder As DTOPurchaseOrder = oLog.Request
                    retval = FEB2.Customer.CcxOrMe(exs, oPurchaseOrder.Contact)
                Case DTOTpvRequest.Modes.Alb
                    Dim oDelivery As DTODelivery = oLog.Request
                    FEB2.Delivery.Load(oDelivery, exs)
                    retval = FEB2.Customer.CcxOrMe(exs, oDelivery.Customer)
                Case DTOTpvRequest.Modes.Impagat
                    Dim oImpagat As DTOImpagat = oLog.Request
                    retval = oImpagat.Csb.Contact
            End Select
        End If
        Return retval
    End Function

    Shared Async Function TitularNom(oLog As DTOTpvLog, exs As List(Of Exception)) As Task(Of String)
        Dim oContact As DTOContact = Await Titular(oLog, exs)
        Dim retval As String = TitularNom(oContact, exs)
        Return retval
    End Function

    Shared Function TitularNom(oTitular As DTOContact, exs As List(Of Exception)) As String
        Dim retval As String = "anonim"
        If oTitular IsNot Nothing Then
            FEB2.Contact.Load(oTitular, exs)
            retval = oTitular.FullNom
        End If
        Return retval
    End Function

    Shared Function Banc(oEmp As DTOEmp, exs As List(Of Exception)) As DTOBanc
        Dim retval As DTOBanc = Nothing
        Dim oGuid As Guid = FEB2.Default.EmpGuidSync(oEmp, DTODefault.Codis.BancTpv, exs)
        If oGuid.Equals(Guid.Empty) Then
            exs.Add(New Exception("Falta configurar el banc del Tpv"))
        Else
            retval = FEB2.Banc.FindSync(oGuid, exs)
        End If
        Return retval
    End Function

    Shared Function CcaConcepte(exs As List(Of Exception), oBanc As DTOBanc, oTitular As DTOContact, oLog As DTOTpvLog) As String
        Dim sBanc As String = oBanc.Abr
        Dim sOrder As String = oLog.Ds_Order
        Dim sPurpose As String = Purpose(exs, oLog)
        Dim sTitular As String = TitularNom(oTitular, exs)
        Dim retval As String = String.Format("Tpv {0} {1} - {2} {3}", sBanc, sOrder, sPurpose, sTitular)
        If retval.Length > 60 Then retval = retval.Substring(0, 60)
        Return retval
    End Function

    Shared Function Purpose(exs As List(Of Exception), oLog As DTOTpvLog) As String
        Dim retval As String = oLog.Ds_ProductDescription
        If retval = "" Then
            Select Case oLog.Mode
                Case DTOTpvRequest.Modes.Alb
                    Dim oDelivery As DTODelivery = oLog.Request
                    FEB2.Delivery.Load(oDelivery, exs)
                    retval = "cobro albará " & oDelivery.Id
                Case DTOTpvRequest.Modes.Pdc
                    Dim oPurchaseOrder As DTOPurchaseOrder = oLog.Request
                    FEB2.PurchaseOrder.Load(oPurchaseOrder, exs)
                    retval = "cobro comanda " & oPurchaseOrder.Num & " "
                Case DTOTpvRequest.Modes.Impagat
                    retval = "cobro impagat "
                Case Else
                    retval = "cobro s/especificar "
            End Select
        End If
        Return retval
    End Function

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
End Class


Public Class TpvLogs
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOTpvLog))
        Return Await Api.Fetch(Of List(Of DTOTpvLog))(exs, "TpvLogs")
    End Function

End Class
