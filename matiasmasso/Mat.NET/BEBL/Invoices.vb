Public Class Invoice

    Shared Function Find(oGuid As Guid) As DTOInvoice
        Dim retval As DTOInvoice = InvoiceLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Pdf(oInvoice As DTOInvoice) As Byte()
        Return InvoiceLoader.Pdf(oInvoice)
    End Function

    Shared Function FromNum(oEmp As DTOEmp, iYea As Integer, serieyNum As String) As DTOInvoice
        Dim oSerie = DTOInvoice.SerieFromSerieyNum(serieyNum)
        Dim iNum = DTOInvoice.NumFromSerieyNum(serieyNum)
        Return InvoiceLoader.FromNum(oEmp, iYea, oSerie, iNum)
    End Function

    Shared Function Load(oInvoice As DTOInvoice) As Boolean
        Return InvoiceLoader.Load(oInvoice)
    End Function


    Shared Function Update(exs As List(Of Exception), oInvoice As DTOInvoice) As Boolean
        Return InvoiceLoader.Update(oInvoice, exs)
    End Function

    Shared Function Delete(oInvoice As DTOInvoice, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = InvoiceLoader.Delete(oInvoice, exs)
        Return retval
    End Function


    Shared Function LogSii(oInvoice As DTOInvoice, exs As List(Of Exception)) As Boolean
        Return InvoiceLoader.LogSii(oInvoice, exs)
    End Function


    Shared Function Url(oInvoice As DTOInvoice, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If oInvoice IsNot Nothing Then
            retval = UrlHelper.Factory(AbsoluteUrl, "factura", oInvoice.Guid.ToString())
        End If
        Return retval
    End Function

    Shared Function LogPrint(exs As List(Of Exception), oInvoice As DTOInvoice, oPrintMode As DTOInvoice.PrintModes, oWinUser As DTOUser, oDestUser As DTOUser, DtFch As Date) As Boolean
        Return InvoiceLoader.LogPrint(oInvoice, oPrintMode, oWinUser, oDestUser, DtFch, exs)
    End Function


    Shared Function MailMessage(ByVal oInvoice As DTOInvoice, oToSubscriptors As List(Of DTOSubscriptor), oCcSubscriptors As List(Of DTOSubscriptor)) As DTOMailMessage
        BEBL.Invoice.Load(oInvoice)
        Dim oLang As DTOLang = oInvoice.Lang

        Dim retval = DTOMailMessage.Factory(DTOSubscriptor.Recipients(oToSubscriptors))
        With retval
            .Cc = DTOSubscriptor.Recipients(oCcSubscriptors)
            .Subject = oLang.Tradueix("Factura electrónica", "Factura electrónica", "e-invoice") & " " & oInvoice.Num.ToString
            .BodyUrl = UrlHelper.Factory(True, "mail/factura", oInvoice.Guid.ToString())
        End With
        Return retval
    End Function

    Shared Function ediOrderFiles(oInvoice As DTOInvoice) As List(Of DTOEdiversaFile)
        Return InvoiceLoader.ediOrderFiles(oInvoice)
    End Function

End Class

Public Class Invoices

    Shared Function All(oExercici As DTOExercici, iMes As Integer) As List(Of DTOInvoice) 'per declaració Iva
        Dim retval As List(Of DTOInvoice) = InvoicesLoader.All(oExercici, iMes)
        Return retval
    End Function

    Shared Function All(oCustomer As DTOCustomer) As List(Of DTOInvoice)
        Return InvoicesLoader.All(oCustomer)
    End Function

    Shared Function Compact(oUser As DTOUser) As List(Of DTOInvoice.Compact)
        Return InvoicesLoader.Compact(oUser, Nothing)
    End Function

    Shared Function Compact(oCustomer As DTOCustomer) As List(Of DTOInvoice.Compact)
        Return InvoicesLoader.Compact(Nothing, oCustomer)
    End Function

    Shared Function Headers(oEmp As DTOEmp, oYearMonth As DTOYearMonth) As List(Of DTOInvoice)
        Return InvoicesLoader.Headers(oEmp, oYearMonth)
    End Function

    Shared Function Printed(oCustomer As DTOCustomer) As List(Of DTOInvoice)
        Dim oAllInvoices As List(Of DTOInvoice) = InvoicesLoader.All(oCustomer)
        Return oAllInvoices.Where(Function(x) x.printMode <> DTOInvoice.PrintModes.NoPrint And x.printMode <> DTOInvoice.PrintModes.Pending).ToList
    End Function

    Shared Function PrintPending(oEmp As DTOEmp, Optional DtFch As Date = Nothing) As List(Of DTOInvoice)
        Return InvoicesLoader.PrintPending(oEmp, DtFch)
    End Function

    Shared Function PrintedCollection(oEmp As DTOEmp) As List(Of DTOInvoice)
        Return InvoicesLoader.PrintedCollection(oEmp)
    End Function

    Shared Function SetNoPrint(exs As List(Of Exception), oInvoices As List(Of DTOInvoice), oUser As DTOUser) As Boolean
        Return InvoicesLoader.SetNoPrint(oInvoices, oUser, exs)
    End Function

    Shared Function Last(oExercici As DTOExercici, oSerie As DTOInvoice.Series) As DTOInvoice
        Dim retval As DTOInvoice = InvoicesLoader.Last(oExercici, oSerie)
        Return retval
    End Function

    Shared Function LastEachSerie(oExercici As DTOExercici) As List(Of DTOInvoice)
        Dim retval = InvoicesLoader.LastEachSerie(oExercici)
        Return retval
    End Function

    '

    Shared Function AvailableNums(oExercici As DTOExercici, oSerie As DTOInvoice.Series) As List(Of Integer)
        Dim retval As List(Of Integer) = InvoiceLoader.AvailableNums(oExercici.Emp, oExercici.Year, oSerie)
        Return retval
    End Function

    Shared Function IntrastatPending(oEmp As DTOEmp, oYearMonth As DTOYearMonth) As List(Of DTOInvoice)
        Dim retval As List(Of DTOInvoice) = InvoicesLoader.IntrastatPending(oEmp, oYearMonth)
        Return retval
    End Function

    Shared Function SiiPending(oEmp As DTOEmp) As List(Of DTOInvoice)
        Dim retval As List(Of DTOInvoice) = InvoicesLoader.SiiPending(oEmp)
        Return retval
    End Function

    Shared Function SendToSii(entorno As DTO.Defaults.Entornos, oOrg As DTOContact, oInvoices As List(Of DTOInvoice), exs As List(Of Exception), Optional ShowProgress As ProgressBarHandler = Nothing) As Boolean
        Dim BlCancelRequest As Boolean
        Dim sLabel As String = ""
        Dim oInvoice As DTOInvoice = Nothing
        Dim iStep As Integer = 1
        Try

            For Each oInvoice In oInvoices
                InvoiceLoader.Load(oInvoice)
                sLabel = String.Format("Pas " & iStep & " de 3 passos: carregant fra.{0} del {1:dd/MM/yyy} a {2}", oInvoice.num, oInvoice.fch, oInvoice.customer.nom)
                If ShowProgress IsNot Nothing Then
                    ShowProgress(0, oInvoices.Count - 1, oInvoices.IndexOf(oInvoice), sLabel, BlCancelRequest)
                    If BlCancelRequest Then
                        Dim idx As Integer = oInvoices.IndexOf(oInvoice)
                        oInvoices = oInvoices.Take(idx + 1)
                        Exit For
                    End If
                End If
            Next

            iStep = 2
            If ShowProgress IsNot Nothing Then
                sLabel = String.Format("Pas " & iStep & " de 3 passos: enviant {0} factures a Hisenda", oInvoices.Count)
                ShowProgress(0, oInvoices.Count - 1, 0, sLabel, BlCancelRequest)
            End If

            Dim oCertificate = BEBL.Cert.X509Certificate2(oOrg)
            AeatSii.FacturasEmitidas.Send(entorno, oInvoices, oCertificate, exs)

            iStep = 3
            For Each oInvoice In oInvoices
                InvoiceLoader.LogSii(oInvoice, exs)
                If ShowProgress IsNot Nothing Then
                    sLabel = String.Format("Pas " & iStep & " de 3 passos: desant fra.{0} del {1:dd/MM/yyy} a {2}", oInvoice.num, oInvoice.fch, oInvoice.customer.nom)
                    ShowProgress(0, oInvoices.Count - 1, oInvoices.IndexOf(oInvoice), sLabel, BlCancelRequest)
                End If
            Next

        Catch ex As Exception
            Dim sInvoiceNum As String = ""
            If oInvoice IsNot Nothing Then
                sInvoiceNum = oInvoice.num
            Else
                sInvoiceNum = "(s/n)"
            End If
            Dim sMsg As String = String.Format("Step {0} factura {1} error: {2}", iStep, sInvoiceNum, ex.Message)
            Try
                If EventLog.SourceExists("MatSched") Then
                    EventLog.WriteEntry("MatSched", sMsg, EventLogEntryType.Warning)
                End If
            Catch ex2 As Security.SecurityException

            End Try
            exs.Add(New Exception(sMsg))
        Finally
            If exs.Count = 0 Then
                ' retval.Succeed("{0} factures emeses enviades correctament fins a {1:dd/MM/yy}", oInvoices.Count, oInvoices.Max(Function(x) x.Fch))
            Else
                'retval.DoneWithErrors("{0} factures emeses enviades amb errors fins a {1:dd/MM/yy}", oInvoices.Count, oInvoices.Max(Function(x) x.Fch))
                'retval.AddExceptions(exs)
            End If

        End Try
        Return exs.Count = 0
    End Function

    Shared Function ClearPrintLog(exs As List(Of Exception), oInvoices As List(Of DTOInvoice)) As Boolean
        Return InvoicesLoader.ClearPrintLog(oInvoices, exs)
    End Function

    Shared Function Summary(oEmp As DTOEmp) As List(Of DTOYearMonth)
        Return InvoicesLoader.Summary(oEmp)
    End Function

    Shared Function LlibreDeFactures(oExercici As DTOExercici, Optional ToFch As Date = Nothing) As List(Of DTOInvoice)
        Return InvoicesLoader.LlibreDeFactures(oExercici, ToFch)
    End Function

End Class
