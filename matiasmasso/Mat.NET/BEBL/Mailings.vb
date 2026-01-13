Public Class Mailing

    Shared Sub Load(oEmp As DTOEmp, ByRef oMailing As DTOMailing)
        MailingLoader.Load(oMailing, oEmp)
    End Sub

    Shared Function XarxaDistribuidors(DtFch As Date) As List(Of DTOLeadChecked)
        Dim retval As New List(Of DTOLeadChecked)
        retval.AddRange(MailingLoader.XarxaDistribuidorsTancada)
        retval.AddRange(MailingLoader.XarxaDistribuidorsOberta(DtFch))
        Return retval
    End Function

    Shared Function Reps(oChannels As List(Of DTODistributionChannel), oBrands As List(Of DTOProductBrand)) As List(Of DTOLeadChecked)
        Dim retval As List(Of DTOLeadChecked)
        If oChannels.Count = 0 Then
            retval = New List(Of DTOLeadChecked)
        Else
            retval = MailingLoader.Reps(oChannels, oBrands)
        End If
        Return retval
    End Function

    Shared Function BodyUrl(oTemplate As DTODefault.MailingTemplates, ParamArray UrlSegments() As String) As String
        Dim oSegments As New List(Of String)
        oSegments.Add("mail")
        oSegments.Add(oTemplate.ToString())
        oSegments.AddRange(UrlSegments)
        Dim retval As String = UrlHelper.FromSegments(True, oSegments.ToArray)
        Return retval
    End Function
End Class
Public Class Mailings

    Shared Function Log(oGuid As Guid, oUsers As IEnumerable(Of DTOUser), exs As List(Of Exception)) As Boolean
        Return MailingLoader.Log(oGuid, oUsers, exs)
    End Function

    Shared Function SendStocks(oEmp As DTOEmp, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If oEmp.MailBoxSmtp = "" Then oEmp = BEBL.Emp.Find(oEmp.Id)
        Dim oSsc = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.Stocks)
        Dim oSubscriptors As List(Of DTOSubscriptor) = SubscriptorsLoader.All(oSsc)

        If oSubscriptors.Count = 0 Then
            retval = True
        Else
            Dim failCount As Integer
            For Each oSubscriptor In oSubscriptors
                If oSubscriptor.Contacts.Count = 0 And oSubscriptor.Contact Is Nothing Then
                    exs.Add(New Exception("no s'ha trobat cap client associat a " & oSubscriptor.EmailAddress))
                    failCount += 1
                Else
                    Dim oMailMsg = MailMsg.Factory(oEmp)
                    With oMailMsg
                        .Add(MailMsg.Recipients.To, oSubscriptor)
                        .Subject = "M+O Stocks"
                        .DownloadBody(oSubscriptor.Lang, DTOTxt.Ids.MailStocks)
                        .AttachExcel(BEBL.Mgz.ExcelStocks(oEmp, oSubscriptor, oEmp.Mgz))
                        If .Send(exs) Then
                            retval = True
                        End If
                    End With
                End If
            Next

            If oSubscriptors.Count = failCount Then
                exs.Add(New Exception(String.Format("No s'ha pogut enviar els stocks a cap dels {0} subscriptors", oSubscriptors.Count)))
            Else
                exs.Add(New Exception(String.Format("Stocks enviats a {0} de {1} subscriptors", oSubscriptors.Count - failCount, oSubscriptors.Count)))
            End If
        End If

        Return retval
    End Function

    Shared Async Function Descatalogats(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of Boolean)
        Dim oTask = DTOTask.Wellknown(DTOTask.Cods.EmailDescatalogats)
        Dim fromFch As Date = TaskLoader.LastSuccessfulLog(oTask)

        'esbrina si hi han productes descatalogats des de llavors
        Dim iCount As Integer = BEBL.ProductSkus.ObsoletsCount(oEmp, fromFch)

        If iCount > 0 Then
            'treu la llista de suscrits
            Dim oSubscription = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.AvisDescatalogats)
            Dim oSubscriptors = BEBL.Subscriptors.All(oSubscription)

            For Each oSubscriptor In oSubscriptors
                Dim oSheet = BEBL.ProductSkus.Obsolets(oSubscriptor, oSubscriptor.lang, fromFch)
                If oSheet.rows.Count > 1 Then
                    Dim sSubject = oSubscriptor.lang.Tradueix("Ultimos productos descatalogados", "Darrers productes descatalogats", "Last products outdated")
                    Dim oMailMessage = DTOMailMessage.Factory(oSubscriptor.EmailAddress, sSubject)
                    'oMailMessage = DTOMailMessage.Factory("matias@matiasmasso.es", sSubject)
                    oMailMessage.bodyUrl = MmoUrl.Factory(True, "mail/descatalogados", oSubscriptor.Guid.ToString)
                    MailMessageHelper.AttachExcel(oMailMessage, oSheet, exs)
                    Await MailMessageHelper.Send(oEmp, oMailMessage, exs)
                End If
            Next
        End If
        Return exs.Count = 0
    End Function

    Shared Async Function BankTransferReminder(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of Boolean)
        'sustituir per la ultima data satisfactopria de la tasca
        Dim vto As Date = DTO.GlobalVariables.Today().AddDays(1)
        Dim oCustomers = BEBL.Pnds.BankTransferReminderDeutors(oEmp, vto)
        Dim oSubscription = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.BankTransferReminder)

        For Each oCustomer In oCustomers
            Dim oSubscriptors = BEBL.Subscriptors.All(oSubscription, oCustomer)
            If oSubscriptors.Count > 0 Then
                Dim sRecipients = oSubscriptors.Select(Function(x) x.emailAddress).ToList
                Dim sSubject = oCustomer.lang.Tradueix("Recordatorio de transferencia", "Recordatori de transferencia", "Bank transfer reminder", "Lembrete de transferência bancária")
                Dim oMailMessage = DTOMailMessage.Factory(sRecipients, sSubject)
                oMailMessage.Bcc = {"cuentas@matiasmasso.es", "matias@matiasmasso.es"}.ToList()
                oMailMessage.bodyUrl = MmoUrl.Factory(True, "mail/BankTransferReminder", oCustomer.Guid.ToString)
                Await MailMessageHelper.Send(oEmp, oMailMessage, exs)
            End If
        Next
        Return exs.Count = 0
    End Function


End Class
