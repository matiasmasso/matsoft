Public Class Frm_Cur
    Private _Cur As Cur
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As Cur)
        MyBase.New()
        Me.InitializeComponent()
        _Cur = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Cur
            TextBoxId.Text = .id
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub TextBoxId_TextChanged(sender As Object, e As EventArgs) Handles TextBoxId.TextChanged
        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs as New List(Of exception)

        If CurLoader.Update(_Cur, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cur))
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar la editorial")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox("eliminem la divisa?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If CurLoader.Delete(_Cur, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cur))
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar la editorial")
            End If
        End If
    End Sub
End Class


