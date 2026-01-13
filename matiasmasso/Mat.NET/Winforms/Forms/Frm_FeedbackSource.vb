Public Class Frm_FeedbackSource

    Private _FeedbackSource As DTOFeedback.SourceClass
    Private TabLoaded As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Enum Tabs
        General
        Feedbacks
    End Enum

    Public Sub New(value As DTOFeedback.SourceClass)
        MyBase.New()
        Me.InitializeComponent()
        _FeedbackSource = value
    End Sub

    Private Sub Frm_FeedbackSource_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.FeedbackSource.Load(exs, _FeedbackSource) Then
            With _FeedbackSource
                TextBoxNom.Text = .Nom
                ButtonOk.Enabled = .isNew
                ButtonDel.Enabled = Not .isNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _FeedbackSource
            .Nom = TextBoxNom.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB2.FeedbackSource.Update(exs, _FeedbackSource) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_FeedbackSource))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
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
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB2.FeedbackSource.Delete(exs, _FeedbackSource) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_FeedbackSource))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Feedbacks
                If Not TabLoaded Then
                    Dim exs As New List(Of Exception)
                    Dim items = Await FEB2.Feedbacks.All(exs, _FeedbackSource)
                    If exs.Count = 0 Then
                        Xl_SourceFeedbacks1.Load(items)
                        TabLoaded = True
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub
End Class


