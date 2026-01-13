Public Class Menu_Incidencia

    Private _Incidencia As DTOIncidencia

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oIncidencia As DTOIncidencia)
        MyBase.New()
        _Incidencia = oIncidencia
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Web(), _
        MenuItem_MailConfirmation(), _
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
        Dim url As String = BLL_Incidencia.Url(_Incidencia, True)
        UIHelper.ShowHtml(url)
    End Sub

    Private Sub Do_MailConfirmation(ByVal sender As Object, ByVal e As System.EventArgs)
        BLL.BLLContact.Load(_Incidencia.Customer)
        Dim oLang As New DTOLang(CType(_Incidencia.Customer.Lang.Id, DTOLang.ids))
        Dim sTo As String = _Incidencia.EmailAdr
        Dim oSsc As New DTOSubscription(DTOSubscription.Ids.NovaIncidencia)
        Dim oBcc As System.Net.Mail.MailAddressCollection = BLL.BLLSubscription.EmailAddressCollection(oSsc)
        Dim sSubject As String = "M+O: " & oLang.Tradueix("Registro de Incidencia Postventa", "Registre de Incidencia Postvenda", "Service incident") & " " & _Incidencia.Id & " " & BLL_Customer.NomComercialOrNom(_Incidencia.Customer)
        Dim sBodyUrl As String = BLL.BLLMailing.BodyUrl(BLL.BLLMailing.Templates.Incidencia, _Incidencia.Guid.ToString)

        Dim exs as New List(Of exception)
        If Not MatOutlook.NewMessage(sTo, "", oBcc.ToString, sSubject, , sBodyUrl, , exs) Then
            UIHelper.WarnError( exs, "error al redactar missatge")
        End If

    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem la incidencia " & _Incidencia.Id & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If Incidencialoader.Delete(_Incidencia, exs) Then
                MsgBox("Incidencia " & _Incidencia.Id & " eliminada", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError( exs, "error al eliminar la incidencia")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class


