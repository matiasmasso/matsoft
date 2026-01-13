
Imports System.Net
Imports FEB
Imports Microsoft.Office.Interop
Imports Newtonsoft.Json.Linq

Public Class Menu_Invoice
    Inherits Menu_Base

    Private _Invoices As List(Of DTOInvoice)
    Private _ShowProgress As ProgressBarHandler
    Public Event RequestToToggleProgressBar(sender As Object, e As MatEventArgs)


    Public Sub New(oInvoices As List(Of DTOInvoice), Optional ShowProgress As ProgressBarHandler = Nothing)
        MyBase.New
        _Invoices = oInvoices
        _ShowProgress = ShowProgress
    End Sub

    Public Shadows Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Del(),
        MenuItem_eFra(),
        MenuItem_eMailAll(),
        MenuItem_Pdf(),
                MenuItem_SavePdfs(),
        MenuItem_CopyLink(),
        MenuItem_Edi(),
        MenuItem_Advance()
        })
        'MenuItem_eMail(), _
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        oMenuItem.Enabled = _Invoices.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Del
        Return oMenuItem
    End Function


    Private Function MenuItem_eFra() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "enviar e-factura"

        oMenuItem.Image = My.Resources.MailSobreGroc
        AddHandler oMenuItem.Click, AddressOf Do_eFra
        Return oMenuItem
    End Function

    Private Function MenuItem_eMail() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "e-mail"
        oMenuItem.Image = My.Resources.printer
        oMenuItem.Enabled = _Invoices.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_eMail
        Return oMenuItem
    End Function

    Private Function MenuItem_eMailAll() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "e-mail a autoritzats"
        oMenuItem.Image = My.Resources.printer
        oMenuItem.Enabled = _Invoices.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_eMailAll
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pdf"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_Pdf
        Return oMenuItem
    End Function

    Private Function MenuItem_SavePdfs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Desar els pdfs"
        oMenuItem.Image = My.Resources.save_16
        AddHandler oMenuItem.Click, AddressOf Do_SavePdfs
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        oMenuItem.Enabled = _Invoices.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_Edi() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Edi"
        oMenuItem.DropDownItems.Add("Enviar", Nothing, AddressOf Do_EdiQueue)
        oMenuItem.DropDownItems.Add("Desar missatge Ediversa", My.Resources.disk, AddressOf Do_EdiversaSaveFile)
        oMenuItem.DropDownItems.Add("Desar missatge Edi", My.Resources.disk, AddressOf Do_EdiSaveFile)
        oMenuItem.DropDownItems.Add("Retrocedir a pendent d'enviar", Nothing, AddressOf Do_ClearEdi)
        Return oMenuItem
    End Function

    Private Function MenuItem_Advance() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "avançades"
        oMenuItem.DropDownItems.Add(SubMenuItem_NoPrint)
        oMenuItem.DropDownItems.Add(SubMenuItem_PdfRegenerate)
        'oMenuItem.DropDownItems.Add(SubMenuItem_PndRegenerate)
        oMenuItem.DropDownItems.Add(SubMenuItem_ECIEDIReassign)
        oMenuItem.DropDownItems.Add(SubMenuItem_SendToSii)
        oMenuItem.DropDownItems.Add(SubMenuItem_SiiLog)
        oMenuItem.DropDownItems.Add(SubMenuItem_CopyCuid)
        Return oMenuItem
    End Function

    Private Function SubMenuItem_NoPrint() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        'oMenuItem.Enabled = Not mCsb.VtoCca.Exists
        AddHandler oMenuItem.Click, AddressOf Do_NoPrint
        oMenuItem.Text = "ocultar a la web"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_PdfRegenerate() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        'oMenuItem.Enabled = Not mCsb.VtoCca.Exists
        AddHandler oMenuItem.Click, AddressOf Do_PdfRegenerate
        oMenuItem.Text = "regenera pdf"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_ECIEDIReassign() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        'oMenuItem.Enabled = Not mCsb.VtoCca.Exists
        AddHandler oMenuItem.Click, AddressOf ECIEDIReassign
        oMenuItem.Text = "ECI: reassigna comanda Edi"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_PndRegenerate() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        'oMenuItem.Enabled = Not mCsb.VtoCca.Exists
        AddHandler oMenuItem.Click, AddressOf Do_Pndregenerate
        oMenuItem.Text = "reassigna fitxer EDI"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_SendToSii() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        'oMenuItem.Enabled = Not mCsb.VtoCca.Exists
        AddHandler oMenuItem.Click, AddressOf Do_SendToSii
        oMenuItem.Text = "enviar a Hisenda (Sii)"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_SiiLog() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        'oMenuItem.Enabled = Not mCsb.VtoCca.Exists
        AddHandler oMenuItem.Click, AddressOf Do_SiiLog
        oMenuItem.Text = "consulta Hisenda (Sii)"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_CopyCuid() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_CopyGuid
        oMenuItem.Text = "copiar Guid"
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Invoice(_Invoices.First)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Show()
        End With
    End Sub

    Private Async Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & DTOInvoice.Caption(_Invoices) & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            For Each item As DTOInvoice In _Invoices
                Dim exs As New List(Of Exception)
                If Await FEB.Invoice.Delete(item, exs) Then
                    MyBase.RefreshRequest(Me, MatEventArgs.Empty)
                Else
                    exs.Add(New Exception("error al eliminar la factura " & item.Num))
                    UIHelper.WarnError(exs)
                End If
            Next
        End If
    End Sub


    Private Async Sub Do_eFra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If Await FEB.Invoices.SendToEmail(_Invoices, Current.Session.User, DTO.GlobalVariables.Now(), Nothing, exs) Then
            MsgBox(String.Format("{0} factures correctament enviades per email", _Invoices.Count))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Pdf(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        For Each oInvoice As DTOInvoice In _Invoices
            If FEB.Invoice.Load(oInvoice, exs) Then
                FEB.Customer.Load(oInvoice.Customer, exs)
            End If
        Next

        Dim oCert = Await FEB.Cert.Find(GlobalVariables.Emp.Org, exs)
        Dim sGuid As String = _Invoices.First.Guid.ToString
        If _Invoices.Count = 1 Then
            Dim oinvoice = _Invoices.First
            If oinvoice.DocFile Is Nothing Then
                MsgBox("Aquesta factura no està redactada encara", MsgBoxStyle.Exclamation)
            Else
                Dim oDocFile = Await FEB.DocFile.FindWithStream(oinvoice.DocFile.Hash, exs)
                If exs.Count = 0 Then
                    If Not Await UIHelper.ShowStreamAsync(exs, oDocFile) Then
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        Else
            Dim oPdf As New LegacyHelper.PdfAlb(_Invoices)
            Dim oByteArray() As Byte = oPdf.Stream(exs, oCert)
            If exs.Count = 0 Then
                Dim oDocFile = LegacyHelper.DocfileHelper.Factory(exs, oByteArray)
                If exs.Count = 0 Then
                    If Not Await UIHelper.ShowStreamAsync(exs, oDocFile) Then
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub Do_ClearEdi()
        Dim exs As New List(Of Exception)
        If Await FEB.Invoices.ClearPrintLog(exs, _Invoices) Then
            RefreshRequest(Me, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
    '

    Private Async Sub Do_EdiversaSaveFile()
        Dim exs As New List(Of DTOEdiversaException)
        Dim ex2 As New List(Of Exception)
        Dim oInvoice As DTOInvoice = _Invoices.First
        If FEB.Invoice.Load(oInvoice, ex2) Then
            Dim src As String = Await FEB.EdiversaInvoice.EdiMessage(Current.Session.Emp, oInvoice, exs)
            If exs.Count = 0 Then
                Dim sFilename As String = DTOInvoice.ediFileName(oInvoice)
                UIHelper.SaveTextFileDialog(src, sFilename)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_EdiSaveFile()
        Dim exs As New List(Of Exception)
        Dim oInvoice As DTOInvoice = _Invoices.First
        If FEB.Invoice.Load(oInvoice, exs) Then
            Dim src As String = oInvoice.EdiMessage(GlobalVariables.Emp)
            Dim sFilename As String = DTOInvoice.ediFileName(oInvoice)
            UIHelper.SaveTextFileDialog(src, sFilename)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_EdiQueue()
        Dim oSortedInvoices As List(Of DTOInvoice) = _Invoices.OrderBy(Function(x) x.Num).ToList

        Dim oEdiFiles As New List(Of DTOEdiversaFile)
        Dim exs As New List(Of DTOEdiversaException)
        For Each oInvoice As DTOInvoice In oSortedInvoices
            Dim oEdiFile As DTOEdiversaFile = Await FEB.EdiversaInvoice.EdiFile(Current.Session.Emp, oInvoice, exs)
            oEdiFiles.Add(oEdiFile)
        Next

        If exs.Count = 0 Then
            Dim ex2 As New List(Of Exception)
            Dim DtFch As Date = DTO.GlobalVariables.Now()
            For Each oEdifile As DTOEdiversaFile In oEdiFiles
                Dim oLog As New DTOInvoicePrintLog
                With oLog
                    .EdiversaFile = oEdifile
                    .Invoice = oEdifile.Source
                    .WinUser = Current.Session.User
                    .Fch = DtFch
                End With
                Dim oInvoice As DTOInvoice = oEdifile.Source
                If Not Await FEB.EdiversaFile.QueueInvoice(ex2, oLog) Then
                    For Each ex In ex2
                        Dim oEx = DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, oInvoice, "error al enrutar factura " & oInvoice.Num & ". " & ex.Message)
                        exs.Add(oEx)
                    Next
                End If
            Next
        End If

        If exs.Count = 0 Then
            RefreshRequest(Me, New MatEventArgs(oSortedInvoices))
        Else
            Dim oFrm As New Frm_EDiversaExceptions(exs)
            oFrm.Show()
        End If

    End Sub

    Private Async Sub Do_NoPrint(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)

        If Await FEB.Invoices.SetNoPrint(exs, _Invoices, Current.Session.User) Then
            MsgBox("aquestes factures ja no son accessibles des de internet", MsgBoxStyle.Information)
        Else
            UIHelper.WarnError(exs)
        End If

        RefreshRequest(Me, MatEventArgs.Empty)
    End Sub

    Private Async Sub Do_PdfRegenerate(ByVal sender As Object, ByVal e As System.EventArgs)
        MyBase.ToggleProgressBarRequest(True)

        Dim exs As New List(Of Exception)
        For Each oInvoice As DTOInvoice In _Invoices
            If FEB.Invoice.Load(oInvoice, exs) Then
                'If FEB.Customer.Load(oInvoice.Customer, exs) Then
                Dim oPdf As New LegacyHelper.PdfAlb(oInvoice)
                Dim oCert = Await FEB.Cert.Find(GlobalVariables.Emp.Org, exs)
                Dim oByteArray = oPdf.Stream(exs, oCert)
                Dim oDocFile = LegacyHelper.DocfileHelper.Factory(exs, oByteArray, MimeCods.Pdf)
                If exs.Count = 0 Then
                    Dim oCca As DTOCca = oInvoice.Cca
                    oCca.DocFile = oDocFile
                    If Not Await FEB.Cca.UpdateOnlyDocfile(exs, oCca) Then
                        exs.Add(New Exception("error al regenerar pdf de la factura " & DTOInvoice.FullConcept(oInvoice)))
                    End If
                End If
            Else
                exs.Add(New Exception("error al regenerar pdf de la factura " & DTOInvoice.FullConcept(oInvoice)))
            End If

        Next

        MyBase.ToggleProgressBarRequest(False)

        If exs.Count = 0 Then
            RefreshRequest(Me, MatEventArgs.Empty)
            MsgBox("pdf de factures regenerats", MsgBoxStyle.Information, "MAT.NET")
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ECIEDIReassign()
        Dim exs As New List(Of Exception)
        Dim oInvoice = Await FEB.Invoice.Find(_Invoices.First.Guid, exs)
        If exs.Count = 0 Then
            Dim oDeliveries = oInvoice.Deliveries
            If oDeliveries.Count = 1 Then
                Dim oOrders = oDeliveries.First.getPurchaseOrders
                Select Case oOrders.Count
                    Case 0
                        UIHelper.WarnError("Factura sense comandes, no es pot resoldre automaticament")
                    Case 1
                        Dim docnum As String = DTOEci.NumeroDeComanda(oOrders.First)
                        Dim oEdiversaOrderHeaders = Await FEB.EdiversaOrders.searchByDocNum(exs, docnum)
                        If exs.Count = 0 Then
                            Select Case oEdiversaOrderHeaders.Count
                                Case 0
                                    UIHelper.WarnError("No s'ha trobat cap fitxer Edi amb el numero de comanda '" & docnum & "', no es pot resoldre automaticament")
                                Case 1
                                    Dim order = oEdiversaOrderHeaders.First
                                    Dim oPdc = oOrders.First
                                    FEB.PurchaseOrder.Load(oPdc, exs)
                                    Dim rc = MsgBox("Assignem el fitxer " & order.DocNum & " del " & order.FchDoc.ToShortDateString & " a la nostre comanda " & oOrders.First.formattedId & "?")
                                    If rc = MsgBoxResult.Ok Then
                                        If Await FEB.EdiversaOrder.SetResult(exs, order, oPdc) Then
                                            MsgBox("fitxer Edi reassignat a la comanda ")
                                        Else
                                            UIHelper.WarnError(exs)
                                        End If
                                    Else
                                        MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Information)
                                    End If
                                    'Stop
                                Case Else
                                    UIHelper.WarnError("S'han trobat " & oEdiversaOrderHeaders.Count & " fitxers Edi amb el numero de comanda '" & docnum & "', no es pot resoldre automaticament")
                            End Select
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Case Else
                        UIHelper.WarnError("Factura mes de una comanda, no es pot resoldre automaticament")
                End Select
            Else
                UIHelper.WarnError("Factura sense albarans o amb mes de un albarà, no es pot resoldre automaticament")
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Pndregenerate(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oCta = Await FEB.PgcCta.FromCod(DTOPgcPlan.Ctas.Clients, Current.Session.Emp, exs)
        If exs.Count = 0 Then
            For Each oInvoice As DTOInvoice In _Invoices
                If FEB.Invoice.Load(oInvoice, exs) Then
                    If FEB.Customer.Load(oInvoice.Customer, exs) Then
                        Dim oPnd = DTOPnd.Factory(Current.Session.Emp)
                        With oPnd
                            .Invoice = oInvoice
                            .FraNum = oInvoice.Num
                            .Yef = oInvoice.Fch.Year
                            .Amt = DTOInvoice.getTotal(oInvoice)
                            .Contact = oInvoice.Customer
                            .Cfp = oInvoice.Customer.PaymentTerms.Cod
                            .Cta = oCta
                            .Fch = oInvoice.Fch
                            .Vto = oInvoice.Vto
                            .Cca = oInvoice.Cca
                            .Cod = DTOPnd.Codis.Deutor
                        End With

                        If Not Await FEB.Pnd.Update(oPnd, exs) Then
                            UIHelper.WarnError(exs, "eror al desar la partida pendent de la fra." & oInvoice.Num)
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            Next

            RefreshRequest(Me, New MatEventArgs(_Invoices))

            MsgBox("partides pendents regenerades", MsgBoxStyle.Information, "MAT.NET")
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub Do_SendToSii()
        Dim exs As New List(Of Exception)
        Dim oX509Cert = Await FEB.Cert.X509Certificate2(GlobalVariables.Emp.Org, exs)
        If exs.Count = 0 Then

            If Await FEB.Invoices.SendToSii(exs, Current.Session.Emp, DTO.Defaults.Entornos.produccion, _Invoices, oX509Cert, _ShowProgress) Then
                Dim iOk As Integer = _Invoices.Where(Function(x) x.SiiLog.Result = DTOSiiLog.Results.Correcto).Count
                Dim sb As New Text.StringBuilder
                If _Invoices.Count = iOk Then
                    If iOk = 1 Then
                        sb.AppendLine(String.Format("factura {0} enviada correctament", _Invoices.First.Num))
                    Else
                        sb.AppendLine(String.Format("{0} factures enviades correctament", iOk))
                    End If
                Else
                    sb.AppendLine(String.Format("enviades correctament {0} de {1} factures", iOk, _Invoices.Count))
                End If
                Dim oCsvInvoice = _Invoices.FirstOrDefault(Function(x) x.SiiLog.Csv.isNotEmpty())
                If oCsvInvoice IsNot Nothing Then
                    sb.AppendLine("csv: " & oCsvInvoice.SiiLog.Csv)
                    sb.AppendLine("data: " & Format(oCsvInvoice.SiiLog.Fch, "dd/MM/yy HH:mm"))
                End If
                MsgBox(sb.ToString())
            Else
                UIHelper.WarnError(exs)
            End If
            RefreshRequest(Me, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub Do_CopyGuid()
        UIHelper.CopyToClipboard(_Invoices.First.Guid.ToString)
    End Sub

    Private Async Sub Do_SiiLog()
        Dim exs As New List(Of Exception)
        If FEB.Invoice.Load(_Invoices.First, exs) Then
            If exs.Count = 0 Then
                Dim oX509Cert = Await FEB.Cert.X509Certificate2(GlobalVariables.Emp.Org, exs)
                If exs.Count = 0 Then
                    Dim oLog As DTOSiiLog = AeatSii.FacturasEmitidas.QuerySiiLog(DTO.Defaults.Entornos.produccion, oX509Cert, _Invoices.First)
                    If oLog Is Nothing Then
                        MsgBox("sense dades")
                    Else
                        If Await FEB.Invoice.LogSii(exs, _Invoices.First) Then
                            MsgBox("factura actualitzada correctament" & vbCrLf & "resultat: " & oLog.Result.ToString & vbCrLf & "csv: " & oLog.Csv & vbCrLf & "data: " & Format(oLog.Fch, "dd/MM/yy HH:mm") & vbCrLf & "errors: " & oLog.ErrMsg)
                        Else
                            UIHelper.WarnError(exs, "No s'ha pogut actualitzar la factura" & vbCrLf & "resultat: " & oLog.Result.ToString & vbCrLf & "csv: " & oLog.Csv & vbCrLf & "data: " & Format(oLog.Fch, "dd/MM/yy HH:mm") & vbCrLf & "errors: " & oLog.ErrMsg)
                        End If
                    End If
                Else
                    UIHelper.WarnError(exs, "Error al llegir el certificat digital")
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
        'Stop
    End Sub

    Private Async Sub Do_Html(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If _Invoices.Count = 1 Then
            If FEB.Invoice.Load(_Invoices.First, exs) Then
                If Not Await UIHelper.ShowStreamAsync(exs, _Invoices.First.DocFile) Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            MsgBox("cal triar nomes una factura", MsgBoxStyle.Exclamation)
        End If
    End Sub


    Private Async Sub Do_eMail(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim sFileName As String = DTOInvoice.Filename(_Invoices, "pdf")

        Dim oOlApp As New Outlook.Application
        Dim oNewMail As Outlook.MailItem = oOlApp.CreateItem(Outlook.OlItemType.olMailItem)
        If DTOInvoice.sameCustomer(_Invoices) Then
            Dim sRecipients = Await FEB.Subscriptors.Recipients(exs, GlobalVariables.Emp, DTOSubscription.Wellknowns.Facturacio, _Invoices.First.Customer)
            oNewMail.To = sRecipients.First
        End If
        With oNewMail
            .Subject = sFileName
            .Attachments.Add(sFileName)
            .Display()
        End With

    End Sub

    Private Async Sub Do_eMailAll(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If Await FEB.Invoices.SendToEmail(_Invoices, Current.Session.User, DTO.GlobalVariables.Now(), Nothing, exs) Then
            MsgBox("factures enviades correctament", MsgBoxStyle.Information, "MAT.NET")
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_SavePdfs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oDlg As New FolderBrowserDialog
        With oDlg
            .ShowNewFolderButton = True
            If .ShowDialog = DialogResult.OK Then
                RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(True))
                Dim path As String = String.Format("{0}\justificants assentaments", .SelectedPath)
                System.IO.Directory.CreateDirectory(path)
                For Each oInvoice As DTOInvoice In _Invoices
                    Dim url = FEB.DocFile.DownloadUrl(oInvoice.DocFile, True)
                    Dim fileName = String.Format("{0}\factura {1:0000}.{2:00000}.pdf", path, oInvoice.Fch.Year, oInvoice.NumeroYSerie)
                    Using client As New WebClient()
                        client.DownloadFile(url, fileName)
                    End Using
                Next
                RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
            End If
        End With

    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oInvoice As DTOInvoice = _Invoices.First
        Dim exs As New List(Of Exception)
        If FEB.Invoice.Load(oInvoice, exs) Then
            Dim sUrl As String = FEB.DocFile.DownloadUrl(oInvoice.DocFile, True)
            Clipboard.SetDataObject(sUrl, True)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

End Class
