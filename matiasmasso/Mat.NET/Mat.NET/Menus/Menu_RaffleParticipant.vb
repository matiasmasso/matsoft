Public Class Menu_RaffleParticipant
    Private _RaffleParticipant As DTORaffleParticipant

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oRaffleParticipant As DTORaffleParticipant)
        MyBase.New()
        _RaffleParticipant = oRaffleParticipant
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_MailParticipationConfirmation(), _
        MenuItem_CopyLinkToMailParticipationConfirmation(), _
        MenuItem_SetAsWinner(), _
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

    Private Function MenuItem_MailParticipationConfirmation() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "email confirmació participació"
        oMenuItem.Image = My.Resources.MailSobreGroc
        AddHandler oMenuItem.Click, AddressOf Do_MailParticipationConfirmation
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLinkToMailParticipationConfirmation() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "copiar enllaç a email confirmació participació"
        AddHandler oMenuItem.Click, AddressOf Do_CopyLinkToMailParticipationConfirmation
        Return oMenuItem
    End Function

    Private Function MenuItem_SetAsWinner() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "designar com guanyador"
        oMenuItem.Image = My.Resources.candau
        oMenuItem.Visible = BLL.BLLSession.Current.User.Rol.IsSuperAdmin
        AddHandler oMenuItem.Click, AddressOf Do_SetAsWinner
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
        Dim oFrm As New Frm_RaffleParticipant(_RaffleParticipant)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_MailParticipationConfirmation()
        Dim exs as New List(Of exception)
        If BLL.BLLMail.RaffleParticipation(_RaffleParticipant, exs) Then
            MsgBox("enviat email confirmant participació al sorteig a " & _RaffleParticipant.User.EmailAddress, MsgBoxStyle.Information)
        Else
            UIHelper.WarnError(exs, "error al enviar email a " & _RaffleParticipant.User.EmailAddress)
        End If
    End Sub

    Private Sub Do_CopyLinkToMailParticipationConfirmation()
        Dim sUrl As String = BLL.BLLMailing.BodyUrl(BLL.BLLMailing.Templates.RaffleParticipation, _RaffleParticipant.Guid.ToString)
        UIHelper.CopyLink(sUrl)
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _RaffleParticipant.User.EmailAddress & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If BLL.BLLRaffleParticipant.Delete(_RaffleParticipant, exs) Then
                MsgBox(" " & _RaffleParticipant.User.EmailAddress & " eliminat", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_SetAsWinner()
        Dim exs as New List(Of exception)
        If BLL.BLLRaffleParticipant.SetAsWinner(_RaffleParticipant, exs) Then
            RefreshRequest(Nothing, New MatEventArgs(_RaffleParticipant))
        Else
            UIHelper.WarnError( exs)
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class


