

Public Class Menu_Csa
    Private _Csas As List(Of DTOCsa)
    Private _ShowProgress As ProgressBarHandler

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)


    Public Sub New(ByVal oCsa As DTOCsa, Optional ShowProgress As ProgressBarHandler = Nothing)
        MyBase.New()
        _Csas = New List(Of DTOCsa)
        _Csas.Add(oCsa)
        _ShowProgress = ShowProgress
    End Sub


    Public Sub New(ByVal oCsas As List(Of DTOCsa), Optional ShowProgress As ProgressBarHandler = Nothing)
        MyBase.New()
        _Csas = oCsas
        _ShowProgress = ShowProgress
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
            MenuItem_Zoom(),
            MenuItem_Excel(),
            menuitem_FileSEPACORE(),
            menuitem_FileExportLaCaixa(),
            MenuItem_Undo(),
            MenuItem_Despeses()
        })
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        oMenuItem.Enabled = _Csas.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function


    Private Function MenuItem_Excel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel"
        oMenuItem.Image = My.Resources.Excel
        AddHandler oMenuItem.Click, AddressOf Do_Excel
        Return oMenuItem
    End Function


    Private Function menuitem_FileSEPACORE() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Fitxer SEPA CORE"
        oMenuItem.Image = My.Resources.save_16
        oMenuItem.Enabled = _Csas.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_FileSEPACORE
        Return oMenuItem
    End Function

    Private Function menuitem_FileExportLaCaixa() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Fitxer Exportació La Caixa"
        oMenuItem.Image = My.Resources.save_16
        oMenuItem.Enabled = _Csas.Count = 1
        If _Csas IsNot Nothing Then
            Dim oCsa As DTOCsa = _Csas(0)
            Dim sBancNom As String = oCsa.Banc.Nom
            Dim IsLaCaixa As Boolean = sBancNom.ToUpper.Contains("CAIXA")
            If Not IsLaCaixa Then oMenuItem.Visible = False
        End If
        AddHandler oMenuItem.Click, AddressOf Do_FileExportLaCaixa
        Return oMenuItem
    End Function


    Private Function MenuItem_Undo() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retrocedir"
        oMenuItem.Image = My.Resources.UNDO
        oMenuItem.Enabled = _Csas.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Undo
        Return oMenuItem
    End Function

    Private Function MenuItem_Despeses() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Despeses"
        If _Csas(0).Despeses IsNot Nothing Then
            If _Csas(0).Despeses.Val <> 0 Then oMenuItem.Enabled = False
        End If
        oMenuItem.Enabled = _Csas.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Despeses
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Csa(_Csas.First)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Undo(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & CsasNums() & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Csas.Delete(_Csas, exs) Then
                MsgBox("Remeses eliminades", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(sender, MatEventArgs.Empty)
            Else
                MsgBox("No s'ha pogut retrocedir" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub




    Private Async Sub Do_FileExportLaCaixa()
        Dim oCsa As DTOCsa = _Csas.First
        'Dim oldCsa As New Csa(oCsa.Guid)
        Dim sFilename As String = oCsa.filename()
        'UIHelper.SaveTextFileDialog(oldCsa.FileText, sFilename)
        'Dim textStream As String = BLLLaCaixaRemesaExportacio.Text(oCsa)
        Dim exs As New List(Of Exception)
        Dim textStream = Await FEB2.Csa.LaCaixaRemesaExportacio(oCsa, exs)
        If exs.Count = 0 Then
            UIHelper.SaveTextFileDialog(textStream, sFilename)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_FileSEPACORE(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oGuid As Guid = _Csas(0).Guid
        Dim oCsa = Await FEB2.Csa.Find(oGuid, exs)
        If exs.Count = 0 Then
            If DTOCsa.ValidateSepaCore(oCsa, exs) Then
                Dim XML As String = SepaCoreHelper.SepaCoreXML(exs, Current.Session.Emp, oCsa)
                If exs.Count = 0 Then
                    UIHelper.SaveXmlFileDialog(XML, "remesa " & oCsa.formattedId())
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Async Sub Do_Despeses(ByVal sender As Object, ByVal e As System.EventArgs)
        If _Csas(0).Descomptat Then
            root.ExeCsaDespeses(_Csas.First)
        Else
            Dim exs As New List(Of Exception)
            If Await FEB2.Csa.SaveRemesaCobrament(_Csas.First, Current.Session.User, exs) Then
                MsgBox("despeses registrades correctament", MsgBoxStyle.Information)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Function CsasNums() As String
        Select Case _Csas.Count
            Case 0
                Return "(cap remesa)"
            Case 1
                Return "Remesa " & _Csas(0).Id
            Case 2, 3, 4, 5
                Dim s As String = "Remeses "
                Dim i As Integer
                For i = 0 To _Csas.Count - 1
                    If i > 0 Then s = s & ","
                    s = s & _Csas(i).Id
                Next
                Return s
            Case Else
                Return "Remeses " & _Csas(0).Id & ",..."
        End Select
    End Function

    Private Sub ShowPdf()

    End Sub

    Private Sub Do_Excel()
        Dim oSheet As MatHelperStd.ExcelHelper.Sheet = FEB2.Csas.Excel(_Csas)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class
