Public Class Xl_SkuWiths
    Inherits DataGridView

    Private _ParentSku As DTOProductSku
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        SkuNom
        Qty
    End Enum

    Public Shadows Sub Load(oParentSku As DTOProductSku)
        _ParentSku = oParentSku
        _ControlItems = New ControlItems
        For Each item As DTOSkuWith In oParentSku.SkuWiths
            Dim oControlItem As New ControlItem(item)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public Function Values() As List(Of GuidQty)
        Dim retval As New List(Of GuidQty)
        For Each oControlItem In _ControlItems
            Dim item As New GuidQty With {
                .Guid = oControlItem.Source.Child.Guid,
                .Qty = oControlItem.Qty
            }
            retval.Add(item)
        Next
        Return retval
    End Function


    Private Sub LoadGrid()
        _AllowEvents = False

        MyBase.AutoGenerateColumns = False
        With MyBase.RowTemplate
            .Height = MyBase.Font.Height * 1.3
        End With
        MyBase.SelectionMode = DataGridViewSelectionMode.CellSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = True
        MyBase.MultiSelect = False
        MyBase.AllowUserToResizeRows = False
        MyBase.ReadOnly = False
        MyBase.Columns.Clear()

        Dim oBindingSource As New BindingSource
        oBindingSource.AllowNew = True
        oBindingSource.DataSource = _ControlItems
        MyBase.DataSource = oBindingSource

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.SkuNom)
            .HeaderText = "concepte"
            .DataPropertyName = "SkuNom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "quant"
            .DataPropertyName = "Qty"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = False
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        oContextMenu.Items.Add("Afegir", Nothing, AddressOf RequestToAddNew)
        Dim oControlItem As ControlItem = CurrentControlItem()
        If oControlItem IsNot Nothing Then
            oContextMenu.Items.Add("Editar", Nothing, AddressOf Do_Edit)
            oContextMenu.Items.Add("Retirar", Nothing, AddressOf RequestToRemove)
            oContextMenu.Items.Add("-")
            Dim item As DTOSkuWith = oControlItem.Source
            Dim oMenu As New Menu_ProductSku(item.Child)
            oContextMenu.Items.AddRange(oMenu.Range)
        End If
        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Edit()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim item As New GuidQty With {
            .Guid = oControlItem.Source.Child.Guid,
            .Qty = oControlItem.Qty
        }

        Dim oFrm As New Frm_SkuWith(item)
        AddHandler oFrm.AfterUpdate, AddressOf UpdateRequest
        AddHandler oFrm.DeleteRequest, AddressOf DeleteRequest
        oFrm.Show()
    End Sub

    Private Sub RequestToAddNew()
        Dim oFrm As New Frm_SkuWith()
        AddHandler oFrm.AfterUpdate, AddressOf UpdateRequest
        AddHandler oFrm.DeleteRequest, AddressOf DeleteRequest
        oFrm.Show()
    End Sub

    Private Async Sub UpdateRequest(sender As Object, e As MatEventArgs) 'from edit forn
        Dim exs As New List(Of Exception)
        Dim oValue = e.Argument
        Dim oValues = Values()
        oValues.RemoveAll(Function(x) x.Guid.Equals(oValue.Guid))
        oValues.Add(oValue)
        If Await FEB.SkuWiths.Update(exs, _ParentSku.Guid, oValues) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs())
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub DeleteRequest(sender As Object, e As MatEventArgs) 'from edit forn
        Dim exs As New List(Of Exception)
        Dim oChildSku As DTOProductSku = e.Argument
        Dim oValues = Values()
        oValues.RemoveAll(Function(x) x.Guid.Equals(oChildSku.Guid))
        If Await FEB.SkuWiths.Update(exs, _ParentSku.Guid, oValues) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs())
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub RequestToRemove() 'from context menu
        Dim exs As New List(Of Exception)
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oChildGuid = oControlItem.Source.Child.Guid
        Dim oValues = Values()
        oValues.RemoveAll(Function(x) x.Guid.Equals(oChildGuid))
        If Await FEB.SkuWiths.Update(exs, _ParentSku.Guid, oValues) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs())
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then SetContextMenu()
    End Sub

    Protected Class ControlItem
        Property Source As DTOSkuWith
        Property SkuNom As String
        Property Qty As Integer

        Public Sub New(item As DTOSkuWith)
            MyBase.New()
            _Source = item
            _SkuNom = item.Child.nom.Tradueix(Current.Session.Lang)
            _Qty = item.Qty
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


