Public Class Frm_SearchMisc
    Private _SearchMisc As DTOSearchMisc
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOSearchMisc)
        MyBase.New()
        Me.InitializeComponent()
        _SearchMisc = value
        BLL.BLLSearchMisc.Load(_SearchMisc)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _SearchMisc
            DateTimePicker1.Value = .Fch
            TextBoxUrl.Text = .UrlFriendlySegment
            Xl_LangsTextTitol.Load(.Title)
            Xl_LangsTextExtracte.Load(.Excerpt)
            TextBoxTags.Text = TextHelper.StringListToMultiline(.Keywords)
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxTags.TextChanged,
         DateTimePicker1.ValueChanged,
          Xl_LangsTextTitol.AfterUpdate,
           Xl_LangsTextExtracte.AfterUpdate,
            TextBoxUrl.TextChanged,
             DateTimePicker1.ValueChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _SearchMisc
            .Title = Xl_LangsTextTitol.Value
            .Excerpt = Xl_LangsTextExtracte.Value
            .Fch = DateTimePicker1.Value
            .UrlFriendlySegment = TextBoxUrl.Text
            .Keywords = TextHelper.StringListFromMultiline(TextBoxTags.Text)
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLSearchMisc.Update(_SearchMisc, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_SearchMisc))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLSearchMisc.Delete(_SearchMisc, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_SearchMisc))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


