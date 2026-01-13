Public Class Frm_LangSelection
    Private _allowEvents As Boolean
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Private Sub Frm_LangSelection_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oLang As DTOLang = Current.Session.Lang
        Select Case oLang.Id
            Case DTOLang.Ids.CAT
                RadioButtonCat.Checked = True
            Case DTOLang.Ids.ENG
                RadioButtonEng.Checked = True
            Case DTOLang.Ids.POR
                RadioButtonPor.Checked = True
            Case Else
                RadioButtonEsp.Checked = True
        End Select
        _allowEvents = True
    End Sub

    Private Function SelectedLang() As DTOLang
        Dim retval As DTOLang = Nothing
        If RadioButtonCat.Checked Then
            retval = DTOLang.CAT
        ElseIf RadioButtonEng.Checked Then
            retval = DTOLang.ENG
        ElseIf RadioButtonPor.Checked Then
            retval = DTOLang.POR
        Else
            retval = DTOLang.ESP
        End If
        Return retval
    End Function

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim oLang As DTOLang = SelectedLang()
        RaiseEvent AfterUpdate(Me, New MatEventArgs(oLang))
        Me.Close()
    End Sub



    Private Sub RadioButton_Click(sender As Object, e As EventArgs) Handles _
        RadioButtonEsp.Click,
        RadioButtonCat.Click,
        RadioButtonEng.Click,
        RadioButtonPor.Click

        If _allowEvents Then
            ButtonOk.Enabled = True
        End If

    End Sub
End Class