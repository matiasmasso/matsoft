

Public Class Xl_WebMenuItems
    Private mMode As modes
    Private mParent As Object
    Private mSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse
    Private mAllowEvents As Boolean

    Public Event AfterSelect(sender As Object, e As System.EventArgs)

    Private Enum modes
        NotSet
        ByRol
        ByGroup
        All
    End Enum

    Private Enum Cols
        Guid
        Group
        Nom
        Url
    End Enum

    Public WriteOnly Property Rol As DTORol
        Set(value As DTORol)
            mMode = modes.ByRol
            mParent = value
            LoadGrid()
        End Set
    End Property

    Public WriteOnly Property Group As DTOWebMenuGroup
        Set(value As DTOWebMenuGroup)
            mMode = modes.ByGroup
            mParent = value
            LoadGrid()
        End Set
    End Property

    Public Sub LoadAllItems(Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        mMode = modes.All
        mSelectionMode = oSelectionMode
        LoadGrid()
    End Sub

    Public ReadOnly Property Items As List(Of DTOWebMenuItem)
        Get
            Dim oItems As New List(Of DTOWebMenuItem)
            For Each oRow As DataGridViewRow In DataGridView1.Rows

                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                Dim oItem As DTOWebMenuItem = BLL.BLLWebMenuItem.find(oGuid)
                oItems.Add(oItem)
            Next
            Return oItems
        End Get
    End Property

    Private Sub LoadGrid()
        Dim SQL As String = ""
        Dim oDs As DataSet = Nothing

        Select Case mMode
            Case modes.ByRol
                SQL = "SELECT M.Guid, G.Nom, M.Esp, M.url " _
                    & "FROM WebMenuItemsxRol R INNER JOIN " _
                    & "WebMenuItems M ON R.WebMenuItem=M.Guid LEFT OUTER JOIN " _
                    & "WebmenuGroups G ON M.WebmenuGroup=G.Guid " _
                    & "WHERE R.Rol=@Id " _
                    & "GROUP BY M.Guid, G.Nom, M.Esp, M.url " _
                    & "ORDER BY M.Ord"
                oDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@Id", CType(mParent, Rol).Id.ToString)
            Case modes.ByGroup
                SQL = "SELECT M.Guid,'',M.Esp,M.Url " _
                    & "FROM WebMenuItems M " _
                    & "WHERE WebmenuGroup=@GUID ORDER BY Ord"
                oDs = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@Guid", CType(mParent, DTOWebMenuGroup).Guid.ToString)
            Case modes.All
                SQL = "SELECT M.Guid, G.Nom, M.Esp, M.Url " _
                    & "FROM WebMenuItems M LEFT OUTER JOIN " _
                    & "WebmenuGroups G ON M.WebmenuGroup=G.Guid " _
                    & "ORDER BY G.Ord, M.Ord"
                oDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        End Select

        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Group)
                .HeaderText = "Grup"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
                .Visible = mMode <> modes.ByGroup
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "Menu"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With
            With .Columns(Cols.Url)
                .HeaderText = "adreça"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        mAllowEvents = True
        SetContextMenu()
    End Sub

    Private Function CurrentItem() As DTOWebMenuItem
        Dim retval As DTOWebMenuItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = GuidHelper.GetGuid(oRow.Cells(Cols.Guid).Value)
            retval = BLL.BLLWebMenuItem.Find(oGuid)
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oWebMenuItem As DTOWebMenuItem = CurrentItem()
        Dim oMenuItem As New ToolStripMenuItem

        If oWebMenuItem IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("zoom", Nothing, AddressOf Zoom)
            oContextMenu.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("eliminar", Nothing, AddressOf Del)
            oMenuItem.Visible = mMode <> modes.All
            oContextMenu.Items.Add(oMenuItem)
        End If

        oMenuItem = New ToolStripMenuItem("afegir", Nothing, AddressOf AddNew)
        oMenuItem.Visible = mMode <> modes.All
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As System.EventArgs) Handles DataGridView1.DoubleClick
        If mSelectionMode = BLL.Defaults.SelectionModes.Selection Then
            RaiseEvent AfterSelect(CurrentItem, e)
        Else
            Zoom()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Zoom()
        Dim oWebMenuItem As DTOWebMenuItem = CurrentItem()
        Dim oFrm As New Frm_WebMenuItem(oWebMenuItem)
        AddHandler oFrm.afterupdate, AddressOf refreshrequest
        oFrm.Show()
    End Sub

    Private Sub Del()
        Dim oWebMenuItem As DTOWebMenuItem = CurrentItem()
    End Sub

    Private Sub AddNew()
        Dim oFrm As New Frm_WebMenuItems(bll.dEFAULTS.SelectionModes.Selection)
        AddHandler oFrm.AfterSelect, AddressOf onItemAdded
        oFrm.Show()
    End Sub

    Private Sub onItemAdded(sender As Object, e As System.EventArgs)
        Select Case mMode
            Case modes.ByRol
            Case modes.ByGroup
                Dim oGroup As DTOWebMenuGroup = CType(mParent, DTOWebMenuGroup)
                oGroup.Items = Me.Items
                Dim exs As New List(Of Exception)
                BLL.BLLWebMenuGroup.UpdateWebMenuGroup(oGroup, exs)
        End Select
        RefreshRequest(sender, e)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
        Dim oGrid As DataGridView = DataGridView1

        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        If oRow IsNot Nothing Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

End Class
