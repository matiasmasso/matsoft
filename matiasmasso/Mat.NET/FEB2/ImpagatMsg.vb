Public Class ImpagatMsg

    Protected Friend Enum params
        unknown
        Vto = 1
        Concepte = 2
        SCtaCte = 3
        Nominal = 4
        Despeses = 5
        Total = 6
        DeadLineIncidencia = 7
        NCtaCte = 8
        NMailCtas = 9
        UrlTpv = 10
        NTelCtas = 11
    End Enum

    Shared Async Function MaiMessage(oImpagat As DTOImpagat, exs As List(Of Exception)) As Task(Of DTOMailMessage)
        Dim sTo = Await MailTo(oImpagat, exs)
        Dim retval = DTOMailMessage.Factory(sTo)
        With retval
            .Cc = Await MailCc(oImpagat, exs)
            .Subject = Subject(oImpagat)
            .Body = Await Body(oImpagat, exs)
        End With
        Return retval
    End Function

    Protected Shared Async Function MailTo(oImpagat As DTOImpagat, exs As List(Of Exception)) As Task(Of String)
        Dim oEmails = Await FEB2.Emails.All(exs, oImpagat.Csb.Contact)
        Dim sRecipients = oEmails.Select(Function(x) x.EmailAddress)
        Dim retval = String.Join(";", sRecipients.ToArray)
        Return retval
    End Function

    Protected Shared Async Function MailCc(oImpagat As DTOImpagat, exs As List(Of Exception)) As Task(Of List(Of String))
        Dim oAdr As DTOAddress = oImpagat.Csb.Contact.Address
        FEB2.Contact.Load(oImpagat.Csb.Contact, exs)
        Dim oChannel As DTODistributionChannel = DTOContact.DistributionChannel(oImpagat.Csb.Contact)
        Dim oZip As DTOZip = oAdr.Zip
        Dim oEmails = Await FEB2.Reps.Emails(exs, oZip, oChannel)
        Dim retval = oEmails.Select(Function(x) x.EmailAddress).ToList
        retval.Add("matias@matiasmasso.es")
        Return retval
    End Function

    Protected Shared Function Subject(oImpagat As DTOImpagat) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(oImpagat.Csb.Contact.Lang.Tradueix("IMPAGADO", "IMPAGAT", "UNPAYMENT"))
        sb.Append(" " & DTOAmt.CurFormatted(oImpagat.Csb.Amt))
        sb.Append(" " & oImpagat.Csb.Contact.FullNom)
        Return sb.ToString
    End Function

    Protected Shared Async Function Body(oImpagat As DTOImpagat, exs As List(Of Exception)) As Task(Of String)
        Dim retval As String = ""
        Dim oTxt = Await FEB2.Txt.Find(DTOTxt.Ids.MailImpago, exs)
        If exs.Count = 0 Then
            retval = oTxt.ToHtml(oImpagat.Csb.Contact.Lang)
            Dim iParamCount As Integer = [Enum].GetValues(GetType(params)).Length
            Dim sParams() As String
            ReDim sParams(iParamCount)

            Dim oTotal As DTOAmt = oImpagat.Csb.Amt.Clone
            oTotal.Add(oImpagat.Gastos)

            Dim sConcepte As String = oImpagat.Csb.Txt
            If oImpagat.Csb.Invoice IsNot Nothing Then
                sConcepte = "<a href='" & FEB2.DocFile.DownloadUrl(oImpagat.Csb.Invoice.DocFile, True) & "'>" & sConcepte & "</a>"
            End If

            sParams(params.Vto) = TextHelper.VbFormat(oImpagat.csb.vto, "dd/MM/yy")
            sParams(params.Concepte) = sConcepte
            sParams(params.SCtaCte) = FEB2.Iban.ToHtml(oImpagat.Csb.Iban)
            sParams(params.Nominal) = DTOAmt.CurFormatted(oImpagat.Csb.Amt)
            sParams(params.Despeses) = DTOAmt.CurFormatted(DTOImpagat.GetGastos(oImpagat))
            sParams(params.Total) = DTOAmt.CurFormatted(oTotal)
            sParams(params.DeadLineIncidencia) = TextHelper.VbFormat(oImpagat.csb.vto.AddDays(15), "dd/MM/yy")

            Dim oIban As DTOIban = oImpagat.Csb.Csa.Banc.Iban
            'Stop

            sParams(params.NCtaCte) = FEB2.Iban.ToHtml(oImpagat.Csb.Csa.Banc.Iban)
            sParams(params.NMailCtas) = "cuentas@matiasmasso.es"
            sParams(params.UrlTpv) = FEB2.Impagat.UrlTpv(oImpagat, True)
            sParams(params.NTelCtas) = "932541520"

            For i As Integer = 1 To iParamCount
                Dim sKey As String = "@" & TextHelper.VbFormat(i, "00")
                retval = retval.Replace(sKey, sParams(i))
            Next
        End If

        Return retval
    End Function

End Class
