
Public Class Frm_WebRoutes
    Private mSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse
    Private mAllowEvents As Boolean

    Public Event AfterSelect(sender As Object, e As System.EventArgs)

    Private Enum Cols
        Id
        Route
        Url
    End Enum

    Public Sub New(Optional oSelectionmode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        mSelectionMode = oSelectionmode
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        mAllowEvents = False
        Dim SQL As String = "SELECT Id,Route,Url FROM WebPages ORDER BY Route,Url"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
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
            With .Columns(Cols.Route)
                .HeaderText = "ruta"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Url)
                .HeaderText = "Url"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        mAllowEvents = True
        SetContextMenu()
    End Sub

    Private Function CurrentItem() As DTOWebPage
        Dim retval As DTOWebPage = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oId As DTOWebPage.Ids = CType(oRow.Cells(Cols.Id).Value, DTOWebPage.Ids)
            retval = New DTOWebPage(oId)
            With retval
                .Url = oRow.Cells(Cols.Url).Value
                .Route = oRow.Cells(Cols.Route).Value
            End With
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        'Dim oWebMenuGroup As WebRoute = CurrentItem()
        'Dim oMenuItem As ToolStripMenuItem
        'If oWebMenuGroup IsNot Nothing Then
        ' oMenuItem = New ToolStripMenuItem("zoom", Nothing, AddressOf Zoom)
        ' oContextMenu.Items.Add(oMenuItem)
        ' End If
        ' oMenuItem = New ToolStripMenuItem("afegir", Nothing, AddressOf AddNew)
        ' oContextMenu.Items.Add(oMenuItem)

        '        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As System.EventArgs) Handles DataGridView1.DoubleClick
        If mSelectionMode = BLL.Defaults.SelectionModes.Selection Then
            RaiseEvent AfterSelect(CurrentItem, e)
            Me.Close()
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
        Dim item As DTOWebPage = CurrentItem()
        Dim oFrm As New Frm_WebRoute(item)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub AddNew()
        'Dim item As New DTO()
        'Dim oFrm As New Frm_WebRoute(oWebRoute)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Route
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