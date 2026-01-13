Public Class Frm_AreaProvincia
    Private _AreaProvincia As DTOAreaProvincia
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOAreaProvincia)
        MyBase.New()
        Me.InitializeComponent()
        _AreaProvincia = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _AreaProvincia
            TextBoxNom.Text = .ToString
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
        With _AreaProvincia
            .Nom = TextBoxNom.Text
        End With
        Dim exs As New List(Of Exception)
        If BLL.BLLAreaProvincia.Update(_AreaProvincia, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_AreaProvincia))
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
            If BLL.BLLAreaProvincia.Delete(_AreaProvincia, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_AreaProvincia))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


