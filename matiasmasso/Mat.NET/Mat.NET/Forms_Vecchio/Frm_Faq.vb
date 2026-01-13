

Public Class Frm_Faq
    Private mFaq As Faq
    Private mRols As List(Of DTORol)
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oFaq As Faq)
        MyBase.New()
        Me.InitializeComponent()
        mFaq = oFaq
        Refresca()
        ButtonDel.Enabled = (mFaq.Children(Rol.Ids.SuperUser).Count = 0)
    End Sub

    Private Sub Refresca()
        With mFaq
            ComboBoxAcces.SelectedIndex = CInt(.AccessLevel)
            TextBoxQuestion.Text = .Question.Text
            If .ExternalUrl > "" Then
                RadioButtonExternalUrl.Checked = True
                TextBoxExternalUrl.Text = .ExternalUrl
            Else
                RadioButtonText.Checked = True
                TextBoxAnswer.Text = .Answer.Text
            End If
            SetRadioButtons()
            ButtonRolsAllowed.Enabled = (.AccessLevel = Faq.AccessLevels.SegunRol)
        End With
        mAllowEvents = True
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
     TextBoxQuestion.TextChanged, _
      TextBoxAnswer.TextChanged, _
       RadioButtonExternalUrl.CheckedChanged, _
        RadioButtonText.CheckedChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub TextBoxExternalUrl_TextChanged(sender As Object, e As System.EventArgs) Handles TextBoxExternalUrl.TextChanged
        If mAllowEvents Then
            ButtonOk.Enabled = True
            ButtonExternalUrl.Enabled = TextBoxExternalUrl.Text > ""
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mFaq
            .AccessLevel = CurrentAccessLevel()
            .Question.Text = TextBoxQuestion.Text
            If mRols IsNot Nothing Then
                .Rols = mRols
            End If
            .Answer.Text = TextBoxAnswer.Text
            If RadioButtonExternalUrl.Checked Then
                .ExternalUrl = TextBoxExternalUrl.Text
            Else
                .ExternalUrl = ""
            End If
            .Update()
        End With
        RaiseEvent AfterUpdate(mFaq, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Function CurrentAccessLevel() As Faq.AccessLevels
        Dim iLevel As Integer = ComboBoxAcces.SelectedIndex
        Dim retVal As Faq.AccessLevels = CType(iLevel, Faq.AccessLevels)
        Return retVal
    End Function

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim oRetVal As MsgBoxResult = MsgBox("eliminem aquesta consulta?", MsgBoxStyle.OkCancel, "MAT.NET")
        If oRetVal = MsgBoxResult.Ok Then
            If mFaq.AllowDelete Then
                mFaq.Delete()
                RaiseEvent AfterUpdate(mFaq, EventArgs.Empty)
                Me.Close()
            Else
                MsgBox("cal eliminar primer les consultes que penjen d'ella", MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        End If
    End Sub

    Private Sub ButtonRolsAllowed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRolsAllowed.Click
        Dim oFrm As New Frm_Rols_Allowed(mFaq.Rols)
        AddHandler oFrm.AfterUpdate, AddressOf onRolsUpdated
        oFrm.Show()
    End Sub

    Private Sub onRolsUpdated(ByVal sender As Object, ByVal e As System.EventArgs)
        mRols = CType(sender, Rols)
        ButtonOk.Enabled = True
    End Sub


    Private Sub ComboBoxAcces_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxAcces.SelectedIndexChanged
        If mAllowEvents Then
            ButtonRolsAllowed.Enabled = (CurrentAccessLevel() = Faq.AccessLevels.SegunRol)
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub RadioButtonExternalUrl_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonExternalUrl.CheckedChanged
        If mAllowEvents Then SetRadioButtons()
    End Sub

    Private Sub SetRadioButtons()
        If RadioButtonText.Checked Then
            TextBoxExternalUrl.Enabled = False
            ButtonExternalUrl.Enabled = False
            TextBoxAnswer.Enabled = True
        Else
            TextBoxExternalUrl.Enabled = True
            ButtonExternalUrl.Enabled = TextBoxExternalUrl.Text > ""
            TextBoxAnswer.Enabled = False
        End If
    End Sub

    Private Sub ButtonExternalUrl_Click(sender As Object, e As System.EventArgs) Handles ButtonExternalUrl.Click
        Dim sUrl As String = TextBoxExternalUrl.Text
        UIHelper.ShowHtml(sUrl)
    End Sub
End Class