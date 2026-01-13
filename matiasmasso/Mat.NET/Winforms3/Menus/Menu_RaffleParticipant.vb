Public Class Menu_RaffleParticipant
    Private _RaffleParticipants As List(Of DTORaffleParticipant)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oRaffleParticipants As IEnumerable(Of DTORaffleParticipant))
        MyBase.New()
        _RaffleParticipants = oRaffleParticipants
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
        oMenuItem.Enabled = _RaffleParticipants.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_MailParticipationConfirmation() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "email confirmació participació"
        oMenuItem.Image = My.Resources.MailSobreGroc
        oMenuItem.Enabled = _RaffleParticipants.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_MailParticipationConfirmation
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLinkToMailParticipationConfirmation() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "copiar enllaç a email confirmació participació"
        oMenuItem.Enabled = _RaffleParticipants.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyLinkToMailParticipationConfirmation
        Return oMenuItem
    End Function

    Private Function MenuItem_SetAsWinner() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "designar com guanyador"
        oMenuItem.Image = My.Resources.candau
        oMenuItem.Enabled = _RaffleParticipants.Count = 1
        oMenuItem.Visible = Current.Session.User.Rol.IsSuperAdmin
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
        Dim oFrm As New Frm_RaffleParticipant(_RaffleParticipants.First)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_MailParticipationConfirmation()
        Dim exs As New List(Of Exception)
        Dim oMailMessage = Await FEB.RaffleParticipant.RaffleParticipationMailMessage(exs, GlobalVariables.Emp, _RaffleParticipants.First)
        If Await FEB.MailMessage.Send(exs, Current.Session.User, oMailMessage) Then
            MsgBox("enviat email confirmant participació al sorteig a " & _RaffleParticipants.First.User.EmailAddress, MsgBoxStyle.Information)
        Else
            UIHelper.WarnError(exs, "error al enviar email a " & _RaffleParticipants.First.User.EmailAddress)
        End If
    End Sub

    Private Sub Do_CopyLinkToMailParticipationConfirmation()
        Dim sUrl As String = FEB.Mailing.BodyUrl(DTODefault.MailingTemplates.RaffleParticipation, _RaffleParticipants.First.Guid.ToString())
        UIHelper.CopyLink(sUrl)
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _RaffleParticipants.Count & " participants?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.RaffleParticipants.Delete(exs, _RaffleParticipants) Then
                MsgBox("Eliminats " & _RaffleParticipants.Count & " participants", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar els participants")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Async Sub Do_SetAsWinner()
        Dim exs As New List(Of Exception)
        If Await FEB.RaffleParticipant.SetAsWinner(_RaffleParticipants.First, exs) Then
            RefreshRequest(Nothing, New MatEventArgs(_RaffleParticipants.First))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class


