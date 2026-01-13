Imports System.IO
Imports System.ComponentModel


Public Class Frm_Blog
    Private mAllowEvents As Boolean
    Private mOcultarValidats As Boolean

    Private _BackgroundWorker As BackgroundWorker

    Private Enum Epigrafes
        Leads
        Subscriptors
        Posts
        SearchKeys
        Sorteos
    End Enum

    Private Sub Frm_Blog_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadMainGrid()
        SelectEpigrafe(Epigrafes.Leads)
        LoadDetailsGrid()
        mAllowEvents = True
    End Sub

    Private Function GetEpigrafes() As DataTable
        Dim oTb As New DataTable
        oTb.Columns.Add("epigrafe", System.Type.GetType("System.String"))

        For Each s As String In [Enum].GetNames(GetType(Epigrafes))
            Dim oRow As DataRow = oTb.NewRow
            oRow(0) = s
            oTb.Rows.Add(oRow)
        Next
        Return oTb
    End Function

    Private Sub SelectEpigrafe(oEpigrafe As Epigrafes)
        Dim iRow As Integer = 0
        If DataGridView1.Rows.Count > CInt(oEpigrafe) Then
            iRow = CInt(oEpigrafe)
        End If
        DataGridView1.CurrentCell = DataGridView1.Rows(iRow).Cells(0)
        SetContextMenuMain()
    End Sub

    Private Sub LoadMainGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            Dim oTable As DataTable = GetEpigrafes()
            Dim iRows As Integer = oTable.Rows.Count
            .DataSource = oTable.DefaultView
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(0)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With

    End Sub

    Private Sub LoadDetailsGrid(Optional oSortedColumn As DataGridViewColumn = Nothing)
        mAllowEvents = False
        Dim oCurrentEpigrafe As Epigrafes = CurrentEpigrafe()
        Select Case oCurrentEpigrafe
            Case Epigrafes.Leads
                LoadLeads(oSortedColumn)
            Case Epigrafes.Subscriptors
                LoadSubscriptors()
            Case Epigrafes.Posts
                LoadPosts()
            Case Epigrafes.SearchKeys
                LoadSearchKeys()
            Case Epigrafes.Sorteos
                LoadSorteos()
        End Select
        SetContextMenuDetail()
        mAllowEvents = True
    End Sub

    Private Sub LoadLeads(Optional oSortedColumn As DataGridViewColumn = Nothing)
        Dim SQL As String = "SELECT Guid, adr, Nom, Cognoms, BirthYea, Pais, Tel, Source, FchCreated, FchDeleted " _
                            & "FROM Email WHERE EMP=1 AND Email.Guid NOT IN " _
                            & "(SELECT EmailGuid FROM EMAIL_CLIS) AND Source>0 "

        If TextBoxSearch.Text > "" Then
            SQL = SQL & "AND Adr LIKE '%" & TextBoxSearch.Text & "%' "
        End If

        Dim SQLORDER As String = "ORDER BY FchCreated Desc, adr"

        If oSortedColumn IsNot Nothing Then
            If oSortedColumn.Index = 1 Then
                SQLORDER = "ORDER BY adr"
            End If
        End If

        SQL = SQL & SQLORDER

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim iSubscriptors As Integer
        For Each oRow As DataRow In oTb.Rows
            If IsDBNull(oRow("FchDeleted")) Then
                iSubscriptors += 1
            End If
        Next

        LabelStats.Text = Format(iSubscriptors, "#,###") & " subscriptors"

        With DataGridView2
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowDrop = True
            .AllowUserToResizeRows = False

            With .Columns(0)
                .Visible = False
            End With
            With .Columns(1)
                .HeaderText = "email"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(2)
                .HeaderText = "nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
            End With
            With .Columns(3)
                .HeaderText = "Cognoms"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(4)
                .HeaderText = "any"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
            End With
            With .Columns(5)
                .HeaderText = "pais"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 25
            End With
            With .Columns(6)
                .HeaderText = "tel"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 30
            End With
            With .Columns(7)
                .HeaderText = "src"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 25
            End With
            With .Columns(8)
                .HeaderText = "alta"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(9)
                .Visible = False
            End With

        End With

    End Sub

    Private Sub LoadSubscriptors()
        Dim SQL As String = "SELECT Guid, eMail, FchCreated FROM WpSubscriptors ORDER BY FchCreated Desc, email"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        With DataGridView2
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(0)
                .Visible = False
            End With
            With .Columns(1)
                .HeaderText = "email"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(2)
                .HeaderText = "alta"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

        End With


    End Sub

    Private Sub LoadPosts()
        Dim SQL As String = "SELECT id,post_date,post_title,comment_count,url FROM OPENQUERY(WORDPRESS995,'SELECT id,post_date,post_title,comment_count, guid as url FROM wp_posts where post_type like ''post'' AND post_status like ''publish'' ORDER BY post_date DESC')"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        With DataGridView2
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(0)
                .HeaderText = "Id"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 30
            End With
            With .Columns(1)
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(2)
                .HeaderText = "titol"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(3)
                .HeaderText = "comentaris"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
            End With
            With .Columns(4)
                .HeaderText = "url"
                .Visible = False
            End With

        End With
    End Sub

    Private Sub LoadSearchKeys()
        Dim oSearchRequests As List(Of DTOSearchRequest) = BLL.BLLSearchRequests.All()
        Dim oControl As New Xl_SearchRequests
        oControl.Load(oSearchRequests)
        oControl.Dock = DockStyle.Fill
        SplitContainer1.Panel2.Controls.Add(oControl)
        oControl.BringToFront()
    End Sub

    Private Sub LoadSorteos()
        Dim oRaffles As List(Of DTOContestBase) = BLL.BLLRaffles.AllBases()
        Dim SQL As String = "SELECT Sorteos.Guid,Sorteos.FchFrom,Sorteos.FchTo,Sorteos.Title, " _
                            & "count(Distinct SorteoLeads.Lead) AS leads, " _
                            & "SUM(CASE WHEN Guid.FchCreated >= Sorteos.FchFrom THEN 1 ELSE 0 END) AS NewLeads " _
                            & "FROM Sorteos " _
                            & "LEFT OUTER JOIN SorteoLeads ON SorteoLeads.Sorteo = Sorteos.Guid  " _
                            & "LEFT OUTER JOIN Guid ON SorteoLeads.Lead = Guid.Guid " _
                            & "GROUP BY Sorteos.Guid,Sorteos.FchFrom,Sorteos.FchTo,Sorteos.Title " _
                            & "ORDER BY Sorteos.FchTo DESC"

        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView2
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns("Guid")
                .Visible = False
            End With
            With .Columns("FchFrom")
                .HeaderText = "des de"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns("FchTo")
                .HeaderText = "fins"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns("Title")
                .HeaderText = "titol"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns("Leads")
                .HeaderText = "leads"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
                .DefaultCellStyle.Format = "#,###"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns("NewLeads")
                .HeaderText = "nous"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
                .DefaultCellStyle.Format = "#,###"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

        End With
    End Sub

    Private Function CurrentEpigrafe() As Epigrafes
        Dim iRow As Integer = DataGridView1.CurrentRow.Index
        Dim retval As Epigrafes = CType(iRow, Epigrafes)
        Return retval
    End Function

    Private Function CurrentUsers() As List(Of DTOUser)
        Dim retval As New List(Of DTOUser)
        Dim oGrid As DataGridView = DataGridView2

        If oGrid.SelectedRows.Count > 0 Then
            Dim oRow As DataGridViewRow
            For Each oRow In oGrid.SelectedRows
                Dim oGuid As Guid = oGrid.CurrentRow.Cells(0).Value
                Dim oUser As New DTOUser(oGuid)
                retval.Add(oUser)
            Next
        Else
            Dim oUser As DTOUser = CurrentUser()
            If oUser IsNot Nothing Then
                retval.Add(CurrentUser)
            End If
        End If
        Return retval
    End Function

    Private Function CurrentUser() As DTOUser
        Dim retval As DTOUser = Nothing
        If DataGridView2.CurrentRow IsNot Nothing Then
            Dim oGuid As Guid = DataGridView2.CurrentRow.Cells(0).Value
            retval = New DTOUser(oGuid)
        End If
        Return retval
    End Function


    Private Function CurrentPost() As DTOBlogPost
        Dim PostId As Integer = DataGridView2.CurrentRow.Cells(0).Value
        Dim retval As DTOBlogPost = BLL.BLLWpPost.FromId(PostId)
        Return retval
    End Function

    Private Function CurrentRaffle() As DTORaffle
        Dim retval As DTORaffle = Nothing
        If DataGridView2.CurrentRow IsNot Nothing Then
            Dim oGuid As Guid = DataGridView2.CurrentRow.Cells(0).Value
            retval = BLL.BLLRaffle.Find(oGuid)
        End If
        Return retval
    End Function

    Private Sub SetContextMenuMain()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As Epigrafes = CurrentEpigrafe()
        If oItm >= 0 Then
            Select Case oItm
                Case Epigrafes.Leads
                    oMenuItem = New ToolStripMenuItem("exportar tots els emails actius", Nothing, AddressOf Do_ExportActiveLeads)
                    oContextMenuStrip.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("exportar emails no suscrits a WordPress", Nothing, AddressOf Do_ExportActiveNonWpLeads)
                    oContextMenuStrip.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("importar emails i noms de Excel", Nothing, AddressOf Do_ImportLeadsFromExcel)
                    oContextMenuStrip.Items.Add(oMenuItem)
                Case Epigrafes.Subscriptors
                    oMenuItem = New ToolStripMenuItem("importar fitxer de subscriptors de WordPress", Nothing, AddressOf Do_ImportarFitxerDeSubscriptorsDeWordPress)
                    oContextMenuStrip.Items.Add(oMenuItem)
                Case Epigrafes.Posts
                    oMenuItem = New ToolStripMenuItem("importar emails dels comentaris de WordPress", Nothing, AddressOf Do_ImportarEmailsDelsComentaris)
                    oContextMenuStrip.Items.Add(oMenuItem)
                Case Epigrafes.Sorteos
                    oMenuItem = New ToolStripMenuItem("afegir nou sorteig", Nothing, AddressOf Do_Sorteo_AddNew)
                    oContextMenuStrip.Items.Add(oMenuItem)

            End Select
        End If

        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub SetContextMenuDetail()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing


        Select Case CurrentEpigrafe()
            Case Epigrafes.Leads
                Dim oItms As List(Of DTOUser) = CurrentUsers()
                If oItms.Count > 0 Then
                    oMenuItem = New ToolStripMenuItem("zoom", Nothing, AddressOf Do_Zoom)
                    oMenuItem.Enabled = (oItms.Count = 1)
                    oContextMenuStrip.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("copiar enllaç", Nothing, AddressOf Do_LeadCopyLink)
                    oMenuItem.Enabled = (oItms.Count = 1)
                    oContextMenuStrip.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("passar primer cognom a nom", Nothing, AddressOf Do_LeadSwitchCognomToNom)
                    oMenuItem.Enabled = (oItms.Count = 1)
                    oContextMenuStrip.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("enviar email activació subscripció", Nothing, AddressOf Do_SendActivationRequest)
                    oMenuItem.Enabled = (oItms.Count = 1 And oItms(0).FchActivated = Nothing)
                    oContextMenuStrip.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("donar de baixa la subscripció i enviar email", Nothing, AddressOf Do_LeadUnSubscribe)
                    oMenuItem.Enabled = (oItms.Count = 1 And oItms(0).FchDeleted = Nothing)
                    oContextMenuStrip.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("total " & oItms.Count & " leads")
                    oMenuItem.Visible = (oItms.Count > 1)
                    oContextMenuStrip.Items.Add(oMenuItem)
                End If

                Dim sOcultarValidatsLabel As String = IIf(mOcultarValidats, "mostrar tots", "ocultar validats")
                oMenuItem = New ToolStripMenuItem(sOcultarValidatsLabel, Nothing, AddressOf Do_OcultarValidats)
                oMenuItem = New ToolStripMenuItem("Afegir", Nothing, AddressOf Do_LeadNew)
                oContextMenuStrip.Items.Add(oMenuItem)
            Case Epigrafes.Posts
                Dim oItm As DTOBlogpost = CurrentPost()

                If oItm IsNot Nothing Then
                    Dim oMenu_BlogPost As New Menu_BlogPost(oItm)
                    oContextMenuStrip.Items.AddRange(oMenu_BlogPost.Range)
                End If
        End Select

        DataGridView2.ContextMenuStrip = oContextMenuStrip
    End Sub




    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenuMain()
            LoadDetailsGrid()
        End If
    End Sub

    Private Sub DataGridView2_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView2.CellFormatting
        Select Case CurrentEpigrafe()
            Case Epigrafes.Leads
                Select Case e.ColumnIndex
                    Case 1
                        Dim sEmail As String = e.Value
                        If Not maxisrvr.IsValidEmailAddress(sEmail) Then
                            e.CellStyle.BackColor = Color.Yellow
                        End If
                End Select
        End Select
    End Sub

    Private Sub DataGridView2_DoubleClick(sender As Object, e As System.EventArgs) Handles DataGridView2.DoubleClick
        Do_Zoom()
    End Sub

    Private Sub DataGridView2_RowPrePaint(sender As Object, e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView2.RowPrePaint
        Select Case CurrentEpigrafe()
            Case Epigrafes.Leads
                Dim oRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                Dim iFchBaja As Integer = 9
                If Not IsDBNull(oRow.Cells(iFchBaja).Value) Then
                    oRow.DefaultCellStyle.BackColor = Color.LightGray
                End If
        End Select
    End Sub

    Private Sub DataGridView2_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView2.SelectionChanged
        If mAllowEvents Then
            'Stop
            SetContextMenuDetail()
        End If
    End Sub

    Private Sub Do_ImportarFitxerDeSubscriptorsDeWordPress()
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "importar subscriptors de WordPress"
            .Filter = "documents csv (*.csv)|*.csv"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim iCount As Integer
                Dim iNew As Integer
                Dim iSwitchedSrc As Integer
                Dim oArray As ArrayList = maxisrvr.getTextLinesArrayFromFilename(.FileName)

                For Each sLine As String In oArray
                    iCount += 1
                    Dim sEmail As String = Microsoft.VisualBasic.Left(sLine, 100)
                    Dim oLead As Lead = LeadLoader.FromEmail(sEmail)
                    If oLead Is Nothing Then
                        iNew += 1
                        oLead = New Lead
                        With oLead
                            .Email = sEmail
                            .Source = Lead.Sources.WpFollower
                        End With
                        LeadLoader.Update(oLead)
                    ElseIf oLead.Source = Lead.Sources.WpComment Then
                        iSwitchedSrc += 1
                        oLead.Source = Lead.Sources.WpFollower
                        LeadLoader.Update(oLead)
                    End If
                Next

                LoadDetailsGrid()
                MsgBox("importats " & iNew.ToString & vbCrLf & "actualitzats " & iSwitchedSrc.ToString & vbCrLf & "sobre un total de " & iCount.ToString)

            End If
        End With
    End Sub

    Private Sub Do_ImportarEmailsDelsComentaris()
        Dim SQL As String = "SELECT comment_author_email,MAX(comment_author) as comment_author " _
                            & "FROM OPENQUERY(WORDPRESS995,'SELECT comment_post_ID,comment_author,comment_author_email FROM wp_comments WHERE comment_approved=1 and comment_author_email>''''') " _
                            & "WHERE comment_author_email collate Modern_Spanish_CI_AS not in (SELECT Adr FROM Email) " _
                            & "GROUP BY comment_author_email"
        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        Dim iCommentCount As Integer
        Do While oDrd.Read
            Dim sEmail As String = oDrd("comment_author_email").ToString
            Dim sNom As String = oDrd("comment_author").ToString
            If sEmail.Length > 100 Or sNom.Length > 50 Then Stop
            Dim oUser As New DTOUser(App.Current.Lang)
            With oUser
                .EmailAddress = sEmail
                .Cognoms = sNom
                .Source = Lead.Sources.WpComment
                .Rol = New DTORol(DTORol.Ids.Lead)
                .Password = RandomString(5)
            End With
            Dim exs As New List(Of exception)
            If BLL.BLLUser.Update(oUser, exs) Then
                iCommentCount += 1
            Else
                UIHelper.WarnError(exs, "error al desar l'usuari")
            End If
        Loop
        oDrd.Close()
        MsgBox("Importats " & iCommentCount & " emails de comentaris", MsgBoxStyle.Information)
    End Sub

    Private Sub Do_Zoom()
        Select Case CurrentEpigrafe()
            Case Epigrafes.Leads
                Dim oUser As DTOUser = CurrentUser
                Dim oFrm As New Frm_User(oUser)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case Epigrafes.Posts
                Dim oPost As DTOBlogpost = CurrentPost()
                MsgBox("zoom post " & oPost.Id)
            Case Epigrafes.Sorteos
                Dim oRaffle As DTORaffle = CurrentRaffle
                Dim oFrm As New Frm_Raffle(oRaffle)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
        End Select
    End Sub

    Private Sub Do_LeadNew()
        Dim oUser As New DTOUser(BLL.BLLApp.Lang)
        oUser.Source = Lead.Sources.Manual
        Dim oFrm As New Frm_User(oUser)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_OcultarValidats()
        mOcultarValidats = Not mOcultarValidats
        LoadLeads(DataGridView2.SortedColumn)
    End Sub

    Private Sub Do_Sorteo_AddNew()
        'Dim oSorteo As New Sorteo()
        'Dim oFrm As New Frm_Sorteo(oSorteo)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub Do_LeadCopyLink()
        Dim oUser As DTOUser = CurrentUser()
        'Dim sUrl As String = BLL_Usuari.url(True)
        'Clipboard.SetDataObject(sUrl, True)
        'MsgBox("adreça copiada al portapapers:" & vbCrLf & sUrl)
    End Sub

    Private Sub Do_LeadSwitchCognomToNom()
        Dim oUser As DTOUser = CurrentUser()
        BLL.BLLUser.Load(oUser)
        Dim sCognoms() As String = oUser.Cognoms.Split(" ")
        oUser.Nom = StrConv(sCognoms(0), VbStrConv.ProperCase)
        Select Case sCognoms.Length
            Case 1
                oUser.Cognoms = "-"
            Case Else
                Dim iPos As Integer = oUser.Cognoms.IndexOf(" ")
                oUser.Cognoms = StrConv(oUser.Cognoms.Substring(iPos + 1), VbStrConv.ProperCase)
        End Select

        Dim exs As New List(Of exception)
        If BLL.BLLUser.Update(oUser, exs) Then
            RefreshRequest(Me, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs, "error al desar l'usuari")
        End If
    End Sub

    Private Sub Do_SendActivationRequest()
        'Dim oUser As User = CurrentUser()
        'oUser.SendActivationRequest()
    End Sub

    Private Sub Do_LeadUnSubscribe()
        Dim oUser As DTOUser = CurrentUser()
        BLL.BLLUser.Load(oUser)
        oUser.FchDeleted = Now
        Dim exs As New List(Of exception)
        If BLL.BLLUser.Update(oUser, exs) Then
            Dim oLang As DTOLang = oUser.Lang
            Dim oTxt As New Txt(Txt.Ids.LeadAccountBaixa)
            Dim sBody As String = oTxt.ToHtml(oLang, BLL.BLLUser.GetSaludo(oUser), BLL.BLLUser.GetFirma(oUser))
            Dim sSubject As String = oLang.Tradueix("baja de subscripción", "baixa de subscripció", "account removal")
            BLL.MailHelper.SendMail(oUser.EmailAddress, , , sSubject, sBody, BLL.FileSystemHelper.OutputFormat.HTML, , exs)
            RefreshRequest(Nothing, System.EventArgs.Empty)
        Else
            UIHelper.WarnError(exs, "error al cancelar subscripció")
        End If

    End Sub

    Private Sub Do_LeadUnSubscribeMail()
        Dim oUser As DTOUser = CurrentUser()
        Dim sUrl As String = BLL.BLLUser.emailBaixa(oUser, True)
        UIHelper.ShowHtml(sUrl)
    End Sub


    Private Sub Do_PostNewsletter()
        Dim oPost As DTOBlogpost = CurrentPost()
        Dim sUrl As String = BLL_BlogPost.BodyUrlForMailing(oPost)

        Dim oEmails As New Emails
        With oEmails
            .AddRange(WpBlogLoader.LeadsNoSuscritsAWordPress)
            .AddRange(RepsLoader.ActiveEmails)
            .AddRange(ClientsLoader.EmailsBotiguersForBlog)
            .AddRange(ClientsLoader.EmailsEshopsForBlog)
            .Add(EmailLoader.FromAddress("info@matiasmasso.es"))
            '.Add(EmailLoader.FromAddress("matias@matiasmasso.es"))
        End With

        Dim oMailing As New Mailing(Today)
        With oMailing
            .Subject = oPost.Title
            .Body = BLL.FileSystemHelper.DownloadHtml(sUrl)
            .Recipients = Mailing.UniqueRecipients(oEmails)
        End With

        Dim exs As New List(Of exception)
        If MailingLoader.Update(oMailing, exs) Then
            'MailingLoader.Send5000()
            'MsgBox("enviats " & oMailing.RecipientsCount & " missatges")
            MsgBox("mailing desat correctament, será enviat a raó de 5.000 missatges/dia")
        Else
            UIHelper.WarnError(exs, "error al desar el mailing")
        End If
    End Sub


    Private Sub Do_ExportActiveNonWpLeads()
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = "exportar leads actius no suscrits al wp"
            .Filter = "documents csv (*.csv)|*.csv"
            .FileName = "emailsActiusNoSuscritsaWp.csv"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim writeFile As System.IO.TextWriter = New System.IO.StreamWriter(.FileName)
                Dim oEmails As Emails = WpBlogLoader.LeadsNoSuscritsAWordPress

                For Each oEmail As Email In oEmails
                    writeFile.WriteLine(oEmail.Adr)
                Next
                writeFile.Flush()
                writeFile.Close()
                writeFile = Nothing
                MsgBox(oEmails.Count & " emails no suscrits")
            End If

        End With

    End Sub


    Private Sub Do_ExportActiveLeads()
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = "exportar leads actius"
            .Filter = "documents csv (*.csv)|*.csv"
            .FileName = "emailsActius.csv"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim SQL As String = "SELECT Guid, Adr FROM Email WHERE Rol=98 AND FchDeleted IS NULL"
                Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
                Dim writeFile As System.IO.TextWriter = New System.IO.StreamWriter(.FileName)
                Dim iCount As Integer
                Do While oDrd.Read
                    Dim sEmail As String = oDrd("adr").ToString
                    writeFile.WriteLine(sEmail)
                    iCount += 1
                Loop
                oDrd.Close()
                writeFile.Flush()
                writeFile.Close()
                writeFile = Nothing
                MsgBox(iCount & " leads")
            End If

        End With

    End Sub

    Private Sub Do_ImportLeadsFromExcel()
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "importar leads i noms de Csv"
            .Filter = "documents csv (*.csv)|*.csv"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim sFile As String = BLL.FileSystemHelper.GetStringContentFromFile(.FileName)
                Dim sLines() As String = sFile.Split(vbCrLf)
                With ProgressBar1
                    .Visible = True
                    .Minimum = 0
                    .Value = 0
                    .Maximum = sLines.Length
                End With
                Application.DoEvents()
                Dim iCount As Integer
                For Each sLine As String In sLines
                    Dim sFields() As String = sLine.Split(";")
                    Dim sEmail As String = sFields(0).Trim
                    Dim oLead As Lead = LeadLoader.FromEmail(sEmail)
                    If oLead Is Nothing Then
                        oLead = New Lead
                        oLead.Email = sEmail
                        If sFields.Length > 1 Then
                            oLead.Cognoms = sFields(1)
                        End If
                        oLead.Source = Lead.Sources.Manual
                        LeadLoader.Update(oLead)
                        iCount += 1
                    End If
                    ProgressBar1.Increment(1)
                    Application.DoEvents()
                Next

                MsgBox("importats " & iCount & " leads de un total de " & sLines.Length)
                ProgressBar1.Visible = False
                RefreshRequest(Nothing, EventArgs.Empty)
            End If

        End With
    End Sub



    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = 1
        Dim oGrid As DataGridView = DataGridView2


        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        If oRow IsNot Nothing Then
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadDetailsGrid(oGrid.SortedColumn)

        If oGrid.CurrentRow Is Nothing Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub TextBoxSearch_TextChanged(sender As Object, e As System.EventArgs) Handles TextBoxSearch.TextChanged
        If mAllowEvents Then
            LoadDetailsGrid()
        End If
    End Sub

#Region "DragDrop"

    Private Sub DataGridView2_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView2.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Or e.Data.GetDataPresent("FileGroupDescriptor") Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub


    Private Sub DataGridView2_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView2.DragDrop
        If e.Data.GetDataPresent("FileGroupDescriptor") Then
            'supports a drop of a Outlook message

            Dim objMI As Microsoft.Office.Interop.Outlook.MailItem

            Dim objOL As New Microsoft.Office.Interop.Outlook.Application
            Dim oExplorer As Microsoft.Office.Interop.Outlook.Explorer = objOL.ActiveExplorer
            Dim oSelection As Microsoft.Office.Interop.Outlook.Selection = oExplorer.Selection
            For Each objMI In oSelection
                Dim sSubject As String = objMI.Subject
                If sSubject.ToLower.Contains("baja") Then
                    Do_Unsubscribe(objMI)
                Else
                    Stop
                End If
            Next
        End If
    End Sub

    Private Sub Do_Unsubscribe(objMI As Microsoft.Office.Interop.Outlook.MailItem)
        Dim sFrom As String = objMI.SenderEmailAddress.ToLower
        Dim oEmail As Email = EmailLoader.FromAddress(sFrom)
        If oEmail Is Nothing Then
            MsgBox(sFrom & " no está registrat", MsgBoxStyle.Exclamation)
        Else
            Dim Found As Boolean
            For Each oRow As DataGridViewRow In DataGridView2.Rows
                If oRow.Cells(1).Value.ToString.ToLower = sFrom Then
                    mAllowEvents = False
                    DataGridView2.ClearSelection()
                    oRow.Selected = True
                    DataGridView2.CurrentCell = oRow.Cells(1)
                    DataGridView2.FirstDisplayedScrollingRowIndex = oRow.Index
                    mAllowEvents = True
                    SetContextMenuDetail()
                    Found = True
                    Exit For
                End If
            Next
            If Not Found Then
                MsgBox(sFrom & " existeix pero no está registrat com a lead", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub
#End Region
End Class