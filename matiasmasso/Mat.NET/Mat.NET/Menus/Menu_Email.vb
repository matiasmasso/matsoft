

Public Class Menu_Email
    Private mEmail As Email

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oEmail As Email)
        MyBase.New()
        mEmail = oEmail
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Copy(), _
        MenuItem_CopyPwd(), _
        MenuItem_MailNewMsg(), _
        MenuItem_MailStk(), _
        MenuItem_MailPwd(), _
        MenuItem_Avisame(), _
        MenuItem_WebPro(), _
        MenuItem_WebVisitsLog() _
        })
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

    Private Function MenuItem_Copy() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar adreça email"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_Copy
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyPwd() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar password"
        oMenuItem.Image = My.Resources.Copy

        Dim oContact As DTOContact
        If mEmail.DefaultContact Is Nothing Then
            Dim oContacts As Contacts = mEmail.Contacts()
            If oContacts.Count > 0 Then
                oContact = BLL.BLLContact.Find(oContacts(0).Guid)
            End If
        Else
            oContact = BLL.BLLContact.Find(mEmail.DefaultContact.Guid)
        End If

        oMenuItem.Enabled = BLL.BLLUser.IsAllowedToRead(BLL.BLLSession.Current.User, oContact)
        AddHandler oMenuItem.Click, AddressOf Do_CopyPwd
        Return oMenuItem
    End Function

    Private Function MenuItem_MailNewMsg() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "nou missatge"
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_MailNewMsg
        Return oMenuItem
    End Function

    Private Function MenuItem_MailStk() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "stocks"
        oMenuItem.Image = My.Resources.dau
        AddHandler oMenuItem.Click, AddressOf Do_MailStk
        Return oMenuItem
    End Function

    Private Function MenuItem_MailPwd() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "password"
        oMenuItem.Image = My.Resources.candau
        AddHandler oMenuItem.Click, AddressOf Do_MailPwd
        Return oMenuItem
    End Function

    Private Function MenuItem_Avisame() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Avisame"
        oMenuItem.Image = My.Resources.Outlook_16
        AddHandler oMenuItem.Click, AddressOf Do_Avisame
        Return oMenuItem
    End Function

    Private Function MenuItem_WebPro() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "extranet"
        oMenuItem.Enabled = BLL.BLLUser.IsUserAllowedToRead(BLL.BLLSession.Current.User, mEmail.Rol)
        oMenuItem.Image = My.Resources.iExplorer
        AddHandler oMenuItem.Click, AddressOf Do_WebPro
        Return oMenuItem
    End Function

    Private Function MenuItem_WebVisitsLog() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "visitas web"
        oMenuItem.Enabled = BLL.BLLUser.IsUserAllowedToRead(BLL.BLLSession.Current.User, mEmail.Rol)
        oMenuItem.Image = My.Resources.notepad
        AddHandler oMenuItem.Click, AddressOf Do_EmailWebVisitsLog
        Return oMenuItem
    End Function


   



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Contact_Email(mEmail)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Copy(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(mEmail.Adr, True)
    End Sub

    Private Sub Do_CopyPwd(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(mEmail.Pwd, True)
    End Sub

    Private Sub Do_MailNewMsg(ByVal sender As Object, ByVal e As System.EventArgs)
        root.NewMail(mEmail.Adr)
    End Sub

    Private Sub Do_MailStk(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSubscriptor As New DTOSubscriptor(mEmail.Guid, New DTOSubscription(DTOSubscription.Ids.Stocks))
        BLL.BLLUser.Load(oSubscriptor)
        If BLL.BLLSubscriptor.SendStocks(oSubscriptor, exs) = EventLogEntryType.Information Then
            MsgBox("stocks enviats correctament", MsgBoxStyle.Information, "MAT.NET")
        Else
            MsgBox("stocks no enviats", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub Do_MailPwd(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oLang As DTOLang = mEmail.Lang
        Dim sSubject As String = oLang.Tradueix("CODIGOS DE ACCESO INTERNET", "CODIS D'ACCES INTERNET", "PASSWORD FOR INTERNET ACCESS")
        Dim sBody As String = oLang.Tradueix("Contraseña", "Clau de pas", "Password") & ": " & mEmail.Pwd
        Dim sErr As String = ""

        Dim exs as new list(Of Exception)
        If BLL.MailHelper.SendMail(mEmail.Adr, , , sSubject, sBody, , , exs) Then
            MsgBox("password enviat a " & mEmail.Adr, MsgBoxStyle.Information, "MAT.NET")
        Else
            MsgBox("error al enviar el password a " & mEmail.Adr & ":" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub Do_Avisame(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Avisame
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Email = mEmail
            .Show()
        End With
    End Sub

    Private Sub Do_EmailWebVisitsLog(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_EmailLog(mEmail)
        oFrm.Show()
    End Sub

    Private Sub Do_WebPro(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim CurrentRol As DTORol = BLL.BLLSession.Current.User.Rol
        Dim DestinationRol As DTORol = mEmail.Rol
        Dim oContact As DTOContact = BLL.BLLContact.Find(mEmail.DefaultContact.Guid)

        If BLL.BLLUser.IsAllowedToRead(BLL.BLLSession.Current.User, oContact) Then
            Dim sUrl As String = BLL.Defaults.FromSegments(True, "guid", mEmail.Guid.ToString)
            UIHelper.ShowHtml(sUrl)
        Else
            MsgBox("operacio no autoritzada per politica de privacitat", MsgBoxStyle.Exclamation, "MAT.NET")
            BLL.MailHelper.MailErr(My.User.Name & " browse " & mEmail.Adr)
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class
