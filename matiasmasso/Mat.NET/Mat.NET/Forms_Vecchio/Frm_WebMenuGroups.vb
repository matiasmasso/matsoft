

Public Class Frm_WebMenuGroups
    Private mSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse
    Private mAllowEvents As Boolean

    Public Event AfterSelect(sender As Object, e As System.EventArgs)

    Private Enum Cols
        Id
        Nom
    End Enum

    Public Sub New(Optional oSelectionmode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        mSelectionMode = oSelectionmode
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        mAllowEvents = False
        Dim SQL As String = "SELECT GUID,NOM FROM WebMenuGroups ORDER BY ORD"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = True
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        mAllowEvents = True
        SetContextMenu()
    End Sub

    Private Function CurrentItem() As DTOWebMenuGroup
        Dim retval As DTOWebMenuGroup = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = CType(oRow.Cells(Cols.Id).Value, Guid)
            retval = BLL.BLLWebMenuGroup.Find(oGuid)
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oWebMenuGroup As DTOWebMenuGroup = CurrentItem()
        Dim oMenuItem As ToolStripMenuItem
        If oWebMenuGroup IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("zoom", Nothing, AddressOf Zoom)
            oContextMenu.Items.Add(oMenuItem)
        End If
        oMenuItem = New ToolStripMenuItem("afegir", Nothing, AddressOf AddNew)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As System.EventArgs) Handles DataGridView1.DoubleClick
        If mSelectionMode = BLL.Defaults.SelectionModes.Selection Then
            RaiseEvent AfterSelect(CurrentItem, e)
            Me.Close()
        Else
            Zoom()
        End If
    End Sub

#Region "dragdrop"
    Private Sub DataGridView1_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim hti As DataGridView.HitTestInfo
            hti = DataGridView1.HitTest(e.X, e.Y)
            Select Case hti.Type
                Case System.Windows.Forms.DataGrid.HitTestType.Cell
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(hti.RowIndex)
                    Dim oGuid As Guid = CType(oRow.Cells(Cols.Id).Value, Guid)
                    Dim oItem As DTOWebMenuGroup = BLL.BLLWebMenuGroup.Find(oGuid)
                    DataGridView1.DoDragDrop(oItem, DragDropEffects.Move)

            End Select
        End If
    End Sub



    Private Sub DataGridView1_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragDrop
        If (e.Data.GetDataPresent(GetType(DTOWebMenuGroup))) Then

            Dim p As Point = DataGridView1.PointToClient(New Point(e.X, e.Y))
            Dim hit As DataGridView.HitTestInfo = DataGridView1.HitTest(p.X, p.Y)
            Select Case hit.Type
                Case System.Windows.Forms.DataGrid.HitTestType.Cell
                    Dim oDestRow As DataGridViewRow = DataGridView1.Rows(hit.RowIndex)
                    Dim oDestGuid As Guid = CType(oDestRow.Cells(Cols.Id).Value, Guid)
                    Dim oDest As DTOWebMenuGroup = BLL.BLLWebMenuGroup.Find(oDestGuid)
                    Dim oSrc As DTOWebMenuGroup = e.Data.GetData(GetType(DTOWebMenuGroup))
                    BLL.BLLWebMenuItems.Switch(oSrc, oDest)
                    RefreshRequest(sender, e)
            End Select
        End If
    End Sub

#End Region

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Zoom()
        Dim oWebMenuGroup As DTOWebMenuGroup = CurrentItem()
        Dim oFrm As New Frm_WebmenuGroup(oWebMenuGroup)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub AddNew()
        Dim oWebMenuGroup As New DTOWebMenuGroup(System.Guid.NewGuid)
        Dim oFrm As New Frm_WebmenuGroup(oWebMenuGroup)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
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



    Private Sub DataGridView1_DragOver(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragOver
        If (e.Data.GetDataPresent(GetType(DTOWebMenuGroup))) Then
            e.Effect = DragDropEffects.Move
        End If

    End Sub

End Class