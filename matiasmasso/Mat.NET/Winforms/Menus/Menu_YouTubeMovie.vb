Public Class Menu_YouTubeMovie

    Inherits Menu_Base

    Private _YouTubeMovies As List(Of DTOYouTubeMovie)
    Private _YouTubeMovie As DTOYouTubeMovie

    Public Sub New(ByVal oYouTubeMovies As List(Of DTOYouTubeMovie))
        MyBase.New()
        _YouTubeMovies = oYouTubeMovies
        If _YouTubeMovies IsNot Nothing Then
            If _YouTubeMovies.Count > 0 Then
                _YouTubeMovie = _YouTubeMovies.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oYouTubeMovie As DTOYouTubeMovie)
        MyBase.New()
        _YouTubeMovie = oYouTubeMovie
        _YouTubeMovies = New List(Of DTOYouTubeMovie)
        If _YouTubeMovie IsNot Nothing Then
            _YouTubeMovies.Add(_YouTubeMovie)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Play())
        MyBase.AddMenuItem(MenuItem_CopyLink())
        MyBase.AddMenuItem(MenuItem_CopyPlugin())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _YouTubeMovies.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Play() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Visualitzar"
        oMenuItem.Enabled = _YouTubeMovies.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Play
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Enabled = _YouTubeMovies.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyPlugin() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar plugin"
        oMenuItem.Enabled = _YouTubeMovies.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyPlugin
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
        Dim oFrm As New Frm_Youtube(_YouTubeMovie)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub Do_Play()
        Dim sUrl As String = DTOYouTubeMovie.Url_YouTubeSite(_YouTubeMovie)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub Do_CopyLink()
        Dim sUrl As String = DTOYouTubeMovie.Url_YouTubeSite(_YouTubeMovie)
        Clipboard.SetDataObject(sUrl, True)
    End Sub

    Private Sub Do_CopyPlugin()
        Dim sb As New Text.StringBuilder

        sb.AppendLine("<div class='video-responsive'>")
        sb.AppendLine(String.Format("    <iframe src='{0}' frameborder='0' allow='accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture' allowfullscreen></iframe>", DTOYouTubeMovie.UrlEmbedPrefix & _YouTubeMovie.YoutubeId))
        sb.AppendLine("</div>")
        Dim src = sb.ToString
        Clipboard.SetDataObject(src, True)
    End Sub


    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.YouTubeMovie.Delete(_YouTubeMovies.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


