Public Class Xl_FraRepComLiquidables
    Private _ControlItems As ControlItems
    Private _CancelRequest As Boolean
    Private _AllowEvents As Boolean

    Private Enum Cols
        RepAbr
        Base
        Comisio
        RepLiqId
        RepLiqFch
        Obs
    End Enum

    Public WriteOnly Property Fra As Fra
        Set(value As Fra)
            _ControlItems = New ControlItems

            Dim oItems As RepComsLiquidables = RepComsLiquidables.FromFra(value)
            For Each oItem As RepComLiquidable In oItems
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            Next
            LoadGrid()
        End Set
    End Property

    Private Sub LoadGrid()
        With DataGridView1
            .AutoGenerateColumns = False
            .Columns.Clear()

            For i As Integer = Cols.RepAbr To Cols.Obs
                .Columns.Add(New DataGridViewTextBoxColumn)
            Next

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            With .Columns(Cols.RepAbr)
                .HeaderText = "Rep"
                .DataPropertyName = "RepAbr"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Base)
                .HeaderText = "Base"
                .DataPropertyName = "Base"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.Comisio)
                .HeaderText = "Comisio"
                .DataPropertyName = "Comisio"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.RepLiqId)
                .HeaderText = "Liquidació"
                .DataPropertyName = "Fra"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.RepLiqFch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Obs)
                .HeaderText = "Observacions"
                .DataPropertyName = "Obs"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function SelectedItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentItem)
        Return retval
    End Function

    Private Function SelectedRepComsLiquidables() As RepComsLiquidables
        Dim retval As New RepComsLiquidables
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentItem.Source)
        Return retval
    End Function

    Private Function CurrentItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentItem()

        If oControlItem IsNot Nothing Then
            'Dim oMenu_RepComLiquidable As New Menu_RepComLiquidable(SelectedRepComsLiquidables)
            'oContextMenu.Items.AddRange(oMenu_RepComLiquidable.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oItem As ControlItem = oRow.DataBoundItem
        'Select Case oItem.Updated
        '    Case True
        'oRow.DefaultCellStyle.BackColor = Color.LightGray
        '    Case Else
        'oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        'End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As RepComLiquidable

        Public Property RepAbr As String
        Public Property Base As Decimal
        Public Property Comisio As Decimal
        Public Property RepLiqId As Integer
        Public Property RepLiqFch As Date
        Public Property Obs As String

        Public Sub New(oRepComLiquidable As RepComLiquidable)
            MyBase.New()
            _Source = oRepComLiquidable
            With oRepComLiquidable
                _RepAbr = .Rep.Abr
                _Base = .Base.Eur
                _Comisio = .Comisio.Eur
                If .RepLiq IsNot Nothing Then
                    _RepLiqId = .RepLiq.Id
                    _RepLiqFch = .RepLiq.Fch
                End If
                _Obs = ""
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.Collections.CollectionBase

        Public Sub Add(ByVal NewObjMember As ControlItem)
            List.Add(NewObjMember)
        End Sub

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As ControlItem
            Get
                Item = List.Item(vntIndexKey)
            End Get
        End Property

    End Class

End Class
