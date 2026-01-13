

Public Class Menu_Contact
    Inherits Menu_Base

    Private _Contact As DTOContact

    Public Sub New(ByVal oContact As DTOContact)
        MyBase.New()
        _Contact = oContact
        AddMenuItems()
    End Sub


    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Tels())
        MyBase.AddMenuItem(MenuItem_Email())
        MyBase.AddMenuItem(MenuItem_AddNew())
        MyBase.AddMenuItem(MenuItem_CopyToClipboard)
        MyBase.AddMenuItem(MenuItem_Comptes)
        MyBase.AddMenuItem(MenuItem_Correspondencia)
        MyBase.AddMenuItem(MenuItem_CliDocs)
        MyBase.AddMenuItem(MenuItem_Incidencias)
        MyBase.AddMenuItem(MenuItem_SSL)
        MyBase.AddMenuItem(MenuItem_FTP)
        MyBase.AddMenuItem(MenuItem_Client)
        MyBase.AddMenuItem(MenuItem_Proveidor)
        MyBase.AddMenuItem(MenuItem_Rep)
        MyBase.AddMenuItem(MenuItem_Transportista)
        MyBase.AddMenuItem(MenuItem_Mgz)
        MyBase.AddMenuItem(MenuItem_Advanced)

        If Specific() Then
            MyBase.AddMenuItem(MenuItem_Specific)
        End If
    End Sub

    Private Function Specific() As Boolean
        Dim retval As Boolean
        If _Contact.Equals(DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Mayborn)) Or _Contact.Equals(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.Amazon)) Then
            retval = True
        End If
        Return retval
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        If _Contact Is Nothing Then oMenuItem.Enabled = False
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

    Private Function MenuItem_Tels() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "tel"
        AddHandler oMenuItem.MouseEnter, AddressOf TelsDropdown
        'If oMenuItem.DropDownItems.Count = 0 Then
        'oMenuItem.Enabled = False
        'End If
        Return oMenuItem
    End Function


    Private Async Sub TelsDropdown(sender As Object, e As EventArgs)
        Dim exs As New List(Of Exception)
        Dim oMenuItem As ToolStripMenuItem = sender
        Dim oDropDownItem As ToolStripMenuItem = Nothing
        If oMenuItem.DropDownItems.Count = 0 Then
            If _Contact.tels.Count = 0 Then
                _Contact.tels = Await FEB2.ContactTels.All(_Contact, DTOContactTel.Cods.NotSet, exs)
                If exs.Count > 0 Then
                    UIHelper.WarnError(exs)
                End If
            End If
            For Each item In _Contact.tels
                Select Case item.cod
                    Case DTOContactTel.Cods.tel, DTOContactTel.Cods.movil
                        oDropDownItem = New ToolStripMenuItem(DTOContactTel.Formatted(item) & " " & item.obs, Nothing, AddressOf Do_Truca)
                        oDropDownItem.Tag = item
                        oMenuItem.DropDownItems.Add(oDropDownItem)
                End Select
            Next
        End If
    End Sub


    Private Async Sub EmailDropdown(sender As Object, e As EventArgs)
        Dim exs As New List(Of Exception)
        Dim oMenuItem As ToolStripMenuItem = sender
        If oMenuItem.DropDownItems.Count = 0 Then
            Dim oUsers = Await FEB2.Users.All(exs, _Contact)
            If exs.Count = 0 Then
                For Each oUser In oUsers
                    'For Each oUser In oUsers
                    Dim oIcon As Image = Nothing
                    If oUser.BadMail <> DTOEmail.BadMailErrs.None Then
                        oIcon = My.Resources.warn
                    Else
                        'oIcon = Gravatar.Image(oEmail.Adr)
                    End If
                    Dim oMenuItemEmail As New ToolStripMenuItem(oUser.EmailAddress, oIcon)
                    Dim oMenuEmail As New Menu_User(oUser)
                    oMenuItemEmail.DropDownItems.AddRange(oMenuEmail.Range)
                    oMenuItem.DropDownItems.Add(oMenuItemEmail)
                Next
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Function MenuItem_AddNew() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_AddNew
        oMenuItem.Text = "Nou"
        oMenuItem.Image = My.Resources.clip
        Return oMenuItem
    End Function


    Private Function MenuItem_CopyToClipboard() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_CopyToClipboard
        oMenuItem.Text = "Copiar"
        oMenuItem.Image = My.Resources.Resources.Copy
        Return oMenuItem
    End Function


    Private Function MenuItem_SSL() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_SSL
        oMenuItem.Text = "importar certificat"
        oMenuItem.Image = My.Resources.Resources.pfx
        oMenuItem.Visible = Current.Session.User.Rol.id = DTORol.Ids.superUser
        Return oMenuItem
    End Function


    Private Function MenuItem_Ftp() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Ftp"
        oMenuItem.DropDownItems.Add(SubMenuItem_FtpZoom)
        oMenuItem.DropDownItems.Add(SubMenuItem_FtpBrowse)
        oMenuItem.DropDownItems.Add(SubMenuItem_FtpCopyUser)
        oMenuItem.DropDownItems.Add(SubMenuItem_FtpCopyPwd)
        'End If
        Return oMenuItem
    End Function

    Private Function SubMenuItem_FtpZoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_FtpZoom
        oMenuItem.Text = "Fitxa"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_FtpBrowse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_FtpBrowse
        oMenuItem.Text = "Explorar"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_FtpCopyUser() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_FtpCopyUser
        oMenuItem.Text = "Copiar usuari"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_FtpCopyPwd() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_FtpCopyPwd
        oMenuItem.Text = "Copiar password"
        Return oMenuItem
    End Function

    Private Function MenuItem_Comptes() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Comptes"
        oMenuItem.DropDownItems.Add(SubMenuItem_Saldos)
        oMenuItem.DropDownItems.Add(SubMenuItem_Cobraments)
        oMenuItem.DropDownItems.Add(SubMenuItem_Pagaments)
        oMenuItem.DropDownItems.Add(New ToolStripMenuItem("enllaç a Tpv", Nothing, AddressOf Do_TpvRequest))
        'End If
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
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Cobraments() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Cobraments
        oMenuItem.Text = "Cobrar"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Pagaments() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Pagaments
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
        Select Case Current.Session.User.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts
            Case Else
                oMenuItem.Visible = False
        End Select
        oMenuItem.Image = My.Resources.notepad
        Return oMenuItem
    End Function

    Private Function MenuItem_Client() As ToolStripMenuItem
        Dim exs As New List(Of Exception)
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Client..."
        If FEB2.Customer.ExistsSync(exs, _Contact) Then
            oMenuItem.Image = My.Resources.People_Blue
            Dim oCustomer As DTOCustomer = DTOCustomer.fromContact(_Contact)
            oMenuItem.DropDownItems.AddRange(New Menu_Client(oCustomer).Range)
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Proveidor() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case Current.Session.User.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts, DTORol.Ids.LogisticManager, DTORol.Ids.Operadora, DTORol.Ids.Marketing
                Dim exs As New List(Of Exception)
                If FEB2.Proveidor.Exists(_Contact, exs) Then
                    oMenuItem.Image = My.Resources.People_Orange
                    Dim oProveidor As DTOProveidor = DTOProveidor.FromContact(_Contact)
                    oMenuItem.DropDownItems.AddRange(New Menu_Proveidor(oProveidor).Range)
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
        Dim exs As New List(Of Exception)
        If FEB2.Rep.ExistsSync(exs, _Contact) Then
            Dim oRep = DTORep.FromContact(_Contact)
            oMenuItem.DropDownItems.AddRange(New Menu_Rep(oRep).Range)
        Else
            oMenuItem.Visible = False
        End If
        oMenuItem.Text = "rep..."
        Return oMenuItem
    End Function

    Private Function MenuItem_Transportista() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Transportista"
        Dim exs As New List(Of Exception)
        If FEB2.Transportista.ExistsSync(_Contact, exs) Then
            'oMenuItem.Image = My.Resources.People_Orange
            Dim oTransportista As New DTOTransportista(_Contact.Guid)
            oMenuItem.DropDownItems.AddRange(New Menu_Transportista(oTransportista).Range)
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
        If Current.Session.User.Rol.IsSuperAdmin Then
            oMenuItem.Text = "avançat"
            oMenuItem.DropDownItems.Add(SubMenuItem_CopyGuidToClipboard)
            oMenuItem.DropDownItems.Add(SubMenuItem_SetGln)
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function
    Private Function MenuItem_Specific() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "especific"
        If _Contact.Equals(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.Amazon)) Then
            Dim oAmazonCostAndInventoryFeedMenuItem As ToolStripMenuItem = oMenuItem.DropDownItems.Add("Amazon Cost & Inventory Feed")

            oAmazonCostAndInventoryFeedMenuItem.DropDownItems.Add(SubMenuItem_InvRptEdiQueue)
            oAmazonCostAndInventoryFeedMenuItem.DropDownItems.Add(SubMenuItem_InvRptEdiFile)
            oAmazonCostAndInventoryFeedMenuItem.DropDownItems.Add(SubMenuItem_InvRptExcel)
        End If
        Return oMenuItem
    End Function


    Private Function SubMenuItem_InvRptEdiQueue() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_AmazonCostAndInventoryEdiQueue
        oMenuItem.Text = "enviar per Edi"
        oMenuItem.Image = My.Resources.Resources.MailSobreGroc
        Return oMenuItem
    End Function

    Private Function SubMenuItem_InvRptEdiFile() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_AmazonCostAndInventoryFeedFile
        oMenuItem.Text = "desar fitxer Edi"
        oMenuItem.Image = My.Resources.Resources.disk
        Return oMenuItem
    End Function

    Private Function SubMenuItem_InvRptExcel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_AmazonCostAndInventoryExcel
        oMenuItem.Text = "Excel"
        oMenuItem.Image = My.Resources.Resources.Excel
        Return oMenuItem
    End Function

    Private Function SubMenuItem_SetGln() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_SetGln
        oMenuItem.Text = "assigna GLN"
        oMenuItem.Enabled = _Contact.GLN Is Nothing
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



    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Contact(_Contact)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        _Contact = New DTOContact()
        Dim oFrm As New Frm_Contact(_Contact)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CopyToClipboard(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.Contact.Load(_Contact, exs) Then
            Dim src As String = DTOAddress.MultiLine(_Contact.Address)
            Dim sCaption As String = _Contact.FullNom
            Dim oFrm As New Frm_Literal(sCaption, src)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_CopyGuidToClipboard(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sGuid As String = _Contact.Guid.ToString
        Clipboard.SetDataObject(sGuid, True)
        MsgBox("Guid copiat a clipboard:" & vbCrLf & sGuid)
    End Sub

    Private Sub Do_Saldos(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Extracte(_Contact)
        oFrm.Show()
    End Sub

    Private Sub Do_Cobraments(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Cobrament(_Contact)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Pagaments(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oProveidor As New DTOProveidor(_Contact.Guid)
        Dim oFrm As New Frm_Pagament(oProveidor)
        oFrm.Show()
    End Sub

    Private Sub Do_LastMails(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Contact_Correspondencia(_Contact)
        oFrm.Show()
    End Sub

    Private Sub Do_NewMail(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMail = DTOCorrespondencia.Factory(Current.Session.User, _Contact, DTOCorrespondencia.Cods.NotSet)
        Dim oFrm As New Frm_Correspondencia(oMail)
        oFrm.Show()
    End Sub

    Private Sub Do_NewMemo(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oMem = DTOMem.Factory(Current.Session.User)
        With oMem
            If FEB2.Contact.Load(_Contact, exs) Then
                .Contact = _Contact.ToGuidNom()
                Select Case Current.Session.User.Rol.id
                    Case DTORol.Ids.rep, DTORol.Ids.comercial
                        .Cod = DTOMem.Cods.Rep
                    Case DTORol.Ids.accounts
                        .Cod = DTOMem.Cods.Impagos
                End Select
            Else
                UIHelper.WarnError(exs)
            End If
        End With

        Dim oFrm As New Frm_Mem(oMem)
        oFrm.Show()
    End Sub

    Private Sub Do_Clidocs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CliDocs(_Contact)
        oFrm.Show()
    End Sub

    Private Sub Do_SSL(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Cert(_Contact)
        oFrm.Show()
    End Sub

    Private Sub Do_FtpZoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFtp As New DTOFtpserver(_Contact)
        Dim oFrm As New Frm_Ftpserver(oFtp)
        oFrm.Show()
    End Sub

    Private Async Sub Do_FtpBrowse(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oFtp = Await FEB2.Ftpserver.Find(exs, _Contact)
        If exs.Count = 0 Then
            If oFtp Is Nothing Then
                UIHelper.WarnError(New Exception("No hi ha cap servidor Ftp configurat per aquest contacte"))
            Else
                Process.Start(oFtp.Servername)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_FtpCopyUser(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oFtp = Await FEB2.Ftpserver.Find(exs, _Contact)
        If exs.Count = 0 Then
            If oFtp Is Nothing Then
                UIHelper.WarnError(New Exception("No hi ha cap servidor Ftp configurat per aquest contacte"))
            Else
                UIHelper.CopyToClipboard(oFtp.Username, "usuari copiat al portapapers")
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_FtpCopyPwd(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oFtp = Await FEB2.Ftpserver.Find(exs, _Contact)
        If exs.Count = 0 Then
            If oFtp Is Nothing Then
                UIHelper.WarnError(New Exception("No hi ha cap servidor Ftp configurat per aquest contacte"))
            Else
                UIHelper.CopyToClipboard(oFtp.Password, "password copiat al portapapers")
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Sub Do_NewIncidenciaProducte()
        Dim exs As New List(Of Exception)
        If FEB2.Contact.Load(_Contact, exs) Then
            Dim oCustomer = DTOCustomer.FromContact(_Contact)
            Dim oIncidencia As DTOIncidencia = DTOIncidencia.Factory(oCustomer, DTOIncidencia.Srcs.Producte)
            Dim oFrm As New Frm_Incidencia(oIncidencia)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_NewIncidenciaTransport()
        Dim exs As New List(Of Exception)
        If FEB2.Contact.Load(_Contact, exs) Then
            Dim oCustomer = DTOCustomer.FromContact(_Contact)
            Dim oIncidencia As DTOIncidencia = DTOIncidencia.Factory(oCustomer, DTOIncidencia.Srcs.Transport)
            Dim oFrm As New Frm_Incidencia(oIncidencia)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_LastIncidencies()
        Dim oQuery = DTOIncidenciaQuery.Factory(Current.Session.User)
        With oQuery
            .Customer = New DTOCustomer(_Contact.Guid)
        End With
        Dim oFrm As New Frm_Last_Incidencies(oQuery)
        oFrm.Show()
    End Sub

    Private Async Sub Do_AmazonCostAndInventoryEdiQueue()
        Dim exs As New List(Of Exception)
        Dim oCustomer = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.Amazon)
        If Await FEB2.EdiversaInvRpt.Send(exs, GlobalVariables.Emp, oCustomer) Then
            MsgBox("fitxer Edi en cua")
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_AmazonCostAndInventoryFeedFile()
        Dim exs As New List(Of Exception)
        Dim oCustomer = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.Amazon)
        Dim src As String = Await FEB2.EdiversaInvRpt.Src(exs, Current.Session.Emp, oCustomer)
        If exs.Count = 0 Then
            Dim sFilename = String.Format("Amazon Cost & Inventory feed {0:yyyyMMddHHmmss}", Now)
            UIHelper.SaveTextFileDialog(src, "Amazon Cost & Inventory feed", sFilename)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_AmazonCostAndInventoryExcel()
        Dim exs As New List(Of Exception)
        Dim oCustomer = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.Amazon)
        Dim oSheet = Await FEB2.EdiversaInvRpt.Excel(exs, GlobalVariables.Emp, oCustomer)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_SetGln()
        Dim exs As New List(Of Exception)
        If FEB2.Contact.Load(_Contact, exs) Then
            If _Contact.GLN Is Nothing Then
                If Await FEB2.Aecoc.NextEanToContact(GlobalVariables.Emp, _Contact, exs) Then
                    RefreshRequest(Me, MatEventArgs.Empty)
                    MsgBox("assignat el GLN " & _Contact.GLN.Value)
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                MsgBox("Aquest contacte ja te GLN assignat: " & _Contact.GLN.Value)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub




    Private Sub Do_Truca(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        Dim oTel As DTOContactTel = oMenuItem.Tag
        Cx3OutboundCall.MakeCall(oTel.value)
    End Sub


    Private Sub Do_TpvRequest()
        Dim oFrm As New Frm_TpvRequest(_Contact)
        oFrm.Show()
    End Sub


End Class
