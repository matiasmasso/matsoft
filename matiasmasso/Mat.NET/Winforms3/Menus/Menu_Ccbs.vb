Imports FEB
Imports System.Net

Public Class Menu_Ccbs
    Inherits Menu_Base

    Private _Values As List(Of DTOCcb)
    Private _IsAllowedToBrowse As Boolean

    Public Event RequestToToggleProgressBar(sender As Object, e As MatEventArgs)

    Public Sub New(ByVal values As List(Of DTOCcb))
        MyBase.New()
        _Values = values

        If _Values.Count > 0 Then
            Dim oFirstCca As DTOCca = _Values.First.Cca
            _IsAllowedToBrowse = FEB.Cca.IsAllowedToBrowse(oFirstCca, Current.Session.User)
        End If
    End Sub


    Public Shadows Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(),
        MenuItem_Web(),
        MenuItem_CopyLink(),
        MenuItem_Pdf(),
        MenuItem_PdfAdd(),
        MenuItem_Clon(),
        MenuItem_Del(),
        MenuItem_BookFra(),
        MenuItem_RemittanceAdvice(),
        MenuItem_SavePdfs(),
        MenuItem_Specific()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        If _Values.Count = 1 Then
            oMenuItem.Enabled = _IsAllowedToBrowse
        Else
            oMenuItem.Visible = False
        End If
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Web() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Web"
        oMenuItem.Image = My.Resources.iExplorer
        If _Values.Count > 1 Then
            oMenuItem.Visible = False
        Else
            'oMenuItem.Enabled = mCcas(0).PdfStream IsNot Nothing
            If Not _IsAllowedToBrowse Then oMenuItem.Enabled = False
        End If
        AddHandler oMenuItem.Click, AddressOf Do_Web
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        If _Values.Count > 1 Then
            oMenuItem.Visible = False
        Else
            'oMenuItem.Enabled = mCcas(0).PdfStream IsNot Nothing
            If Not _IsAllowedToBrowse Then oMenuItem.Enabled = False
        End If
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        If _Values.Count > 1 Then
            oMenuItem.Visible = False
        Else
            oMenuItem.Text = "Pdf"
            oMenuItem.Image = My.Resources.pdf
            If _Values.Count > 0 Then
                oMenuItem.Enabled = _Values.First.Cca.DocFile IsNot Nothing
                If Not _IsAllowedToBrowse Then oMenuItem.Enabled = False
            End If
        End If
        AddHandler oMenuItem.Click, AddressOf Do_Pdf
        Return oMenuItem
    End Function

    Private Function MenuItem_PdfAdd() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Image = My.Resources.pdf
        If _Values.Count > 1 Then
            oMenuItem.Visible = False
        Else
            If Not _IsAllowedToBrowse Then oMenuItem.Enabled = False
            If _Values.Count > 0 Then
                If _Values.First.Cca.DocFile Is Nothing Then
                    oMenuItem.Text = "importar Pdf"
                Else
                    oMenuItem.Text = "sustituir Pdf"
                End If
            Else
                oMenuItem.Visible = False
            End If
        End If
        AddHandler oMenuItem.Click, AddressOf Do_PdfAdd
        Return oMenuItem
    End Function



    Private Function MenuItem_Clon() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Clon"
        oMenuItem.Image = My.Resources.tampon
        oMenuItem.Enabled = _IsAllowedToBrowse
        AddHandler oMenuItem.Click, AddressOf Do_Clon
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        For Each item As DTOCcb In _Values
            Dim oCca As DTOCca = item.Cca
            If Not FEB.Cca.IsAllowedToBrowse(oCca, Current.Session.User) Then
                oMenuItem.Enabled = False
                Exit For
            End If
        Next
        AddHandler oMenuItem.Click, AddressOf Do_Del
        Return oMenuItem
    End Function

    Private Function MenuItem_BookFra() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Registre llibre fres."
        'oMenuItem.Image = My.Resources.tampon
        'oMenuItem.Enabled = mAllowBrowse
        AddHandler oMenuItem.Click, AddressOf Do_BookFra
        Return oMenuItem
    End Function

    Private Function MenuItem_RemittanceAdvice() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Remittance advice"
        If Not Current.Session.User.Rol.isSuperAdmin Then
            oMenuItem.Visible = False
        End If
        'oMenuItem.Image = My.Resources.tampon
        'oMenuItem.Enabled = mAllowBrowse
        AddHandler oMenuItem.Click, AddressOf Do_RemittanceAdvice
        Return oMenuItem
    End Function

    Private Function MenuItem_SavePdfs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Desar tots els justificants"
        oMenuItem.Image = My.Resources.save_16
        AddHandler oMenuItem.Click, AddressOf Do_SavePdfs
        Return oMenuItem
    End Function

    Private Function MenuItem_Specific() As ToolStripMenuItem
        Dim exs As New List(Of Exception)
        Dim oMenuItem As New ToolStripMenuItem

        If _Values.Count = 0 Then
            oMenuItem.Visible = False
        Else
            Dim oCcd As DTOCca.CcdEnum = _Values.First.Cca.Ccd
            Select Case oCcd
                Case DTOCca.CcdEnum.FacturaNostre
                    Dim oInvoice = FEB.Invoice.FromNumSync(exs, Current.Session.Emp, _Values.First.Cca.Fch.Year, _Values.First.Cca.Cdn)
                    If exs.Count = 0 Then
                        If oInvoice Is Nothing Then
                            oMenuItem.Visible = False
                        Else
                            If oInvoice IsNot Nothing Then
                                Dim oInvoices As New List(Of DTOInvoice)
                                oInvoices.Add(oInvoice)
                                oMenuItem.Text = "Factura..."
                                oMenuItem.Image = My.Resources.OpenZoom
                                oMenuItem.DropDownItems.AddRange(New Menu_Invoice(oInvoices).Range)
                            Else
                                oMenuItem.Visible = False
                            End If
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Case DTOCca.CcdEnum.Amortitzacions
                    Dim oAmortitzacioItem = FEB.AmortizationItem.FromCcaSync(_Values.First.Cca, exs)
                    If oAmortitzacioItem Is Nothing Then
                        oMenuItem.Visible = False
                    Else
                        oMenuItem.Text = "Amortització..."
                        oMenuItem.Image = My.Resources.OpenZoom
                        oMenuItem.DropDownItems.AddRange(New Menu_AmortizationItem(oAmortitzacioItem).Range)
                    End If
                Case DTOCca.CcdEnum.InmovilitzatAlta
                    Dim oAmortitzacio = FEB.Amortization.FromAltaSync(exs, _Values.First.Cca)
                    If oAmortitzacio Is Nothing Then
                        oMenuItem.Text = "Activar"
                        AddHandler oMenuItem.Click, AddressOf Do_Specific_ActivarInmobilitzat
                    Else
                        oMenuItem.Text = "Actiu..."
                        oMenuItem.Image = My.Resources.OpenZoom
                        oMenuItem.DropDownItems.AddRange(New Menu_Amortization(oAmortitzacio).Range)
                    End If
                Case DTOCca.CcdEnum.InmovilitzatBaixa
                    Dim oAmortitzacio = FEB.Amortization.FromBaixaSync(exs, _Values.First.Cca)
                    If oAmortitzacio Is Nothing Then
                        oMenuItem.Visible = False
                    Else
                        oMenuItem.Text = "Actiu..."
                        oMenuItem.Image = My.Resources.OpenZoom
                        oMenuItem.DropDownItems.AddRange(New Menu_Amortization(oAmortitzacio).Range)
                    End If

                Case DTOCca.CcdEnum.TransferNorma34
                    oMenuItem.Text = "Transferencia..."
                    oMenuItem.Image = My.Resources.save_16
                    AddHandler oMenuItem.Click, AddressOf Do_Specific_Transfer

                Case DTOCca.CcdEnum.Venciment
                    oMenuItem.Text = "Efecte..."
                    oMenuItem.DropDownItems.Add("zoom", Nothing, AddressOf Do_Specific_Csb)
                    oMenuItem.DropDownItems.Add("retrocedir", My.Resources.del, AddressOf Do_Specific_CsbRevert)

                    'Case DTOCca.CcdEnum.FacturaProveidor
                    'oMenuItem.Text = "fra.proveidor..."
                    'oMenuItem.Image = My.Resources.OpenZoom
                    'AddHandler oMenuItem.Click, AddressOf Do_Specific_FraProveidor

                Case Else
                    Dim BlEspecific As Boolean = False
                    Dim oCca As DTOCca = _Values.First.Cca
                    If FEB.Cca.Load(oCca, exs) Then
                        For Each oCcb As DTOCcb In oCca.Items
                            Select Case Left(oCcb.Cta.Id, 2)
                                Case "20", "21", "22"
                                    BlEspecific = True
                                    oMenuItem.Text = "activar..."
                                    oMenuItem.Image = My.Resources.printer
                                    AddHandler oMenuItem.Click, AddressOf Do_Specific_ActivarInmobilitzat
                                    Exit For
                            End Select

                        Next
                        If Not BlEspecific Then
                            oMenuItem.Visible = False
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
            End Select
        End If
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As DTOCca = _Values.First.Cca
        Dim oFrm As New Frm_Cca(oCca)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Web(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As DTOCca = _Values.First.Cca
        Dim sUrl As String = "http://www.matiasmasso.es/pro/wAuditCca.aspx?Guid=" & oCca.Guid.ToString
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(_Values.First.Cca.DocFile)
    End Sub

    Private Async Sub Do_Pdf(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As DTOCca = _Values.First.Cca
        Dim exs As New List(Of Exception)
        If Not Await UIHelper.ShowStreamAsync(exs, oCca.DocFile) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_PdfAdd(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim sTitle As String = "Importar justificant"
        Dim oDocFile As DTODocFile = Nothing
        If UIHelper.LoadPdfDialog(oDocFile, sTitle) Then
            Dim oCca As DTOCca = _Values.First.Cca
            oCca.DocFile = oDocFile
            oCca.Id = Await FEB.Cca.Update(exs, oCca)
            If exs.Count = 0 Then
                'im oArgs As New AfterUpdateEventArgs(mCca, AfterUpdateEventArgs.Modes.NotSet)
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                MsgBox("error al desar el justificant" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("error al importar justificant" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub


    Private Sub Do_AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As DTOCca = DTOCca.Factory(Today, Current.Session.User, DTOCca.CcdEnum.Manual)
        Dim oFrm As New Frm_Cca(oCca)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Clon(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oCca = _Values.First.Cca
        If FEB.Cca.Load(oCca, exs) Then
            Dim oClon = DTOCca.clon(oCca, Current.Session.User)
            Dim oFrm As New Frm_Cca(oClon)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_BookFra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oCca As DTOCca = _Values.First.Cca
        Dim oBookFra = Await FEB.BookFra.FindOrNew(exs, oCca)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_BookFra(oBookFra)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & CcasNums() & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            For Each oCcb As DTOCcb In _Values
                Await FEB.Cca.Delete(exs, oCcb.Cca)
            Next
            If exs.Count = 0 Then
                'Dim oArgs As AfterUpdateEventArgs = AfterUpdateEventArgs.ForDelete(mCcas)
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                MsgBox("no s'ha pogut eliminar l'assentament" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Function CcasNums() As String
        Select Case _Values.Count
            Case 0
                Return "(cap assentament)"
            Case 1
                Return "assentament " & _Values.First.Cca.Id
            Case 2, 3, 4, 5
                Dim s As String = "assentaments "
                Dim i As Integer
                For i = 0 To _Values.Count - 1
                    If i > 0 Then s = s & ","
                    s = s & _Values(i).Cca.Id
                Next
                Return s
            Case Else
                Return "assentaments " & _Values.First.Cca.Id & ",..."
        End Select
    End Function



    Private Sub Do_Specific_Transfer(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oBancTransfer As DTOBancTransferOld = BLLBancTransferOld.Find(_Values.First.Cca.Guid)
        'Dim oFrm As New Frm_BancTransfer(oBancTransfer)
        'oFrm.Show()
    End Sub

    Private Async Sub Do_Specific_Csb(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB.Cca.Load(_Values.First.Cca, exs) Then
            Dim oCsb = Await FEB.Csb.Find(_Values.First.Cca.Ref.Guid, exs)
            If exs.Count = 0 Then
                Dim oFrm As New Frm_Csb(oCsb)
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Specific_CsbRevert(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oCca As DTOCca = _Values.First.Cca
        If Await FEB.Csb.RevertVto(oCca, exs) Then
            MsgBox("venciment retrocedit correctament" & vbCrLf & oCca.Fch.ToShortDateString & " " & oCca.Concept)
            MyBase.RefreshRequest(Me, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs, "error al retrocedir el venciment")
        End If
    End Sub

    Private Async Sub Do_Specific_ActivarInmobilitzat(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As DTOCca = _Values.First.Cca
        Dim exs As New List(Of Exception)
        If FEB.Cca.Load(oCca, exs) Then
            Dim oAmortitzacio = DTOAmortization.Factory(oCca)
            If oAmortitzacio IsNot Nothing Then
                Dim oDefaultTipus = Await FEB.Amortizations.DefaultTipus(exs)
                If exs.Count = 0 Then
                    oAmortitzacio.Tipus = DTOAmortizationTipus.ForCta(oDefaultTipus, oAmortitzacio.Cta)
                    Dim oFrm As New Frm_Amortization(oAmortitzacio)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                MsgBox("No s'ha pogut composar la partida de actiu", MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_RemittanceAdvice()
        Dim oCca As DTOCca = _Values.First.Cca
        Dim exs As New List(Of Exception)
        If Not Await MatOutlook.RemittanceAdvice(oCca, exs) Then
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
                Dim oCcbs = _Values 'Await FEB.Ccbs.All(exs, GlobalVariables.Emp, _Cce.Exercici.Year, _Cce.Cta)
                If exs.Count = 0 Then
                    For Each oCcb In oCcbs
                        If oCcb.Cca.DocFile IsNot Nothing Then
                            Dim url = FEB.DocFile.DownloadUrl(oCcb.Cca.DocFile, True)
                            Dim fileName As String = String.Format("{0}\{1:0000}.{2:00000}.pdf", path, oCcb.Cca.Fch.Year, oCcb.Cca.Id)
                            'If oCcb.Cca.Ccd = DTOCca.CcdEnum.FacturaNostre Then
                            'fileName = String.Format("{0}\factura {1:0000}.{2:00000}.pdf", path, oCcb.Cca.Fch.Year, oCcb.Cca.Cdn)
                            'End If
                            Using client As New WebClient()
                                client.DownloadFile(url, fileName)
                            End Using
                        End If
                    Next
                    RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
                Else
                    RaiseEvent RequestToToggleProgressBar(Me, New MatEventArgs(False))
                    UIHelper.WarnError(exs)
                End If
            End If
        End With

    End Sub

End Class

