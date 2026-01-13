Public Class Menu_Raffle
    Private _Raffle As DTORaffle

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oRaffle As DTORaffle)
        MyBase.New()
        _Raffle = oRaffle
        BLL.BLLRaffle.Load(_Raffle)
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_WebPlay(), _
        MenuItem_CopyLinkToPlay(), _
        MenuItem_WebZoom(), _
        MenuItem_CopyLinkToZoom(), _
        MenuItem_ExcelParticipantsList(), _
        MenuItem_CopyLinkToReminder(), _
        MenuItem_GetRandomWinner(), _
        MenuItem_SetRandomWinner(), _
        MenuItem_RemoveWinner(), _
        MenuItem_SetAllPendingWinners(), _
        MenuItem_MailWinnerCongrats(), _
        MenuItem_DistributorNotification(), _
        MenuItem_DeliverPrize(), _
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


    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _Raffle.Title & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If BLL.BLLRaffle.Delete(_Raffle, exs) Then
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
        UIHelper.ShowHtml(BLL.BLLRaffle.PlayUrl(_Raffle, True))
    End Sub

    Private Sub Do_CopyLinkToPlay(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(BLL.BLLRaffle.PlayUrl(_Raffle, True))
    End Sub

    Private Sub Do_WebZoom(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.ShowHtml(BLL.BLLRaffle.ZoomUrl(_Raffle, True))
    End Sub

    Private Sub Do_CopyLinkToZoom(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(BLL.BLLRaffle.ZoomUrl(_Raffle, True))
    End Sub

    Private Sub Do_CopyLinkToRemainder(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(BLL.BLLRaffle.MailReminderUrl(_Raffle))
    End Sub

    Private Sub Do_GetRandomWinner()
        Dim oWinner As DTORaffleParticipant = BLL.BLLRaffle.GetRandomWinner(_Raffle)
        If oWinner Is Nothing Then
            MsgBox("no s'ha trobat cap participant valid", MsgBoxStyle.Exclamation)
        Else
            Dim oFrm As New Frm_RaffleParticipant(oWinner)
            oFrm.Show()
        End If
    End Sub

    Private Sub Do_SetRandomWinner()
        Dim exs As New List(Of exception)
        If BLL.BLLRaffle.SetRandomWinner(_Raffle, exs) Then
            If _Raffle.Winner Is Nothing Then
                MsgBox("no s'ha trobat cap participant valid", MsgBoxStyle.Exclamation)
            Else
                MsgBox("assignat un guanyador a l'atzar a aquest sorteig" & vbCrLf & _Raffle.Winner.User.EmailAddress, MsgBoxStyle.Information)

            End If
        Else
            UIHelper.WarnError(exs, "error al assignar un guanyador")
        End If
    End Sub

    Private Sub Do_RemoveWinner()
        Dim exs As New List(Of exception)
        If BLL.BLLRaffle.RemoveWinner(_Raffle, exs) Then
            MsgBox("guanyador retirat", MsgBoxStyle.Information)
        Else
            UIHelper.WarnError(exs, "error al eliminar el guanyador")
        End If
    End Sub

    Private Sub Do_SetAllPendingWinners()


        Dim oWinners As List(Of DTORaffleParticipant) = BLL.BLLRaffles.SetWinners()

        If oWinners.Count > 0 Then
            Dim sb As New System.Text.StringBuilder
            For Each oWinner As DTORaffleParticipant In oWinners
                Dim oUser As DTOUser = oWinner.User
                BLL.BLLUser.Load(oUser)
                sb.AppendLine("sorteig " & oWinner.Raffle.Title)
                If oUser IsNot Nothing Then
                    sb.AppendLine("     guanyador " & oUser.Nom & " " & oUser.Cognoms)
                End If
            Next
            MsgBox(sb.ToString)
        Else
            MsgBox("cap guanyador pendent de assignar")
        End If


    End Sub

    Private Sub Do_ExcelParticipantsList()
        Dim oExcelSheet As DTOExcelSheet = BLL.BLLRaffle.ExcelParticipantsList(_Raffle)
        UIHelper.ShowExcel(oExcelSheet)
    End Sub

    Private Sub Do_MailWinnerCongrats_Outlook()
        Dim oWinner As DTORaffleParticipant = _Raffle.Winner
        BLL.BLLRaffleParticipant.Load(oWinner)

        Dim oLang As DTOLang = oWinner.User.Lang
        BLL.BLLRaffle.Load(oWinner.Raffle)
        Dim sSubject As String = "M+O: ¡Enhorabuena, has ganado el sorteo de " & BLL.BLLRaffle.BrandNom(oWinner.Raffle) & "!"
        Dim sBodyUrl As String = BLL.BLLMailing.BodyUrl(BLL.BLLMailing.Templates.RaffleWinnerCongrats, oWinner.Guid.ToString)

        Dim exs As New List(Of exception)
        If Not MatOutlook.NewMessage(oWinner.User.EmailAddress, "", , sSubject, , sBodyUrl, , exs) Then
            UIHelper.WarnError(exs, "error al redactar missatge")
        End If


    End Sub

    Private Sub Do_RaffleDistributorNotification_Outlook()

        Dim oRaffle As DTORaffle = _Raffle
        BLL.BLLRaffle.Load(oRaffle)
        Dim sSubject As String = "M+O: Su establecimiento ha sido seleccionado por el ganador del último sorteo (Requiere respuesta)"

        Dim sRecipient As String = ""
        Dim oDistribuidor As DTOContact = oRaffle.Winner.Distribuidor
        If oDistribuidor IsNot Nothing Then
            Dim oUser As DTOUser = BLL.BLLContact.DefaultUser(oDistribuidor)
            If oUser IsNot Nothing Then
                sRecipient = oUser.EmailAddress
            End If
        End If

        Dim sBodyUrl As String = BLL.BLLMailing.BodyUrl(BLL.BLLMailing.Templates.RaffleDistributorNotification, oRaffle.Guid.ToString)

        Dim exs As New List(Of exception)
        If Not MatOutlook.NewMessage(sRecipient, "", , sSubject, , sBodyUrl, , exs) Then
            UIHelper.WarnError(exs, "error al redactar missatge")
        End If

    End Sub

    Private Sub Do_MailWinnerCongrats_Exchange()
        Dim oWinner As DTORaffleParticipant = _Raffle.Winner
        BLL.BLLRaffleParticipant.Load(oWinner)

        Dim exs As New List(Of exception)
        If BLL_Mail.RaffleWinnerCongrats(oWinner, exs) Then
            MsgBox("missatge enviat a:" & vbCrLf & _Raffle.Winner.User.EmailAddress)
        Else
            UIHelper.WarnError(exs, "no s'ha pogut enviar el missatge al guanyador")
        End If
    End Sub

    Private Sub Do_DeliverPrize()
        Dim oWinner As DTORaffleParticipant = _Raffle.Winner
        BLL.BLLRaffleParticipant.Load(oWinner)
        BLL.BLLRaffle.Load(_Raffle)

        Dim oCustomer As New DTOCustomer(oWinner.Distribuidor.Guid)

        Dim item As New DTOPurchaseOrderItem()
        With item
            .Sku = BLL.BLLProductSku.Find(_Raffle.Product.Guid)
            .Qty = 1
            .Pending = .Qty
            .ChargeCod = DTOPurchaseOrderItem.ChargeCods.FOC
        End With

        Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.NewCustomerOrder(oCustomer)
        With oPurchaseOrder
            If oWinner.User.Sex = Lead.Sexs.Male Then
                .Concept = "premio para ganador sorteo " & BLL.BLLUser.Nom_i_Cognoms(oWinner.User)
            Else
                .Concept = "premio para ganadora sorteo " & BLL.BLLUser.Nom_i_Cognoms(oWinner.User)
            End If
            .Items.Add(item)
        End With

        If oPurchaseOrder IsNot Nothing Then
            Dim exs As New List(Of Exception)
            If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oPurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
                UIHelper.WarnError(exs)
            Else
                Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                AddHandler oFrm.AfterUpdate, AddressOf AfterDeliver
                oFrm.Show()
            End If
        End If
    End Sub

    Private Sub AfterDeliver(sender As Object, e As MatEventArgs)
        Dim oPdc As Pdc = e.Argument
        Dim exs As New List(Of exception)
        BLL.BLLRaffle.Load(_Raffle)
        _Raffle.FchDelivery = oPdc.Fch
        If Not BLL.BLLRaffle.Update(_Raffle, exs) Then
            UIHelper.WarnError(exs, "error al registrar la data d'enviament al sorteig")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class


