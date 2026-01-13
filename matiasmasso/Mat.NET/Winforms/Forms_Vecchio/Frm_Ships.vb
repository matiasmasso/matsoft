

Public Class Frm_Ships
    Private mAllowEvents As Boolean
    Private mSelectionMode As BLL.Defaults.SelectionModes

    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        MMSI
        Nom
    End Enum

    Public Sub New(ByVal oSelectionMode As BLL.Defaults.SelectionModes)
        MyBase.New()
        Me.InitializeComponent()
        mSelectionMode = oSelectionMode
    End Sub

    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT MMSI,Nom FROM Ship ORDER BY Nom"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.MMSI)
                .HeaderText = "MMSI"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
            End With

            With .Columns(Cols.Nom)
                .HeaderText = "nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            'With .Columns(Cols.Fch)
            '.HeaderText = "data"
            '.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            '.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Width = 70
            'End With

            'With .Columns(Cols.Eur)
            '.HeaderText = "Import"
            '.Width = 120
            '.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            '.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            '.DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            'End With

        End With
    End Sub

    Private Function CurrentItm() As Ship
        Dim oItm As Ship = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iMMSI As Integer = CInt(oRow.Cells(Cols.MMSI).Value)
            oItm = New Ship(iMMSI)
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As Ship = CurrentItm()
        If oItm IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf Zoom)
            oContextMenuStrip.Items.Add(oMenuItem)
        End If

        oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", My.Resources.clip, AddressOf AddNewItm))
        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Select Case mSelectionMode
            Case bll.dEFAULTS.SelectionModes.Selection
                RaiseEvent AfterSelect(CurrentItm, EventArgs.Empty)
                Me.Close()
            Case Else
                Zoom()
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oShip As New Ship
        Dim oFrm As New Frm_Ship(oShip)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Zoom()
        Dim oFrm As New Frm_Ship(CurrentItm())
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
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

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

End Class