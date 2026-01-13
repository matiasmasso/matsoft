Public Class Menu_Ftpserver
    Inherits Menu_Base

    Private _Ftpservers As List(Of DTOFtpserver)
    Private _Ftpserver As DTOFtpserver

    Public Sub New(ByVal oFtpservers As List(Of DTOFtpserver))
        MyBase.New()
        _Ftpservers = oFtpservers
        If _Ftpservers IsNot Nothing Then
            If _Ftpservers.Count > 0 Then
                _Ftpserver = _Ftpservers.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oFtpserver As DTOFtpserver)
        MyBase.New()
        _Ftpserver = oFtpserver
        _Ftpservers = New List(Of DTOFtpserver)
        If _Ftpserver IsNot Nothing Then
            _Ftpservers.Add(_Ftpserver)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Browse())
        MyBase.AddMenuItem(MenuItem_CopyUser())
        MyBase.AddMenuItem(MenuItem_CopyPwd())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _Ftpservers.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Browse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Explorar"
        oMenuItem.Enabled = _Ftpservers.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Browse
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyUser() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar usuari"
        oMenuItem.Enabled = _Ftpservers.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyUser
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyPwd() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar password"
        oMenuItem.Enabled = _Ftpservers.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyPwd
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
        Dim oFrm As New Frm_Ftpserver(_Ftpserver)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Browse(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.Ftpserver.Load(exs, _Ftpserver) Then
            If _Ftpserver.Servername = "" Then
                exs.Add(New Exception("aquest contacte no té vcap servidor Ftp configurat"))
            Else
                Process.Start(_Ftpserver.Servername)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_CopyUser(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.Ftpserver.Load(exs, _Ftpserver) Then
            If _Ftpserver.Servername = "" Then
                exs.Add(New Exception("aquest contacte no té vcap servidor Ftp configurat"))
            Else
                UIHelper.CopyToClipboard(_Ftpserver.Username, "usuari copiat al portapapers")
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_CopyPwd(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.Ftpserver.Load(exs, _Ftpserver) Then
            If _Ftpserver.Servername = "" Then
                exs.Add(New Exception("aquest contacte no té vcap servidor Ftp configurat"))
            Else
                UIHelper.CopyToClipboard(_Ftpserver.Password, "password copiat al portapapers")
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest servidor?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Ftpserver.Delete(exs, _Ftpservers.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el servidor Ftp")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

