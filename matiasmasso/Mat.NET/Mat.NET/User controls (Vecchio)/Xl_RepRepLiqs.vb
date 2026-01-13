Public Class Xl_RepRepLiqs
    Private _Rep As DTORep
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private Enum Cols
        Pdf
        Id
        Fch
        BaseFras
        Comisio
    End Enum


    Public WriteOnly Property Rep As DTORep
        Set(value As DTORep)
            _Rep = value
            LoadGrid()
        End Set
    End Property

    Private Sub LoadGrid()
        _ControlItems = New ControlItems

        Dim oRepLiqs As List(Of DTORepLiq) = BLL_RepLiqs.All(_Rep)
        For Each oItem As DTORepLiq In oRepLiqs
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.5
            End With
            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewImageColumn)
            With .Columns(Cols.Pdf)
                .DataPropertyName = "DocExists"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 20
                .DefaultCellStyle.NullValue = Nothing
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Id)
                .HeaderText = "Liquidació"
                .DataPropertyName = "Id"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .ReadOnly = True
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.BaseFras)
                .ReadOnly = True
                .HeaderText = "Base facturas"
                .DataPropertyName = "BaseFras"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Comisio)
                .ReadOnly = True
                .HeaderText = "Comisio"
                .DataPropertyName = "Comisio"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

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
            'Dim oMenu As New Menu_RepLiq(oControlItem.Source)
            'AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu.Range)
            'oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("Excel", Nothing, AddressOf Do_Excel)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Excel()
        MatExcel.GetExcelFromDataGridView(DataGridView1).Visible = True
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Pdf
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.DocExists Then
                    e.Value = My.Resources.pdf
                Else
                    e.Value = Nothing
                End If
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest(sender As Object, e As EventArgs)
        LoadGrid()
    End Sub


    Protected Class ControlItem
        Public Property Source As DTORepLiq

        Public Property DocExists As Boolean
        Public Property Id As Integer
        Public Property Fch As Date
        Public Property BaseFras As Decimal
        Public Property Comisio As Decimal

        Public Sub New(oRepLiq As DTORepLiq)
            MyBase.New()
            _Source = oRepLiq
            If oRepLiq.Cca IsNot Nothing Then
                _DocExists = oRepLiq.DocFile IsNot Nothing
            End If
            _Id = oRepLiq.Id
            _Fch = oRepLiq.Fch
            _BaseFras = oRepLiq.BaseFras.Eur
            _Comisio = oRepLiq.BaseImponible.Eur
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
