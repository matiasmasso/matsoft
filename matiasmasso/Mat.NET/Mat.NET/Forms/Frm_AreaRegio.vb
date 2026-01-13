Public Class Frm_AreaRegio
    Private _AreaRegio As DTOAreaRegio
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOAreaRegio)
        MyBase.New()
        Me.InitializeComponent()
        _AreaRegio = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _AreaRegio
            TextBoxNom.Text = .Nom
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged
        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _AreaRegio
            .Nom = TextBoxNom.Text
        End With
        Dim exs As New List(Of Exception)
        If BLL.BLLAreaRegio.Update(_AreaRegio, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_AreaRegio))
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
            If BLL.BLLAreaRegio.Delete(_AreaRegio, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_AreaRegio))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


