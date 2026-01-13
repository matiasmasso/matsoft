Public Class Frm_CondicioCapitol

    Private _CondicioCapitol As DTOCondicio.Capitol
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOCondicio.Capitol)
        MyBase.New()
        Me.InitializeComponent()
        _CondicioCapitol = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.CondicioCapitol.Load(_CondicioCapitol, exs) Then
            Dim oLang As DTOLang = Current.Session.Lang
            With _CondicioCapitol

                Me.Text = oLang.Tradueix("Parrafo de ", "Paragraf de ", "Paragraph from ") & .Parent.Title.Tradueix(oLang)

                TextBoxCondicioEsp.Text = .Parent.Title.Esp
                TextBoxCaptionEsp.Text = .Caption.Esp
                TextBoxTextEsp.Text = .Text.Esp

                TextBoxCondicioCat.Text = .Parent.Title.Cat
                TextBoxCaptionCat.Text = .Caption.Cat
                TextBoxTextCat.Text = .Text.Cat

                TextBoxCondicioEng.Text = .Parent.Title.Eng
                TextBoxCaptionEng.Text = .Caption.Eng
                TextBoxTextEng.Text = .Text.Eng

                TextBoxCondicioPor.Text = .Parent.Title.Por
                TextBoxCaptionPor.Text = .Caption.Por
                TextBoxTextPor.Text = .Text.Por

                Xl_UsrLog1.Load(.usrLog)

                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxCaptionEsp.TextChanged,
         TextBoxCaptionEsp.TextChanged,
          TextBoxTextEsp.TextChanged,
        TextBoxCaptionCat.TextChanged,
         TextBoxCaptionCat.TextChanged,
          TextBoxTextCat.TextChanged,
        TextBoxCaptionEng.TextChanged,
         TextBoxCaptionEng.TextChanged,
          TextBoxTextEng.TextChanged,
        TextBoxCaptionPor.TextChanged,
         TextBoxCaptionPor.TextChanged,
          TextBoxTextPor.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _CondicioCapitol
            With .Caption
                .Esp = TextBoxCaptionEsp.Text
                .Cat = TextBoxCaptionCat.Text
                .Eng = TextBoxCaptionEng.Text
                .Por = TextBoxCaptionPor.Text
            End With

            With .Text
                .Esp = TextBoxTextEsp.Text
                .Cat = TextBoxTextCat.Text
                .Eng = TextBoxTextEng.Text
                .Por = TextBoxTextPor.Text
            End With

            .UsrLog.UsrLastEdited = Current.Session.User.ToGuidNom
        End With

        Dim exs As New List(Of Exception)
        If Await FEB.CondicioCapitol.Update(_CondicioCapitol, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_CondicioCapitol))
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
            If Await FEB.CondicioCapitol.Delete(_CondicioCapitol, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_CondicioCapitol))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


