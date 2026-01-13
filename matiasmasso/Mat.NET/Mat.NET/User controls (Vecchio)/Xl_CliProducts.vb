Public Class Xl_CliProducts

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToDelete(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Cli
        Product
    End Enum

    Public Shadows Sub Load(values As List(Of MaxiSrvr.DTOCliProduct))
        _ControlItems = New ControlItems
        For Each oItem As MaxiSrvr.DTOCliProduct In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As MaxiSrvr.DTOCliProduct
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As MaxiSrvr.DTOCliProduct = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        Dim iRow As Integer
        Dim iFirstDisplayedScrollingRowIndex = DataGridView1.FirstDisplayedScrollingRowIndex
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            iRow = oRow.Index
        End If


        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Cli)
                .HeaderText = "Client"
                .DataPropertyName = "Cli"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Product)
                .HeaderText = "Producte"
                .DataPropertyName = "Product"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With

        If iFirstDisplayedScrollingRowIndex >= 0 Then
            DataGridView1.FirstDisplayedScrollingRowIndex = iFirstDisplayedScrollingRowIndex
        End If

        If iRow < DataGridView1.Rows.Count Then
            DataGridView1.CurrentCell = DataGridView1.Rows(iRow).Cells(0)
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of MaxiSrvr.DTOCliProduct)
        Dim retval As New List(Of MaxiSrvr.DTOCliProduct)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

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
            'Dim oMenu_DTOCliProduct As New Menu_DTOCliProduct(SelectedItems.First)
            'AddHandler oMenu_DTOCliProduct.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.Add("eliminar", Nothing, AddressOf Do_Delete)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As MaxiSrvr.DTOCliProduct = CurrentControlItem.Source
        'Dim oFrm As New Frm_DTOCliProduct(oSelectedValue)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Do_Delete()
        RaiseEvent RequestToDelete(Me, New MatEventArgs(CurrentControlItem.Source))
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As MaxiSrvr.DTOCliProduct

        Property Cli As String
        Property Product As String

        Public Sub New(oDTOCliProduct As MaxiSrvr.DTOCliProduct)
            MyBase.New()
            _Source = oDTOCliProduct
            With oDTOCliProduct
                _Cli = .Cli.Nom
                _Product = BLL_Product.Nom(.Product)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class

