Public Class Frm_BlogPost

    Private _BlogPost As DTOBlogPost
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOBlogPost)
        MyBase.New()
        Me.InitializeComponent()
        _BlogPost = value
    End Sub

    Private Sub Frm_BlogPost_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.BlogPost.Load(exs, _BlogPost) Then
            With _BlogPost
                DateTimePicker1.Value = .fch
                Xl_LangsTextTitol.Load(.Title)
                Xl_LangsTextExcerpt.Load(.Excerpt)
                Xl_LangsTextContingut.Load(.Text)
                Xl_LangsTextUrl.Load(.UrlSegment)
                Xl_Image1.LoadAsync(.ThumbnailUrl)
                CheckBoxVisible.Checked = .visible
                RefreshDetails()
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_LangsTextTitol.AfterUpdate,
         Xl_LangsTextExcerpt.AfterUpdate,
          Xl_LangsTextContingut.AfterUpdate,
           Xl_LangsTextUrl.AfterUpdate,
            DateTimePicker1.ValueChanged,
             Xl_Image1.AfterUpdate,
              CheckBoxVisible.CheckedChanged

        If _AllowEvents Then
            RefreshDetails()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub RefreshDetails()
        Dim oLang = Current.Session.Lang
        TextBoxTitol.Text = Xl_LangsTextTitol.Value.Tradueix(oLang)
        TextBoxExcerpt.Text = Xl_LangsTextExcerpt.Value.Tradueix(oLang)
        TextBoxContingut.Text = Xl_LangsTextContingut.Value.Tradueix(oLang)
        TextBoxUrl.Text = _BlogPost.Url().AbsoluteUrl(oLang)
        Xl_UsrLog1.Load(_BlogPost.UsrLog)
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _BlogPost
            .Fch = DateTimePicker1.Value
            .Thumbnail = LegacyHelper.ImageHelper.Converter(Xl_Image1.Bitmap)
            .Title = Xl_LangsTextTitol.Value
            .Excerpt = Xl_LangsTextExcerpt.Value
            .Text = Xl_LangsTextContingut.Value
            .UrlSegment = Xl_LangsTextUrl.Value
            .visible = CheckBoxVisible.Checked
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.BlogPost.Upload(exs, _BlogPost) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_BlogPost))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs, "error al desar el post")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.BlogPost.Delete(exs, _BlogPost) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_BlogPost))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub BrowseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BrowseToolStripMenuItem.Click
        Dim sUrl As String = _BlogPost.Url().AbsoluteUrl(DTOLang.ESP)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub CopyLinkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyLinkToolStripMenuItem.Click
        Dim sUrl As String = _BlogPost.Url().AbsoluteUrl(DTOLang.ESP)
        Clipboard.SetDataObject(sUrl, True)
    End Sub
End Class


