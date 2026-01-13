Public Class Menu_FairGuest

    Private _FairGuest As DTOFairGuest

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oFairGuest As DTOFairGuest)
        MyBase.New()
        _FairGuest = oFairGuest
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_MailConfirmacioOutlook(),
        MenuItem_MailConfirmacioExchange(),
        MenuItem_Delete()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_MailConfirmacioOutlook() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "email confirmacio (Outlook)"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_MailConfirmacioOutlook
        Return oMenuItem
    End Function

    Private Function MenuItem_MailConfirmacioExchange() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "email confirmacio (Exchange)"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_MailConfirmacioExchange
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_FairGuest(_FairGuest)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_MailConfirmacioOutlook(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        'BLLMail.QuizConfirmation(BLLMailing.Templates.FairGuestConfirmation, Current.Session.User, exs, )

        Dim sSubject As String = "Confirmación registro programa compradores Ifema"
        Dim sBodyUrl As String = BLL.BLLMailing.BodyUrl(BLLMailing.Templates.FairGuestConfirmation, {_FairGuest.Guid.ToString})
        MatOutlook.NewMessage(sTo:=_FairGuest.Email, sCc:="", sSubject:=sSubject, sHtmlBody:=sBodyUrl)
    End Sub

    Private Sub Do_MailConfirmacioExchange(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If BLLMail.QuizConfirmation(Current.Session.Emp, BLLMailing.Templates.FairGuestConfirmation, _FairGuest.UserCreated, exs, {_FairGuest.Guid.ToString}) Then
            MsgBox("Confirmació enviada a " & _FairGuest.Email)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _FairGuest.LastName & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLFairGuest.Delete(_FairGuest, exs) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class


