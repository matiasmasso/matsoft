Public Class Csb 'BEBL
    Shared Function Find(oGuid As Guid) As DTOCsb
        Dim retval As DTOCsb = CsbLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oCsb As DTOCsb) As Boolean
        Dim retval As Boolean = CsbLoader.Load(oCsb)
        Return retval
    End Function

    Shared Function SaveVto(ByRef oCsb As DTOCsb, oUser As DTOUser, oCtas As List(Of DTOPgcCta), exs As List(Of Exception)) As Boolean
        Dim oCta430 As DTOPgcCta = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Clients)
        Dim oCta572 As DTOPgcCta = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.bancs)
        Dim oCta5208 As DTOPgcCta = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.BancsEfectesDescomptats)

        'BEBL.Csb.Load(oCsb)

        Dim oCca As DTOCca = DTOCca.Factory(oCsb.Vto, oUser, DTOCca.CcdEnum.Venciment, oCsb.FormattedId())
        oCca.Concept = "efte." & oCsb.ReadableFormat() & " vençut, " & oCsb.Txt
        oCca.Ref = oCsb

        oCca.AddCredit(oCsb.Amt, oCta430, oCsb.Contact, oCsb.Pnd)
        If oCsb.Csa.Descomptat Then
            oCca.AddSaldo(oCta5208, oCsb.Csa.Banc)
        Else
            oCca.AddSaldo(oCta572, oCsb.Csa.Banc)
        End If

        oCsb.Result = DTOCsb.Results.Vençut
        oCsb.ResultCca = oCca

        Dim retval As Boolean = CsbLoader.SaveVto(oCsb, exs)
        Return retval
    End Function

    Shared Function RevertVto(ByRef oCca As DTOCca, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CsbLoader.RevertVto(oCca, exs)
        Return retval
    End Function

    Shared Function Reclama(exs As List(Of Exception), oUser As DTOUser, oCsb As DTOCsb) As DTOCca
        Dim oBanc As DTOBanc = oCsb.Csa.Banc

        Dim oCtas = BEBL.PgcCtas.Current
        Dim oCtaBanc = oCtas.First(Function(x) x.Codi = DTOPgcPlan.Ctas.bancs)
        Dim oCtaEfts = oCtas.First(Function(x) x.Codi = DTOPgcPlan.Ctas.BancsEfectesDescomptats)

        Dim retval = DTOCca.Factory(DTO.GlobalVariables.Today(), oUser, DTOCca.CcdEnum.ReclamacioEfecte, oCsb.FormattedId())
        retval.Concept = oBanc.Abr & "-reclamació efecte " & oCsb.FormattedId() & " de " & oCsb.Contact.Nom
        retval.AddDebit(oCsb.Amt, oCtaEfts, oBanc, oCsb.Pnd)
        retval.AddSaldo(oCtaBanc, oBanc)

        oCsb.Result = DTOCsb.Results.Reclamat

        Dim oPnd As DTOPnd = oCsb.Pnd
        'FEBL.Pnd.Load(oPnd,exs)
        If BEBL.Pnd.Load(oPnd) Then
            oPnd.Status = DTOPnd.StatusCod.pendent
            oPnd.Csb = Nothing

            CsbLoader.Reclama(oUser, oCsb, retval, oPnd, exs)
        End If
        Return retval
    End Function

    Shared Function RetrocedeixReclamacio(oUser As DTOUser, oCsb As DTOCsb, exs As List(Of Exception)) As Boolean
        If BEBL.Pnd.Load(oCsb.Pnd) Then
            CsbLoader.RetrocedeixReclamacio(oUser, oCsb, exs)
        End If
        Return exs.Count = 0
    End Function


    Shared Function NotifyVto(oEmp As DTOEmp, oCsb As DTOCsb, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oSsc As DTOSubscription = SubscriptionLoader.Find(DTOSubscription.Wellknowns.Facturacio)
        Dim oSubscriptors As List(Of DTOSubscriptor) = SubscriptorsLoader.All(oSsc, oCsb.Contact)
        If oSubscriptors.Count = 0 Then
            Dim oUser As DTOUser = BEBL.Contact.DefaultUser(New DTOContact(oCsb.Contact.Guid))
            If oUser IsNot Nothing Then
                oSubscriptors.Add(oUser)
            End If
        End If

        If oSubscriptors.Count = 0 Then
            exs.Add(New Exception(String.Format("efecte " & oCsb.ReadableFormat() & " de " & oCsb.Contact.FullNom & " sense adreça email")))
        Else
            Dim oBccSsc = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.CopiaAvisVenciment)
            Dim oBccSubscriptors = SubscriptorsLoader.All(oBccSsc)

            Dim oLang = oCsb.Contact.Lang

            Dim oMsg = MailMsg.Factory(oEmp)
            With oMsg
                .AddRange(MailMsg.Recipients.To, oSubscriptors)
                .AddRange(MailMsg.Recipients.Bcc, oBccSubscriptors)
                .Subject = "M+O: " & String.Format(oLang.Tradueix("Aviso de vencimiento {0} {1}", "Avis de venciment {0} {1}", "Due date reminder {0} {1}"), oCsb.vto.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")), DTOAmt.CurFormatted(oCsb.amt))
                If .DownloadBody(exs, DTODefault.MailingTemplates.MailVenciment, oCsb.Guid.ToString()) Then
                    If .Send(exs) Then
                        retval = BEBL.Mailings.Log(oCsb.Guid, oSubscriptors, exs)
                    End If
                End If
            End With

        End If
        Return retval
    End Function

    Shared Function SEPATipoAdeudo(oCsb As DTOCsb) As DTOSepaDirectDebit.TiposAdeudo
        Dim iCount As Integer = CsbsLoader.CountPerMandato(oCsb.Iban)

        Dim retval As DTOSepaDirectDebit.TiposAdeudo = DTOSepaDirectDebit.TiposAdeudo.RCUR
        If iCount = 0 Then retval = DTOSepaDirectDebit.TiposAdeudo.FRST

        Return retval
    End Function

End Class

Public Class Csbs

    Shared Function All(oEmp As DTOEmp,
                        Optional banc As DTOBanc = Nothing,
                        Optional year As Integer = 0,
                        Optional customer As DTOContact = Nothing) As List(Of DTOCsb)
        Dim retval As List(Of DTOCsb) = CsbsLoader.All(oEmp, banc, year, customer)
        Return retval
    End Function

    Shared Function CsbResults(oEmp As DTOEmp) As List(Of DTOCsbResult)
        Return CsbsLoader.CsbResults(oEmp)
    End Function


    Shared Function All(oIban As DTOIban) As List(Of DTOCsb)
        Dim retval As List(Of DTOCsb) = CsbsLoader.All(oIban)
        Return retval
    End Function

    Shared Function mailingLogs(oEmp As DTOEmp, year As Integer) As List(Of DTOCsb)
        Return CsbsLoader.mailingLogs(oEmp, year)
    End Function



    Shared Function PendentsDeGirar(oEmp As DTOEmp, exs As List(Of Exception), Optional oCountry As DTOCountry = Nothing, Optional Sepa As Boolean = True) As List(Of DTOCsb)
        Dim retval As List(Of DTOCsb) = CsbsLoader.PendentsDeGirar(oEmp, exs, oCountry, Sepa)
        Return retval
    End Function

    Shared Function PendentsDeVto(oEmp As DTOEmp, DtFch As Date) As List(Of DTOCsb)
        Dim retval As List(Of DTOCsb) = CsbsLoader.PendentsDeVto(oEmp, DtFch)
        Return retval
    End Function


    Shared Function SaveVtos(oUser As DTOUser, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim iSuccessCount As Integer
        Dim oCtas As List(Of DTOPgcCta) = BEBL.PgcCtas.Current

        Dim oCsbs As List(Of DTOCsb) = CsbsLoader.PendentsDeVencer(oUser.Emp)
        For Each oCsb As DTOCsb In oCsbs
            If BEBL.Csb.SaveVto(oCsb, oUser, oCtas, exs) Then iSuccessCount += 1
        Next

        If iSuccessCount = 0 Then
            If exs.Count = 0 Then
                retval = True
            Else
                exs.Add(New Exception(String.Format("No s'ha pogut registrar cap dels {0} venciments d'avui", oCsbs.Count.ToString())))
            End If
        Else
            If exs.Count = 0 Then
                retval = True
            Else
                exs.Add(New Exception(String.Format("Registrats {0} venciments de {1} efectes", iSuccessCount, oCsbs.Count)))
            End If
        End If
        Return retval
    End Function


    Shared Function NotifyVtos(exs As List(Of Exception), oEmp As DTOEmp, Optional oTask As DTOTask = Nothing) As Boolean
        Dim retval As Boolean
        Dim sb As New System.Text.StringBuilder
        Dim iNotifyVtosDays As Integer = DefaultLoader.EmpInteger(DTODefault.Codis.NotifyVtosDays, oEmp)
        Dim DtVto As Date = DTO.GlobalVariables.Today().AddDays(iNotifyVtosDays)
        Dim oCsbs As List(Of DTOCsb) = CsbsLoader.NextVtosToNotify(oEmp, DtVto)
        Dim iSuccess As Integer
        Dim iFailure As Integer
        If oCsbs.Count = 0 Then
            sb.AppendLine("No s'han trobat venciments del " & DtVto.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")) & " per notificar al lliurat")
        Else
            For Each oCsb As DTOCsb In oCsbs
                If BEBL.Csb.NotifyVto(oEmp, oCsb, exs) Then
                    sb.AppendLine(oCsb.ReadableFormat() & " avisat")
                    iSuccess += 1
                Else
                    exs.Add(New Exception(String.Format("error al avisar {0}", oCsb.ReadableFormat())))
                    iFailure += 1
                End If
            Next
        End If

        If oTask IsNot Nothing Then
            Dim sCount As String = ""
            Select Case oCsbs.Count
                Case 0
                    sCount = String.Format("Cap venciment fins el {0}", DtVto.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")))
                Case 1
                    sCount = String.Format("Un venciment fins el {0}", DtVto.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")))
                Case Else
                    sCount = String.Format("{0} venciments fins el {1:dd/MM/yy}", oCsbs.Count, DtVto)
            End Select
            If iSuccess = 0 And iFailure = 0 Then
                oTask.lastLog.resultCod = DTOTask.ResultCods.empty
                oTask.lastLog.resultMsg = sCount
            ElseIf iSuccess = 0 Then
                oTask.lastLog.resultCod = DTOTask.ResultCods.failed
                oTask.lastLog.resultMsg = String.Format("{0}. Error al enviar les notificacions", sCount)
            ElseIf iFailure = 0 Then
                oTask.lastLog.resultCod = DTOTask.ResultCods.success
                oTask.lastLog.resultMsg = String.Format("{0}. Tots notificats correctament", sCount)
            Else
                oTask.lastLog.resultCod = DTOTask.ResultCods.doneWithErrors
                oTask.lastLog.resultMsg = String.Format("{0}. {1} correctes, {2} amb errors", sCount, iSuccess, iFailure)
            End If
            exs.Clear()
        End If
        retval = exs.Count = 0
        Return retval
    End Function

End Class
