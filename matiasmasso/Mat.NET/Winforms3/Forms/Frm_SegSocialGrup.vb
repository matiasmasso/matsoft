Public Class Frm_SegSocialGrup

    Private _SegSocialGrup As DTOSegSocialGrup
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOSegSocialGrup)
        MyBase.New()
        Me.InitializeComponent()
        _SegSocialGrup = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.SegSocialGrup.Load(_SegSocialGrup, exs) Then
            With _SegSocialGrup
                NumericUpDown1.Value = .Id
                TextBox1.Text = .Nom
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBox1.TextChanged,
         NumericUpDown1.ValueChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _SegSocialGrup
            .Id = NumericUpDown1.Value
            .Nom = TextBox1.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.SegSocialGrup.Update(_SegSocialGrup, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_SegSocialGrup))
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
            If Await FEB.SegSocialGrup.Delete(_SegSocialGrup, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_SegSocialGrup))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


