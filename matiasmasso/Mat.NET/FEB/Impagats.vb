Public Class Impagat
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOImpagat)
        Return Await Api.Fetch(Of DTOImpagat)(exs, "Impagat", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oImpagat As DTOImpagat, exs As List(Of Exception)) As Boolean
        If Not oImpagat.IsLoaded And Not oImpagat.IsNew Then
            Dim pImpagat = Api.FetchSync(Of DTOImpagat)(exs, "Impagat", oImpagat.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOImpagat)(pImpagat, oImpagat, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oImpagat As DTOImpagat, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOImpagat)(oImpagat, exs, "Impagat")
        oImpagat.IsNew = False
    End Function

    Shared Async Function Delete(oImpagat As DTOImpagat, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOImpagat)(oImpagat, exs, "Impagat")
    End Function

    Shared Async Function CalculaGastos(oEmp As DTOEmp, oImpagat As DTOImpagat, exs As List(Of Exception)) As Task(Of DTOAmt)
        Dim retval As DTOAmt = Nothing
        Dim oMinimGastos As DTOAmt = Await [Default].EmpAmt(oEmp, DTODefault.Codis.despesesImpagoMinim, exs)
        If exs.Count = 0 Then
            Dim DcGastosPercentage As Decimal = Await [Default].EmpDecimal(oEmp, DTODefault.Codis.despesesImpago, exs)
            retval = oImpagat.Nominal.Percent(DcGastosPercentage)
            If oMinimGastos.IsGreaterThan(retval) Then
                retval = oMinimGastos
            End If
        End If
        Return retval
    End Function

    Shared Function UrlTpv(value As DTOImpagat, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If value IsNot Nothing Then
            Dim oParameters As New Dictionary(Of String, String)
            oParameters.Add("Guid", value.Guid.ToString())
            oParameters.Add("Mode", DTOTpvRequest.Modes.Impagat)
            Dim sParameter As String = CryptoHelper.UrlFriendlyBase64Json(oParameters)
            retval = UrlHelper.Factory(AbsoluteUrl, "tpv", sParameter)
        End If
        Return retval
    End Function

    Shared Async Function WarnReps(exs As List(Of Exception), oUser As DTOUser, oImpagat As DTOImpagat) As Task(Of Boolean)
        Dim retval As Boolean
        Dim sTo As String = "matias@matiasmasso.es"
        Dim oLliurat As DTOContact = oImpagat.Csb.Contact
        If Contact.Load(oLliurat, exs) Then
            Dim oAdr As DTOAddress = oLliurat.Address
            Dim oChannel = DTOContact.DistributionChannel(oLliurat)
            Dim oEmails = Await Reps.Emails(exs, oAdr.Zip, oChannel)
            If oEmails.Count > 0 Then
                Dim recipients = oEmails.Select(Function(x) x.EmailAddress).ToList
                Dim sSubject As String = String.Format("Impagado {0} de {1} {2}", DTOAmt.CurFormatted(oImpagat.Nominal), oImpagat.Csb.Contact.Nom, oImpagat.Csb.Txt)
                Dim sBody As String = "Mensaje automatizado de comunicado de impagos. Para más información llamar a oficinas"
                Dim oMailMessage = DTOMailMessage.Factory(recipients, sSubject, sBody)
                With oMailMessage
                    .Cc = recipients
                End With
                retval = Await MailMessage.Send(exs, oUser, oMailMessage)
            End If
        End If
        Return retval
    End Function

    Shared Function CobraPerVisa(oEmp As DTOEmp, oLog As DTOTpvLog, exs As List(Of Exception)) As DTOTpvLog
        Dim oImpagat As DTOImpagat = oLog.Request
        If Impagat.Load(oImpagat, exs) Then
            Dim oCobrat As DTOAmt = DTOTpvLog.Amt(oLog)
            Dim oNominal As DTOAmt = oImpagat.Nominal
            Dim oDespeses As DTOAmt = DTOTpvLog.Amt(oLog)

            oDespeses.Substract(oNominal)
            oImpagat.PagatACompte = oCobrat
            oImpagat.status = DTOImpagat.StatusCodes.saldat

            If oDespeses.Eur <> oImpagat.Gastos.Eur Then
                exs.Add(New Exception("Error al cobrar impagat amb Visa, no quadren les despeses" & vbCrLf & "cobrat: " & DTOAmt.CurFormatted(oCobrat) & vbCrLf & " nominal: " & DTOAmt.CurFormatted(oNominal) & vbCrLf & " despeses: " & DTOAmt.CurFormatted(oDespeses)))
            End If

            If oDespeses.IsNotZero Then
                'esmena l'assentament
                Dim oCcbCredit As DTOCcb = oLog.Result.Items.Find(Function(x) x.Dh = DTOCcb.DhEnum.Haber)
                oCcbCredit.Amt = oNominal
                oCcbCredit.Cta = PgcCta.FromCodSync(DTOPgcPlan.Ctas.impagats, oEmp, exs)

                Dim oCtaDespeses = PgcCta.FromCodSync(DTOPgcPlan.Ctas.ImpagosRecuperacioDespeses, oEmp, exs)
                oLog.Result.AddSaldo(oCtaDespeses)
            End If

            oLog = Impagat.CobraPerVisa(oEmp, oLog, exs)
        End If

        Return oLog
    End Function

End Class


Public Class Impagats
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOImpagat))
        Return Await Api.Fetch(Of List(Of DTOImpagat))(exs, "Impagats/fromUser", oUser.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oRep As DTORep) As Task(Of List(Of DTOImpagat))
        Return Await Api.Fetch(Of List(Of DTOImpagat))(exs, "Impagats/fromRep", oRep.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, oContact As DTOContact) As Task(Of List(Of DTOImpagat))
        Return Await Api.Fetch(Of List(Of DTOImpagat))(exs, "Impagats/fromContact", oEmp.Id, oContact.Guid.ToString())
    End Function

    Shared Async Function Kpis(exs As List(Of Exception), oEmp As DTOEmp, fromYear As Integer) As Task(Of List(Of DTOKpi))
        Return Await Api.Fetch(Of List(Of DTOKpi))(exs, "Impagats/kpis", oEmp.Id, fromYear)
    End Function

    Shared Async Function Update(exs As List(Of Exception), oImpagats As List(Of DTOImpagat), oBanc As DTOBanc, DtFch As Date, oNominal As DTOAmt, oCca As DTOCca, oUser As DTOUser) As Task(Of DTOCca)
        Dim retval As DTOCca = Nothing
        Dim oCtas = Await PgcCtas.All(exs)
        If exs.Count = 0 Then
            Dim oCtaImpagats = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Impagats)
            Dim oCtaBanc = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Bancs)

            oCca = DTOCca.Factory(DtFch, oUser, DTOCca.CcdEnum.Impagat)
            If oImpagats.Count = 1 Then
                oCca.Concept = oBanc.Abr & "-Impagat " & oImpagats.First.Csb.Contact.Nom
            Else
                oCca.Concept = oBanc.Abr & "-Impagats diversos"
            End If

            For Each oImpagat As DTOImpagat In oImpagats
                oCca.AddDebit(oImpagat.Csb.Amt, oCtaImpagats, oImpagat.Csb.Contact, oImpagat.Csb.Pnd)

                oImpagat.Status = DTOImpagat.StatusCodes.enNegociacio

                Dim DcGtsPercent As Decimal = Await [Default].EmpDecimal(oUser.Emp, DTODefault.Codis.despesesImpago, exs)
                Dim oGtsMin As DTOAmt = Await [Default].EmpAmt(oUser.Emp, DTODefault.Codis.despesesImpagoMinim, exs)
                Dim oGtsAmt As DTOAmt = oImpagat.Nominal.Percent(DcGtsPercent)
                If oGtsMin.IsGreaterThan(oGtsAmt) Then
                    oGtsAmt = oGtsMin
                End If

                oImpagat.Gastos = oGtsAmt
            Next

            oCca.AddSaldo(oCtaBanc, oBanc)

            Dim value As New DTOImpagats
            With value
                .Impagats = oImpagats
                .Cca = oCca
            End With
            retval = Await Api.Execute(Of DTOImpagats, DTOCca)(value, exs, "Impagats")
        End If

        Return retval
    End Function

End Class