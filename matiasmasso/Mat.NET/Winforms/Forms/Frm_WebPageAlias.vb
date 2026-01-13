Public Class Frm_WebPageAlias

    Private _WebPageAlias As DTOWebPageAlias
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOWebPageAlias)
        MyBase.New()
        Me.InitializeComponent()
        _WebPageAlias = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.WebPageAlias.Load(_WebPageAlias, exs) Then
            With _WebPageAlias
                TextBoxUrlFrom.Text = .UrlFrom
                TextBoxUrlTo.Text = .UrlTo
                ComboBoxDomain.SelectedIndex = .domain
                ButtonOk.Enabled = False
                ButtonDel.Enabled = .UrlTo > ""
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxUrlFrom.TextChanged, TextBoxUrlTo.TextChanged, ComboBoxDomain.SelectedIndexChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _WebPageAlias
            .UrlFrom = TextBoxUrlFrom.Text
            .UrlTo = TextBoxUrlTo.Text
            .domain = ComboBoxDomain.SelectedIndex
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.WebPageAlias.Update(_WebPageAlias, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_WebPageAlias))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.WebPageAlias.Delete(_WebPageAlias, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_WebPageAlias))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


