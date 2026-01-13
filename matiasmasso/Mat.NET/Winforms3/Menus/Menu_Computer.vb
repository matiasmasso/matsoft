Imports IronBarCode

Public Class Menu_Computer
    Inherits Menu_Base

    Private _Computers As List(Of DTOComputer)
    Private _Computer As DTOComputer

    Public Sub New(ByVal oComputers As List(Of DTOComputer))
        MyBase.New()
        _Computers = oComputers
        If _Computers IsNot Nothing Then
            If _Computers.Count > 0 Then
                _Computer = _Computers.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oComputer As DTOComputer)
        MyBase.New()
        _Computer = oComputer
        _Computers = New List(Of DTOComputer)
        If _Computer IsNot Nothing Then
            _Computers.Add(_Computer)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_CopyUrl())
        MyBase.AddMenuItem(MenuItem_SaveQR())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _Computers.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyUrl() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar url"
        oMenuItem.Enabled = _Computers.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyUrl
        Return oMenuItem
    End Function

    Private Function MenuItem_SaveQR() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "desar codi QR"
        oMenuItem.Enabled = _Computers.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_SaveQRCode
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
        Dim oFrm As New Frm_Computer(_Computer)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub
    Private Sub Do_CopyUrl(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyToClipboard(_Computer.Url)
    End Sub

    Private Sub Do_SaveQRCode(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = String.Format("Desar codi QR per {0}", _Computer.Nom)
            .Filter = "imatges Jpg|*.jpg|tots els fitxers|*.*"
            .FileName = String.Format("QR {0}.jpg", _Computer.Nom)
            If .ShowDialog = DialogResult.OK Then
                'Dim Qrcode = IronBarCode.QRCodeWriter.CreateQrCode(_Computer.Url)
                'Qrcode.SaveAsJpeg(.FileName)
            End If
        End With
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Computer.Delete(exs, _Computers.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

