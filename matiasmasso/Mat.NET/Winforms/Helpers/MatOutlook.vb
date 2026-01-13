Module MatOutlook

    Public Async Function RemittanceAdvice(oCca As DTOCca, exs As List(Of Exception)) As Task(Of Boolean)
        Dim oRemittanceAdvice = Await FEB2.RemittanceAdvice.FromCca(exs, oCca)
        Dim oProveidor As DTOProveidor = oRemittanceAdvice.Proveidor
        If FEB2.Contact.Load(oProveidor, exs) Then
            Dim sRecipients = Await FEB2.Subscriptors.Recipients(exs, GlobalVariables.Emp, DTOSubscription.Wellknowns.Comptabilitat, oProveidor)
            Dim oMailMessage = DTOMailMessage.Factory(sRecipients)
            With oMailMessage
                .Bcc = {"matias@matiasmasso.es"}.ToList
                .Subject = oProveidor.Lang.Tradueix("Aviso de transferencia", "Avis de transferència", "Bank transfer notification")
                .BodyUrl = FEB2.UrlHelper.Factory(True, "mail/remittanceAdvice", oCca.Guid.ToString())
            End With

            If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
        Return exs.Count = 0
    End Function

    Public Async Function RemittanceAdvice(oBeneficiari As DTOBancTransferBeneficiari, exs As List(Of Exception)) As Task(Of Boolean)
        Dim sRecipients = Await FEB2.Subscriptors.Recipients(exs, GlobalVariables.Emp, DTOSubscription.Wellknowns.Comptabilitat, oBeneficiari.Contact)
        Dim oMailMessage = DTOMailMessage.Factory(sRecipients)
        With oMailMessage
            .Bcc = {"matias@matiasmasso.es"}.ToList
            .Subject = oBeneficiari.Contact.Lang.Tradueix("Aviso de transferencia", "Avis de transferència", "Bank transfer notification")
            .BodyUrl = FEB2.UrlHelper.Factory(True, "mail/remittanceAdvice2", oBeneficiari.Guid.ToString())
        End With

        If Not Await OutlookHelper.Send(oMailMessage, exs) Then
            UIHelper.WarnError(exs)
        End If
        Return exs.Count = 0
    End Function
End Module
