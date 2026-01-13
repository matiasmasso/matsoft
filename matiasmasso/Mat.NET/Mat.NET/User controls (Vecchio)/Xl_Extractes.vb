Public Class Xl_Extractes

    Private _DataSource As Extractes
    Private _DefaultItem As Extracte
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Id
        Nom
    End Enum

    Public Shadows Sub Load(oDataSource As Extractes, Optional oDefaultItem As Extracte = Nothing)
        _DataSource = oDataSource
        _DefaultItem = oDefaultItem
        LoadGrid()
    End Sub

    Public Function SelectedItem() As Extracte
        Dim oControlItem As ControlItem = CurrentItem()
        Dim retval As Extracte = Nothing
        If oControlItem IsNot Nothing Then
            retval = oControlItem.Source
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        'Dim oMenuItem As ToolStripMenuItem = _IncludeObsoletsMenuItem
        'oContextMenu.Items.Add(oMenuItem)
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow Is Nothing Then
            If DataGridView1.Rows.Count > 0 Then
                oRow = DataGridView1.Rows(0)
                DataGridView1.CurrentCell = oRow.Cells(Cols.Nom)
            End If
        Else
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function



    Private Sub LoadGrid()
        _AllowEvents = False

        _ControlItems = New ControlItems
        Dim oSelectedItem As ControlItem = Nothing
        For Each oExtracte As Extracte In _DataSource
            Dim oControlItem As New ControlItem(oExtracte)
            If oExtracte.Cta.Equals(_DefaultItem.Cta) Then oSelectedItem = oControlItem
            _ControlItems.Add(oControlItem)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .ReadOnly = True
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False
            .RowHeadersVisible = False
            .ColumnHeadersVisible = True
            .AutoGenerateColumns = False
            .Columns.Clear()
            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect


            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Id)
                .DataPropertyName = "Id"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .DataPropertyName = "Txt"
                .HeaderText = "compte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.SelectionBackColor = .DefaultCellStyle.BackColor
            End With
        End With

        If oSelectedItem IsNot Nothing Then
            Dim iRow As Integer = _ControlItems.IndexOf(oSelectedItem)
            DataGridView1.CurrentCell = DataGridView1.Rows(iRow).Cells(Cols.Id)
        End If

        _AllowEvents = True
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs)
        If _AllowEvents Then
            Dim oControlItem As ControlItem = CurrentItem()
            Dim oExtracte As Extracte = oControlItem.Source
            Dim oEventArgs As New MatEventArgs(oExtracte)
            RaiseEvent onItemSelected(Me, oEventArgs)
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        'If e.ColumnIndex <> Cols.Id Then
        'e.CellStyle.SelectionBackColor = e.CellStyle.BackColor
        'e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor
        'End If
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oExtracte As Extracte = CurrentItem.Source
        Dim oCta As PgcCta = oExtracte.Cta
        Dim oFrm As New Frm_PgcCta(oCta)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(sender As Object, e As EventArgs)
        For Each oControlItem In _ControlItems
            If oControlItem.Source.Cta.Equals(sender) Then
                _DefaultItem = oControlItem.Source
                Exit For
            End If
        Next

        LoadGrid()
    End Sub

    Private Sub DataGridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        'Dim dgv As DataGridView = sender
        'Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        'If oRow.Selected Then
        ' Dim iWidth As Integer = dgv.Width
        ' Dim r As Rectangle = dgv.GetRowDisplayRectangle(e.RowIndex, False)
        ' Dim rect As New Rectangle(r.X, r.Y, iWidth - 1, r.Height - 1)
        ' 'draw the border around the selected row using the highlight color and using a border width of 2
        ' ControlPaint.DrawBorder(e.Graphics, rect,
        '     SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
        '     SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
        '     SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
        '     SystemColors.Highlight, 2, ButtonBorderStyle.Solid)
        ' End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        oRow.DefaultCellStyle.SelectionBackColor = oRow.DefaultCellStyle.BackColor
    End Sub

    Private Sub DataGridView1_SelectionChanged1(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            Dim oControlItem As ControlItem = CurrentItem()
            Dim oExtracte As Extracte = oControlItem.Source
            Dim oArgs As New MatEventArgs(oExtracte)
            RaiseEvent onItemSelected(Me, oArgs)
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As Extracte
        Public Property Doc As Boolean
        Public Property Id As Integer
        Public Property Fch As Date
        Public Property Txt As String
        Public Property Deb As Decimal
        Public Property Hab As Decimal
        Public Property Sdo As Decimal

        Public Sub New(oExtracte As Extracte)
            MyBase.New()
            _Source = oExtracte
            With oExtracte.Cta
                _Id = .Id
                _Txt = .Cat
            End With
        End Sub
    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class
