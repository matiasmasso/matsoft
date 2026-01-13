Public Class Menu_Raffle
    Private _Raffle As DTORaffle

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oRaffle As DTORaffle)
        MyBase.New()
        _Raffle = oRaffle
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Dim exs As New List(Of Exception)
        FEB2.Raffle.Load(_Raffle, exs)
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_WebPlay(),
        MenuItem_CopyLinkToPlay(),
        MenuItem_WebZoom(),
        MenuItem_CopyLinkToZoom(),
        MenuItem_ExcelParticipantsList(),
        MenuItem_CopyLinkToReminder(),
        MenuItem_GetRandomWinner(),
        MenuItem_SetRandomWinner(),
        MenuItem_RemoveWinner(),
        MenuItem_SetAllPendingWinners(),
        MenuItem_MailWinnerCongrats(),
        MenuItem_DistributorNotification(),
        MenuItem_DeliverPrize(),
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

    Private Function MenuItem_WebPlay() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "participar"
        AddHandler oMenuItem.Click, AddressOf Do_WebPlay
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLinkToPlay() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç a participar"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLinkToPlay
        Return oMenuItem
    End Function

    Private Function MenuItem_WebZoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "fitxa web"
        AddHandler oMenuItem.Click, AddressOf Do_WebZoom
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLinkToZoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç a la fitxa"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLinkToZoom
        Return oMenuItem
    End Function

    Private Function MenuItem_ExcelParticipantsList() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Llistat de participants"
        oMenuItem.Image = My.Resources.Excel
        AddHandler oMenuItem.Click, AddressOf Do_ExcelParticipantsList
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLinkToReminder() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç a recordatori"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLinkToRemainder
        Return oMenuItem
    End Function

    Private Function MenuItem_GetRandomWinner() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Extreu guanyador a l'atzar"
        AddHandler oMenuItem.Click, AddressOf Do_GetRandomWinner
        Return oMenuItem
    End Function

    Private Function MenuItem_SetRandomWinner() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Assignar un guanyador a l'atzar"
        AddHandler oMenuItem.Click, AddressOf Do_SetRandomWinner
        Return oMenuItem
    End Function

    Private Function MenuItem_RemoveWinner() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retira el guanyador"
        oMenuItem.Image = My.Resources.aspa
        AddHandler oMenuItem.Click, AddressOf Do_RemoveWinner
        Return oMenuItem
    End Function

    Private Function MenuItem_SetAllPendingWinners() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "assigna els guanyadors pendents de tots els sortejos"
        AddHandler oMenuItem.Click, AddressOf Do_SetAllPendingWinners
        Return oMenuItem
    End Function

    Private Function MenuItem_MailWinnerCongrats() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "envia missatge al guanyador"
        oMenuItem.Image = My.Resources.MailSobreGrocBlau
        AddHandler oMenuItem.Click, AddressOf Do_MailWinnerCongrats_Outlook
        Return oMenuItem
    End Function

    Private Function MenuItem_DistributorNotification() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "envia missatge al distribuidor"
        oMenuItem.Image = My.Resources.star
        AddHandler oMenuItem.Click, AddressOf Do_RaffleDistributorNotification_Outlook
        Return oMenuItem
    End Function

    Private Function MenuItem_DeliverPrize() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "envia el premi"
        oMenuItem.Image = My.Resources.star_green
        oMenuItem.Enabled = (_Raffle.FchDistributorReaction <> Nothing And _Raffle.FchDelivery = Nothing)
        AddHandler oMenuItem.Click, AddressOf Do_DeliverPrize
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
        Dim oFrm As New Frm_Raffle(_Raffle)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _Raffle.Title & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Raffle.Delete(_Raffle, exs) Then
                MsgBox(" " & _Raffle.Title & " eliminat", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_WebPlay(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.ShowHtml(FEB2.Raffle.PlayUrl(_Raffle, True))
    End Sub

    Private Sub Do_CopyLinkToPlay(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(FEB2.Raffle.PlayUrl(_Raffle, True))
    End Sub

    Private Sub Do_WebZoom(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.ShowHtml(FEB2.Raffle.ZoomUrl(_Raffle, True))
    End Sub

    Private Sub Do_CopyLinkToZoom(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(FEB2.Raffle.ZoomUrl(_Raffle, True))
    End Sub

    Private Sub Do_CopyLinkToRemainder(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(FEB2.Raffle.MailReminderUrl(_Raffle))
    End Sub

    Private Async Sub Do_GetRandomWinner()
        Dim exs As New List(Of Exception)
        If FEB2.Raffle.Load(_Raffle, exs) Then
            Dim oValidParticipants = Await FEB2.RaffleParticipants.Valids(_Raffle, exs)
            Dim oWinner = DTORaffle.GetRandomWinner(oValidParticipants)
            If oWinner Is Nothing Then
                MsgBox("no s'ha trobat cap participant valid", MsgBoxStyle.Exclamation)
            Else
                Dim oFrm As New Frm_RaffleParticipant(oWinner)
                oFrm.Show()
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_SetRandomWinner()
        Dim exs As New List(Of Exception)
        Dim oTaskResult = Await FEB2.Raffle.SetRandomWinner(_Raffle, exs)
        If oTaskResult.Success Then
            If _Raffle.Winner Is Nothing Then
                MsgBox("no s'ha trobat cap participant valid", MsgBoxStyle.Exclamation)
            Else
                MsgBox("assignat un guanyador a l'atzar a aquest sorteig" & vbCrLf & _Raffle.Winner.User.EmailAddress, MsgBoxStyle.Information)
            End If
        Else
            UIHelper.WarnError(oTaskResult.Exceptions, "error al assignar un guanyador")
        End If
    End Sub

    Private Async Sub Do_RemoveWinner()
        Dim exs As New List(Of Exception)
        If Await FEB2.Raffle.RemoveWinner(exs, _Raffle) Then
            MsgBox("guanyador retirat", MsgBoxStyle.Information)
        Else
            UIHelper.WarnError(exs, "error al eliminar el guanyador")
        End If
    End Sub

    Private Async Sub Do_SetAllPendingWinners()
        Dim exs As New List(Of Exception)
        Dim oTaskResult = Await FEB2.Raffles.SetWinners(Current.Session.User, exs)
        If oTaskResult.Success Then
            MsgBox(oTaskResult.ResultReport, MsgBoxStyle.Information)
        Else
            MsgBox(oTaskResult.ResultReport, MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Async Sub Do_ExcelParticipantsList()
        Dim exs As New List(Of Exception)
        Dim oSheet = Await FEB2.RaffleParticipants.ExcelParticipantsList(exs, _Raffle)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_MailWinnerCongrats_Outlook()
        Dim exs As New List(Of Exception)

        Dim oWinner As DTORaffleParticipant = _Raffle.Winner
        If oWinner Is Nothing Then
            UIHelper.WarnError("No hi ha cap guanyador en aquest sorteig")
        Else
            If FEB2.RaffleParticipant.Load(exs, oWinner) Then
                Dim oLang As DTOLang = oWinner.User.lang
                If FEB2.Raffle.Load(oWinner.Raffle, exs) Then
                    Dim sSubject As String = "M+O: ¡Enhorabuena, has ganado el sorteo de " & oWinner.Raffle.Brand.nom.Tradueix(Current.Session.Lang) & "!"
                    Dim sBodyUrl As String = FEB2.Mailing.BodyUrl(DTODefault.MailingTemplates.raffleWinnerCongrats, oWinner.Guid.ToString())

                    Dim oMailMessage = DTOMailMessage.Factory(oWinner.User.EmailAddress)
                    With oMailMessage
                        .subject = sSubject
                        .bodyUrl = sBodyUrl
                    End With

                    If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If

    End Sub

    Private Async Sub Do_RaffleDistributorNotification_Outlook()
        Dim exs As New List(Of Exception)
        Dim oRaffle As DTORaffle = _Raffle
        If FEB2.Raffle.Load(oRaffle, exs) Then
            Dim oLang = oRaffle.winner.Distribuidor.lang
            If oLang Is Nothing Then oLang = oRaffle.lang
            Dim sSubject As String = oLang.Tradueix("M+O: Su establecimiento ha sido seleccionado por el ganador del último sorteo (Requiere respuesta)", "", "", "M+O: O seu estabelecimento foi selecionado pelo ganhador do último sorteio (Requere resposta)")

            Dim sRecipient As String = ""
            Dim oDistribuidor As DTOContact = oRaffle.winner.Distribuidor
            If oDistribuidor IsNot Nothing Then
                Dim oUser As DTOUser = Await FEB2.Contact.DefaultUser(oDistribuidor, exs)
                If oUser IsNot Nothing Then
                    sRecipient = oUser.EmailAddress
                End If
            End If

            Dim sBodyUrl As String = FEB2.Mailing.BodyUrl(DTODefault.MailingTemplates.RaffleDistributorNotification, oRaffle.Guid.ToString())

            Dim oMailMessage = DTOMailMessage.Factory(sRecipient)
            With oMailMessage
                .subject = sSubject
                .bodyUrl = sBodyUrl
            End With

            If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub Do_MailWinnerCongrats_Exchange()
        Dim exs As New List(Of Exception)
        Dim oWinner As DTORaffleParticipant = _Raffle.Winner
        Dim oMailMessage = Await FEB2.RaffleParticipant.RaffleWinnerCongrats(exs, GlobalVariables.Emp, oWinner)
        If Await FEB2.MailMessage.Send(exs, Current.Session.User, oMailMessage) Then
            MsgBox("missatge enviat a:" & vbCrLf & oWinner.User.EmailAddress)
        Else
            UIHelper.WarnError(exs, "no s'ha pogut enviar el missatge al guanyador")
        End If

    End Sub

    Private Async Sub Do_DeliverPrize()
        Dim exs As New List(Of Exception)
        Dim oWinner As DTORaffleParticipant = _Raffle.Winner
        If FEB2.RaffleParticipant.Load(exs, oWinner) Then
            If FEB2.Raffle.Load(_Raffle, exs) Then
                Dim oCustomer = Await FEB2.Customer.Find(exs, oWinner.Distribuidor.Guid)
                If exs.Count = 0 Then
                    Dim oPurchaseOrder = DTOPurchaseOrder.Factory(oCustomer, Current.Session.User)
                    With oPurchaseOrder
                        If oWinner.User.Sex = DTOUser.Sexs.Male Then
                            .Concept = "premio para ganador sorteo " & DTOUser.Nom_i_Cognoms(oWinner.User)
                        Else
                            .Concept = "premio para ganadora sorteo " & DTOUser.Nom_i_Cognoms(oWinner.User)
                        End If
                    End With

                    Dim item As New DTOPurchaseOrderItem()
                    With item
                        .PurchaseOrder = oPurchaseOrder
                        .sku = Await FEB2.ProductSku.Find(exs, _Raffle.Product.Guid, GlobalVariables.Emp.Mgz)
                        .qty = 1
                        .Pending = .Qty
                        .ChargeCod = DTOPurchaseOrderItem.ChargeCods.FOC
                    End With

                    If exs.Count = 0 Then
                        oPurchaseOrder.Items.Add(item)

                        If oPurchaseOrder IsNot Nothing Then
                            If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oPurchaseOrder.Contact, DTOAlbBloqueig.Codis.PDC, exs) Then
                                Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                                AddHandler oFrm.AfterUpdate, AddressOf AfterDeliver
                                oFrm.Show()
                            Else
                                UIHelper.WarnError(exs)
                            End If
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If

        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub AfterDeliver(sender As Object, e As MatEventArgs)
        Dim oPurchaseorder As DTOPurchaseOrder = e.Argument
        Dim exs As New List(Of Exception)
        If FEB2.Raffle.Load(_Raffle, exs) Then
            _Raffle.FchDelivery = oPurchaseorder.Fch
            If Not Await FEB2.Raffle.Update(exs, _Raffle) Then
                UIHelper.WarnError(exs, "error al registrar la data d'enviament al sorteig")
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class


