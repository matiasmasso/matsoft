Public Class Frm_Incentius
    Private _Product As Product
    Private _MenuItemIncludeObsolets As ToolStripMenuItem

    Private Enum Cols
        FchFrom
        FchTo
        Nom
        Dts
    End Enum

    Public Sub New(Optional oProduct As Product = Nothing, Optional BlIncludeObsolets As Boolean = False)
        MyBase.New()
        Me.InitializeComponent()
        _Product = oProduct
        _MenuItemIncludeObsolets = New ToolStripMenuItem("Incluir obsolets", Nothing, AddressOf LoadGrid)
        _MenuItemIncludeObsolets.CheckOnClick = True
        _MenuItemIncludeObsolets.Checked = BlIncludeObsolets
    End Sub

    Private Sub Frm_Incentius_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim oIncentius As Incentius = MaxiSrvr.Incentius.All(_Product, _MenuItemIncludeObsolets.Checked)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add(New DataGridViewTextBoxColumn())
            .Columns.Add(New DataGridViewTextBoxColumn())
            .Columns.Add(New DataGridViewTextBoxColumn())
            .Columns.Add(New DataGridViewTextBoxColumn())
            .DataSource = oIncentius
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            With .Columns(Cols.FchFrom)
                .DataPropertyName = "FchFrom"
                .HeaderText = "desde"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.FchTo)
                If _MenuItemIncludeObsolets.Checked Then
                    .DataPropertyName = "FchTo"
                    .HeaderText = "caduca"
                    .Width = 60
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "dd/MM/yy"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                Else
                    .Visible = False
                End If
            End With
            With .Columns(Cols.Nom)
                .DataPropertyName = "NomEsp"
                .HeaderText = "Oferta"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Dts)
                .DataPropertyName = "QtyDtosText"
                .HeaderText = "Escalat"
                .Width = 120
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
        End With
    End Sub

    Private Function CurrentIncentiu() As Incentiu
        Dim retval As Incentiu = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oFrm As New Frm_IncentiuOld
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Incentiu = CurrentIncentiu()
            .Show()
        End With
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oIncentiu As Incentiu = oRow.DataBoundItem

        If oIncentiu.IsActiveDuringDate(Today) Then
            oRow.DefaultCellStyle.BackColor = Color.White
        Else
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oIncentiu As Incentiu = CurrentIncentiu()

        If oIncentiu IsNot Nothing Then
            Dim oMenu_Incentiu As New Menu_Incentiu(oIncentiu)
            AddHandler oMenu_Incentiu.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Incentiu.Range)
        End If
        oContextMenu.Items.Add(_MenuItemIncludeObsolets)
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
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

    Private Sub ToolStripButtonAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonAdd.Click
        Dim oItm As New Incentiu()
        Dim oFrm As New Frm_IncentiuOld
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Incentiu = oItm
            .Show()
        End With
    End Sub
End Class