Imports SixLabors.ImageSharp

Public Class Menu_MediaResource

    Inherits Menu_Base

    Private _MediaResources As List(Of DTOMediaResource)
    Private _MediaResource As DTOMediaResource
    Private _ShowProgress As ProgressBarHandler


    Public Sub New(ByVal oMediaResources As List(Of DTOMediaResource), ShowProgress As ProgressBarHandler)
        MyBase.New()
        _MediaResources = oMediaResources
        _ShowProgress = ShowProgress
        If _MediaResources IsNot Nothing Then
            If _MediaResources.Count > 0 Then
                _MediaResource = _MediaResources.First
            End If
        End If
    End Sub

    Public Shadows Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_CopyLink(),
        MenuItem_Download(),
        MenuItem_Delete()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _MediaResources.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Download() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Exportar"
        AddHandler oMenuItem.Click, AddressOf Do_Download
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Enabled = _MediaResources.Count = 1
        oMenuItem.Text = "Copiar..."
        oMenuItem.DropDownItems.Add("enllaç", Nothing, AddressOf Do_CopyLink)
        oMenuItem.DropDownItems.Add("identificador", Nothing, AddressOf Do_CopyGuid)
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
        Dim oFrm As New Frm_MediaResource(_MediaResource)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = FEB2.MediaResource.Url(_MediaResource, True)
        UIHelper.CopyLink(sUrl)
    End Sub

    Private Sub Do_CopyGuid(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyToClipboard(_MediaResource.Guid.ToString)
    End Sub

    Private Async Sub Do_Download(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oDlg As New SaveFileDialog()
        With oDlg
            .Title = "Desar recurs"
            .Filter = UIHelper.SavefileDialogFilter(_MediaResource.Mime)
            .FileName = DTOMediaResource.FriendlyName(_MediaResource)
            If .ShowDialog Then
                MyBase.ToggleProgressBarRequest(True)
                Dim url = FEB2.MediaResource.Url(_MediaResource, True)
                Dim oImage = Await MatHelperStd.ImageHelper.DownloadFromWebsiteAsync(exs, url)
                If exs.Count = 0 Then
                    If oImage Is Nothing Then
                        MyBase.ToggleProgressBarRequest(False)
                        UIHelper.WarnError("la imatge es buida")
                    Else
                        Try
                            Select Case _MediaResource.Mime
                                Case MimeCods.Jpg
                                    Await oImage.SaveAsJpegAsync(.FileName)
                                Case MimeCods.Png
                                    Await oImage.SaveAsPngAsync(.FileName)
                                Case MimeCods.Gif
                                    Await oImage.SaveAsGifAsync(.FileName)
                            End Select
                            MyBase.ToggleProgressBarRequest(False)

                        Catch ex As Exception
                            MyBase.ToggleProgressBarRequest(False)
                            UIHelper.WarnError(exs)
                        End Try
                    End If
                Else
                    MyBase.ToggleProgressBarRequest(False)
                    UIHelper.WarnError(exs)
                End If
            End If
        End With


    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sQuery As String = IIf(_MediaResources.Count = 1, "Eliminem aquest recurs?", String.Format("Eliminem aquest {0} recursos?", _MediaResources.Count))
        Dim rc As MsgBoxResult = MsgBox(sQuery, MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.MediaResources.Delete(exs, _MediaResources) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub


End Class

