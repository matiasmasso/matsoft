Public Class Xl_ProductsBase

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Sub Load(value As List(Of DTOProduct0))
        _ControlItems = New ControlItems
        For Each oItem As DTOProduct0 In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTOProduct0
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOProduct0 = oControlItem.Source
            Return retval
        End Get
    End Property

    Public ReadOnly Property Values As List(Of DTOProduct0)
        Get
            Dim retval As New List(Of DTOProduct0)
            For Each oControlItem As ControlItem In _ControlItems
                retval.Add(oControlItem.Source)
            Next
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
            .ColumnHeadersVisible = False
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

    Private Function SelectedItems() As List(Of DTOProduct0)
        Dim retval As New List(Of DTOProduct0)
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
            Dim oMenu_ProductBase As New Menu_ProductBase(SelectedItems.First)
            AddHandler oMenu_ProductBase.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_ProductBase.Range)
        End If

        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOProduct0 = CurrentControlItem.Source
        'Dim oFrm As New Frm_ProductBase(oSelectedValue)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        Select Case e.KeyCode
            Case Keys.Delete
                Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
                If oRow IsNot Nothing Then
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    _ControlItems.Remove(oControlItem)
                    RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                End If
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Do_AddNew()
        'Dim oFrm As New Frm_Arts(Frm_Arts.SelectionModes.SelectProductBase)
        'AddHandler oFrm.AfterSelect, AddressOf onProductBaseSelected
        'oFrm.Show()
    End Sub

    Private Sub onProductBaseSelected(sender As Object, e As MatEventArgs)
        Dim oControlItem As New ControlItem(e.Argument)
        _ControlItems.Add(oControlItem)
        LoadGrid()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub RefreshRequest()

    End Sub

    Protected Class ControlItem
        Public Property Source As DTOProduct0

        Public Property Nom As String

        Public Sub New(oProductBase As DTOProduct0)
            MyBase.New()
            _Source = oProductBase
            With oProductBase
                _Nom = BLL_Product.FullNom(oProductBase)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class


