Public Class Menu_Incidencia

    Private _Incidencia As DTOIncidencia

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oIncidencia As DTOIncidencia)
        MyBase.New()
        _Incidencia = oIncidencia
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Web(),
        MenuItem_MailConfirmation(),
        MenuItem_SatRecall(),
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

    Private Function MenuItem_Web() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Fitxa web"
        oMenuItem.Image = My.Resources.iExplorer
        AddHandler oMenuItem.Click, AddressOf Do_Web
        Return oMenuItem
    End Function

    Private Function MenuItem_SatRecall() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Recullida fabricant"
        AddHandler oMenuItem.Click, AddressOf Do_SatRecall
        Return oMenuItem
    End Function

    Private Function MenuItem_MailConfirmation() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "mail de confirmació"
        oMenuItem.Image = My.Resources.Outlook_16
        AddHandler oMenuItem.Click, AddressOf Do_MailConfirmation
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
        Dim oFrm As New Frm_Incidencia(_Incidencia)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Web(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url As String = FEB2.UrlHelper.Factory(True, _Incidencia.UrlSegment)
        UIHelper.ShowHtml(url)
    End Sub

    Private Sub Do_SatRecall()
        Dim exs As New List(Of Exception)
        If FEB2.Incidencia.Load(exs, _Incidencia) Then
            Dim oSatRecall = DTOSatRecall.Factory(_Incidencia)
            If exs.Count = 0 Then
                Dim oFrm As New Frm_SatRecall(oSatRecall)
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If

        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub Do_MailConfirmation(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.Incidencia.Load(exs, _Incidencia) Then
            If FEB2.Contact.Load(_Incidencia.Customer, exs) Then
                Dim oLang = _Incidencia.Customer.Lang
                Dim sTo As String = _Incidencia.emailAdr
                Dim oSsc = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.NovaIncidencia)

                Dim oMailMessage = DTOMailMessage.Factory(sTo)
                With oMailMessage
                    .bcc = Await FEB2.Subscriptors.Recipients(exs, GlobalVariables.Emp, DTOSubscription.Wellknowns.NovaIncidencia,)
                    .Subject = "M+O: " & oLang.Tradueix("Registro de Incidencia Postventa", "Registre de Incidencia Postvenda", "Service incident") & " " & _Incidencia.Num & " " & _Incidencia.Customer.NomComercialOrDefault
                    .BodyUrl = FEB2.Mailing.BodyUrl(DTODefault.MailingTemplates.Incidencia, _Incidencia.Guid.ToString())
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
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem la incidencia " & _Incidencia.Num & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Incidencia.Delete(exs, _Incidencia) Then
                MsgBox("Incidencia " & _Incidencia.Num & " eliminada", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar la incidencia")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class


