Public Class Frm_Survey

    Private _Survey As DTOSurvey
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOSurvey)
        MyBase.New()
        Me.InitializeComponent()
        _Survey = value
        BLL.BLLSurvey.Load(_Survey)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Survey
            DateTimePickerFchFrom.Value = .FchFrom

            If .FchTo > DateTimePickerFchTo.MinDate Then
                DateTimePickerFchTo.Value = .FchTo
            End If

            TextBoxTitle.Text = .Title
            TextBoxText.Text = .Text
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxTitle.TextChanged,
         TextBoxText.TextChanged,
          DateTimePickerFchFrom.ValueChanged,
           DateTimePickerFchTo.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Survey
            .FchFrom = DateTimePickerFchFrom.Value
            .FchTo = DateTimePickerFchTo.Value
            .Title = TextBoxTitle.Text
            .Text = TextBoxText.Text
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLSurvey.Update(_Survey, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Survey))
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
            If BLL.BLLSurvey.Delete(_Survey, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Survey))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class