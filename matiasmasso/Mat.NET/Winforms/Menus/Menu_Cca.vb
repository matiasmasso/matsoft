

Public Class Menu_Cca
    Private _Cca As DTOCca
    Private _Ccas As List(Of DTOCca)

    Private mAllowBrowse As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)


    Public Sub New(ByVal oCca As DTOCca)
        MyBase.New()
        If oCca IsNot Nothing Then
            _Cca = oCca
            _Ccas = New List(Of DTOCca)
            _Ccas.Add(_Cca)
        End If
    End Sub


    Public Sub New(ByVal oCcas As List(Of DTOCca))
        MyBase.New()
        If oCcas IsNot Nothing Then
            _Ccas = oCcas
            If _Ccas.Count > 0 Then
                _Cca = _Ccas.First
            End If
        End If
    End Sub

    Public Function Range() As ToolStripMenuItem()
        mAllowBrowse = FEB2.Cca.IsAllowedToBrowse(_Cca, Current.Session.User)
        Return (New ToolStripMenuItem() {MenuItem_Zoom(),
        MenuItem_Web(),
        MenuItem_CopyLink(),
        MenuItem_Pdf(),
        MenuItem_PdfAdd(),
        MenuItem_Clon(),
        MenuItem_Del(),
        MenuItem_BookFra(),
        MenuItem_RemittanceAdvice(),
         MenuItem_Specific()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        If _Ccas.Count = 1 Then
            oMenuItem.Enabled = mAllowBrowse
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
        If _Ccas.Count > 1 Then
            oMenuItem.Visible = False
        Else
            If Not mAllowBrowse Then oMenuItem.Enabled = False
        End If
        AddHandler oMenuItem.Click, AddressOf Do_Web
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        If _Ccas.Count > 1 Then
            oMenuItem.Visible = False
        Else
            If Not mAllowBrowse Then oMenuItem.Enabled = False
        End If
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pdf"
        oMenuItem.Image = My.Resources.pdf
        oMenuItem.Enabled = _Cca.DocFile IsNot Nothing
        If _Ccas.Count > 1 Then
            oMenuItem.Visible = False
        Else
            If Not mAllowBrowse Then oMenuItem.Enabled = False
        End If
        AddHandler oMenuItem.Click, AddressOf Do_Pdf
        Return oMenuItem
    End Function

    Private Function MenuItem_PdfAdd() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Image = My.Resources.pdf
        If _Ccas.Count > 1 Then
            oMenuItem.Visible = False
        Else
            If Not mAllowBrowse Then oMenuItem.Enabled = False
            If _Cca.DocFile Is Nothing Then
                oMenuItem.Text = "importar Pdf"
            Else
                oMenuItem.Text = "sustituir Pdf"
            End If
        End If
        AddHandler oMenuItem.Click, AddressOf Do_PdfAdd
        Return oMenuItem
    End Function


    Private Function MenuItem_Clon() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Clon"
        oMenuItem.Image = My.Resources.tampon
        oMenuItem.Enabled = mAllowBrowse
        AddHandler oMenuItem.Click, AddressOf Do_Clon
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        Dim oCca As DTOCca
        For Each oCca In _Ccas
            If Not FEB2.Cca.IsAllowedToBrowse(oCca, Current.Session.User) Then
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
        If Not Current.Session.User.Rol.IsSuperAdmin Then
            oMenuItem.Visible = False
        End If
        'oMenuItem.Image = My.Resources.tampon
        'oMenuItem.Enabled = mAllowBrowse
        AddHandler oMenuItem.Click, AddressOf Do_RemittanceAdvice
        Return oMenuItem
    End Function

    Private Function MenuItem_Specific() As ToolStripMenuItem
        Dim exs As New List(Of Exception)
        Dim oMenuItem As New ToolStripMenuItem
        Select Case _Cca.Ccd
            Case DTOCca.CcdEnum.FacturaNostre
                Dim oInvoice = FEB2.Invoice.FromNumSync(exs, Current.Session.Emp, Year(_Cca.Fch), _Cca.Cdn)
                If exs.Count = 0 Then
                    If oInvoice IsNot Nothing Then
                        Dim oInvoices As New List(Of DTOInvoice)
                        oInvoices.Add(oInvoice)
                        oMenuItem.Text = "Factura..."
                        oMenuItem.Image = My.Resources.OpenZoom
                        oMenuItem.DropDownItems.AddRange(New Menu_Invoice(oInvoices).Range)
                    Else
                        oMenuItem.Visible = False
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Case DTOCca.CcdEnum.Amortitzacions
            Case DTOCca.CcdEnum.InmovilitzatBaixa
                Dim oAmortitzacio = FEB2.Amortization.FromBaixaSync(exs, _Cca)
                If oAmortitzacio Is Nothing Then
                    oMenuItem.Visible = False
                Else
                    oMenuItem.Text = "Actiu..."
                    oMenuItem.Image = My.Resources.OpenZoom
                    oMenuItem.DropDownItems.AddRange(New Menu_Amortization(oAmortitzacio).Range)
                End If
            Case DTOCca.CcdEnum.InmovilitzatAlta
                Dim oAmortitzacio = FEB2.Amortization.FromAltaSync(exs, _Cca)
                If oAmortitzacio Is Nothing Then
                    oMenuItem.Visible = False
                Else
                    oMenuItem.Text = "Actiu..."
                    oMenuItem.Image = My.Resources.OpenZoom
                    oMenuItem.DropDownItems.AddRange(New Menu_Amortization(oAmortitzacio).Range)
                End If

            Case DTOCca.CcdEnum.TransferNorma34
                oMenuItem.Text = "Transferencia..."
                oMenuItem.DropDownItems.Add("zoom", Nothing, AddressOf Do_Specific_TransferZoom)
                oMenuItem.DropDownItems.Add("desar fitxer Sepa", My.Resources.save_16, AddressOf Do_Specific_TransferSepaFile)
                oMenuItem.DropDownItems.Add("retrocedir", My.Resources.aspa, AddressOf Do_Specific_TransferRemove)

            'Case DTOCca.CcdEnum.FacturaProveidor
            'oMenuItem.Text = "fra.proveidor..."
            'oMenuItem.Image = My.Resources.OpenZoom
            'AddHandler oMenuItem.Click, AddressOf Do_Specific_FraProveidor

            Case DTOCca.CcdEnum.Venciment
                oMenuItem.Text = "Efecte..."
                oMenuItem.DropDownItems.Add("zoom", Nothing, AddressOf Do_Specific_Csb)
                oMenuItem.DropDownItems.Add("retrocedir", My.Resources.del, AddressOf Do_Specific_CsbRevert)

            Case DTOCca.CcdEnum.Nomina
                oMenuItem.Text = "Nómina"
                AddHandler oMenuItem.Click, AddressOf Do_Specific_Nomina
            Case Else
                Dim BlEspecific As Boolean = False

                Dim oCcb As DTOCcb
                For Each oCcb In _Cca.Items
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
        End Select
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Cca(_Cca)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Web(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim sUrl As String = "http://www.matiasmasso.es/pro/wAuditCca.aspx?Guid=" & _Cca.Guid.ToString
        Process.Start(sUrl)
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(_Cca.DocFile)
    End Sub

    Private Async Sub Do_Pdf(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If Not Await UIHelper.ShowStreamAsync(exs, _Cca.DocFile) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_PdfAdd(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim sTitle As String = "Importar justificant"
        Dim oDocFile As DTODocFile = Nothing
        If UIHelper.LoadPdfDialog(oDocFile, sTitle) Then
            If FEB2.Cca.Load(_Cca, exs) Then
                _Cca.DocFile = oDocFile
                _Cca.Id = Await FEB2.Cca.Update(exs, _Cca)
                If exs.Count = 0 Then
                    RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Else
                    MsgBox("error al desar el justificant" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
                End If
            Else
                UIHelper.WarnError(exs)
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
        If FEB2.Cca.Load(_Cca, exs) Then
            Dim oClon = DTOCca.Clon(_Cca, Current.Session.User)
            Dim oFrm As New Frm_Cca(oClon)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_BookFra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oBookFra = Await FEB2.BookFra.FindOrNew(exs, _Cca)
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
            For Each item As DTOCca In _Ccas
                Await FEB2.Cca.Delete(exs, item)
            Next

            If exs.Count = 0 Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Function CcasNums() As String
        Select Case _Ccas.Count
            Case 0
                Return "(cap assentament)"
            Case 1
                Return "assentament " & _Ccas.First.Id
            Case 2, 3, 4, 5
                Dim s As String = "assentaments "
                Dim i As Integer
                For i = 0 To _Ccas.Count - 1
                    If i > 0 Then s = s & ","
                    s = s & _Ccas(i).Id
                Next
                Return s
            Case Else
                Return "assentaments " & _Ccas.First.Id & ",..."
        End Select
    End Function


    Private Sub Do_Specific_TransferZoom(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oBancTransferPool = FEB2.Await BancTransferPoolOld.Find(mCca.Guid)
        'Dim oFrm As New Frm_BancTransferPool(oBancTransferPool)
        'oFrm.Show()
    End Sub

    Private Async Sub Do_Specific_TransferSepaFile(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oBancTransferPool = Await FEB2.BancTransferPool.FromCca(_Cca, exs)
        FEB2.BancTransferPool.Load(oBancTransferPool, exs)
        Dim XmlSource As String = Await FEB2.SepaCreditTransfer.XML(Current.Session.Emp, oBancTransferPool, exs)
        If exs.Count = 0 Then
            Dim sFilename As String = oBancTransferPool.DefaultFilename()
            UIHelper.SaveXmlFileDialog(XmlSource, sFilename)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Specific_TransferRemove(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Retrocedim aquesta transferencia?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            Dim oBancTransferPool = Await FEB2.BancTransferPool.FromCca(_Cca, exs)
            If Await FEB2.BancTransferPool.Delete(oBancTransferPool, exs) Then
                RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub Do_Specific_ActivarInmobilitzat(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.Cca.Load(_Cca, exs) Then
            Dim oAmortitzacio = DTOAmortization.Factory(_Cca)
            If oAmortitzacio IsNot Nothing Then
                Dim oDefaultTipus = Await FEB2.Amortizations.DefaultTipus(exs)
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

    Private Async Sub Do_Specific_Csb(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.Cca.Load(_Cca, exs) Then
            Dim oCsb = Await FEB2.Csb.Find(_Cca.Ref.Guid, exs)
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
        If Await FEB2.Csb.RevertVto(_Cca, exs) Then
            MsgBox("venciment retrocedit correctament" & vbCrLf & _Cca.Fch.ToShortDateString & " " & _Cca.Concept)
        Else
            UIHelper.WarnError(exs, "error al retrocedir el venciment")
        End If
    End Sub

    Private Sub Do_Specific_Nomina()
        Dim oNomina As New DTONomina(_Cca)
        Dim oFrm As New Frm_Nomina(oNomina)
        AddHandler oFrm.afterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_RemittanceAdvice()
        Dim exs As New List(Of Exception)
        If Not Await MatOutlook.RemittanceAdvice(_Cca, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub
End Class

