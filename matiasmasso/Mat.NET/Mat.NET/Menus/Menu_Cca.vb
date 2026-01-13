

Public Class Menu_Cca
    Private _Cca As DTOCca
    Private _Ccas As List(Of DTOCca)

    Private mCcas As Ccas
    Private mCca As Cca
    Private mAllowBrowse As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oCca As Cca)
        MyBase.New()
        If oCca IsNot Nothing Then
            _Cca = New DTOCca(oCca.Guid)
            _Ccas = New List(Of DTOCca)
            _Ccas.Add(_Cca)

            mCca = oCca
            mCcas = New Ccas
            mCcas.Add(mCca)
        End If
    End Sub

    Public Sub New(ByVal oCca As DTOCca)
        MyBase.New()
        If oCca IsNot Nothing Then
            _Cca = oCca
            _Ccas = New List(Of DTOCca)
            _Ccas.Add(_Cca)

            mCca = New Cca(oCca.Guid)
            mCcas = New Ccas
            mCcas.Add(mCca)
        End If
    End Sub

    Public Sub New(ByVal oCcas As Ccas)
        MyBase.New()
        If oCcas IsNot Nothing Then
            mCcas = oCcas
            If mCcas.Count > 0 Then mCca = mCcas(0)

            _Ccas = New List(Of DTOCca)
            For Each oCca As Cca In oCcas
                _Ccas.Add(New DTOCca(oCca.Guid))
            Next
            If _Ccas.Count > 0 Then _Cca = _Ccas(0)
        End If
    End Sub

    Public Sub New(ByVal oCcas As List(Of DTOCca))
        MyBase.New()
        If oCcas IsNot Nothing Then
            _Ccas = oCcas
            If _Ccas.Count > 0 Then
                _Cca = _Ccas(0)
            End If
            If _Ccas.Count > 0 Then _Cca = _Ccas(0)

            mCcas = New Ccas
            For Each oCca As DTOCca In oCcas
                mCcas.Add(New Cca(oCca.Guid))
            Next
            If mCcas.Count > 0 Then mCca = mCcas(0)
        End If
    End Sub

    Public Function Range() As ToolStripMenuItem()
        mAllowBrowse = mCca.AllowBrowse
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
        If mCcas.Count = 1 Then
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
        If mCcas.Count > 1 Then
            oMenuItem.Visible = False
        Else
            'oMenuItem.Enabled = mCcas(0).PdfStream IsNot Nothing
            If Not mAllowBrowse Then oMenuItem.Enabled = False
        End If
        AddHandler oMenuItem.Click, AddressOf Do_Web
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        If mCcas.Count > 1 Then
            oMenuItem.Visible = False
        Else
            'oMenuItem.Enabled = mCcas(0).PdfStream IsNot Nothing
            If Not mAllowBrowse Then oMenuItem.Enabled = False
        End If
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pdf"
        oMenuItem.Image = My.Resources.pdf
        oMenuItem.Enabled = mCca.DocFile IsNot Nothing
        If mCcas.Count > 1 Then
            oMenuItem.Visible = False
        Else
            'oMenuItem.Enabled = mCcas(0).PdfStream IsNot Nothing
            If Not mAllowBrowse Then oMenuItem.Enabled = False
        End If
        AddHandler oMenuItem.Click, AddressOf Do_Pdf
        Return oMenuItem
    End Function

    Private Function MenuItem_PdfAdd() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Image = My.Resources.pdf
        If mCcas.Count > 1 Then
            oMenuItem.Visible = False
        Else
            'oMenuItem.Enabled = mCcas(0).PdfStream IsNot Nothing
            If Not mAllowBrowse Then oMenuItem.Enabled = False
            If mCca.DocFile Is Nothing Then
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
        oMenuItem.Enabled = mAllowBrowse
        AddHandler oMenuItem.Click, AddressOf Do_Clon
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        Dim oCca As Cca
        For Each oCca In mCcas
            If oCca.AllowBrowse = False Then
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
        Select Case mCca.Ccd
            Case DTOCca.CcdEnum.IngresXecs
                oMenuItem.Text = "full d'ingres"
                oMenuItem.Image = My.Resources.printer
                AddHandler oMenuItem.Click, AddressOf Do_Specific_XecIngres
            Case DTOCca.CcdEnum.FacturaNostre
                Dim oFra As Fra = Fra.FromNum(mCca.emp, mCca.yea, mCca.Cdn)
                If oFra.Exists Then
                    oMenuItem.Text = "Factura..."
                    oMenuItem.Image = My.Resources.OpenZoom
                    oMenuItem.DropDownItems.AddRange(New Menu_Fra(oFra).Range)
                Else
                    oMenuItem.Visible = False
                End If
            Case DTOCca.CcdEnum.Amortitzacions, DTOCca.CcdEnum.InmovilitzatBaixa, DTOCca.CcdEnum.InmovilitzatAlta
                Dim oMrt As New Mrt(mCca.emp, mCca.Cdn)
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

                Dim oCcb As Ccb
                For Each oCcb In mCca.ccbs
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
        Dim oFrm As New Frm_Cca(mCca)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.show
    End Sub

    Private Sub Do_Web(ByVal sender As Object, ByVal e As System.EventArgs)
        'root.ShowBigFile(mCca.BigFile)

        Dim sUrl As String = "http://www.matiasmasso.es/pro/wAuditCca.aspx?Guid=" & mCca.Guid.ToString
        Process.Start("IExplore.exe", sUrl)
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(mCca.DocFile)
    End Sub

    Private Sub Do_Pdf(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.ShowStream(mCca.DocFile)
    End Sub

    Private Sub Do_PdfAdd(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs as New List(Of exception)
        Dim sTitle As String = "Importar justificant"
        Dim oDocFile As DTODocFile = Nothing
        If UIHelper.LoadPdfDialog(oDocFile, sTitle) Then
            mCca.DocFile = oDocFile
            If mCca.Update( exs) Then
                'im oArgs As New AfterUpdateEventArgs(mCca, AfterUpdateEventArgs.Modes.NotSet)
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                MsgBox("error al desar el justificant" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("error al importar justificant" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub Do_Search(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowCcaSearch(mCca.yea)
    End Sub

    Private Sub Do_AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As New Cca(mCca.emp)
        With oCca
            .fch = Today
        End With
        Dim oFrm As New Frm_Cca(oCca)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.show
    End Sub

    Private Sub Do_Clon(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Cca(mCca.Clon)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.show()
    End Sub

    Private Sub Do_BookFra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oBookFra As BookFra = BookFraLoader.FindOrNew(mCca)
        Dim oFrm As New Frm_BookFra(oBookFra)

        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & CcasNums() & "?", MsgBoxStyle.OKCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If mCcas.Delete( exs) Then
                'Dim oArgs As AfterUpdateEventArgs = AfterUpdateEventArgs.ForDelete(mCcas)
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                MsgBox("no s'ha pogut eliminar l'assentament" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Function CcasNums() As String
        Select Case mCcas.Count
            Case 0
                Return "(cap assentament)"
            Case 1
                Return "assentament " & mCcas(0).Id
            Case 2, 3, 4, 5
                Dim s As String = "assentaments "
                Dim i As Integer
                For i = 0 To mCcas.Count - 1
                    If i > 0 Then s = s & ","
                    s = s & mCcas(i).Id
                Next
                Return s
            Case Else
                Return "assentaments " & mCcas(0).Id & ",..."
        End Select
    End Function

    Private Sub Do_Specific_XecIngres(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oXecsPresentacio As XecsPresentacio = XecLoader.PresentacioFromCca(mCca)
        UIHelper.ShowStream(oXecsPresentacio.DocFile)
    End Sub

    Private Sub Do_Specific_Transfer(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oBancTransfer As New BancTransfer(mCca)
        Dim oFrm As New Frm_Banc_Transfer(oBancTransfer)
        oFrm.Show()
    End Sub

    Private Sub Do_Specific_ActivarInmobilitzat(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMrt As Mrt = mCca.AddActiu
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
        Dim oFacturaDeProveidor As New FacturaDeProveidor(mCca.Guid)
        Dim oFrm As New Frm_FraProveidor(oFacturaDeProveidor)
        oFrm.Show()
    End Sub

    Private Sub UpdateCcaFromMrt(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMrt As Mrt = CType(sender, Mrt)
        mCca.SaveCcd(DTOCca.CcdEnum.InmovilitzatAlta, oMrt.Id)
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_RemittanceAdvice()
        MatOutlook.RemittanceAdvice(mCca)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub
End Class

