Imports System.Xml

Public Class Xl_ImportacioArts

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean
    Private _DirtyCell As Boolean
    Private _LastValidatedObject As ProductSku

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Qty
    End Enum

    Public Shadows Sub Load(ByVal oPurchaseOrderItems As List(Of PurchaseOrderItem))
        _ControlItems = New ControlItems
        For Each oPurchaseOrderItem As PurchaseOrderItem In oPurchaseOrderItems
            Dim oItem As New ControlItem(oPurchaseOrderItem)
            _ControlItems.Add(oItem)
        Next

        LoadGrid()
    End Sub

    Public Function PurchaseOrderItems() As List(Of PurchaseOrderItem)
        Dim retval As New List(Of PurchaseOrderItem)
        If _ControlItems IsNot Nothing Then
            For Each oControlItem As ControlItem In _ControlItems
                Dim oItem As PurchaseOrderItem = oControlItem.Source
                oItem.Qty = oControlItem.Qty
                retval.Add(oItem)
            Next
        End If
        Return retval
    End Function

    Private Sub LoadGrid()
        _AllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .ReadOnly = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False
            .AllowUserToAddRows = True
            .AllowUserToDeleteRows = True
            .RowHeadersVisible = True
            .ColumnHeadersVisible = True
            .AutoGenerateColumns = False

            Dim oBindingSource As New BindingSource
            oBindingSource.AllowNew = True
            oBindingSource.DataSource = _ControlItems
            .DataSource = oBindingSource

            .Columns.Clear()

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .HeaderText = "producte"
                .DataPropertyName = "nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Qty)
                .HeaderText = "quantitat"
                .DataPropertyName = "qty"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,###"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()


        If oControlItem IsNot Nothing Then
            Dim oArt As New Art(oControlItem.Source.Sku.Guid)
            Dim oMenu_Art As New Menu_Art(oArt)
            oContextMenu.Items.AddRange(oMenu_Art.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            _ControlItems.Remove(CurrentControlItem)
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub DataGridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
        'Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        'Dim oControlitem As ControlItem = oRow.DataBoundItem
        'Stop
    End Sub

    Private Sub DataGridView1_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then SetContextMenu()
    End Sub

    Private Sub DataGridView1_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        _DirtyCell = True
    End Sub

    Private Sub DataGridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        If _DirtyCell Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            Select Case e.ColumnIndex
                Case Cols.Nom
                    If e.FormattedValue = "" Then
                        _LastValidatedObject = Nothing
                    ElseIf e.FormattedValue = CType(oRow.DataBoundItem, ControlItem).Nom Then
                        _DirtyCell = False
                    Else
                        Dim oSku As ProductSku = Finder.FindSku(BLL.BLLApp.Mgz, e.FormattedValue)
                        If oSku Is Nothing Then
                            e.Cancel = True
                        Else
                            _LastValidatedObject = oSku
                        End If
                    End If
            End Select
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub DataGridView1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        If _DirtyCell Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Select Case e.ColumnIndex
                Case Cols.Nom
                    Dim oSku As ProductSku = _LastValidatedObject
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    With oControlItem
                        If .Source Is Nothing Then
                            .Source = New PurchaseOrderItem
                        End If
                        .Source.Sku = oSku
                        .Nom = oSku.Sku.ToString & " " & oSku.Nom_Esp
                    End With
                Case Cols.Qty
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    'oControlItem.Qty
                    'Stop
            End Select
            _DirtyCell = False
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As PurchaseOrderItem

        Public Property Nom As String
        Public Property Qty As Integer

        Public Sub New()
            'constructor senzill obligatori per afegir linies
            MyBase.New()
        End Sub

        Public Sub New(oPurchaseOrderItem As PurchaseOrderItem)
            MyBase.New()
            _Source = oPurchaseOrderItem

            _Nom = oPurchaseOrderItem.Sku.Sku.ToString & " " & oPurchaseOrderItem.Sku.Nom_Esp
            _Qty = oPurchaseOrderItem.Qty

        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class



End Class

