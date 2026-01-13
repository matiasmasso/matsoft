

Public Class Menu_Contact
    Private mContact As Contact
    Private _Contact As DTOContact
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oContact As Contact)
        MyBase.New()
        mContact = oContact
        _Contact = New DTOContact(oContact.Guid)
    End Sub

    Public Sub New(ByVal oContact As DTOContact)
        MyBase.New()
        _Contact = oContact
        mContact = New Contact(oContact.Guid)
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), _
                                         MenuItem_Email(), _
                                         MenuItem_AddNew(), _
        MenuItem_CopyToClipboard(), _
        MenuItem_test(), _
        MenuItem_Comptes(), _
        MenuItem_ComptesVell(), _
        MenuItem_Correspondencia(), _
        MenuItem_Meetings(), _
        MenuItem_CliDocs(), _
        MenuItem_Incidencias(), _
        MenuItem_Credencials(), _
        MenuItem_Ftp(), _
        MenuItem_SSL(), _
        MenuItem_Client(), _
        MenuItem_Proveidor(), _
        MenuItem_Rep(), _
        MenuItem_Transportista(), _
        MenuItem_Mgz(), _
        MenuItem_Advanced()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        If mContact.Id = 0 Then oMenuItem.Enabled = False
        Return oMenuItem
    End Function

    Private Function MenuItem_Email() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "email"
        AddHandler oMenuItem.MouseEnter, AddressOf EmailDropdown
        'If oMenuItem.DropDownItems.Count = 0 Then
        'oMenuItem.Enabled = False
        'End If
        Return oMenuItem
    End Function

    Private Function EmailDropdown(sender As Object, e As EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        If oMenuItem.DropDownItems.Count = 0 Then

            Dim oEmails As Emails = Emails.FromContact(mContact)
            For Each oEmail As Email In oEmails
                Dim oIcon As Image = Nothing
                If oEmail.BadMail <> Email.BadMailErrs.None Then
                    oIcon = My.Resources.warn
                Else
                    'oIcon = Gravatar.Image(oEmail.Adr)
                End If
                Dim oMenuItemEmail As New ToolStripMenuItem(oEmail.Adr, oIcon)
                Dim oMenuEmail As New Menu_Email(oEmail)
                oMenuItemEmail.DropDownItems.AddRange(oMenuEmail.Range)
                oMenuItem.DropDownItems.Add(oMenuItemEmail)
            Next
        End If
    End Function

    Private Function MenuItem_AddNew() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_AddNew
        oMenuItem.Text = "Nou"
        oMenuItem.Image = My.Resources.clip
        Return oMenuItem
    End Function

    Private Function MenuItem_test() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_test
        oMenuItem.Text = "nou format de fitxa"
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyToClipboard() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_CopyToClipboard
        oMenuItem.Text = "Copiar"
        oMenuItem.Image = My.Resources.Resources.Copy
        Return oMenuItem
    End Function

    Private Function MenuItem_Credencials() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Credencials
        oMenuItem.Text = "Credencials"
        Return oMenuItem
    End Function

    Private Function MenuItem_Ftp() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Ftp
        oMenuItem.Text = "Ftp"
        oMenuItem.Visible = False 'tallat per no provocar un contact.setitm       mContact.FtpPwd > ""
        'oMenuItem.Image = My.Resources.Resources.Copy
        Return oMenuItem
    End Function


    Private Function MenuItem_SSL() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_SSL
        oMenuItem.Text = "importar certificat"
        oMenuItem.Image = My.Resources.Resources.pfx
        oMenuItem.Visible = BLL.BLLSession.Current.User.Rol.id = Rol.Ids.SuperUser
        Return oMenuItem
    End Function

    Private Function MenuItem_Comptes() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Comptes"
        If mContact.ExistComptes Then
            oMenuItem.DropDownItems.Add(SubMenuItem_Saldos)
            oMenuItem.DropDownItems.Add(SubMenuItem_Cobraments)
            oMenuItem.DropDownItems.Add(SubMenuItem_Pagaments)
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_ComptesVell() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_SaldosVell
        oMenuItem.Text = "Comptes (Vell)"
        oMenuItem.Visible = BLL.BLLSession.Current.User.Rol.Id = DTORol.Ids.Accounts Or BLL.BLLSession.Current.User.Rol.Id = DTORol.Ids.SuperUser
        'oMenuItem.Image = My.Resources.Resources.Copy
        Return oMenuItem
    End Function

    Private Function MenuItem_Incidencias() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Incidencies"
        oMenuItem.DropDownItems.Add(SubMenuItem_LastIncidencies)
        oMenuItem.DropDownItems.Add(SubMenuItem_NewIncidenciaProducte)
        oMenuItem.DropDownItems.Add(SubMenuItem_NewIncidenciaTransport)
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Saldos() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Saldos
        oMenuItem.Text = "Sumes i saldos"
        oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Cobraments() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        If mContact.ExistPnds(Pnd.Codis.Deutor) Then
            AddHandler oMenuItem.Click, AddressOf Do_Cobraments
        Else
            oMenuItem.Enabled = False
        End If
        oMenuItem.Text = "Cobrar"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Pagaments() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        If mContact.ExistPnds(Pnd.Codis.Creditor) Then
            AddHandler oMenuItem.Click, AddressOf Do_Pagaments
        Else
            oMenuItem.Enabled = False
        End If
        oMenuItem.Text = "Pagar"
        Return oMenuItem
    End Function


    Private Function MenuItem_Correspondencia() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Correspondencia"
        oMenuItem.Image = My.Resources.MailSobreGroc
        oMenuItem.DropDownItems.Add(SubMenuItem_LastMails)
        oMenuItem.DropDownItems.Add(SubMenuItem_NewMail)
        oMenuItem.DropDownItems.Add(SubMenuItem_NewMemo)
        Return oMenuItem
    End Function

    Private Function MenuItem_Meetings() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Meetings
        oMenuItem.Text = "meetings"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_LastMails() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastMails
        oMenuItem.Text = "mails i memos"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_NewMail() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewMail
        oMenuItem.Text = "nou mail..."
        oMenuItem.Image = My.Resources.MailSobreObert
        oMenuItem.ShortcutKeys = Shortcut.CtrlM
        Return oMenuItem
    End Function

    Private Function SubMenuItem_NewMemo() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewMemo
        oMenuItem.Text = "nou memo..."
        oMenuItem.ShortcutKeys = Shortcut.CtrlN
        Return oMenuItem
    End Function

    Private Function MenuItem_CliDocs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Clidocs
        oMenuItem.Text = "documents"
        Select Case BLL.BLLSession.Current.User.Rol.id
            Case Rol.Ids.SuperUser, Rol.Ids.Admin, Rol.Ids.Accounts
            Case Else
                oMenuItem.Visible = False
        End Select
        oMenuItem.Image = My.Resources.notepad
        Return oMenuItem
    End Function

    Private Function MenuItem_Client() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Client..."
        If mContact.Client.Exists Then
            oMenuItem.Image = My.Resources.People_Blue
            oMenuItem.DropDownItems.AddRange(New Menu_Client(New Client(mContact.Guid)).Range)
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Proveidor() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case BLL.BLLSession.Current.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts, Rol.Ids.LogisticManager, Rol.Ids.Operadora
                If mContact.Proveidor.Exists Then
                    oMenuItem.Image = My.Resources.People_Orange
                    oMenuItem.DropDownItems.AddRange(New Menu_Proveidor(New Proveidor(mContact.Guid)).Range)
                Else
                    oMenuItem.Visible = False
                End If
            Case Else
                oMenuItem.Visible = False
        End Select
        oMenuItem.Text = "Proveidor..."
        Return oMenuItem
    End Function

    Private Function MenuItem_Rep() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        If mContact.Rep.Exists Then
            oMenuItem.DropDownItems.AddRange(New Menu_Rep(New DTORep(mContact.Guid)).Range)
        Else
            oMenuItem.Visible = False
        End If
        oMenuItem.Text = "rep..."
        Return oMenuItem
    End Function

    Private Function MenuItem_Transportista() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Transportista"
        If mContact.Transportista.Exists Then
            'oMenuItem.Image = My.Resources.People_Orange
            oMenuItem.DropDownItems.AddRange(New Menu_Transportista(New Transportista(mContact.Guid)).Range)
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Mgz() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Visible = False
        'oMenuItem.Text = "Magatzem"
        'If mContact.Mgz.Exists Then
        'oMenuItem.Image = My.Resources.People_Orange
        'oMenuItem.DropDownItems.AddRange(New Menu_Mgz(New Mgz(mContact.Emp, mContact.Id)).Range)
        'Else
        'oMenuItem.Visible = False
        'End If
        Return oMenuItem
    End Function

    Private Function SubMenuItem_NewIncidenciaProducte() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewIncidenciaProducte
        oMenuItem.Text = "nova incidencia producte"
        oMenuItem.Image = My.Resources.NewDoc
        Return oMenuItem
    End Function

    Private Function SubMenuItem_NewIncidenciaTransport() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewIncidenciaTransport
        oMenuItem.Text = "nova incidencia transport"
        oMenuItem.Image = My.Resources.NewDoc
        Return oMenuItem
    End Function

    Private Function SubMenuItem_LastIncidencies() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastIncidencies
        oMenuItem.Text = "ultimes incidencies"
        'oMenuItem.Image = My.Resources.Spv
        Return oMenuItem
    End Function

    Private Function MenuItem_Advanced() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        If BLL.BLLSession.Current.User.Rol.IsSuperAdmin Then
            oMenuItem.Text = "avançat"
            oMenuItem.DropDownItems.Add(SubMenuItem_CopyGuidToClipboard)
            oMenuItem.DropDownItems.Add(SubMenuItem_Representants)
            oMenuItem.DropDownItems.Add(SubMenuItem_Representades)
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Representants() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_ShowRepresentants
        oMenuItem.Text = "representants"
        'oMenuItem.Image = My.Resources.Resources.Copy
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Representades() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_ShowRepresentades
        oMenuItem.Text = "representades"
        'oMenuItem.Image = My.Resources.Resources.Copy
        Return oMenuItem
    End Function

    Private Function SubMenuItem_CopyGuidToClipboard() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_CopyGuidToClipboard
        oMenuItem.Text = "Copiar Guid"
        oMenuItem.Image = My.Resources.Resources.Copy
        Return oMenuItem
    End Function

    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Test(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Contact2(mContact)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Contact(mContact)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        mContact = New Contact(BLL.BLLApp.Emp)
        Dim oFrm As New Frm_Contact(mContact)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CopyToClipboard(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Str As String = mContact.AdrFullAscii(True) & vbCrLf
        Dim sCaption As String = mContact.Clx
        root.ShowLiteral(sCaption, Str)
    End Sub

    Private Sub Do_CopyGuidToClipboard(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sGuid As String = mContact.Guid.ToString
        Clipboard.SetDataObject(sGuid, True)
        MsgBox("Guid copiat a clipboard:" & vbCrLf & sGuid)
    End Sub

    Private Sub Do_Saldos(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oFrm As New Frm_CliCtas(mContact)
        Dim oFrm As New Frm_Extracte(_Contact)
        oFrm.Show()
    End Sub

    Private Sub Do_SaldosVell(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CliCtas(mContact)
        'Dim oFrm As New Frm_Extracte(_Contact)
        oFrm.Show()
    End Sub

    Private Sub Do_Cobraments(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Cobrament(mContact)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Pagaments(ByVal sender As Object, ByVal e As System.EventArgs)
        root.WzPagament(mContact)
    End Sub

    Private Sub Do_LastMails(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowContactMails(mContact)
    End Sub

    Private Sub Do_NewMail(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMail As New Mail(mContact.Emp, Today)
        oMail.Contacts.Add(mContact)

        Dim oFrm As New Frm_Contact_Mail(oMail)
        oFrm.Show()
    End Sub

    Private Sub Do_NewMemo(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMem As New DTOMem
        With oMem
            .Usr = BLL.BLLSession.Current.User
            .Fch = Today
            .Contact = New DTOContact(mContact.Guid)
            BLL.BLLContact.Load(.Contact)

            Select Case .Usr.Rol.Id
                Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                    .Cod = DTOMem.Cods.Rep
                Case DTORol.Ids.Accounts
                    .Cod = DTOMem.Cods.Impagos
            End Select

        End With

        Dim oFrm As New Frm_Mem(oMem)
        oFrm.Show()
    End Sub

    Private Sub Do_Clidocs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CliDocs(mContact)
        oFrm.Show()
    End Sub

    Private Sub Do_Ftp(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Ftp(mContact.FtpUrl, mContact.FtpUserName, mContact.FtpPwd)
        oFrm.Show()
    End Sub

    Private Sub Do_SSL(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "IMPORTAR CERTIFICAT ELECTRONIC"
            .Filter = "*.pfx|*.pfx|tots els arxius|*.*"
            If .ShowDialog Then
                Dim sFch As String = InputBox("caducitat (DD/MM/YYYY):", "IMPORTACIO DE CERTIFICAT")
                If IsDate(sFch) Then
                    Dim oCert As MaxiSrvr.Cert = mContact.Cert
                    Dim sPwd As String = InputBox("password:", "IMPORTACIO DE CERTIFICAT")
                    Dim exs As New List(Of Exception)
                    If oCert.ImportFromFile(.FileName, sPwd, CDate(sFch), exs) Then
                        oCert.Update()
                    Else
                        UIHelper.WarnError(exs, "error al importar certificat" & Environment.NewLine)
                    End If
                Else
                    MsgBox("caducitat no valida", MsgBoxStyle.Exclamation, "IMPORTACIO DE CERTIFICAT")
                End If
            End If
        End With
    End Sub


    Private Sub Do_NewIncidenciaProducte()
        Dim oCustomer As New DTOCustomer(mContact.Guid)
        Dim oIncidencia As DTOIncidencia = BLL_Incidencia.NewFromCustomer(oCustomer, DTOIncidencia.Srcs.Producte)
        Dim oFrm As New Frm_Incidencia(oIncidencia)
        oFrm.Show()
    End Sub

    Private Sub Do_NewIncidenciaTransport()
        Dim oCustomer As New DTOCustomer(mContact.Guid)
        Dim oIncidencia As DTOIncidencia = BLL_Incidencia.NewFromCustomer(oCustomer, DTOIncidencia.Srcs.Transport)
        Dim oFrm As New Frm_Incidencia(oIncidencia)
        oFrm.Show()
    End Sub

    Private Sub Do_LastIncidencies()
        Dim oQuery As DTOIncidenciaQuery = BLL_Incidencies.DefaultQuery(BLL.BLLSession.Current.User)
        With oQuery
            .Customer = New DTOCustomer(mContact.Guid)
        End With
        Dim oFrm As New Frm_Last_Incidencies(oQuery)
        oFrm.Show()
    End Sub


    Private Sub Do_ShowRepresentants()
        Dim oFrm As New Frm_IntlAgents(mContact, Frm_IntlAgents.Modes.Representants)
        oFrm.Show()
    End Sub

    Private Sub Do_ShowRepresentades()
        Dim oFrm As New Frm_IntlAgents(mContact, Frm_IntlAgents.Modes.Representades)
        oFrm.Show()
    End Sub


    Private Sub Do_Truca(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        ' MatCommunicator.Truca(oMenuItem.Text)
    End Sub

    Private Sub Do_Credencials()
        Dim oContact As New DTOContact(mContact.Guid)
        Dim oFrm As New Frm_Credencials(oContact)
        oFrm.Show()
    End Sub

    Private Sub Do_Meetings()
        Dim oContact As New DTOContact(mContact.Guid)
        Dim oFrm As New Frm_Meetings(oContact)
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class
