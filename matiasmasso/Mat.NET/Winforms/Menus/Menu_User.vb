

Public Class Menu_User
    Private _User As DTOUser

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oUser As DTOUser)
        MyBase.New()
        _User = oUser
    End Sub


    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_ZoomUser(),
        MenuItem_Copy(),
        MenuItem_CopyPwd(),
        MenuItem_MailNewMsg(),
        MenuItem_MailPwd(),
        MenuItem_WebPro(),
        MenuItem_WebProDeveloper(),
        MenuItem_BaixaConsumidor(),
        MenuItem_Advanced()
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

    Private Function MenuItem_ZoomUser() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Fitxa consumidor"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_ZoomConsumer
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

    Private Function MenuItem_MailPwd() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "enviar password"
        oMenuItem.Image = My.Resources.candau
        AddHandler oMenuItem.Click, AddressOf Do_MailPwd
        Return oMenuItem
    End Function

    Private Function MenuItem_WebPro() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "extranet"
        oMenuItem.Enabled = DTOUser.IsUserAllowedToRead(Current.Session.User, _User)
        oMenuItem.Image = My.Resources.iExplorer
        AddHandler oMenuItem.Click, AddressOf Do_WebPro
        Return oMenuItem
    End Function

    Private Function MenuItem_BaixaConsumidor() As ToolStripItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "baixa consumidor"
        AddHandler oMenuItem.Click, AddressOf Do_BaixaConsumidor
        Return oMenuItem
    End Function
    Private Function MenuItem_WebProDeveloper() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "extranet (local)"
        oMenuItem.Visible = Current.Session.User.Equals(DTOUser.Wellknown(DTOUser.Wellknowns.matias))
        oMenuItem.Image = My.Resources.iExplorer
        AddHandler oMenuItem.Click, AddressOf Do_WebProDeveloper
        Return oMenuItem
    End Function


    Private Function MenuItem_Advanced() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "avançat"
        oMenuItem.DropDownItems.Add(MenuItem_ExcelStocks)
        oMenuItem.DropDownItems.Add(MenuItem_Demo)
        Return oMenuItem
    End Function

    Private Function MenuItem_ExcelStocks() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Stocks en Excel"
        AddHandler oMenuItem.Click, AddressOf Do_ExcelStocks
        Return oMenuItem
    End Function

    Private Function MenuItem_Demo() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "demo"
        Select Case Current.Session.User.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
            Case Else
                oMenuItem.Visible = False
        End Select
        AddHandler oMenuItem.Click, AddressOf Do_Demo
        Return oMenuItem
    End Function








    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Contact_Email(_User)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_ZoomConsumer(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_User(_User)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Copy(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(_User.EmailAddress, True)
    End Sub

    Private Sub Do_CopyPwd(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oContact As DTOContact = Nothing
        If _User.Contact Is Nothing Then
            If _User.Contacts Is Nothing Then
                If FEB2.User.Load(exs, _User) Then
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
            Dim oContacts As List(Of DTOContact) = _User.Contacts()
            If oContacts.Count > 0 Then
                oContact = oContacts.First
            End If
        Else
            oContact = _User.Contact
        End If

        If FEB2.User.IsAllowedToRead(Current.Session.User, oContact) Then
            Clipboard.SetDataObject(_User.Password, True)
        Else
            UIHelper.WarnError("Operació no autoritzada per aquest usuari")
        End If

    End Sub

    Private Sub Do_MailNewMsg(ByVal sender As Object, ByVal e As System.EventArgs)
        root.NewMail(_User.EmailAddress)
    End Sub


    Private Async Sub Do_MailPwd(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If Await FEB2.User.EmailPassword(_User, exs) Then
            MsgBox("Contrasenya enviada correctament a " & _User.EmailAddress)
        Else
            UIHelper.WarnError(exs, "error al enviar el password a " & _User.EmailAddress)
        End If
    End Sub


    Private Sub Do_WebPro(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.User.Load(exs, _User) Then
            Dim CurrentRol As DTORol = Current.Session.User.Rol
            Dim DestinationRol As DTORol = _User.Rol
            Dim oContact As DTOContact = _User.contact

            If FEB2.User.IsAllowedToRead(Current.Session.User, oContact) Then
                Dim sUrl As String = FEB2.UrlHelper.Factory(True, "guid", _User.Guid.ToString(), Current.Session.User.Guid.ToString())
                UIHelper.ShowHtml(sUrl)
            Else
                MsgBox("operacio no autoritzada per politica de privacitat", MsgBoxStyle.Exclamation, "MAT.NET")
                'FEB2.MailMessage.MailErr(Current.Session.User, My.User.Name & " browse " & _User.EmailAddress)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_WebProDeveloper(ByVal sender As Object, ByVal e As System.EventArgs)
        If Current.Session.User.Rol.id = DTORol.Ids.superUser Then
            Dim sUrl = DTOApp.Current.DebuggerUrl("guid", _User.Guid.ToString(), Current.Session.User.Guid.ToString())
            UIHelper.ShowHtml(sUrl)
        Else
            MsgBox("operacio no autoritzada per politica de privacitat", MsgBoxStyle.Exclamation, "MAT.NET")
        End If

    End Sub

    Private Async Sub Do_BaixaConsumidor()
        Dim exs As New List(Of Exception)

        Dim oUser = Await FEB2.User.Find(_User.Guid, exs)
        If oUser.FchDeleted <> Nothing Then
            UIHelper.WarnError("Aquest usuari ja està donat de baixa des del " & oUser.FchDeleted.ToShortDateString)
        Else
            oUser.FchDeleted = Now
            If Await FEB2.User.Update(exs, oUser) Then
                _User = oUser
                RaiseEvent AfterUpdate(Me, New MatEventArgs(oUser))
            Else
                UIHelper.WarnError(exs)
            End If
        End If

    End Sub

    Private Async Sub Do_ExcelStocks()
        Dim exs As New List(Of Exception)
        Dim oUser = Await FEB2.User.Find(_User.Guid, exs)
        If exs.Count = 0 Then
            Dim oSheet = Await FEB2.ProductStocks.Excel(exs, Current.Session.Emp, oUser)
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Demo()
        Dim exs As New List(Of Exception)
        SaveSettingString(DTOSession.CookieSessionNameBackup, Current.Session.User.Guid.ToString())
        Dim oUser = Await FEB2.User.Find(_User.Guid, exs)
        If exs.Count = 0 Then
            With Current.Session
                .User = oUser
                .Rol = .User.Rol
                .Lang = .User.Lang
            End With

            SaveSettingString(DTOSession.CookieSessionName, oUser.Guid.ToString())
            SaveSettingString(DTOSession.CookiePersistName, "1")

            Dim oForms As FormCollection = Application.OpenForms
            For i As Integer = oForms.Count - 1 To 0 Step -1
                If TypeOf oForms(i) Is Frm__Idx Then
                    If Not Await DirectCast(oForms(i), Frm__Idx).Refresca(exs) Then
                        UIHelper.WarnError(exs)
                    End If
                Else
                    oForms(i).Close()
                End If
            Next
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class
