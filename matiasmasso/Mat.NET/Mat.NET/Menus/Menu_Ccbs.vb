Public Class Menu_Ccbs
    Inherits Menu_Base

    Private _Values As List(Of DTOCcb)
    Private _IsAllowedToBrowse As Boolean

    Public Sub New(ByVal values As List(Of DTOCcb))
        MyBase.New()
        _Values = values

        Dim oFirstCca As DTOCca = _Values.First.Cca
        _IsAllowedToBrowse = BLL.BLLCca.IsAllowedToBrowse(oFirstCca, BLL.BLLSession.Current.User)
    End Sub


    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), _
        MenuItem_Web(), _
        MenuItem_CopyLink(), _
        MenuItem_Pdf(), _
        MenuItem_PdfAdd(), _
        MenuItem_Search(), _
        MenuItem_Clon(), _
        MenuItem_Del(), _
        MenuItem_BookFra(), _
        MenuItem_RemittanceAdvice(), _
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
            oMenuItem.Enabled = _Values.First.Cca.DocFile IsNot Nothing
            If Not _IsAllowedToBrowse Then oMenuItem.Enabled = False
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
            If _Values.First.Cca.DocFile Is Nothing Then
                oMenuItem.Text = "importar Pdf"
            Else
                oMenuItem.Text = "sustituir Pdf"
            End If
        End If
        AddHandler oMenuItem.Click, AddressOf Do_PdfAdd
        Return oMenuItem
    End Function

    Private Function MenuItem_Search() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Buscar"
        oMenuItem.Image = My.Resources.search_16
        AddHandler oMenuItem.Click, AddressOf Do_Search
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
            If Not BLL.BLLCca.IsAllowedToBrowse(oCca, BLL.BLLSession.Current.User) Then
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
        If Not BLL.BLLSession.Current.User.Rol.IsSuperAdmin Then
            oMenuItem.Visible = False
        End If
        'oMenuItem.Image = My.Resources.tampon
        'oMenuItem.Enabled = mAllowBrowse
        AddHandler oMenuItem.Click, AddressOf Do_RemittanceAdvice
        Return oMenuItem
    End Function

    Private Function MenuItem_Specific() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem

        Select Case _Values.First.Cca.Ccd
            Case DTOCca.CcdEnum.IngresXecs
                oMenuItem.Text = "full d'ingres"
                oMenuItem.Image = My.Resources.printer
                AddHandler oMenuItem.Click, AddressOf Do_Specific_XecIngres
                oMenuItem.Visible = _Values.Count = 1
            Case DTOCca.CcdEnum.FacturaNostre
                Dim oFra As Fra = Fra.FromNum(BLL.BLLApp.Emp, _Values.First.Cca.Fch.Year, _Values.First.Cca.Cdn)
                If oFra.Exists Then
                    oMenuItem.Text = "Factura..."
                    oMenuItem.Image = My.Resources.OpenZoom
                    oMenuItem.DropDownItems.AddRange(New Menu_Fra(oFra).Range)
                Else
                    oMenuItem.Visible = False
                End If
            Case DTOCca.CcdEnum.Amortitzacions, DTOCca.CcdEnum.InmovilitzatBaixa, DTOCca.CcdEnum.InmovilitzatAlta
                Dim oMrt As New Mrt(BLL.BLLApp.Emp, _Values.First.Cca.Cdn)
                If oMrt.Exists Then
                    oMenuItem.Text = "Actiu..."
                    oMenuItem.Image = My.Resources.OpenZoom
                    oMenuItem.DropDownItems.AddRange(New Menu_Mrt(oMrt).Range)
                Else
                    oMenuItem.Visible = False
                End If

            Case DTOCca.CcdEnum.TransferNorma34
                oMenuItem.Text = "Transferencia..."
                oMenuItem.Image = My.Resources.save_16
                AddHandler oMenuItem.Click, AddressOf Do_Specific_Transfer

                'Case DTOCca.CcdEnum.FacturaProveidor
                'oMenuItem.Text = "fra.proveidor..."
                'oMenuItem.Image = My.Resources.OpenZoom
                'AddHandler oMenuItem.Click, AddressOf Do_Specific_FraProveidor

            Case Else
                Dim BlEspecific As Boolean = False
                Dim oCca As DTOCca = _Values.First.Cca
                BLL.BLLCca.Load(oCca)
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
        End Select
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As New Cca(_Values.First.Cca.Guid)
        Dim oFrm As New Frm_Cca(oCca)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Web(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As New Cca(_Values.First.Cca.Guid)
        Dim sUrl As String = "http://www.matiasmasso.es/pro/wAuditCca.aspx?Guid=" & oCca.Guid.ToString
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(_Values.First.Cca.DocFile)
    End Sub

    Private Sub Do_Pdf(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.ShowStream(_Values.First.Cca.DocFile)
    End Sub

    Private Sub Do_PdfAdd(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of exception)
        Dim sTitle As String = "Importar justificant"
        Dim oDocFile As DTODocFile = Nothing
        If UIHelper.LoadPdfDialog(oDocFile, sTitle) Then
            Dim oCca As DTOCca = _Values.First.Cca
            oCca.DocFile = oDocFile
            If BLL.BLLCca.Update(oCca, exs) Then
                'im oArgs As New AfterUpdateEventArgs(mCca, AfterUpdateEventArgs.Modes.NotSet)
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                MsgBox("error al desar el justificant" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("error al importar justificant" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub Do_Search(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowCcaSearch(_Values.First.Cca.Fch.Year)
    End Sub

    Private Sub Do_AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As New Cca(BLL.BLLApp.Emp)
        With oCca
            .fch = Today
        End With
        Dim oFrm As New Frm_Cca(oCca)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.show()
    End Sub

    Private Sub Do_Clon(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As New Cca(_Values.First.Cca.Guid)
        Dim oFrm As New Frm_Cca(oCca.Clon)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_BookFra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As New Cca(_Values.First.Cca.Guid)
        Dim oBookFra As BookFra = BookFraLoader.FindOrNew(oCca)
        Dim oFrm As New Frm_BookFra(oBookFra)

        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & CcasNums() & "?", MsgBoxStyle.OKCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            For Each oCcb As DTOCcb In _Values
                BLL.BLLCca.Delete(oCcb.Cca, exs)
            Next
            If exs.Count = 0 Then
                'Dim oArgs As AfterUpdateEventArgs = AfterUpdateEventArgs.ForDelete(mCcas)
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                MsgBox("no s'ha pogut eliminar l'assentament" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
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

    Private Sub Do_Specific_XecIngres(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As New Cca(_Values.First.Cca.Guid)
        Dim oXecsPresentacio As XecsPresentacio = XecLoader.PresentacioFromCca(oCca)
        UIHelper.ShowStream(oXecsPresentacio.DocFile)
    End Sub

    Private Sub Do_Specific_Transfer(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As New Cca(_Values.First.Cca.Guid)
        Dim oBancTransfer As New BancTransfer(oCca)
        Dim oFrm As New Frm_Banc_Transfer(oBancTransfer)
        oFrm.Show()
    End Sub

    Private Sub Do_Specific_ActivarInmobilitzat(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As New Cca(_Values.First.Cca.Guid)
        Dim oMrt As Mrt = oCca.AddActiu
        If oMrt IsNot Nothing Then
            Dim oFrm As New Frm_Mrt
            AddHandler oFrm.AfterUpdate, AddressOf UpdateCcaFromMrt
            With oFrm
                .Mrt = oMrt
                .Show()
            End With
        Else
            MsgBox("No s'ha pogut composar la partida de actiu", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub Do_Specific_FraProveidor(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As New Cca(_Values.First.Cca.Guid)
        Dim oFacturaDeProveidor As New FacturaDeProveidor(oCca.Guid)
        Dim oFrm As New Frm_FraProveidor(oFacturaDeProveidor)
        oFrm.Show()
    End Sub

    Private Sub UpdateCcaFromMrt(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As New Cca(_Values.First.Cca.Guid)
        Dim oMrt As Mrt = CType(sender, Mrt)
        oCca.SaveCcd(DTOCca.CcdEnum.InmovilitzatAlta, oMrt.Id)
        MyBase.RefreshRequest(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_RemittanceAdvice()
        Dim oCca As New Cca(_Values.First.Cca.Guid)
        MatOutlook.RemittanceAdvice(oCca)
    End Sub


End Class

