
Public Class Menu_CertificatIrpf
    Inherits Menu_Base

    Private _CertificatIrpfs As List(Of DTOCertificatIrpf)
    Private _CertificatIrpf As DTOCertificatIrpf

    Public Sub New(ByVal oCertificatIrpfs As List(Of DTOCertificatIrpf))
        MyBase.New()
        _CertificatIrpfs = oCertificatIrpfs
        If _CertificatIrpfs IsNot Nothing Then
            If _CertificatIrpfs.Count > 0 Then
                _CertificatIrpf = _CertificatIrpfs.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oCertificatIrpf As DTOCertificatIrpf)
        MyBase.New()
        _CertificatIrpf = oCertificatIrpf
        _CertificatIrpfs = New List(Of DTOCertificatIrpf)
        If _CertificatIrpf IsNot Nothing Then
            _CertificatIrpfs.Add(_CertificatIrpf)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Browse())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Browse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Document"
        oMenuItem.Enabled = _CertificatIrpfs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Browse
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CertificatIrpf(_CertificatIrpf)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Browse(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If _CertificatIrpfs.Count > 0 Then
            UIHelper.ShowPdf(_CertificatIrpfs.First.DocFile)
        End If
    End Sub


    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim s As String = String.Format("Eliminem {0} certificat{1}?", _CertificatIrpfs.Count, IIf(_CertificatIrpfs.Count = 1, "", "s"))
        Dim rc As MsgBoxResult = MsgBox(s, MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            Dim failed As Integer = 0
            Dim succeeded As Integer = 0
            MyBase.ToggleProgressBarRequest(True)
            For Each item In _CertificatIrpfs
                If Await FEB2.CertificatIrpf.Delete(exs, item) Then
                    succeeded += 1
                Else
                    failed += 1
                End If
            Next
            MyBase.ToggleProgressBarRequest(False)

            If exs.Count > 0 Then
                UIHelper.WarnError(exs, String.Format("error al eliminar {0} certificats", failed))
            End If

            If succeeded > 0 Then MyBase.RefreshRequest(Me, New MatEventArgs())
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

