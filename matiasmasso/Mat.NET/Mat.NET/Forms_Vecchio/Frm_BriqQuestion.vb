Public Class Frm_BriqQuestion

    Private _BriqQuestion As BriqQuestion
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As BriqQuestion)
        MyBase.New()
        Me.InitializeComponent()
        _BriqQuestion = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _BriqQuestion
            TextBox1.Text = .Text
            RadioButtonTrue.Checked = .Answer = MaxiSrvr.TriState.Verdadero
            RadioButtonFalse.Checked = .Answer = MaxiSrvr.TriState.Falso
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBox1.TextChanged
        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _BriqQuestion
            .Text = TextBox1.Text
            If RadioButtonTrue.Checked Then .Answer = MaxiSrvr.TriState.Verdadero
            If RadioButtonFalse.Checked Then .Answer = MaxiSrvr.TriState.Falso
        End With

        Dim exs as New List(Of exception)
        If BriqQuestionLoader.Update(_BriqQuestion, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_BriqQuestion))
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BriqQuestionloader.Delete(_BriqQuestion, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_BriqQuestion))
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar")
            End If
        End If
    End Sub
End Class

