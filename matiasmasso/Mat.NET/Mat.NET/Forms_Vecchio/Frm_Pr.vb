Public Class Frm_Pr

    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mAllowEvents As Boolean
    Private mMenuItemsEspecialitats As ArrayList

    Private Enum Nodes
        Root
        Editorials
        Revistes
        Creativitat
        Contactes
    End Enum

    Private Enum ColsEditorial
        Guid
        Nom
    End Enum

    Private Enum ColsRevista
        Guid
        Nom
        Editorial
    End Enum

    Private Enum ColsCreativitat
        Guid
        TpaNom
        AdNom
    End Enum

    Private Enum ColsContact
        Guid
        Status
        email
        Nom
        clx
        obs
    End Enum

    Private Sub Frm_Pr_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadTree()
    End Sub

    Private Sub LoadTree()
        Dim oRootNode As New MaxiSrvr.TreeNodeObj("PR", , Nodes.Root)
        TreeView1.Nodes.Add(oRootNode)

        oRootNode.Nodes.Add(New MaxiSrvr.TreeNodeObj("editorials", , Nodes.Editorials))
        oRootNode.Nodes.Add(New MaxiSrvr.TreeNodeObj("revistes", , Nodes.Revistes))
        oRootNode.Nodes.Add(New MaxiSrvr.TreeNodeObj("creativitat", , Nodes.Creativitat))
        oRootNode.Nodes.Add(New MaxiSrvr.TreeNodeObj("contactes", , Nodes.Contactes))

        TreeView1.ExpandAll()
        Dim oDefaultNode As MaxiSrvr.TreeNodeObj = TreeView1.Nodes(0).Nodes(0)
        TreeView1.SelectedNode = oDefaultNode
        LoadGrid(oDefaultNode)
    End Sub


    Private Function SelectedNode() As MaxiSrvr.TreeNodeObj
        Dim oNode As MaxiSrvr.TreeNodeObj = CType(TreeView1.SelectedNode, MaxiSrvr.TreeNodeObj)
        Return oNode
    End Function

    Private Function CurrentNodeCod() As Nodes
        Dim retVal As Nodes
        Dim oNode As MaxiSrvr.TreeNodeObj = SelectedNode()
        If oNode IsNot Nothing Then
            retVal = oNode.Cod
        End If
        Return retVal
    End Function

    Private Sub TreeView1_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        LoadGrid(e.Node)
    End Sub

    Private Sub LoadGrid(oNode As MaxiSrvr.TreeNodeObj)
        Select Case oNode.Cod()
            Case Nodes.Editorials
                LoadEditorials()
            Case Nodes.Revistes
                LoadRevistes()
            Case Nodes.Creativitat
                LoadGridCreativitat()
            Case Nodes.Contactes
                LoadGridContacts()
        End Select
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadEditorials()
        Dim SQL As String = "SELECT PrRevistas.EditorialGuid, CliGral.RaoSocial AS Nom " _
        & "FROM PRREVISTAS INNER JOIN " _
        & "CliGral ON PRREVISTAS.EditorialGuid = CliGral.Guid " _
        & "GROUP BY PrRevistas.EditorialGuid, CliGral.RaoSocial " _
        & "ORDER BY Nom"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        mAllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb.DefaultView
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowDrop = False
            .AllowUserToResizeRows = False

            With .Columns("EditorialGuid")
                .Visible = False
            End With
            With .Columns("Nom")
                .HeaderText = "editorial"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
    End Sub

    Private Sub LoadRevistes()
        Dim SQL As String = "SELECT PRREVISTAS.GUID, PRREVISTAS.NOM AS REVISTA,CLIGRAL.RAOSOCIAL " _
        & "FROM PRREVISTAS INNER JOIN " _
        & "CliGral ON PRREVISTAS.EditorialGuid = CliGral.Guid " _
        & "ORDER BY PRREVISTAS.NOM"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        mAllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb.DefaultView
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowDrop = False
            .AllowUserToResizeRows = False

            With .Columns("GUID")
                .Visible = False
            End With
            With .Columns("REVISTA")
                .HeaderText = "revista"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns("RAOSOCIAL")
                .HeaderText = "editorial"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
    End Sub

    Private Sub LoadGridCreativitat()
        Dim SQL As String = "SELECT PRAD.Guid, Product.Nom, PRAD.Nom " _
        & "FROM PRAD INNER JOIN " _
        & "Product ON PRAD.Product = Product.Guid " _
        & "WHERE Product.EMP=@EMP " _
        & "ORDER BY Product.Nom, Product.Obsoleto, PRAD.Nom"


        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@Emp", mEmp.Id)
        Dim oTb As DataTable = oDs.Tables(0)
        mAllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb.DefaultView
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowDrop = False
            .AllowUserToResizeRows = False

            With .Columns(ColsCreativitat.Guid)
                .Visible = False
            End With
            With .Columns(ColsCreativitat.TpaNom)
                .HeaderText = "marca"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(ColsCreativitat.AdNom)
                .HeaderText = "anunci"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
    End Sub

    Private Function FiltraContactsPerEspecialitat() As Boolean
        Dim retval As Boolean = False
        Dim oMenuItem As ToolStripMenuItem

        If mMenuItemsEspecialitats Is Nothing Then
            mMenuItemsEspecialitats = New ArrayList
            For Each s As String In [Enum].GetNames(GetType(PrContact.Especialitats))
                oMenuItem = New ToolStripMenuItem(s, Nothing, AddressOf RefreshRequest)
                oMenuItem.CheckOnClick = True
                oMenuItem.Checked = True
                mMenuItemsEspecialitats.Add(oMenuItem)
            Next
        Else
            For Each oMenuItem In mMenuItemsEspecialitats
                If oMenuItem.Checked = False Then
                    retval = True
                    Exit For
                End If
            Next
        End If

        Return retval
    End Function

    Private Sub LoadGridContacts()

        Dim SQL As String = "SELECT PRCONTACT.GUID,PRCONTACT.STATUS,PRCONTACT.EMAIL,PRCONTACT.NOM,CLX.CLX,OBSERVACIONS FROM PRCONTACT LEFT OUTER JOIN " _
                            & "CLX ON PRCONTACT.EMP=CLX.EMP AND PRCONTACT.CLI=CLX.CLI " _
                            & "WHERE PRCONTACT.STATUS=0 AND CLX.EMP=@EMP "

        If FiltraContactsPerEspecialitat() Then
            Dim FirstTime As Boolean = True
            SQL = SQL & " AND ("
            For Each oMenuItem As ToolStripMenuItem In mMenuItemsEspecialitats
                If oMenuItem.Checked Then
                    If FirstTime Then
                        FirstTime = False
                    Else
                        SQL = SQL & " OR "
                    End If
                    Dim oEspecialitat As PrContact.Especialitats = [Enum].Parse(GetType(PrContact.Especialitats), oMenuItem.Text)
                    SQL = SQL & "PRCONTACT.ESPECIALITAT=" & CInt(oEspecialitat).ToString & " "
                End If
            Next
            SQL = SQL & ") "
        End If

        SQL = SQL & "ORDER BY PRCONTACT.ESPECIALITAT,PRCONTACT.CODIENTITAT,PRCONTACT.CARREC"


        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@EMP", mEmp.Id)
        Dim oTb As DataTable = oDs.Tables(0)
        mAllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb.DefaultView
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowDrop = False
            .AllowUserToResizeRows = False
            .ReadOnly = True

            With .Columns(ColsContact.Guid)
                .Visible = False
            End With
            With .Columns(ColsContact.Status)
                .Visible = False
            End With
            With .Columns(ColsContact.email)
                .HeaderText = "email"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(ColsContact.Nom)
                .HeaderText = "destinatari"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(ColsContact.clx)
                .HeaderText = "entitat"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(ColsContact.obs)
                .HeaderText = "observacions"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
    End Sub

    Private Function CurrentPrAd() As PrAd
        Dim retVal As PrAd = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As New Guid(oRow.Cells(ColsCreativitat.Guid).Value.ToString)
            retVal = New PrAd(oGuid)
        End If
        Return retVal
    End Function

    Private Function CurrentEditorial() As PrEditorial
        Dim retVal As PrEditorial = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(ColsEditorial.Guid).Value
            retVal = New PrEditorial(oGuid)
        End If
        Return retVal
    End Function

    Private Function CurrentRevista() As PrRevista
        Dim retVal As PrRevista = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As New Guid(oRow.Cells(ColsRevista.Guid).Value.ToString)
            retVal = New PrRevista(oGuid)
        End If
        Return retVal
    End Function

    Private Function CurrentContact() As PrContact
        Dim retVal As PrContact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As New Guid(oRow.Cells(ColsContact.Guid).Value.ToString)
            retVal = New PrContact(oGuid)
        End If
        Return retVal
    End Function

    Private Sub DataGridView1_DoubleClick(sender As Object, e As System.EventArgs) Handles DataGridView1.DoubleClick
        Select Case CurrentNodeCod()
            Case Nodes.Creativitat
                Dim oPrAd As PrAd = CurrentPrAd()
                If oPrAd IsNot Nothing Then
                    Dim oFrm As New Frm_PrAd(oPrAd)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                End If

            Case Nodes.Editorials
                Dim oPrEditorial As PrEditorial = CurrentEditorial()
                'Dim oFrm As New Frm_PrEditorial(oPrEditorial)

            Case Nodes.Revistes
                Dim oRevista As PrRevista = CurrentRevista()
                If oRevista IsNot Nothing Then
                    Dim oFrm As New Frm_PrRevista(Frm_PrRevista.Modes.Consulta, oRevista)
                    oFrm.Show()
                End If

            Case Nodes.Contactes
                Dim oPrContact As PrContact = CurrentContact()
                If oPrContact IsNot Nothing Then
                    Dim oFrm As New Frm_PrContact(oPrContact)
                    oFrm.Show()
                End If
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then SetContextMenu()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing
        Select Case CurrentNodeCod()
            Case Nodes.Creativitat
                Dim oPrAd As PrAd = CurrentPrAd()
                If oPrAd IsNot Nothing Then
                    Dim oMenu_Prad As New Menu_PrAd(oPrAd)
                    AddHandler oMenu_Prad.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Prad.Range)
                End If
                oMenuItem = New ToolStripMenuItem("afegir nou", Nothing, AddressOf AddNewPrAd)
                oContextMenu.Items.Add(oMenuItem)

            Case Nodes.Editorials
                Dim oPrEditorial As PrEditorial = CurrentEditorial()
                oMenuItem = New ToolStripMenuItem("afegir nou", Nothing, AddressOf AddNewPrEditorial)
                oContextMenu.Items.Add(oMenuItem)

            Case Nodes.Revistes
                Dim oRevista As PrRevista = CurrentRevista()
                If oRevista IsNot Nothing Then
                    Dim oMenu_PrRevista As New Menu_PrRevista(oRevista)
                    AddHandler oMenu_PrRevista.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_PrRevista.Range)
                End If
                oMenuItem = New ToolStripMenuItem("afegir nova", Nothing, AddressOf AddNewPrRevista)
                oContextMenu.Items.Add(oMenuItem)

            Case Nodes.Contactes
                Dim oItm As PrContact = CurrentContact()
                If oItm IsNot Nothing Then
                    oMenuItem = New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf ZoomPrContact)
                    oContextMenu.Items.Add(oMenuItem)

                    oMenuItem = New ToolStripMenuItem("afegir nou", Nothing, AddressOf AddNewPrContact)
                    oContextMenu.Items.Add(oMenuItem)

                    oMenuItem = New ToolStripMenuItem("eliminar", My.Resources.del, AddressOf DeletePrContact)
                    oMenuItem.Enabled = oItm.AllowDelete
                    oContextMenu.Items.Add(oMenuItem)

                    oContextMenu.Items.Add("-")

                    oMenuItem = New ToolStripMenuItem("copiar emails", My.Resources.Copy, AddressOf CopyPrContactEmails)
                    oContextMenu.Items.Add(oMenuItem)

                    oContextMenu.Items.Add("-")

                    For Each oMenuItem In mMenuItemsEspecialitats
                        oContextMenu.Items.Add(oMenuItem)
                    Next

                End If

        End Select

        DataGridView1.ContextMenuStrip = oContextMenu

    End Sub

    Private Sub AddNewPrEditorial()
        Dim oPrEditorial As New PrEditorial(mEmp)
        Dim oFrm As New Frm_PrEditorials(Frm_PrEditorials.Modes.Consulta, oPrEditorial, Nothing)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestPrAds
        oFrm.Show()

    End Sub

    Private Sub AddNewPrRevista()
        'Dim oPrRevista As New PrRevista(BLL.BLLApp.Emp, 0)
        'Dim oFrm As New Frm_PrEditorials(Frm_PrEditorials.Modes.Consulta, oPrEditorial, Nothing)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestPrAds
        'oFrm.Show()

    End Sub


    Private Sub AddNewPrAd()
        Dim oPrAd As New PrAd()
        Dim oFrm As New Frm_PrAd(oPrAd)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub AddNewPrContact(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oItem As New PrContact
        Dim oFrm As New Frm_PrContact(oItem)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub ZoomPrContact()
        Dim oFrm As New Frm_PrContact(CurrentContact())
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DeletePrContact()
        Dim oItm As PrContact = CurrentContact()
        If oItm.AllowDelete Then
            Dim rc As MsgBoxResult = MsgBox("eliminem " & oItm.Nom & "?", MsgBoxStyle.OkCancel, "MAT.NET")
            If rc = MsgBoxResult.Ok Then
                Dim BlSuccess As Boolean = oItm.Delete
                If Not BlSuccess Then
                    MsgBox("no es pot eliminar", MsgBoxStyle.Exclamation, "MAT.NET")
                End If
            End If
        Else
            MsgBox("no es pot eliminar", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
        Dim oFrm As New Frm_PrContact(CurrentContact())
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub CopyPrContactEmails()
        Dim sb As New System.Text.StringBuilder
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            If sb.Length > 0 Then sb.Append(";" & vbCrLf)
            sb.Append(oRow.Cells(ColsContact.email).Value)
        Next
        Clipboard.SetDataObject(sb.ToString, True)

        MsgBox("copiades " & DataGridView1.Rows.Count.ToString & " adreçes email al portapapers", MsgBoxStyle.Information)
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        mAllowEvents = False
        Dim i As Integer = DataGridView1.CurrentRow.Index
        Dim j As Integer = DataGridView1.CurrentCell.ColumnIndex
        Dim iFirstRow As Integer = DataGridView1.FirstDisplayedScrollingRowIndex()

        LoadGrid(TreeView1.SelectedNode)

        If DataGridView1.RowCount > 0 Then
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow
            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(j)
            End If
        End If

    End Sub

End Class