Public Class Menu_Contract

    Inherits Menu_Base

    Private _Contracts As List(Of DTOContract)
    Private _Contract As DTOContract

    Public Sub New(ByVal oContracts As List(Of DTOContract))
        MyBase.New()
        _Contracts = oContracts
        If _Contracts IsNot Nothing Then
            If _Contracts.Count > 0 Then
                _Contract = _Contracts.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oContract As DTOContract)
        MyBase.New()
        _Contract = oContract
        _Contracts = New List(Of DTOContract)
        If _Contract IsNot Nothing Then
            _Contracts.Add(_Contract)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Browse())
        MyBase.AddMenuItem(MenuItem_CopyLink())
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Browse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Veure document"
        oMenuItem.Enabled = _Contracts.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Browse
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Enabled = _Contracts.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Fitxa"
        oMenuItem.Enabled = _Contracts.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Enabled = Current.Session.User.Rol.IsMainboard
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Browse(ByVal sender As Object, ByVal e As System.EventArgs)
        If _Contract.docFile Is Nothing Then
            UIHelper.WarnError("falta el document del contracte")
        Else
            Dim sUrl As String = FEB.DocFile.DownloadUrl(_Contract.docFile, True)
            Process.Start(sUrl)
        End If
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(_Contract.DocFile)
    End Sub

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Contract(_Contract)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Contract.Delete(_Contracts.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


