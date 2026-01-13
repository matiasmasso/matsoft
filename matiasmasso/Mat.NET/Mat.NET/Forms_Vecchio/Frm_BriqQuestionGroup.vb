Public Class Frm_BriqQuestionGroup
    Private _BriqQuestionGroup As BriqQuestionGroup
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As BriqQuestionGroup)
        MyBase.New()
        Me.InitializeComponent()
        _BriqQuestionGroup = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _BriqQuestionGroup
            TextBoxTitle.Text = .Title
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxTitle.TextChanged
        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _BriqQuestionGroup
            .Title = TextBoxTitle.Text
        End With

        Dim exs as New List(Of exception)
        If BriqQuestionGrouploader.Update(_BriqQuestionGroup, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_BriqQuestionGroup))
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
            If BriqQuestionGrouploader.Delete(_BriqQuestionGroup, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_BriqQuestionGroup))
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar")
            End If
        End If
    End Sub
End Class

