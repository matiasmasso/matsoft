Public Class Xl_Products

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Sub Load(value As List(Of DTOProduct))
        _ControlItems = New ControlItems
        For Each oItem As DTOProduct In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTOProduct
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOProduct = oControlItem.Source
            Return retval
        End Get
    End Property

    Public ReadOnly Property Values As List(Of DTOProduct)
        Get
            Dim retval As New List(Of DTOProduct)
            If _ControlItems IsNot Nothing Then
                For Each oControlItem As ControlItem In _ControlItems
                    retval.Add(oControlItem.Source)
                Next
            End If
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
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
            With .Columns(Cols.Nom)
                .HeaderText = "Nom"
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
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

    Private Function SelectedItems() As List(Of DTOProduct)
        Dim retval As New List(Of DTOProduct)
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
            'Dim oMenu_Product As New Menu_Product(SelectedItems.First)
            'AddHandler oMenu_Product.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_Product.Range)
        End If

        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOProduct = CurrentControlItem.Source
        'Dim oFrm As New Frm_Product(oSelectedValue)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Do_AddNew()
        Dim oFrm As New Frm_Arts(Frm_Arts.SelModes.SelectProduct)
        AddHandler oFrm.AfterSelect, AddressOf onProductSelected
        oFrm.Show()
    End Sub

    Private Sub onProductSelected(sender As Object, e As MatEventArgs)
        Dim oControlItem As New ControlItem(e.Argument)
        _ControlItems.Add(oControlItem)
        LoadGrid()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub RefreshRequest()

    End Sub

    Protected Class ControlItem
        Public Property Source As DTOProduct

        Public Property Nom As String

        Public Sub New(oProduct As DTOProduct)
            MyBase.New()
            _Source = oProduct
            With oProduct
                _Nom = BLL.BLLProduct.FullNom(oProduct)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class


