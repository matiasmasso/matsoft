Public Class Frm_Content
    Private _Content As DTOContent = Nothing
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Tabs
        General
        Title
        Excerpt
        Content
        Url
    End Enum

    Public Sub New(ByVal oContent As DTOContent)
        MyBase.New()
        Me.InitializeComponent()
        _Content = oContent
    End Sub

    Private Sub Frm_Content_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Content.Load(exs, _Content) Then
            refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub refresca()
        Dim exs As New List(Of Exception)
        With _Content
            Xl_LookupTextboxButtonUrl.Text = .Url.AbsoluteUrl(Current.Session.Lang)
            Xl_LangsTextTitol.Load(.Title)
            Xl_LangsTextExcerpt.Load(.Excerpt)
            Xl_LangsTextContingut.Load(.Text)
            Xl_LangsTextUrl.Load(.UrlSegment)
            CheckBoxVisible.Checked = .Visible
            TextBoxVisitCount.Text = .VisitCount
            ButtonDel.Enabled = Not .IsNew


            _AllowEvents = True
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        CheckBoxVisible.CheckedChanged,
         Xl_LangsTextTitol.AfterUpdate,
          Xl_LangsTextExcerpt.AfterUpdate,
           Xl_LangsTextContingut.AfterUpdate,
            Xl_LangsTextUrl.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Content
            .Title = Xl_LangsTextTitol.Value
            .Excerpt = Xl_LangsTextExcerpt.Value
            .Text = Xl_LangsTextContingut.Value
            .UrlSegment = Xl_LangsTextUrl.Value
            .Visible = CheckBoxVisible.Checked
        End With

        If Await FEB2.Content.Update(exs, _Content) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Content))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar el contingut")
        End If
    End Sub



    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest contingut?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Content.Delete(exs, _Content) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar el contingut")
            End If
        End If
    End Sub

    Private Sub Xl_LookupTextboxButtonUrl_onLookUpRequest(sender As Object, e As MatEventArgs) Handles Xl_LookupTextboxButtonUrl.onLookUpRequest
        TabControl1.SelectedIndex = Tabs.Url
    End Sub

    Private Sub Xl_LangsTextUrl_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LangsTextUrl.AfterUpdate
        Xl_LookupTextboxButtonUrl.Text = MmoUrl.Factory(True, "content", Xl_LangsTextUrl.Value.Tradueix(Current.Session.Lang))
    End Sub
End Class
